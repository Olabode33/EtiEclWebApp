import { GetBatchEclForEditOutput, GetAllEclForWorkspaceDto } from './../../../../shared/service-proxies/service-proxies';
import { Component, OnInit, ViewEncapsulation, ViewChild, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ApprovalModalComponent } from '@app/main/eclShared/approve-ecl-modal/approve-ecl-modal.component';
import { EclAuditInfoComponent } from '../_subs/ecl-audit-info/ecl-audit-info.component';
import { EditEclReportDateComponent } from '../_subs/edit-EclReportDate/edit-EclReportDate.component';
import { FrameworkEnum, GetEclForEditOutput, CreateOrEditEclDto, GetEclUploadForViewDto, CreateOrEditEclApprovalDto, UploadDocTypeEnum, GeneralStatusEnum, DataTypeEnum, EclStatusEnum, BatchEclsServiceProxy, CommonLookupServiceProxy, TokenAuthServiceProxy, EntityDtoOfGuid, GetBatchEclUploadForViewDto, CreateOrEditObeEclUploadDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { HttpClient } from '@angular/common/http';
import { AppConsts } from '@shared/AppConsts';
import { Location } from '@angular/common';
import { finalize } from 'rxjs/operators';
import { FileUpload } from 'primeng/primeng';

@Component({
  selector: 'app-view-batchEcl',
  templateUrl: './view-batchEcl.component.html',
  styleUrls: ['./view-batchEcl.component.css'],
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()]
})
export class ViewBatchEclComponent extends AppComponentBase implements OnInit {

    @ViewChild('approvalModal', { static: true }) approvalModal: ApprovalModalComponent;
    @ViewChild('eclAuditInfoTag', { static: true }) eclAuditInfoTag: EclAuditInfoComponent;
    @ViewChild('editEclReportDate', { static: true }) editEclReportDate: EditEclReportDateComponent;
    @ViewChild('UploadLoanSnapshot', { static: true }) excelUploadLoanSnapshot: FileUpload;
    @ViewChild('UploadPaymentSchedule', { static: true }) excelUploadPaymentSchedule: FileUpload;

    isLoading = false;
    isLoadingUploads = false;

    _eclId = '';
    _eclFramework: FrameworkEnum;

    uploadPaymentUrl = '';
    uploadLoanbookUrl = '';
    uploadAssetBookUrl = '';

    eclDetails: GetBatchEclForEditOutput = new GetBatchEclForEditOutput();
    eclDto: CreateOrEditEclDto = new CreateOrEditEclDto();
    eclUploads: GetBatchEclUploadForViewDto[] = new Array<GetBatchEclUploadForViewDto>();
    approvalDto: CreateOrEditEclApprovalDto = new CreateOrEditEclApprovalDto();
    subEclList: GetAllEclForWorkspaceDto[] = new Array();

    frameworkEnum = FrameworkEnum;
    uploadDocEnum = UploadDocTypeEnum;
    statusEnum = GeneralStatusEnum;
    dataTypeEnum = DataTypeEnum;
    eclStatusEnum = EclStatusEnum;

    uploadRefreshInterval = 10000;

    constructor(
        injector: Injector,
        private _eclServiceProxy: BatchEclsServiceProxy,
        private _commonServiceProxy: CommonLookupServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _router: Router,
        private _location: Location,
        private _httpClient: HttpClient
    ) {
        super(injector);
        this.isLoading = true;
        this.uploadPaymentUrl = AppConsts.remoteServiceBaseUrl + '/EclRawData/ImportPaymentScheduleFromExcel';
        this.uploadLoanbookUrl = AppConsts.remoteServiceBaseUrl + '/EclRawData/ImportLoanbookFromExcel';
    }

    ngOnInit() {
        //this.isLoading = true;
        this._activatedRoute.paramMap.subscribe(params => {
            this._eclId = params.get('eclId');
            this._eclFramework = FrameworkEnum.Batch;

            this.configureEclAuditInfoSubComponent();
            this.configureApprovalModal(this.l('ApproveFrameworkEcl', 'Batch'));
            this.getEclDetails();
            this.getEclUploadSummary();
        });
    }

