import { CreateOrEditLoanImpairmentInputParameterDto, CreateOrEditLoanImpairmentHaircutDto, CreateOrEditLoanImpairmentRecoveryDto, CreateOrEditLoanImpairmentScenarioDto, CreateOrEditLoanImpairmentKeyParameterDto } from './../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter, OnInit} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { LoanImpairmentRegistersServiceProxy, CreateOrEditLoanImpairmentRegisterDto, CalibrationStatusEnum } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {Observable} from "@node_modules/rxjs";
import { Location } from '@angular/common';
import * as XLSX from 'xlsx';

@Component({
    templateUrl: './create-or-edit-loanImpairmentRegister.component.html',
    animations: [appModuleAnimation()]
})
export class CreateOrEditLoanImpairmentRegisterComponent extends AppComponentBase implements OnInit {
    active = false;
    saving = false;

    loanImpairmentRegister: CreateOrEditLoanImpairmentRegisterDto = new CreateOrEditLoanImpairmentRegisterDto();
    statusEnum = CalibrationStatusEnum;
    showInputCard = true;
    showHaircutCard = true;
    showRecoveryCard = true;
    showCalibrationCard = true;
    showScenarioCard = true;
    inputParameter = new CreateOrEditLoanImpairmentInputParameterDto();

    haircutRecovery = new CreateOrEditLoanImpairmentHaircutDto();
    loanImpairmentRecovery = new Array<CreateOrEditLoanImpairmentRecoveryDto>();
    loanImpairmentScenarios = new Array<CreateOrEditLoanImpairmentScenarioDto>();
    calibrationOfKeyParameters = new Array<CreateOrEditLoanImpairmentKeyParameterDto>();

    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _loanImpairmentRegistersServiceProxy: LoanImpairmentRegistersServiceProxy,
        private _router: Router,
        private _location: Location
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    show(loanImpairmentRegisterId?: string): void {

        if (!loanImpairmentRegisterId) {
            this.loanImpairmentRegister = new CreateOrEditLoanImpairmentRegisterDto();
            this.loanImpairmentRegister.id = loanImpairmentRegisterId;
            this.loanImpairmentRegister.status = this.statusEnum.Draft;

            this.inputParameter.reportingDate = moment().endOf('month');

            this.active = true;
        } else {
            this._loanImpairmentRegistersServiceProxy.getLoanImpairmentRegisterForEdit(loanImpairmentRegisterId).subscribe(result => {
                this.loanImpairmentRegister = result.loanImpairmentRegister;


                this.active = true;
            });
        }

    }

    private saveInternal(): Observable<void> {
            this.saving = true;


        return this._loanImpairmentRegistersServiceProxy.createOrEdit(this.loanImpairmentRegister)
         .pipe(finalize(() => {
            this.saving = false;
            this.notify.info(this.l('SavedSuccessfully'));
         }));
    }

    save(): void {

        this.message.confirm(
            'Submit for approval?',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.saving = true;
                    console.log(this.inputParameter.reportingDate);
                    this.inputParameter.reportingDate = moment(this.inputParameter.reportingDate).add(1, 'hour');
                    console.log(JSON.stringify(this.inputParameter.reportingDate));

                    this.loanImpairmentRegister.inputParameter = this.inputParameter;
                    this.loanImpairmentRegister.haircutRecovery = this.haircutRecovery;
                    this.loanImpairmentRegister.loanImpairmentRecovery = this.loanImpairmentRecovery;
                    this.loanImpairmentRegister.loanImpairmentScenarios = this.loanImpairmentScenarios;
                    this.loanImpairmentRegister.calibrationOfKeyParameters = this.calibrationOfKeyParameters;

                    this._loanImpairmentRegistersServiceProxy.createOrEdit(this.loanImpairmentRegister)
                        .pipe(finalize(() => {
                            this.saving = false;
                        })).subscribe(() => {
                            this.notify.info(this.l('SavedSuccessfully'));
                            this._router.navigate( ['/app/main/loanImpairmentsRegisters/loanImpairmentRegisters']);
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

            if (type == 'LoanImpairmentRecovery') {
                this.loanImpairmentRecovery = [];
                data.forEach(r => {
                    let ir = new CreateOrEditLoanImpairmentRecoveryDto();
                    ir.loanSale = r['Loan_Sale'];
                    ir.cashRecovery = r['Cash_Recovery'];
                    ir.property = r['Property'];
                    ir.recovery = r['Recovery'];
                    ir.shares = r['Shares'];
                    this.loanImpairmentRecovery.push(ir);
                });
            }

            else if (type == 'LoanImpairmentScenarioOptions') {
                this.loanImpairmentScenarios = [];
                data.forEach(r => {
                    let ir = new CreateOrEditLoanImpairmentScenarioDto();
                    ir.applyOverridesBaseScenario = r['Apply_Overrides_Base_Scenario'];
                    ir.applyOverridesDownturnScenario = r['Apply_Overrides_Downturn_Scenario'];
                    ir.applyOverridesOptimisticScenario = r['Apply_Overrides_Optimistic_Scenario'];
                    ir.baseScenario = r['Base_Scenario'];
                    ir.bestScenarioOverridesValue = r['Best_Scenario_Overrides_Value'];
                    ir.downturnScenarioOverridesValue = r['Downturn_Scenario_Overrides_Value'];
                    ir.optimisticScenario = r['Optimistic_Scenario'];
                    ir.optimisticScenarioOverridesValue = r['Optimistic_Scenario_Overrides_Value'];
                    ir.scenarioOption = r['Scenario_Option'];
                    this.loanImpairmentScenarios.push(ir);
                });
            }

            else if(type == 'CalibrationOfKeyParameters') {
                this.calibrationOfKeyParameters = [];
                data.forEach(r => {
                    let ir = new CreateOrEditLoanImpairmentKeyParameterDto();
                    ir.expectedCashFlow = r['Expected_Cash_Flow'];
                    ir.revisedCashFlow = r['Revised_Cash_Flow'];
                    ir.year = r['Year'];

                    this.calibrationOfKeyParameters.push(ir);
                });
            }

        }
    }

    onUploadExcelError(): void {
        this.notify.error(this.l('ImportCalibrationDataFailed'));
    }

}
