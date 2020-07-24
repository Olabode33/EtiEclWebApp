import { InputBehaviouralTermsDto, ResultBehaviouralTermsDto, CalibrationEadCcfSummaryServiceProxy, InputCcfSummaryDto, ResultEadCcfSummaryDto, CalibrationLgdHairCutServiceProxy, InputLgdHaircutDto, ResultLgdHairCutSummaryDto, CalibrationLgdRecoveryRateServiceProxy, InputLgdRecoveryRateDto, ResultLgdRecoveryRateDto, CalibrationPdCrDrServiceProxy, InputPdCrDrDto, ResultPd12MonthsDto, ResultPd12MonthsSummaryDto, CalibrationMacroAnalysisServiceProxy, CreateOrEditMacroAnalysisApprovalDto, CreateOrEditMacroAnalysisRunDto, MacroResultPrincipalComponentDto, MacroResultStatisticsDto, MacroResultCorMatDto, MacroResultIndexDataDto, MacroResultPrincipalComponentSummaryDto, EntityDto, MacroeconomicVariableDto, GetMacroUploadSummaryDto, CommonLookupServiceProxy, GetSelectedMacroeconomicVariableDto } from '../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import {
    CalibrationEadBehaviouralTermsServiceProxy, CalibrationEadBehaviouralTermUserLookupTableDto, CreateOrEditCalibrationRunDto, GeneralStatusEnum, CreateOrEditEclApprovalDto, EntityDtoOfGuid, CalibrationStatusEnum, EclAuditInfoDto, EclApprovalAuditInfoDto
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ApprovalModalComponent } from '@app/main/eclShared/approve-ecl-modal/approve-ecl-modal.component';
import { Location } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { AppConsts } from '@shared/AppConsts';
import { FileUpload } from 'primeng/primeng';
import { interval, Subscription } from 'rxjs';
import { FileDownloadService } from '@shared/utils/file-download.service';

const secondsCounter = interval(5000);

@Component({
    selector: 'viewMacroAnalysis',
    templateUrl: './view-macroAnalysis.component.html',
    animations: [appModuleAnimation()]
})
export class ViewMacroAnalysisComponent extends AppComponentBase implements OnInit {

    @ViewChild('approvalModal', { static: true }) approvalModal: ApprovalModalComponent;
    @ViewChild('excelFileUpload', { static: true }) excelFileUpload: FileUpload;

    uploadUrl = '';

    active = false;
    saving = false;

    showUploadCard = true;

    _calibrationId = -1;
    calibration: CreateOrEditCalibrationRunDto = new CreateOrEditCalibrationRunDto();
    auditInfo: EclAuditInfoDto;
    approvalsAuditInfo: EclApprovalAuditInfoDto[];

    userName = '';
    affiliateName = '';

    allUsers: CalibrationEadBehaviouralTermUserLookupTableDto[];

    calibrationStatusEnum = CalibrationStatusEnum;
    genStatusEnum = GeneralStatusEnum;

    totalUploads = 0;
    periods: moment.Moment[] = new Array();
    inputColumns: string[] = new Array();
    inputValues: number[][] = new Array();
    resultPrincipal: MacroResultPrincipalComponentDto[] = new Array();
    resultStatistics: MacroResultStatisticsDto[] = new Array();
    resultCor: MacroResultCorMatDto[] = new Array();
    resultIndex: MacroResultIndexDataDto[] = new Array();
    resultPrincipalSummary: MacroResultPrincipalComponentSummaryDto[] = new Array();
    macroEconomicVariables = new Array<GetSelectedMacroeconomicVariableDto>();
    autoReloadSub: Subscription;

