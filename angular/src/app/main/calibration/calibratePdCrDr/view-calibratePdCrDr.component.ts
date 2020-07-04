import { InputBehaviouralTermsDto, ResultBehaviouralTermsDto, CalibrationEadCcfSummaryServiceProxy, InputCcfSummaryDto, ResultEadCcfSummaryDto, CalibrationLgdHairCutServiceProxy, InputLgdHaircutDto, ResultLgdHairCutSummaryDto, CalibrationLgdRecoveryRateServiceProxy, InputLgdRecoveryRateDto, ResultLgdRecoveryRateDto, CalibrationPdCrDrServiceProxy, InputPdCrDrDto, ResultPd12MonthsDto, ResultPd12MonthsSummaryDto } from '../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, AfterViewInit } from '@angular/core';
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
    selector: 'viewCalibrationPdCrDr',
    templateUrl: './view-calibratePdCrDr.component.html',
    animations: [appModuleAnimation()]
})
export class ViewCalibrationPdCrDrComponent extends AppComponentBase implements OnInit, AfterViewInit {

    @ViewChild('approvalModal', { static: true }) approvalModal: ApprovalModalComponent;
    @ViewChild('excelFileUpload', { static: true }) excelFileUpload: FileUpload;

    uploadUrl = '';

    active = false;
    saving = false;

    showUploadCard = true;
    showHistoricCard = true;

    _calibrationId = '';
    calibration: CreateOrEditCalibrationRunDto = new CreateOrEditCalibrationRunDto();
    auditInfo: EclAuditInfoDto;
    approvalsAuditInfo: EclApprovalAuditInfoDto[];

    userName = '';
    affiliateName = '';

    allUsers: CalibrationEadBehaviouralTermUserLookupTableDto[];

    calibrationStatusEnum = CalibrationStatusEnum;
    genStatusEnum = GeneralStatusEnum;

    totalUploads = 0;
    totalHistoric = 0;
    uploads: InputPdCrDrDto[] = new Array();
    historic: InputPdCrDrDto[] = new Array();
    result: ResultPd12MonthsDto[] = new Array();
    resultSummary: ResultPd12MonthsSummaryDto = new ResultPd12MonthsSummaryDto();

    autoReloadSub: Subscription;

    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _calibrationServiceProxy: CalibrationPdCrDrServiceProxy,
        private _location: Location,
        private _httpClient: HttpClient,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/CalibrationData/ImportPdCrDrFromExcel';
        this.auditInfo = new EclAuditInfoDto();
        this.approvalsAuditInfo = new Array<EclApprovalAuditInfoDto>();
    }

    ngOnInit(): void {
        this._activatedRoute.paramMap.subscribe(params => {
            this._calibrationId = params.get('calibrationId');
            this.configureApprovalModal();
            this.show(this._calibrationId);
            this.getInputSummary();
        });
    }

    ngAfterViewInit(): void {
        this.getHistoricSummary();
    }

    configureApprovalModal(): void {
        let approvalDto = new CreateOrEditEclApprovalDto();
        approvalDto.eclId = this._calibrationId;
        approvalDto.reviewComment = '';
        approvalDto.status = GeneralStatusEnum.Draft;

        this.approvalModal.configure({
            title: this.l('ApproveCalibrationRun'),
            serviceProxy: this._calibrationServiceProxy,
            dataSource: approvalDto
        });
    }

    show(calibrationId?: string): void {

        if (!calibrationId) {
            this.calibration = new CreateOrEditCalibrationRunDto();
            this.calibration.id = calibrationId;
            this.userName = '';

            this.active = true;
        } else {
            this._calibrationServiceProxy.getCalibrationForEdit(calibrationId).subscribe(result => {
                this.calibration = result.calibration;
                this.userName = result.closedByUserName;
                this.affiliateName = result.affiliateName;
                this.auditInfo = result.auditInfo;
                this.approvalsAuditInfo = result.auditInfo.approvals;

                if (result.calibration.status === CalibrationStatusEnum.Completed || result.calibration.status === CalibrationStatusEnum.AppliedToEcl) {
                    this.getResults();
                }
                this.active = true;
            });
        }
    }

    getInputSummary(): void {
        this._calibrationServiceProxy.getInputSummary(this._calibrationId).subscribe(result => {
            this.totalUploads = result.total;
            this.uploads = result.items;
            // if (this.totalUploads > 0 && this.autoReloadSub) {
            //     this.autoReloadSub.unsubscribe();
            // }
        });
    }

    getHistoricSummary(): void {
        this._calibrationServiceProxy.getHistorySummary(this._calibrationId).subscribe(result => {
            this.totalHistoric = result.total;
            this.historic = result.items;
        });
    }

    getResults(): void {
        this._calibrationServiceProxy.getResult(this._calibrationId).subscribe(result => {
            this.result = result.pd12Months;
            this.resultSummary = result.pd12MonthsSummary;
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
                    let dto = new EntityDtoOfGuid();
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
                    let dto = new EntityDtoOfGuid();
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
        let dto = new EntityDtoOfGuid();
        dto.id = this._calibrationId;
        this._calibrationServiceProxy.generateReport(dto).subscribe(() => {
            this.message.success(this.l('EclReportGenerationStartedNotification'));
        });
    }

    applyToEcl(): void {
        this.message.confirm(
            this.l('ApplyCalibrationToEclInfo') + '<br><b>' + this.l('ApplyCalibrationNote') + '</b>', '',
            (isConfirmed) => {
                if (isConfirmed) {
                    let dto = new EntityDtoOfGuid();
                    dto.id = this._calibrationId;
                    this._calibrationServiceProxy.applyToEcl(dto)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('ApplyCalibrationProcessStart'));
                        });
                }
            }, true
        );
    }

    goBack(): void {
        this._location.back();
    }

    exportToExcel(): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this._calibrationId;
        this._calibrationServiceProxy.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    exportHistoric(): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this._calibrationId;
        this._calibrationServiceProxy.exportHistoryToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    uploadData(data: { files: File }): void {
        const formData: FormData = new FormData();
        const file = data.files[0];
        formData.append('file', file, file.name);
        formData.append('calibrationId', this._calibrationId);

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
                return 'danger';
            default:
                return 'dark';
        }
    }

    autoReloadUploadSummary(): void {
        this.autoReloadSub = secondsCounter.subscribe(n => {
                                console.log('Auto-reload: ' + n);
                                this.getInputSummary();
                            });
    }

}