    configureApprovalModal(title: string): void {

        this.approvalDto.eclId = this._eclId;
        this.approvalDto.reviewComment = '';
        this.approvalDto.status = GeneralStatusEnum.Draft;

        this.approvalModal.configure({
            title: title,
            serviceProxy: this._eclServiceProxy,
            dataSource: this.approvalDto
        });
    }

    configureEclAuditInfoSubComponent(): void {
        this.eclAuditInfoTag.load(this._eclId, this._eclFramework);
    }

    configureEclReportDateSubComponent(eclDto?: CreateOrEditEclDto): void {
        if (eclDto) {
            this.editEclReportDate.configure({
                eclDto: eclDto,
                serviceProxy: this._eclServiceProxy
            });
        } else {
            this.editEclReportDate.configure({
                eclDto: new CreateOrEditEclDto(),
                serviceProxy: this._eclServiceProxy
            });
        }
    }

    goBack() {
        this._location.back();
    }

    viewEcl(framework: FrameworkEnum, eclId: string): void {
        this._router.navigate(['/app/main/ecl/view/', framework.toString(), eclId]);
    }

    getEclDetails() {
        this.isLoading = true;
            this._eclServiceProxy.getEclDetailsForEdit(this._eclId)
                .subscribe(result => {
                    this.eclDetails = result;
                    this.eclDto = result.eclDto;
                    this.subEclList = result.subEcls;
                    this.configureEclReportDateSubComponent(result.eclDto);
                    console.log(result);

                    if (this.eclDto.status === EclStatusEnum.LoadingAssumptions ) {
                        setTimeout(() => {
                            this.getEclDetails();
                        }, this.uploadRefreshInterval);
                    }

                    this.isLoading = false;
                });
    }

    getEclUploadSummary(): void {
        this.isLoadingUploads = true;
            this._eclServiceProxy.getEclUploads(this._eclId).subscribe(result => {
                this.eclUploads = result;
                this.isLoadingUploads = false;
                setTimeout(() => this.autoReloadUploadSummary(), this.uploadRefreshInterval);
            });
    }

    editReportDate(): void {
        this.editEclReportDate.show();
    }

    submitEcl(): void {
        if (typeof this._eclServiceProxy.submitForApproval === 'function') {
            this.message.confirm(
                this.l('SubmitForApproval') + '?',
                (isConfirmed) => {
                    if (isConfirmed) {
                        let dto = new EntityDtoOfGuid();
                        dto.id = this._eclId;
                        this._eclServiceProxy.submitForApproval(dto)
                            .subscribe(() => {
                                this.getEclDetails();
                                this.notify.success(this.l('SubmittedSuccessfully'));
                            });
                    }
                }
            );
        } else {
            this.notify.error('Error: Function not available!');
        }
    }

    approveEcl() {
        this.approvalModal.show();
    }

    runEclComputation() {
        this.message.confirm(
            this.l('StartEclRun'),
            (isConfirmed) => {
                if (isConfirmed) {
                    let dto = new EntityDtoOfGuid();
                    dto.id = this._eclId;
                    this._eclServiceProxy.runEcl(dto)
                        .subscribe(() => {
                            this.getEclDetails();
                            this.notify.success(this.l('EclRunProcessStart'));
                        });
                }
            }
        );
    }

