import { CurrentPeriodDatesServiceProxy, ReceivablesForecastsServiceProxy, ReceivablesAuditInfoDto, ReceivablesApprovalsServiceProxy, CreateOrEditReceivablesResultDto } from './../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ReceivablesRegistersServiceProxy, CreateOrEditReceivablesRegisterDto, CalibrationStatusEnum, CreateOrEditReceivablesInputDto, CreateOrEditCurrentPeriodDateDto, CreateOrEditReceivablesForecastDto, EntityDtoOfGuid, ReceivablesResultsServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Observable } from "@node_modules/rxjs";
import { Location } from '@angular/common';
import * as XLSX from 'xlsx';
import { ApprovalModalComponent } from '@app/main/eclShared/approve-ecl-modal/approve-ecl-modal.component';
import { FileDownloadService } from '@shared/utils/file-download.service';

@Component({
    templateUrl: './view-receivablesRegister.component.html',
    animations: [appModuleAnimation()]
})
export class ViewReceivablesRegisterComponent extends AppComponentBase implements OnInit {
    @ViewChild('approvalModal', { static: true }) approvalModal: ApprovalModalComponent;

    active = false;
    saving = false;
    calibrationStatusEnum = CalibrationStatusEnum;
    receivablesRegister: CreateOrEditReceivablesRegisterDto = new CreateOrEditReceivablesRegisterDto();
    showForecastCard = true;
    showInputCard = true;
    showCurrentPeriodDataCard = true;
    statusEnum = CalibrationStatusEnum;
    inputParameter = new CreateOrEditReceivablesInputDto();
    currentPeriodData = new Array<CreateOrEditCurrentPeriodDateDto>();
    forecastData = new Array<CreateOrEditReceivablesForecastDto>();
    auditInfo = new ReceivablesAuditInfoDto();
    receivablesResults = new Array<CreateOrEditReceivablesResultDto>();
    showResultsCard = true;

    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _receivablesRegistersServiceProxy: ReceivablesRegistersServiceProxy,
        private _router: Router,
        private _location: Location,
        private _fileDownloadService: FileDownloadService,
        private _currentPeriodDatesServiceProxy: CurrentPeriodDatesServiceProxy,
        private _receivablesForecastsServiceProxy: ReceivablesForecastsServiceProxy,
        private _receivablesApprovalsServiceProxy: ReceivablesApprovalsServiceProxy,
        private _receivablesResultsServiceProxy: ReceivablesResultsServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    reloadPage(): void {
        this.show(this.receivablesRegister.id);
    }

    show(receivablesRegisterId: string): void {

            this._receivablesRegistersServiceProxy.getReceivablesRegisterForEdit(receivablesRegisterId).subscribe(result => {

                this.receivablesRegister = result.receivablesRegister;
                this.inputParameter = result.receivablesRegister.inputParameter;
                this.inputParameter.reportingDate = this.inputParameter.reportingDate.toDate() as any;

                this.currentPeriodData = result.receivablesRegister.currentPeriodData;
                this.forecastData = result.receivablesRegister.forecastData;
                this.active = true;
                if (this.receivablesRegister.status == CalibrationStatusEnum.Completed) {
                    this._receivablesResultsServiceProxy.getResults(this.receivablesRegister.id).subscribe(res => {
                        this.receivablesResults = res;
                    });
                }
                this._receivablesApprovalsServiceProxy.getReceivablesApprovals(receivablesRegisterId).subscribe(result => {
                    this.auditInfo = result;
                });
            });


    }

    exportCurrentPeriodDataToExcel(): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this.receivablesRegister.id;
        this._currentPeriodDatesServiceProxy.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    exportForecastToExcel(): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this.receivablesRegister.id;
        this._receivablesForecastsServiceProxy.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    exportResultsToExcel(): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this.receivablesRegister.id;
        this._receivablesResultsServiceProxy.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    goBack(): void {
        this._location.back();
    }

    approve() {
        this.approvalModal.show();
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

    onUploadExcelError(): void {
        this.notify.error(this.l('ImportCalibrationDataFailed'));
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

            if (type == 'CurrentPeriodData') {
                this.currentPeriodData = [];
                data.forEach(r => {
                    let cpd = new CreateOrEditCurrentPeriodDateDto();
                    cpd.account = r['Account'];
                    cpd.zeroTo90 = r['0-90 Days'];
                    cpd.ninetyOneTo180 = r['91-180 Days'];
                    cpd.oneEightyOneTo365 = r['180-365 Days'];
                    cpd.over365 = r['> 365 Days'];

                    this.currentPeriodData.push(cpd);
                });
            }

            else if (type == 'ForecastData') {
                this.forecastData = [];
                data.forEach(r => {
                    let cpd = new CreateOrEditReceivablesForecastDto();
                    cpd.period = r['Period'];
                    cpd.optimistic = r['Optimistic'];
                    cpd.base = r['Base'];
                    cpd.downturn = r['Downturn'];

                    this.forecastData.push(cpd);
                });
            }

        }
    }

}
