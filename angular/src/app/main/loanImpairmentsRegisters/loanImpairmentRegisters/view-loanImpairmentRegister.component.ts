import { LoanImpairmentModelResultsServiceProxy } from './../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { LoanImpairmentRegistersServiceProxy, CalibrationStatusEnum, CreateOrEditLoanImpairmentRegisterDto, CreateOrEditLoanImpairmentInputParameterDto, CreateOrEditLoanImpairmentHaircutDto, CreateOrEditLoanImpairmentRecoveryDto, CreateOrEditLoanImpairmentScenarioDto, CreateOrEditLoanImpairmentKeyParameterDto, GetLoanImpairmentRegisterForEditOutput, LoanImpairmentApprovalsServiceProxy, LoanImpairmentAuditInfoDto, EntityDtoOfGuid, LoanImpairmentScenariosServiceProxy, LoanImpairmentRecoveriesServiceProxy, LoanImpairmentKeyParametersServiceProxy, CreateOrEditLoanImpairmentModelResultDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Location } from '@angular/common';
import { ApprovalModalComponent } from '@app/main/eclShared/approve-ecl-modal/approve-ecl-modal.component';
import { FileDownloadService } from '@shared/utils/file-download.service';

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
    showResultCard = true;
    inputParameter = new CreateOrEditLoanImpairmentInputParameterDto();
    results = new Array<CreateOrEditLoanImpairmentModelResultDto>();
    haircutRecovery = new CreateOrEditLoanImpairmentHaircutDto();
    loanImpairmentRecovery = new Array<CreateOrEditLoanImpairmentRecoveryDto>();
    loanImpairmentScenarios = new Array<CreateOrEditLoanImpairmentScenarioDto>();
    calibrationOfKeyParameters = new Array<CreateOrEditLoanImpairmentKeyParameterDto>();

    loanImpairmentScenario = new CreateOrEditLoanImpairmentScenarioDto();

    auditInfo = new LoanImpairmentAuditInfoDto();

    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
         private _loanImpairmentRegistersServiceProxy: LoanImpairmentRegistersServiceProxy,
         private _loanImpairmentScenariosServiceProxy: LoanImpairmentScenariosServiceProxy,
         private _loanImpairmentRecoveriesServiceProxy: LoanImpairmentRecoveriesServiceProxy,
         private _loanImpairmentKeyParametersServiceProxy: LoanImpairmentKeyParametersServiceProxy,
         private _loanImpairmentApprovalsServiceProxy: LoanImpairmentApprovalsServiceProxy,
         private _loanImpairmentResultsServiceProxy: LoanImpairmentModelResultsServiceProxy,
         private _router: Router,

         private _location: Location,
         private _fileDownloadService: FileDownloadService

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
                if (result.loanImpairmentRegister.loanImpairmentScenarios.length > 0) {
                    this.loanImpairmentScenario = result.loanImpairmentRegister.loanImpairmentScenarios[0];
                }
                if (this.loanImpairmentRegister.status == CalibrationStatusEnum.Completed) {
                    this._loanImpairmentResultsServiceProxy.getResults(this.loanImpairmentRegister.id).subscribe(res => {
                        this.results = res;
                    });
                }
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

    exportImpairmentRecovery(): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this.loanImpairmentRegister.id;
        this._loanImpairmentRecoveriesServiceProxy.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    exportImpairmentScenarios(): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this.loanImpairmentRegister.id;
        this._loanImpairmentScenariosServiceProxy.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    exportKeyParameters(): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this.loanImpairmentRegister.id;
        this._loanImpairmentKeyParametersServiceProxy.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    exportResult() {
        let dto = new EntityDtoOfGuid();
        dto.id = this.loanImpairmentRegister.id;
        this._loanImpairmentResultsServiceProxy.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    rerun() {
        this._loanImpairmentRegistersServiceProxy.rerun(this.loanImpairmentRegister).subscribe(() => {
            this.notify.success(this.l('Successful'));
            this._router.navigate( ['/app/main/loanImpairmentsRegisters/loanImpairmentRegisters']);

        });
    }
}