    //#region Upload
    //#region Loanbook Upload
    uploadLoanbook(data: { files: File }): void {
        this.message.confirm(
            this.l('ExistingDataWouldBeReplaced'),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    let upload = new CreateOrEditObeEclUploadDto();
                    upload.docType = UploadDocTypeEnum.LoanBook;
                    upload.eclId = this._eclId;
                    upload.status = GeneralStatusEnum.Processing;
                    upload.uploadComment = '';

                    this._eclServiceProxy.createUploadRecord(upload).subscribe(result => {
                        this.startLoanbookUpload(data, result);
                        this.getEclUploadSummary();
                    });
                }
            }
        );
    }

    startLoanbookUpload(data: { files: File }, uploadSummaryId: string): void {
        const formData: FormData = new FormData();
        const file = data.files[0];
        formData.append('file', file, file.name);
        formData.append('uploadSummaryId', uploadSummaryId);
        formData.append('framework', this._eclFramework.toString());

        this._httpClient
            .post<any>(this.uploadLoanbookUrl, formData)
            .pipe(finalize(() => this.excelUploadPaymentSchedule.clear()))
            .subscribe(response => {
                if (response.success) {
                    this.notify.success(this.l('ImportLoanbookProcessStart'));
                    this.autoReloadUploadSummary();
                } else if (response.error != null) {
                    this.notify.error(this.l('ImportLoanbookUploadFailed'));
                }
            });
    }
    //#endregion

    //#region Playment Schedule Upload
    uploadPaymentSchedule(data: { files: File }): void {
        this.message.confirm(
            this.l('ExistingDataWouldBeReplaced'),
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    let upload = new CreateOrEditObeEclUploadDto();
                    upload.docType = UploadDocTypeEnum.PaymentSchedule;
                    upload.eclId = this._eclId;
                    upload.status = GeneralStatusEnum.Processing;
                    upload.uploadComment = '';

                    this._eclServiceProxy.createUploadRecord(upload).subscribe(result => {
                        this.startPaymentUpload(data, result);
                        this.getEclUploadSummary();
                    });
                }
            }
        );
    }

    startPaymentUpload(data: { files: File }, uploadSummaryId: string): void {
        const formData: FormData = new FormData();
        const file = data.files[0];
        formData.append('file', file, file.name);
        formData.append('uploadSummaryId', uploadSummaryId);
        formData.append('framework', this._eclFramework.toString());

        this._httpClient
            .post<any>(this.uploadPaymentUrl, formData)
            .pipe(finalize(() => this.excelUploadPaymentSchedule.clear()))
            .subscribe(response => {
                if (response.success) {
                    this.notify.success(this.l('ImportPaymentScheduleProcessStart'));
                    this.autoReloadUploadSummary();
                } else if (response.error != null) {
                    this.notify.error(this.l('ImportPaymentScheduleUploadFailed'));
                }
            });
    }
    //#endregion

    onUploadExcelError(): void {
        this.notify.error(this.l('ImportEclDataFailed'));
    }

    //#region Data Export
    exportData(item: GetEclUploadForViewDto): void {
        switch (item.eclUpload.docType) {
            case UploadDocTypeEnum.LoanBook:
                this.exportLoanBook(this._eclId);
                break;

            case UploadDocTypeEnum.PaymentSchedule:
                this.exportPaymentSchedule(this._eclId);
                break;

            default:
                break;
        }
    }

    exportLoanBook(id: string): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this._eclId;
        this._eclServiceProxy.exportLoanBookToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    exportPaymentSchedule(id: string): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this._eclId;
        this._eclServiceProxy.exportPaymentScheduleToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }
    //#endregion Data Export

    deleteUploadSummary(id: string): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._eclServiceProxy.deleteUploadRecord(id)
                        .subscribe(() => {
                            this.getEclUploadSummary();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
    //#endregion

    getStatusLabelClass(uploadStatus: GeneralStatusEnum): string {
        switch (uploadStatus) {
            case GeneralStatusEnum.Draft:
                return 'primary';
            case GeneralStatusEnum.Submitted:
            case GeneralStatusEnum.Processing:
                return 'warning';
            case GeneralStatusEnum.Completed:
            case GeneralStatusEnum.Approved:
                return 'success';
            case GeneralStatusEnum.Rejected:
            case GeneralStatusEnum.Failed:
                return 'danger';
            default:
                return 'dark';
        }
    }

    eclReviewed(event?: any): void {
        if (event !== null) {
            // if (event.status === GeneralStatusEnum.Approved) {
            //     setTimeout(() => this.runEclComputation(), 3000);
            // }
            this.getEclDetails();
        }
    }

    autoReloadUploadSummary(): void {
        let processing = this.eclUploads.filter(x => x.eclUpload.status === GeneralStatusEnum.Processing);
        if (processing.length > 0) {
            //this.getEclUploadSummary();
            setTimeout(() => this.getEclUploadSummary(), this.uploadRefreshInterval);
        }
    }

}
