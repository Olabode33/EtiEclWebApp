import { GetEclForEditOutput, CreateOrEditEclDto, EclUploadDto, CreateOrEditEclApprovalDto, GetEclUploadForViewDto } from './../../../../shared/service-proxies/service-proxies';
import { Component, OnInit, ViewEncapsulation, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ApprovalModalComponent } from '@app/main/eclShared/approve-ecl-modal/approve-ecl-modal.component';
import { FrameworkAssumptionsComponent } from '@app/main/assumptions/_subs/frameworkAssumptions/frameworkAssumptions.component';
import { EadInputAssumptionsComponent } from '@app/main/assumptions/_subs/eadInputAssumptions/eadInputAssumptions.component';
import { LgdInputAssumptionsComponent } from '@app/main/assumptions/_subs/lgdInputAssumptions/lgdInputAssumptions.component';
import { PdInputAssumptionsComponent } from '@app/main/assumptions/_subs/pdInputAssumptions/pdInputAssumptions.component';
import { FileUpload } from 'primeng/primeng';
import { GetRetailEclForEditOutput, CreateOrEditRetailEclDto, GetRetailEclUploadForViewDto, UploadDocTypeEnum, GeneralStatusEnum, DataTypeEnum, EclStatusEnum, AssumptionGroupEnum, EadInputAssumptionGroupEnum, LdgInputAssumptionGroupEnum, RetailEclsServiceProxy, RetailEclUploadsServiceProxy, TokenAuthServiceProxy, FrameworkEnum, WholesaleEclsServiceProxy, WholesaleEclUploadsServiceProxy, ObeEclsServiceProxy, ObeEclUploadsServiceProxy, InvestmentEclsServiceProxy, CreateOrEditRetailEclApprovalDto, GetWholesaleEclForEditOutput, CreateOrEditWholesaleEclDto, GetWholesaleEclUploadForViewDto, GetObeEclForEditOutput, CreateOrEditObeEclDto, GetObeEclUploadForViewDto, CreateOrEditObeEclApprovalDto, CreateOrEditInvestmentEclApprovalDto, AssumptionDto, EadInputAssumptionDto, LgdAssumptionDto, GetAllPdAssumptionsDto, GetAllInvSecPdAssumptionsDto, EntityDtoOfGuid, CreateOrEditWholesaleEclUploadDto, CreateOrEditRetailEclUploadDto, CreateOrEditObeEclUploadDto, CreateOrEditWholesaleEclApprovalDto, InvestmentEclUploadsServiceProxy, GetInvestmentEclUploadForViewDto, CreateOrEditInvestmentEclUploadDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { HttpClient } from '@angular/common/http';
import { AppConsts } from '@shared/AppConsts';
import { Location } from '@angular/common';
import { finalize } from 'rxjs/operators';
import { interval } from 'rxjs';
import { FakeResultData } from '@app/main/retail/view-retailEcl/view-retailEcl.component';
import { EclOverrideComponent } from '../_subs/ecl-override/ecl-override.component';
import { EclResultsComponent } from '../_subs/ecl-results/ecl-results.component';
import { EclAuditInfoComponent } from '../_subs/ecl-audit-info/ecl-audit-info.component';

const secondsCounter = interval(5000);

@Component({
    selector: 'app-view-ecl',
    templateUrl: './view-ecl.component.html',
    styleUrls: ['./view-ecl.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ViewEclComponent extends AppComponentBase implements OnInit {
    //TODO: Convert to use _sub component
    //TODO: Include pre-override result view (Computed)

    @ViewChild('approvalModal', { static: true }) approvalModal: ApprovalModalComponent;
    @ViewChild('frameworkAssumptionTag', { static: true }) frameworkAssumptionTag: FrameworkAssumptionsComponent;
    @ViewChild('eadInputAssumptionTag', { static: true }) eadInputAssumptionTag: EadInputAssumptionsComponent;
    @ViewChild('lgdInputAssumptionTag', { static: true }) lgdInputAssumptionTag: LgdInputAssumptionsComponent;
    @ViewChild('pdInputAssumptionTag', { static: true }) pdInputAssumptionTag: PdInputAssumptionsComponent;
    @ViewChild('UploadPaymentSchedule', { static: true }) excelUploadPaymentSchedule: FileUpload;
    @ViewChild('eclOverrideTag', { static: true }) eclOverrideTag: EclOverrideComponent;
    @ViewChild('eclResultTag', { static: true }) eclResultTag: EclResultsComponent;
    @ViewChild('eclAuditInfoTag', { static: true }) eclAuditInfoTag: EclAuditInfoComponent;


    isLoading = false;
    isLoadingUploads = false;

    _eclId = '';
    _eclFramework: FrameworkEnum;
    _eclServiceProxy: any;
    _eclUploadServiceProxy: any;

    uploadPaymentUrl = '';
    uploadLoanbookUrl = '';
    uploadAssetBookUrl = '';

    eclDetails: GetEclForEditOutput = new GetEclForEditOutput();
    eclDto: CreateOrEditEclDto = new CreateOrEditEclDto();
    eclUploads: GetEclUploadForViewDto[] = new Array<GetEclUploadForViewDto>();
    approvalDto: CreateOrEditEclApprovalDto = new CreateOrEditEclApprovalDto();

    frameworkEnum = FrameworkEnum;
    uploadDocEnum = UploadDocTypeEnum;
    statusEnum = GeneralStatusEnum;
    dataTypeEnum = DataTypeEnum;
    eclStatusEnum = EclStatusEnum;
    assumptionGroupEnum = AssumptionGroupEnum;
    eadAssumptionGroupEnum = EadInputAssumptionGroupEnum;
    lgdAssumptionGroupEnum = LdgInputAssumptionGroupEnum;

    fakeResultData: FakeResultData = new FakeResultData();
    showFakeTopExposure = false;

    constructor(
        injector: Injector,
        private _wholesaleEclServiceProxy: WholesaleEclsServiceProxy,
        private _wholesaleUploadServiceProxy: WholesaleEclUploadsServiceProxy,
        private _retailEcLsServiceProxy: RetailEclsServiceProxy,
        private _retailEclUploadServiceProxy: RetailEclUploadsServiceProxy,
        private _obeEclServiceProxy: ObeEclsServiceProxy,
        private _obeEclUploadServiceProxy: ObeEclUploadsServiceProxy,
        private _investmentEclServiceProxy: InvestmentEclsServiceProxy,
        private _investmentEclUploadServiceProxy: InvestmentEclUploadsServiceProxy,
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
        this.uploadAssetBookUrl = AppConsts.remoteServiceBaseUrl + '/EclRawData/ImportAssetFromExcel';
    }

    ngOnInit() {
        //this.isLoading = true;
        this._activatedRoute.paramMap.subscribe(params => {
            this._eclId = params.get('eclId');
            this._eclFramework = +params.get('framework');

            this.configureServiceProxies();
            this.configureDataSources();
            this.configureEclOverrideSubComponent();
            this.configureEclResultSubComponent();
            this.configureEclAuditInfoSubComponent();
            this.getEclDetails();
            this.getEclUploadSummary();
        });
    }

    configureServiceProxies(): void {
        switch (this._eclFramework) {
            case FrameworkEnum.Wholesale:
                this._eclServiceProxy = this._wholesaleEclServiceProxy;
                this._eclUploadServiceProxy = this._wholesaleUploadServiceProxy;
                break;
            case FrameworkEnum.Retail:
                this._eclServiceProxy = this._retailEcLsServiceProxy;
                this._eclUploadServiceProxy = this._retailEclUploadServiceProxy;
                break;
            case FrameworkEnum.OBE:
                this._eclServiceProxy = this._obeEclServiceProxy;
                this._eclUploadServiceProxy = this._obeEclUploadServiceProxy;
                break;
            case FrameworkEnum.Investments:
                this._eclServiceProxy = this._investmentEclServiceProxy;
                this._eclUploadServiceProxy = this._investmentEclUploadServiceProxy;
                break;
            default:
                throw Error(this.l('FrameworkDoesNotExistError'));
        }
    }

    configureDataSources(): void {
        switch (this._eclFramework) {
            case FrameworkEnum.Wholesale:
                this.configureApprovalModal(this.l('ApproveFrameworkEcl', 'Wholesale'));
                break;
            case FrameworkEnum.Retail:
                this.configureApprovalModal(this.l('ApproveFrameworkEcl', 'Retail'));
                break;
            case FrameworkEnum.OBE:
                this.configureApprovalModal(this.l('ApproveFrameworkEcl', 'OBE'));
                break;
            case FrameworkEnum.Investments:
                this.configureApprovalModal(this.l('ApproveFrameworkEcl', 'Investment'));
                break;
            default:
                throw Error(this.l('FrameworkDoesNotExistError'));
        }
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

    configureEclOverrideSubComponent(): void {
        this.eclOverrideTag.load(this._eclId, this._eclFramework);
    }

    configureEclResultSubComponent(): void {
        this.eclResultTag.load(this._eclId, this._eclFramework);
    }

    configureEclAuditInfoSubComponent(): void {
        this.eclAuditInfoTag.load(this._eclId, this._eclFramework);
    }

    getUploadDto(): any {
        switch (this._eclFramework) {
            case FrameworkEnum.Wholesale:
                return new CreateOrEditWholesaleEclUploadDto();
            case FrameworkEnum.Retail:
                return new CreateOrEditRetailEclUploadDto();
            case FrameworkEnum.OBE:
                return new CreateOrEditObeEclUploadDto();
            case FrameworkEnum.Investments:
                return new CreateOrEditInvestmentEclUploadDto();
            default:
                throw Error('FrameworkDoesNotExistError');
        }
    }

    goBack() {
        this._location.back();
    }

    getEclUploadSummary(): void {
        if (typeof this._eclUploadServiceProxy.getEclUploads === 'function') {
            this.isLoadingUploads = true;
            this._eclUploadServiceProxy.getEclUploads(this._eclId).subscribe(result => {
                this.eclUploads = result;
                this.isLoadingUploads = false;
            });
        }
    }

    getEclDetails() {
        if (typeof this._eclServiceProxy.getEclDetailsForEdit === 'function') {
            this.isLoading = true;
            this._eclServiceProxy.getEclDetailsForEdit(this._eclId)
                                 .subscribe(result => {
                                    this.eclDetails = result;
                                    if (this.checkDtoProp('eclDto', result)) {
                                        this.eclDto = result.eclDto;
                                    }
                                    if (this.hasProp('frameworkAssumption', result)) {
                                        this.loadGeneralAssumptionComponent(result.frameworkAssumption);
                                    }
                                    if (this.checkDtoProp('eadInputAssumptions', result)) {
                                        this.loadEadAssumptionComponent(result.eadInputAssumptions);
                                    }
                                    if (this.checkDtoProp('lgdInputAssumptions', result)) {
                                        this.loadLgdAssumptionComponent(result.lgdInputAssumptions);
                                    }
                                    this.loadPdAssumptionComponent(result);
                                    //console.log(this.showOverride());
                                    this.eclOverrideTag.display(this.showOverride());
                                    this.eclResultTag.displayResult(this.eclDto.status);
                                    this.isLoading = false;
                                });
        } else {
            throw Error('Function does not exist in service proxy');
        }
    }

    loadGeneralAssumptionComponent(input: AssumptionDto[]): void {
        this.frameworkAssumptionTag.load(input, '', this._eclFramework, true);
    }

    loadEadAssumptionComponent(input: EadInputAssumptionDto[]): void {
        this.eadInputAssumptionTag.load(input, '', this._eclFramework, true);
    }

    loadLgdAssumptionComponent(input: LgdAssumptionDto[]): void {
        this.lgdInputAssumptionTag.load(input, '', this._eclFramework, true);
    }

    loadPdAssumptionComponent(input: any): void {
        let pdAssumption: any;
        if (this._eclFramework === FrameworkEnum.Investments) {
            pdAssumption = new GetAllInvSecPdAssumptionsDto();

            pdAssumption.pdInputAssumption = input.pdInputAssumption;
            pdAssumption.pdInputAssumptionMacroeconomic = input.pdInputAssumptionMacroeconomic;
            pdAssumption.pdInputFitchCummulativeDefaultRate = input.pdInputFitchCummulativeDefaultRate;
        } else {
            pdAssumption = new GetAllPdAssumptionsDto();

            pdAssumption.pdInputAssumption = input.pdInputAssumption;
            pdAssumption.pdInputAssumptionMacroeconomicInput = input.pdInputAssumptionMacroeconomicInput;
            pdAssumption.pdInputAssumptionMacroeconomicProjections = input.pdInputAssumptionMacroeconomicProjections;
            pdAssumption.pdInputAssumptionNonInternalModels = input.pdInputAssumptionNonInternalModels;
            pdAssumption.pdInputAssumptionNplIndex = input.pdInputAssumptionNplIndex;
            pdAssumption.pdInputSnPCummulativeDefaultRate = input.pdInputSnPCummulativeDefaultRate;
        }

        this.pdInputAssumptionTag.load(pdAssumption, '', this._eclFramework,  true);
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
        if (typeof this._eclServiceProxy.runEcl === 'function') {
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
        } else {
            this.notify.error('Error: Function not available!');
        }
    }

    runEclPostComputation() {
        if (typeof this._eclServiceProxy.runPostEcl === 'function') {
            this.message.confirm(
                this.l('StartEclPostOverrideRun'),
                (isConfirmed) => {
                    if (isConfirmed) {
                        let dto = new EntityDtoOfGuid();
                        dto.id = this._eclId;
                        this._eclServiceProxy.runPostEcl(dto)
                            .subscribe(() => {
                                this.getEclDetails();
                                this.notify.success(this.l('EclRunProcessStart'));
                            });
                    }
                }
            );
        } else {
            this.notify.error('Error: Function not available!');
        }
    }

    closeEcl() {
        if (typeof this._eclServiceProxy.closeEcl === 'function') {
            this.message.confirm(
                this.l('CloseEclInfo'),
                (isConfirmed) => {
                    if (isConfirmed) {
                        let dto = new EntityDtoOfGuid();
                        dto.id = this._eclId;
                        this._eclServiceProxy.closeEcl(dto)
                            .subscribe(() => {
                                this.getEclDetails();
                                this.notify.success(this.l('EclCloseStartedNotification'));
                            });
                    }
                }
            );
        } else {
            this.notify.error('Error: Function not available!');
        }
    }

    generateReport(): void {
        if (typeof this._eclServiceProxy.generateReport === 'function') {

            let dto = new EntityDtoOfGuid();
            dto.id = this._eclId;
            this._eclServiceProxy.generateReport(dto).subscribe(() => {
                this.message.success(this.l('EclReportGenerationStartedNotification'));
            });
        } else {
            this.notify.error('Error: Function not available!');
        }
    }

    exportData(item: GetEclUploadForViewDto): void {
        switch (item.eclUpload.docType) {
            case UploadDocTypeEnum.LoanBook:
                this.exportLoanBook(this._eclId);
                break;

            case UploadDocTypeEnum.PaymentSchedule:
                this.exportPaymentSchedule(this._eclId);
                break;

            case UploadDocTypeEnum.AssetBook:
                this.exportAssetBook(item.eclUpload.id);
                break;

            default:
                break;
        }
    }

    exportAssetBook(id: string): void {
        if (typeof this._eclServiceProxy.exportAssetBookToExcel === 'function') {

            let dto = new EntityDtoOfGuid();
            dto.id = id;
            this._eclServiceProxy.exportAssetBookToExcel(dto).subscribe(result => {
                this._fileDownloadService.downloadTempFile(result);
            });
        } else {
            this.notify.error('Error: Function not available!');
        }
    }

    exportLoanBook(id: string): void {
        if (typeof this._eclServiceProxy.exportLoanBookToExcel === 'function') {

            let dto = new EntityDtoOfGuid();
            dto.id = this._eclId;
            this._eclServiceProxy.exportLoanBookToExcel(dto).subscribe(result => {
                this._fileDownloadService.downloadTempFile(result);
            });
        } else {
            this.notify.error('Error: Function not available!');
        }
    }

    exportPaymentSchedule(id: string): void {
        if (typeof this._eclServiceProxy.exportPaymentScheduleToExcel === 'function') {

            let dto = new EntityDtoOfGuid();
            dto.id = this._eclId;
            this._eclServiceProxy.exportPaymentScheduleToExcel(dto).subscribe(result => {
                this._fileDownloadService.downloadTempFile(result);
            });
        } else {
            this.notify.error('Error: Function not available!');
        }
    }

    deleteUploadSummary(id: string): void {
        if (typeof this._eclUploadServiceProxy.delete === 'function') {
            this.message.confirm(
                '',
                this.l('AreYouSure'),
                (isConfirmed) => {
                    if (isConfirmed) {
                        this._eclUploadServiceProxy.delete(id)
                            .subscribe(() => {
                                this.getEclUploadSummary();
                                this.notify.success(this.l('SuccessfullyDeleted'));
                            });
                    }
                }
            );
        } else {
            this.notify.error('Error: Function not available!');
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


    //#region Data Upload
    //#region Loanbook Upload
    uploadLoanbook(data: { files: File }): void {
        let upload = this.getUploadDto();
        upload.docType = UploadDocTypeEnum.LoanBook;
        upload.eclId = this._eclId;
        upload.status = GeneralStatusEnum.Processing;
        upload.uploadComment = '';

        this._eclUploadServiceProxy.createOrEdit(upload).subscribe(result => {
            this.startLoanbookUpload(data, result);
            this.getEclUploadSummary();

        });
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
        let upload = this.getUploadDto();
        upload.docType = UploadDocTypeEnum.PaymentSchedule;
        upload.eclId = this._eclId;
        upload.status = GeneralStatusEnum.Processing;
        upload.uploadComment = '';

        this._eclUploadServiceProxy.createOrEdit(upload).subscribe(result => {
            this.startPaymentUpload(data, result);
            this.getEclUploadSummary();
        });
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

    //#region Assetbook Upload
    uploadAssetBook(data: { files: File }): void {
        let upload = this.getUploadDto();
        upload.docType = UploadDocTypeEnum.AssetBook;
        upload.eclId = this._eclId;
        upload.status = GeneralStatusEnum.Processing;
        upload.uploadComment = '';

        this._eclUploadServiceProxy.createOrEdit(upload).subscribe(result => {
            this.startAssetBookUpload(data, result);
            this.getEclUploadSummary();
        });
    }

    startAssetBookUpload(data: { files: File }, uploadSummaryId: string): void {
        const formData: FormData = new FormData();
        const file = data.files[0];
        formData.append('file', file, file.name);
        formData.append('uploadSummaryId', uploadSummaryId);
        formData.append('framework', this._eclFramework.toString());

        this._httpClient
            .post<any>(this.uploadAssetBookUrl, formData)
            .pipe(finalize(() => this.excelUploadPaymentSchedule.clear()))
            .subscribe(response => {
                if (response.success) {
                    this.notify.success(this.l('ImportAssetBookProcessStart'));
                    this.autoReloadUploadSummary();
                } else if (response.error != null) {
                    this.notify.error(this.l('ImportAssetBookUploadFailed'));
                }
            });
    }
    //#endregion

    onUploadExcelError(): void {
        this.notify.error(this.l('ImportEclDataFailed'));
    }

    autoReloadUploadSummary(): void {
        let processing = this.eclUploads.filter(x => x.eclUpload.status === GeneralStatusEnum.Processing);
        const sub_ = secondsCounter.subscribe(n => {
                            //console.log(`It's been ${n} seconds since subscribing!`);
                            this.getEclUploadSummary();
                        });
        // if (processing.length <= 0) {
        //     sub_.unsubscribe();
        //     this.getEclUploadSummary();
        // }
    }
    //#endregion

    showOverride(): boolean {
        switch (this.eclDto.status) {
            case EclStatusEnum.PreOverrideComplete:
            case EclStatusEnum.PostOverrideComplete:
            case EclStatusEnum.Completed:
            case EclStatusEnum.Closed:
                return true;
            default:
                return false;
        }
    }

    //#region Navigation
    navigateToViewUploadDetails(uploadId: string, docType: UploadDocTypeEnum): void {
        switch (docType) {
            case UploadDocTypeEnum.LoanBook:
                this.navigateToViewLoanbookDetails(uploadId);
                break;
            case UploadDocTypeEnum.PaymentSchedule:
                this.navigateToViewPaymentScheduleDetails(uploadId);
                break;
            case UploadDocTypeEnum.AssetBook:
                this.navigateToViewAssetBookDetails(uploadId);
                break;
            default:
                break;
        }
    }

    navigateToViewLoanbookDetails(uploadId: string): void {
        this._router.navigate(['/app/main/ecl/view/upload/loanbook/', this._eclFramework.toString(), uploadId], { relativeTo: this._activatedRoute});
    }

    navigateToViewPaymentScheduleDetails(uploadId: string): void {
        this._router.navigate(['/app/main/ecl/view/upload/payment/', this._eclFramework.toString(), uploadId], { relativeTo: this._activatedRoute});
    }

    navigateToViewAssetBookDetails(uploadId: string): void {
        this._router.navigate(['/app/main/ecl/view/upload/assetbook/', this._eclFramework.toString(), uploadId], { relativeTo: this._activatedRoute});
    }
    //#endregion

    //#region Utility Functions
    checkDtoProp(prop: string, dto: any): boolean {
        if (this.hasProp(prop, dto)) {
            return true;
        } else {
            throw Error('Dto does not have property: ' + prop);
        }
    }

    hasProp(prop: string, obj: any): boolean {
        if (obj !== undefined) {
            //return Object.prototype.hasOwnProperty.call(this.dataSource, prop);
            return prop in obj;
        }
        return false;
    }

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
                return 'danger';
            default:
                return 'dark';
        }
    }
    //#endregion

}