    uploadSummary: GetMacroUploadSummaryDto = new GetMacroUploadSummaryDto();

    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _calibrationServiceProxy: CalibrationMacroAnalysisServiceProxy,
        private _location: Location,
        private _httpClient: HttpClient,
        private _fileDownloadService: FileDownloadService,
        private _commonServiceProxy: CommonLookupServiceProxy
    ) {
        super(injector);
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/CalibrationData/ImportMacroAnalysisFromExcel';
        this.auditInfo = new EclAuditInfoDto();
        this.approvalsAuditInfo = new Array<EclApprovalAuditInfoDto>();
    }

    ngOnInit(): void {
        this._activatedRoute.paramMap.subscribe(params => {
            this._calibrationId = +params.get('calibrationId');
            this.getMacroEconomicVariables(params.get('affiliateId'));
            this.configureApprovalModal();
            this.show(this._calibrationId);
            this.getInputSummary();
        });
    }

    getMacroEconomicVariables(id) {
        this._calibrationServiceProxy.getMacroEconomicVariables(id).subscribe(res => {
            this.macroEconomicVariables = res;
        });
    }

    configureApprovalModal(status?): void {
        let approvalDto = new CreateOrEditMacroAnalysisApprovalDto();
        approvalDto.macroId = this._calibrationId;
        approvalDto.reviewComment = '';
        approvalDto.status = status == null ? GeneralStatusEnum.Draft : status;

        this.approvalModal.configure({
            title: this.l('ApproveCalibrationRun'),
            serviceProxy: this._calibrationServiceProxy,
            dataSource: approvalDto
        });
    }

    show(calibrationId?: number): void {

        if (!calibrationId) {
            this.calibration = new CreateOrEditCalibrationRunDto();
            this.calibration.id = null;
            this.userName = '';

            this.active = true;
        } else {
            this._calibrationServiceProxy.getCalibrationForEdit(calibrationId).subscribe(result => {
                this.calibration = result.calibration;
                this.userName = result.closedByUserName;
                this.affiliateName = result.affiliateName;
                this.auditInfo = result.auditInfo;
                this.approvalsAuditInfo = result.auditInfo.approvals;

                if(result.calibration.status == CalibrationStatusEnum.AppliedOverride) {
                    this.configureApprovalModal(CalibrationStatusEnum.AppliedOverride);
                }

                if (result.calibration.status === CalibrationStatusEnum.Completed || result.calibration.status === CalibrationStatusEnum.AppliedToEcl || result.calibration.status === CalibrationStatusEnum.AppliedOverride) {
                    this.getResults();
                }
                if (result.calibration.status === CalibrationStatusEnum.Draft ) {
                    this.getUploadSummary();
                }
                this.active = true;
            });
        }
    }

    getInputSummary(): void {
        this._calibrationServiceProxy.getInputSummary(this._calibrationId).subscribe(result => {
            this.totalUploads = result.periods.length;
            this.periods = result.periods;
            this.inputColumns = result.columns;
            this.inputValues = result.values;
        });
    }

    getResults(): void {
        this._calibrationServiceProxy.getResult(this._calibrationId).subscribe(result => {
            this.resultPrincipal = result.principalComponent;
            this.resultStatistics = result.statistics;
            this.resultCor = result.corMat;
            this.resultIndex = result.indexData;
            this.resultPrincipalSummary = result.principalComponentSummary;
        });
    }

    getUploadSummary(): void {
        this._commonServiceProxy.getMacroUploadSummary(this._calibrationId).subscribe(result => {
            this.uploadSummary = result;

            if (result && this.uploadSummary.status === GeneralStatusEnum.Processing) {
                setTimeout(() => this.getUploadSummary(), 5000);
            }
        });
    }

    reloadPage(): void {
        this.show(this._calibrationId);
    }

    submitEcl(): void {
        this.message.confirm(
            this.l('SubmitForApproval') + '?',
            (isConfirmed) => {
                if (isConfirmed) {
                    let dto = new EntityDto();
                    dto.id = this._calibrationId;
                    this._calibrationServiceProxy.submitForApproval(dto)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SubmittedSuccessfully'));
                        });
                }
            }
        );
    }

    approveEcl() {
        this.approvalModal.show();
    }

    runCalibration() {
        this.message.confirm(
            this.l('StartEclRun'),
            (isConfirmed) => {
                if (isConfirmed) {
                    let dto = new EntityDto();
                    dto.id = this._calibrationId;
                    this._calibrationServiceProxy.runCalibration(dto)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('CalibrationProcessStart'));
                        });
                }
            }
        );
    }

    generateReport(): void {
        let dto = new EntityDto();
        dto.id = this._calibrationId;
        this._calibrationServiceProxy.generateReport(dto).subscribe(() => {
            this.message.success(this.l('EclReportGenerationStartedNotification'));
        });
    }

    applyToEcl(): void {
        this.message.confirm(
            this.l('ApplyCalibrationToEclInfo'),
            (isConfirmed) => {
                if (isConfirmed) {
                    let dto = new EntityDto();
                    dto.id = this._calibrationId;
                    this._calibrationServiceProxy.applyToEcl(dto)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('ApplyCalibrationProcessStart'));
                        });
                }
            }
        );
    }

    goBack(): void {
        this._location.back();
    }

    exportToExcel(): void {
        let dto = new EntityDto();
        dto.id = this._calibrationId;
        this._calibrationServiceProxy.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    downloadTemplate(): void {
        let dto = new EntityDto();
        dto.id = this._calibrationId;
        this._calibrationServiceProxy.getInputTemplate(this._calibrationId).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    uploadData(data: { files: File }): void {
        const formData: FormData = new FormData();
        const file = data.files[0];
        formData.append('file', file, file.name);
        formData.append('calibrationId', this._calibrationId.toString());

        this.message.confirm(
            this.l('ExistingDataWouldBeReplaced'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._httpClient
                        .post<any>(this.uploadUrl, formData)
                        .pipe(finalize(() => this.excelFileUpload.clear()))
                        .subscribe(response => {
                            if (response.success) {
                                this.notify.success(this.l('ImportCalibrationDataProcessStart'));
                                this.autoReloadUploadSummary();
                                setTimeout(() => this.getUploadSummary(), 5000);
                            } else if (response.error != null) {
                                this.notify.error(this.l('ImportCalibrationDataUploadFailed'));
                            }
                        });
                }
            }
        );
    }

    onUploadExcelError(): void {
        this.notify.error(this.l('ImportCalibrationDataFailed'));
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

    autoReloadUploadSummary(): void {
        this.autoReloadSub = secondsCounter.subscribe(n => {
                                this.getInputSummary();
                            });
    }

    eraseCalibration(): void {
        this.message.confirm(
            '<span style="color: red">' + this.l('NoteCalibrationErasingThisIsNotReversible') + '</span>',
            this.l('AreYouSurePermanentDeleteCalibration?'),
            (isConfirmed) => {
                if (isConfirmed) {
                    let dto = new EntityDto();
                    dto.id = this._calibrationId;
                    this._calibrationServiceProxy.erase(dto)
                        .subscribe(() => {
                            this.notify.success(this.l('SuccessfullyDeleted'));
                            this.goBack();
                        });
                }
            }, true
        );
    }

}
