import { EadInputAssumptionGroupEnum, LdgInputAssumptionGroupEnum, CreateOrEditObeEclLgdAssumptionDto, CreateOrEditRetailEclApprovalDto, GetAllPdAssumptionsDto, FrameworkEnum, RetailEclUploadsServiceProxy, GetRetailEclUploadForViewDto, CreateOrEditRetailEclUploadDto, EntityDtoOfGuid, LgdAssumptionDto, EadInputAssumptionDto, AssumptionDto } from './../../../../shared/service-proxies/service-proxies';
import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EclStatusEnum, CreateOrEditWholesaleEclDto, WholesaleEclsServiceProxy, RetailEclsServiceProxy, GetRetailEclForEditOutput, CreateOrEditRetailEclAssumptionDto, CreateOrEditRetailEclEadInputAssumptionDto, CreateOrEditRetailEclLgdAssumptionDto, CreateOrEditRetailEclDto, AssumptionGroupEnum, DataTypeEnum, UploadDocTypeEnum, GeneralStatusEnum } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';
import { AppConsts } from '@shared/AppConsts';
import { Location } from '@angular/common';
import { ApproveEclModalComponent } from '@app/main/eclShared/approve-ecl-modal/approve-ecl-modal.component';
import { PdInputAssumptionsComponent } from '@app/main/assumptions/_subs/pdInputAssumptions/pdInputAssumptions.component';
import { HttpClient } from '@angular/common/http';
import { finalize } from 'rxjs/operators';
import { FileUpload } from 'primeng/fileupload';
import { interval } from 'rxjs';
import { FrameworkAssumptionsComponent } from '@app/main/assumptions/_subs/frameworkAssumptions/frameworkAssumptions.component';
import { EadInputAssumptionsComponent } from '@app/main/assumptions/_subs/eadInputAssumptions/eadInputAssumptions.component';
import { LgdInputAssumptionsComponent } from '@app/main/assumptions/_subs/lgdInputAssumptions/lgdInputAssumptions.component';

const secondsCounter = interval(5000);

