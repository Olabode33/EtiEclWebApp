import { FileDownloadService } from './../../../../shared/utils/file-download.service';
import { CreateOrEditHoldCoInputParameterDto, CreateOrEditMacroEconomicCreditIndexDto, CreateOrEditAssetBookDto, MacroEconomicCreditIndicesServiceProxy } from './../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { HoldCoRegistersServiceProxy, CreateOrEditHoldCoRegisterDto, EntityDtoOfGuid, CreateOrEditCalibrationRunDto, CalibrationStatusEnum } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Observable } from "@node_modules/rxjs";
import { HttpClient } from '@angular/common/http';
import { AppConsts } from '@shared/AppConsts';
import { FileUpload } from 'primeng/primeng';
import * as XLSX from 'xlsx';
import { Location } from '@angular/common';
import { ApprovalModalComponent } from '@app/main/eclShared/approve-ecl-modal/approve-ecl-modal.component';


@Component({
    templateUrl: './create-or-edit-holdCoRegister.component.html',
    animations: [appModuleAnimation()]
})
export class CreateOrEditHoldCoRegisterComponent extends AppComponentBase implements OnInit {
    @ViewChild('excelFileUpload', { static: true }) excelFileUpload: FileUpload;

    active = false;
    saving = false;
    showAssetCard = true;
    showIntercompanyCard = true;
    showMacroCard = true;
    holdCoRegister: CreateOrEditHoldCoRegisterDto = new CreateOrEditHoldCoRegisterDto();
    statusEnum = CalibrationStatusEnum;
    inputParameter = new CreateOrEditHoldCoInputParameterDto();
    macroEconomicCreditIndex = new Array<CreateOrEditMacroEconomicCreditIndexDto>();
    assetBook = new Array<CreateOrEditAssetBookDto>();


    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _holdCoRegistersServiceProxy: HoldCoRegistersServiceProxy,
        private _macroEconomicCreditIndices: MacroEconomicCreditIndicesServiceProxy,
        private _router: Router,
        private _location: Location,
        private _fileDownloadService: FileDownloadService,

