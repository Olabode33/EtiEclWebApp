import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { LoanImpairmentRegistersServiceProxy, GetLoanImpairmentRegisterForViewDto, LoanImpairmentRegisterDto , CalibrationStatusEnum, CreateOrEditLoanImpairmentRegisterDto, CreateOrEditLoanImpairmentInputParameterDto, CreateOrEditLoanImpairmentHaircutDto, CreateOrEditLoanImpairmentRecoveryDto, CreateOrEditLoanImpairmentScenarioDto, CreateOrEditLoanImpairmentKeyParameterDto, GetLoanImpairmentRegisterForEditOutput, LoanImpairmentApprovalsServiceProxy, LoanImpairmentAuditInfoDto} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Location } from '@angular/common';
import { ApprovalModalComponent } from '@app/main/eclShared/approve-ecl-modal/approve-ecl-modal.component';

@Component({
    templateUrl: './view-loanImpairmentRegister.component.html',
    animations: [appModuleAnimation()]
})
export class ViewLoanImpairmentRegisterComponent extends AppComponentBase implements OnInit {
    @ViewChild('approvalModal', { static: true }) approvalModal: ApprovalModalComponent;

    active = false;
    saving = false;

    calibrationStatusEnum = CalibrationStatusEnum;

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

    auditInfo = new LoanImpairmentAuditInfoDto();

    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
         private _loanImpairmentRegistersServiceProxy: LoanImpairmentRegistersServiceProxy,
         private _loanImpairmentApprovalsServiceProxy: LoanImpairmentApprovalsServiceProxy,
         private _location: Location

    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    approve() {
        this.approvalModal.show();
    }

    reloadPage(): void {
        this.show(this.loanImpairmentRegister.id);
    }

    show(loanImpairmentRegisterId: string): void {
      this._loanImpairmentRegistersServiceProxy.getLoanImpairmentRegisterForEdit(loanImpairmentRegisterId).subscribe(result => {
        this.loanImpairmentRegister = result.loanImpairmentRegister;
        this.inputParameter = this.loanImpairmentRegister.inputParameter;
        this.inputParameter.reportingDate = this.loanImpairmentRegister.inputParameter.reportingDate.toDate() as any;
        this.calibrationOfKeyParameters = result.loanImpairmentRegister.calibrationOfKeyParameters;
        this.haircutRecovery = result.loanImpairmentRegister.haircutRecovery;
        this.loanImpairmentRecovery = result.loanImpairmentRegister.loanImpairmentRecovery;
        this.loanImpairmentScenarios = result.loanImpairmentRegister.loanImpairmentScenarios;
                this.active = true;
                this._loanImpairmentApprovalsServiceProxy.getLoanImpairmentApprovals(loanImpairmentRegisterId).subscribe(result => {
                    this.auditInfo = result;
                });
            });
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
}