@Component({
    selector: 'app-view-retailEcl',
    templateUrl: './view-retailEcl.component.html',
    styleUrls: ['./view-retailEcl.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ViewRetailEclComponent extends AppComponentBase implements OnInit {

    @ViewChild('aproveEclModal', {static: true}) approveEclModel: ApproveEclModalComponent;
    @ViewChild('frameworkAssumptionTag', {static: true}) frameworkAssumptionTag: FrameworkAssumptionsComponent;
    @ViewChild('eadInputAssumptionTag', {static: true}) eadInputAssumptionTag: EadInputAssumptionsComponent;
    @ViewChild('lgdInputAssumptionTag', {static: true}) lgdInputAssumptionTag: LgdInputAssumptionsComponent;
    @ViewChild('pdInputAssumptionTag', {static: true}) pdInputAssumptionTag: PdInputAssumptionsComponent;
    @ViewChild('UploadPaymentSchedule', {static: true}) excelUploadPaymentSchedule: FileUpload;

    _eclId = '';
    uploadPaymentUrl = '';
    uploadLoanbookUrl = '';
    retailEclDetails: GetRetailEclForEditOutput = new GetRetailEclForEditOutput();
    retailEClDto: CreateOrEditRetailEclDto = new CreateOrEditRetailEclDto();
    retailUploads: GetRetailEclUploadForViewDto[] = new Array();
    frameworkAssumptions: CreateOrEditRetailEclAssumptionDto[] = new Array();
    eadInputAssumptions: CreateOrEditRetailEclEadInputAssumptionDto[] = new Array();
    lgdInputAssumptions: CreateOrEditRetailEclLgdAssumptionDto[] = new Array();

    uploadDocEnum = UploadDocTypeEnum;
    statusEnum = GeneralStatusEnum;
    dataTypeEnum = DataTypeEnum;
    eclStatusEnum = EclStatusEnum;
    assumptionGroupEnum = AssumptionGroupEnum;
    eadAssumptionGroupEnum = EadInputAssumptionGroupEnum;
    lgdAssumptionGroupEnum = LdgInputAssumptionGroupEnum;

    //General Assumption Groups
    scenarioAssumptionGroup: CreateOrEditRetailEclAssumptionDto[] = new Array();
    absoluteCreditQualityAssumptionGroup: CreateOrEditRetailEclAssumptionDto[] = new Array();
    relativeCreditQualityAssumptionGroup: CreateOrEditRetailEclAssumptionDto[] = new Array();
    forwardTransitionsAssumptionGroup: CreateOrEditRetailEclAssumptionDto[] = new Array();
    backwardTransitionAssumptionGroup: CreateOrEditRetailEclAssumptionDto[] = new Array();
    showScenarioGroup = false;
    showAbsolute = false;
    showRelative = false;
    showForward = false;
    showBackWard = false;

    //Ead Assumptions Groups
    ccfGroup: CreateOrEditRetailEclEadInputAssumptionDto[] = new Array();
    virGroup: CreateOrEditRetailEclEadInputAssumptionDto[] = new Array();
    exchangeRateGroup: CreateOrEditRetailEclEadInputAssumptionDto[] = new Array();
    showCcfGroup = false;
    showVirGroup = false;
    showExchangeRateGroup = false;

    //Lgd Assumption Groups
    timeToDefaultGroup: CreateOrEditRetailEclLgdAssumptionDto[] = new Array();
    cureRateGroup: CreateOrEditRetailEclLgdAssumptionDto[] = new Array();
    corHighGroup: CreateOrEditRetailEclLgdAssumptionDto[] = new Array();
    corLowGroup: CreateOrEditRetailEclLgdAssumptionDto[] = new Array();
    collateralGrowthGroup: CreateOrEditRetailEclLgdAssumptionDto[] = new Array();
    collateralTTRGroup: CreateOrEditRetailEclLgdAssumptionDto[] = new Array();
    showUnsecuredRecoveries = false;
    showCorHigh = false;
    showCorLow = false;
    showCollateralGrowth = false;
    showTTR = false;

    fakeResultData: FakeResultData = new FakeResultData();
    showFakeTopExposure = false;

    constructor(
        injector: Injector,
        private _retailEcLsServiceProxy: RetailEclsServiceProxy,
        private _retailEclUploadServiceProxy: RetailEclUploadsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _router: Router,
        private _location: Location,
        private _httpClient: HttpClient
    ) {
        super(injector);
        this.uploadPaymentUrl = AppConsts.remoteServiceBaseUrl + '/EclRawData/ImportPaymentScheduleFromExcel';
        this.uploadLoanbookUrl = AppConsts.remoteServiceBaseUrl + '/EclRawData/ImportLoanbookFromExcel';
    }

    ngOnInit() {
        this._activatedRoute.paramMap.subscribe(params => {
            this._eclId = params.get('eclId');
            this.getEclDetails();
            this.getEclUploadSummary();

            let approveEcl = new CreateOrEditRetailEclApprovalDto();
            approveEcl.retailEclId = this._eclId;
            approveEcl.reviewComment = '';
            approveEcl.status = GeneralStatusEnum.Draft;


            this.approveEclModel.configure({
                title: this.l('ApproveRetailEcl'),
                serviceProxy: this._retailEcLsServiceProxy,
                dataSource: approveEcl
            });
        });
    }

    shortId(): string {
        if (this.retailEClDto !== null) {
            let split = this.retailEClDto.id.split('-');
            return split[split.length - 1];
        } else {
            return '';
        }
    }

    getEclDetails() {
        this._retailEcLsServiceProxy.getRetailEclDetailsForEdit(this._eclId)
                                    .subscribe(result => {
                                        this.retailEclDetails = result;
                                        this.retailEClDto = result.retailEcl;

                                        this.extractGeneralAssumptionGroups(result.frameworkAssumption);
                                        this.extractEadAssumptionGroups(result.eadInputAssumptions);
                                        this.extractLgdAssumptionGroups(result.lgdInputAssumptions);
                                        this.extractPdAssumptionGroups(result);
                                    });
    }

    extractGeneralAssumptionGroups(input: AssumptionDto[]): void {
        //Extract general assumption to groups
        this.scenarioAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === this.assumptionGroupEnum.ScenarioInputs);
        this.absoluteCreditQualityAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === this.assumptionGroupEnum.AbsoluteCreditQuality);
        this.relativeCreditQualityAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === this.assumptionGroupEnum.RelativeCreditQuality);
        this.forwardTransitionsAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === this.assumptionGroupEnum.ForwardTransitions);
        this.backwardTransitionAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === this.assumptionGroupEnum.BackwardTransitions);

        this.frameworkAssumptionTag.load(input, '', FrameworkEnum.Retail);
    }

    extractEadAssumptionGroups(input: EadInputAssumptionDto[]): void {
        //Extract general assumption to groups
        this.ccfGroup = this.eadInputAssumptions.filter(x => x.eadGroup === this.eadAssumptionGroupEnum.CreditConversionFactors);
        this.virGroup = this.eadInputAssumptions.filter(x => x.eadGroup === this.eadAssumptionGroupEnum.VariableInterestRateProjections);
        this.exchangeRateGroup = this.eadInputAssumptions.filter(x => x.eadGroup === this.eadAssumptionGroupEnum.ExchangeRateProjections);

        this.eadInputAssumptionTag.load(input, '', FrameworkEnum.Retail);
    }

    extractLgdAssumptionGroups(input: LgdAssumptionDto[]): void {
        //Extract general assumption to groups
        this.cureRateGroup = this.lgdInputAssumptions.filter(x => x.lgdGroup === this.lgdAssumptionGroupEnum.UnsecuredRecoveriesCureRate);
        this.timeToDefaultGroup = this.lgdInputAssumptions.filter(x => x.lgdGroup === this.lgdAssumptionGroupEnum.UnsecuredRecoveriesTimeInDefault);
        this.corHighGroup = this.lgdInputAssumptions.filter(x => x.lgdGroup === this.lgdAssumptionGroupEnum.CostOfRecoveryHigh);
        this.corLowGroup = this.lgdInputAssumptions.filter(x => x.lgdGroup === this.lgdAssumptionGroupEnum.CostOfRecoveryLow);
        this.collateralGrowthGroup = this.lgdInputAssumptions.filter(x => x.lgdGroup === this.lgdAssumptionGroupEnum.CollateralGrowthBest);
        this.collateralTTRGroup = this.lgdInputAssumptions.filter(x => x.lgdGroup === this.lgdAssumptionGroupEnum.CollateralTTR);

        this.lgdInputAssumptionTag.load(input, '' , FrameworkEnum.Retail);
    }

    extractPdAssumptionGroups(input: GetRetailEclForEditOutput): void {
        let pdAssumption: GetAllPdAssumptionsDto = new GetAllPdAssumptionsDto();
        pdAssumption.pdInputAssumption = input.pdInputAssumption;
        pdAssumption.pdInputAssumptionMacroeconomicInput = input.pdInputAssumptionMacroeconomicInput;
        pdAssumption.pdInputAssumptionMacroeconomicProjections = input.pdInputAssumptionMacroeconomicProjections;
        pdAssumption.pdInputAssumptionNonInternalModels = input.pdInputAssumptionNonInternalModels;
        pdAssumption.pdInputAssumptionNplIndex = input.pdInputAssumptionNplIndex;
        pdAssumption.pdInputSnPCummulativeDefaultRate = input.pdInputSnPCummulativeDefaultRate;

        this.pdInputAssumptionTag.load(pdAssumption, '', FrameworkEnum.Retail,  true);
    }

    getEclUploadSummary(): void {
        this._retailEclUploadServiceProxy.getEclUploads(this._eclId).subscribe(result => {
            this.retailUploads = result;
        });
    }

    editEcl() {
        this.notify.info('Yet to be implemented!!!');
    }

    submitEcl(): void {
        this.message.confirm(
            this.l('SubmitForApproval') + '?',
            (isConfirmed) => {
                if (isConfirmed) {
                    let dto = new EntityDtoOfGuid();
                    dto.id = this._eclId;
                    this._retailEcLsServiceProxy.submitForApproval(dto)
                        .subscribe(() => {
                            this.getEclDetails();
                            this.notify.success(this.l('SubmittedSuccessfully'));
                        });
                }
            }
        );
    }

    approveEcl() {
        this.approveEclModel.show();
    }

    eclReviewed(event?: any): void {
        if (event !== null) {
            if (event.status === GeneralStatusEnum.Approved) {
                setTimeout(() => this.runEclComputation(), 3000);
            }
            this.getEclDetails();
        }
    }

    runEclComputation() {
        this.message.confirm(
            this.l('StartEclRun'),
            (isConfirmed) => {
                if (isConfirmed) {
                    let dto = new EntityDtoOfGuid();
                    dto.id = this._eclId;
                    this._retailEcLsServiceProxy.runEcl(dto)
                        .subscribe(() => {
                            this.getEclDetails();
                            this.notify.success(this.l('EclRunProcessStart'));
                        });
                }
            }
        );
    }

    goBack() {
        this._location.back();
    }

    uploadLoanbook(data: { files: File }): void {
        let upload = new CreateOrEditRetailEclUploadDto();
        upload.docType = UploadDocTypeEnum.LoanBook;
        upload.retailEclId = this._eclId;
        upload.status = GeneralStatusEnum.Processing;
        upload.uploadComment = 'Generic sample';

        this._retailEclUploadServiceProxy.createOrEdit(upload).subscribe(result => {
            this.startLoanbookUpload(data, result);
            this.getEclUploadSummary();

        });

    }

    uploadPaymentSchedule(data: { files: File }): void {
        let upload = new CreateOrEditRetailEclUploadDto();
        upload.docType = UploadDocTypeEnum.PaymentSchedule;
        upload.retailEclId = this._eclId;
        upload.status = GeneralStatusEnum.Processing;
        upload.uploadComment = 'Generic sample';

        this._retailEclUploadServiceProxy.createOrEdit(upload).subscribe(result => {
            this.startPaymentUpload(data, result);
            this.getEclUploadSummary();
            //this.notify.success(this.l('UploadedSuccessfully'));
        });
    }

    onUploadExcelError(): void {
        this.notify.error(this.l('ImportEclDataFailed'));
    }

    startPaymentUpload(data: { files: File }, uploadSummaryId: string): void {
        const formData: FormData = new FormData();
        const file = data.files[0];
        formData.append('file', file, file.name);
        formData.append('uploadSummaryId', uploadSummaryId);
        formData.append('framework', FrameworkEnum.Retail.toString());

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

    startLoanbookUpload(data: { files: File }, uploadSummaryId: string): void {
        const formData: FormData = new FormData();
        const file = data.files[0];
        formData.append('file', file, file.name);
        formData.append('uploadSummaryId', uploadSummaryId);
        formData.append('framework', FrameworkEnum.Retail.toString());

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

    autoReloadUploadSummary(): void {
        let processing = this.retailUploads.filter(x => x.retailEclUpload.status === GeneralStatusEnum.Processing);
        const sub_ = secondsCounter.subscribe(n => {
                            console.log(`It's been ${n} seconds since subscribing!`);
                            this.getEclUploadSummary();
                        });
        // if (processing.length <= 0) {
        //     sub_.unsubscribe();
        //     this.getEclUploadSummary();
        // }
    }

    navigateToViewUploadDetails(uploadId: string, docType: UploadDocTypeEnum): void {
        switch (docType) {
            case UploadDocTypeEnum.LoanBook:
                this.navigateToViewLoanbookDetails(uploadId);
                break;
            case UploadDocTypeEnum.PaymentSchedule:
                this.navigateToViewPaymentScheduleDetails(uploadId);
                break;
            default:
                break;
        }
    }

    navigateToViewLoanbookDetails(uploadId: string): void {
        this._router.navigate(['/app/main/ecl/view/upload/loanbook/', FrameworkEnum.Retail.toString(), uploadId], { relativeTo: this._activatedRoute});
    }

    navigateToViewPaymentScheduleDetails(uploadId: string): void {
        this._router.navigate(['/app/main/ecl/view/upload/payment/', FrameworkEnum.Retail.toString(), uploadId], { relativeTo: this._activatedRoute});
    }

}


export class FakeResultData {
    overrallResult = {
                        totalExposure: 35347992226,
                        totalImpairmentPre: 9437696426,
                        totalImpairmentPost: 9437696426,
                        finalCoverageRatio: 0.2670
                    };

    scenarioImpairmentPre = {
                        bestEstimate: 9425044502,
                        optimistic: 9460292977,
                        downturn: 9437691553
                    };

    scenarioImpairmentPost = {
                        bestEstimate: 9425044502,
                        optimistic: 9460292977,
                        downturn: 9437691553
                    };

    stageImpairmentPre = {
                        stage1Exposure:	 22516270396,
                        Stage1Impairment: 120949689,
                        Stage2Exposure:	 173953707,
                        Stage2Impairment: 6641402,
                        Stage3Exposure: 12657768123,
                        Stage3Impairment: 9310105335
                    };

    stageImpairmentPost = {
                        stage1Exposure:	 22516270396,
                        Stage1Impairment: 120949689,
                        Stage2Exposure:	 173953707,
                        Stage2Impairment: 6641402,
                        Stage3Exposure: 12657768123,
                        Stage3Impairment: 9310105335
                    };

    topExposures = [
        {contractId: 'EXP OD|CONSUMER', customerName: 'HARRY EMMANUEL SOIBIBO', customerNo: '1223623', accountNo: '11008218', exposurePre: 11768283635, impairmentPre: 8826212726.25, coverageRatioPre: 0.75, exposurePost: 11768283635, impairmentPost: 8826212726.25, coverageRatioPost: 0.75},
        {contractId: 'EXP CARD|CONSUMER', customerName: 'IHEROME CHIOMA', customerNo: '1073685', accountNo: '12088462', exposurePre: 498875934, impairmentPre: 374156950.5, coverageRatioPre: 0.75, exposurePost: 498875934, impairmentPost: 374156950.5, coverageRatioPost: 0.75},
        {contractId: '001NMLD150430001', customerName: 'OLIVER ANDREWS', customerNo: '1679074', accountNo: '12063384', exposurePre: 458886393, impairmentPre: 14232.4713275771, coverageRatioPre: 3.10152393809967E-05, exposurePost: 458886393, impairmentPost: 64693.0514889869, coverageRatioPost: 0.000140978360822712},
        {contractId: '001AMLD151180003', customerName: 'BABADE DELE', customerNo: '1211881', accountNo: '1211881', exposurePre: 239162605, impairmentPre: 0, coverageRatioPre: 0, exposurePost: 239162605, impairmentPost: 0, coverageRatioPost: 0},
        {contractId: '001ATLD152120002', customerName: 'OBIGWE AUSTEN IHEANYI', customerNo: '2225126', accountNo: '12046099', exposurePre: 236295935, impairmentPre: 0, coverageRatioPre: 0, exposurePost: 236295935, impairmentPost: 0, coverageRatioPost: 0},
        {contractId: '012ABLD161120003', customerName: 'NELSON A EFIONG', customerNo: '2276946', accountNo: '129624177', exposurePre: 156110269, impairmentPre: 1044248.22980634, coverageRatioPre: 0.0066891706515882, exposurePost: 156110269, impairmentPost: 902468.662894643, coverageRatioPost: 0.00578096923844672},
        {contractId: '012ABLD161120002', customerName: 'SEN. OGOLA FOSTER', customerNo: '9518787', accountNo: '129624782', exposurePre: 155867838, impairmentPre: 1042610.87556199, coverageRatioPre: 0.00668906997710453, exposurePost: 155867838, impairmentPost: 901055.36965289, coverageRatioPost: 0.00578089348780786},
        {contractId: '102ABLD161970004', customerName: 'UMAR IBRAHIM KURFI', customerNo: '9518678', accountNo: '1023027505', exposurePre: 152294379, impairmentPre: 1018684.75783881, coverageRatioPre: 0.00668891895109803, exposurePost: 152294379, impairmentPost: 880380.277559609, coverageRatioPost: 0.00578077985110408},
        {contractId: '102ABLD161450001', customerName: 'YELE OMOGUNWA', customerNo: '9518507', accountNo: '1023028045', exposurePre: 150808347, impairmentPre: 999940.078250944, coverageRatioPre: 0.0066305353658637, exposurePost: 150808347, impairmentPost: 865164.897239087, coverageRatioPost: 0.00573685021054628},
        {contractId: '002ATLD140310002', customerName: 'AKINROYE OLUWOLE TEMITOPE', customerNo: '2229310', accountNo: '22167605', exposurePre: 137188472, impairmentPre: 4587060.60825771, coverageRatioPre: 0.0334361957778618, exposurePost: 137188472, impairmentPost: 4682548.38951352, coverageRatioPost: 0.0341322293429547},
        {contractId: '102ABLD161130001', customerName: 'BADERINWA BAMIDELE SAMSON', customerNo: '9521560', accountNo: '1023028492', exposurePre: 93736340, impairmentPre: 626968.117855954, coverageRatioPre: 0.00668863450243475, exposurePost: 93736340, impairmentPost: 541849.083377946, coverageRatioPost: 0.00578056582300894},
        {contractId: '231AMLD150790001', customerName: 'SENATOR LIYEL IMOKE', customerNo: '3899721', accountNo: '2312062627', exposurePre: 93114470, impairmentPre: 0, coverageRatioPre: 0, exposurePost: 93114470, impairmentPost: 0, coverageRatioPost: 0},
        {contractId: '002AHRL150970003', customerName: 'ADELEKE ADEKOLA AYANDELE', customerNo: '1191043', accountNo: '22165508', exposurePre: 92992521, impairmentPre: 0, coverageRatioPre: 0, exposurePost: 92992521, impairmentPost: 0, coverageRatioPost: 0},
        {contractId: '102ABLD163350002', customerName: 'ADARANIJO TAOFEEK ABIODUN', customerNo: '9518831', accountNo: '1023025147', exposurePre: 91553437, impairmentPre: 610339.197914643, coverageRatioPre: 0.00666648045026035, exposurePost: 91553437, impairmentPost: 527704.527704125, coverageRatioPost: 0.00576389641935698},
        {contractId: '102ABLD160980002', customerName: 'MICHAEL ADENIYI OMOGBEHIN', customerNo: '9521563', accountNo: '1023028526', exposurePre: 89633360, impairmentPre: 594349.735283553, coverageRatioPre: 0.00663089875559225, exposurePost: 89633360, impairmentPost: 514237.668263681, coverageRatioPost: 0.00573712363637469},
        {contractId: '002AHRG132040001', customerName: 'OGBOGU EDWIN', customerNo: '1007246', accountNo: '22144741', exposurePre: 88638652, impairmentPre: 5687470.98283874, coverageRatioPre: 0.0641646827259821, exposurePost: 88638652, impairmentPost: 4974436.65057577, coverageRatioPost: 0.0561204005062686},
        {contractId: '102ABLD161730004', customerName: 'HON. NSE EKPENYONG', customerNo: '9518930', accountNo: '1023025855', exposurePre: 87470684, impairmentPre: 585084.315083515, coverageRatioPre: 0.00668891894207109, exposurePost: 87470684, impairmentPost: 505648.767035378, coverageRatioPost: 0.00578077984431193},
        {contractId: 'EXP OD|2112281686', customerName: 'DIYA OLAJIDE OYEDELE', customerNo: '3054965', accountNo: '2112281686', exposurePre: 77110042, impairmentPre: 16141677.5818559, coverageRatioPre: 0.209333015041749, exposurePost: 77110042, impairmentPost: 21311266.1447999, coverageRatioPost: 0.276374718416052},
        {contractId: '211AHRL130840067', customerName: 'OKIBE ATTAH', customerNo: '3184814', accountNo: '2112274628', exposurePre: 74959189, impairmentPre: 0, coverageRatioPre: 0, exposurePost: 74959189, impairmentPost: 0, coverageRatioPost: 0},
        {contractId: '002AMLD121250669', customerName: 'OFFONG AND HAMDA AMBAH', customerNo: '1065545', accountNo: '22149629', exposurePre: 70176989, impairmentPre: 527700.476985251, coverageRatioPre: 0.00751956566539569, exposurePost: 70176989, impairmentPost: 449865.816415641, coverageRatioPost: 0.00641044625633113},
    ];
}