        private _httpClient: HttpClient
    ) {
        super(injector);

    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    show(holdCoRegisterId?: string): void {

        if (!holdCoRegisterId) {
            this.holdCoRegister = new CreateOrEditHoldCoRegisterDto();
            this.holdCoRegister.id = holdCoRegisterId;
            this.holdCoRegister.status = this.statusEnum.Draft;
            this.active = true;
        } else {
            this._holdCoRegistersServiceProxy.getHoldCoRegisterForEdit(holdCoRegisterId).subscribe(result => {
                this.holdCoRegister = result.holdCoRegister;
                this.inputParameter = this.holdCoRegister.inputParameter;
                this.inputParameter.valuationDate = this.inputParameter.valuationDate.toDate() as any;
                this.inputParameter.assumedMaturityDate = this.inputParameter.assumedMaturityDate.toDate() as any;
                this.inputParameter.assumedStartDate = this.inputParameter.assumedStartDate.toDate() as any;
                this.macroEconomicCreditIndex = this.holdCoRegister.macroEconomicCreditIndex;
                this.assetBook = this.holdCoRegister.assetBook;
                this.active = true;
            });
        }

    }

    uploadData(data: { files: File }, type): void {
        const file = data.files[0];
        const reader: FileReader = new FileReader();
        reader.readAsBinaryString(file);
        reader.onload = (e: any) => {
            const binarystr: string = e.target.result;
            const wb: XLSX.WorkBook = XLSX.read(binarystr, { type: 'binary' });

            const wsname: string = wb.SheetNames[0];
            const ws: XLSX.WorkSheet = wb.Sheets[wsname];

            const data = XLSX.utils.sheet_to_json(ws);

            if (type == 'MacroEconomicCreditIndex') {
                this.macroEconomicCreditIndex = [];
                data.forEach(r => {
                    let meci = new CreateOrEditMacroEconomicCreditIndexDto();
                    meci.bestEstimate = r['Best_Estimate'];
                    meci.downturn = r['Downturn'];
                    meci.month = r['Month'];
                    meci.optimistic = r['Optimistic'];
                    this.macroEconomicCreditIndex.push(meci);
                });
            }

            else if (type == 'AssetBook') {
                this.assetBook = [];
                data.forEach(r => {
                    let abook = new CreateOrEditAssetBookDto();
                    abook.entity = r['Entity'] as string;
                    abook.assetDescription = r['Asset_Description'] as string;
                    abook.assetType = r['Asset_Type'] as string;
                    abook.ratingAgency = r['Rating_Agency'] as string;

                    abook.purchaseDateCreditRating = r['Credit_Rating_At_Purchase_Date'] as string;
                    abook.currentCreditRating = r['Current_Credit_Rating'] as string;
                    abook.nominalAmountACY = r['Nominal_Amount(ACY)'] as number;
                    abook.nominalAmountLCY = r['Nominal_Amount(LCY)'] as number;
                    abook.principalAmortisation = r['Principal_Amortisation'] as string;
                    abook.principalRepaymentTerms = r['Principal_Repayment_Terms'];
                    abook.interestRepaymentTerms = r['Interest_Repayment_Terms'] as string;
                    abook.outstandingBalanceACY = r['Outstanding_Balance(ACY)'] as number;
                    abook.outstandingBalanceLCY = r['Outstanding_Balance(LCY)'] as number;
                    abook.coupon = r['Coupon(%)'] as number;
                    abook.eir = r['EIR(%)'] as number;
                    abook.loanOriginationDate = r['Loan_Origination_Date'];
                    abook.loanMaturityDate = r['Loan_Maturity_Date'];
                    abook.daysPastDue = r['Days_Past_Due'] as number;
                    abook.prudentialClassification = r['Prudential_Classification'] as string;
                    abook.forebearanceFlag = r['Forebearance_Flag'] as string;
                    this.assetBook.push(abook);
                });
            }

        }
    }

    onUploadExcelError(): void {
        this.notify.error(this.l('ImportCalibrationDataFailed'));
    }

    approve() {
    }

    save() {
        this.message.confirm(
            'Submit for approval?',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.saving = true;
                    this.inputParameter.valuationDate = moment(this.inputParameter.valuationDate);
                    this.inputParameter.assumedMaturityDate = moment(this.inputParameter.assumedMaturityDate);
                    this.inputParameter.assumedStartDate = moment(this.inputParameter.assumedStartDate);

                    this.holdCoRegister.assetBook = this.assetBook;
                    this.holdCoRegister.inputParameter = this.inputParameter;
                    this.holdCoRegister.macroEconomicCreditIndex = this.macroEconomicCreditIndex;

                    this._holdCoRegistersServiceProxy.createOrEdit(this.holdCoRegister)
                        .pipe(finalize(() => {
                            this.saving = false;


                        })).subscribe(() => {
                            this.notify.info(this.l('SavedSuccessfully'));
                            this._router.navigate(['/app/main/ivModels/holdCoRegisters']);
                        });
                }
            }, true
        );

    }


    goBack(): void {
        this._location.back();
    }

    getStatusLabelClass(uploadStatus: CalibrationStatusEnum): string {
        switch (uploadStatus) {
            case CalibrationStatusEnum.Draft:
                return 'primary';
            case CalibrationStatusEnum.Submitted:
            case CalibrationStatusEnum.Processing:
            case CalibrationStatusEnum.AwaitngAdditionApproval:
                return 'warning';
            case CalibrationStatusEnum.Completed:
            case CalibrationStatusEnum.Approved:
                return 'success';
            case CalibrationStatusEnum.Rejected:
            case CalibrationStatusEnum.Failed:
                return 'danger';
            default:
                return 'dark';
        }
    }

    exportToExcel(): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this.holdCoRegister.id;
        this._macroEconomicCreditIndices.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }



}
