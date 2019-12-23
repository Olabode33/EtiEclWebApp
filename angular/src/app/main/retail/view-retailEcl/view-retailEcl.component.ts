import { EadInputAssumptionGroupEnum, LdgInputAssumptionGroupEnum, CreateOrEditObeEclLgdAssumptionDto, CreateOrEditRetailEclApprovalDto } from './../../../../shared/service-proxies/service-proxies';
import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EclStatusEnum, CreateOrEditWholesaleEclDto, WholesaleEclsServiceProxy, RetailEclsServiceProxy, GetRetailEclForEditOutput, CreateOrEditRetailEclAssumptionDto, CreateOrEditRetailEclEadInputAssumptionDto, CreateOrEditRetailEclLgdAssumptionDto, CreateOrEditRetailEclDto, AssumptionGroupEnum, DataTypeEnum } from '@shared/service-proxies/service-proxies';
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

@Component({
    selector: 'app-view-retailEcl',
    templateUrl: './view-retailEcl.component.html',
    styleUrls: ['./view-retailEcl.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ViewRetailEclComponent extends AppComponentBase implements OnInit {

    @ViewChild('aproveEclModal', {static: true}) approveEclModel: ApproveEclModalComponent;

    _eclId = '';
    retailEclDetails: GetRetailEclForEditOutput = new GetRetailEclForEditOutput();
    retailEClDto: CreateOrEditRetailEclDto = new CreateOrEditRetailEclDto();
    frameworkAssumptions: CreateOrEditRetailEclAssumptionDto[] = new Array();
    eadInputAssumptions: CreateOrEditRetailEclEadInputAssumptionDto[] = new Array();
    lgdInputAssumptions: CreateOrEditRetailEclLgdAssumptionDto[] = new Array();

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
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _router: Router,
        private _location: Location
    ) {
        super(injector);
    }

    ngOnInit() {
        this._activatedRoute.paramMap.subscribe(params => {
            this._eclId = params.get('eclId');
            this.getEclDetails();

            let retailEcl = new CreateOrEditRetailEclApprovalDto();
            retailEcl.retailEclId = this._eclId;

            this.approveEclModel.configure({
                title: this.l('ApproveRetailEcl'),
                serviceProxy: this._retailEcLsServiceProxy,
                dataSource: retailEcl
            });
        });
    }

    getEclDetails() {
        this._retailEcLsServiceProxy.getRetailEclDetailsForEdit(this._eclId)
                                    .subscribe(result => {
                                        this.retailEclDetails = result;
                                        this.retailEClDto = result.retailEcl;
                                        this.frameworkAssumptions = result.frameworkAssumption;
                                        this.eadInputAssumptions = result.eadInputAssumptions;
                                        this.lgdInputAssumptions = result.lgdInputAssumptions;

                                        this.extractGeneralAssumptionGroups();
                                        this.extractEadAssumptionGroups();
                                        this.extractLgdAssumptionGroups();
                                    });
    }

    extractGeneralAssumptionGroups(): void {
        //Extract general assumption to groups
        this.scenarioAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === this.assumptionGroupEnum.ScenarioInputs);
        this.absoluteCreditQualityAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === this.assumptionGroupEnum.AbsoluteCreditQuality);
        this.relativeCreditQualityAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === this.assumptionGroupEnum.RelativeCreditQuality);
        this.forwardTransitionsAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === this.assumptionGroupEnum.ForwardTransitions);
        this.backwardTransitionAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === this.assumptionGroupEnum.BackwardTransitions);
    }

    extractEadAssumptionGroups(): void {
        //Extract general assumption to groups
        this.ccfGroup = this.eadInputAssumptions.filter(x => x.eadGroup === this.eadAssumptionGroupEnum.CreditConversionFactors);
        this.virGroup = this.eadInputAssumptions.filter(x => x.eadGroup === this.eadAssumptionGroupEnum.VariableInterestRateProjections);
        this.exchangeRateGroup = this.eadInputAssumptions.filter(x => x.eadGroup === this.eadAssumptionGroupEnum.ExchangeRateProjections);
    }

    extractLgdAssumptionGroups(): void {
        //Extract general assumption to groups
        this.cureRateGroup = this.lgdInputAssumptions.filter(x => x.lgdGroup === this.lgdAssumptionGroupEnum.UnsecuredRecoveriesCureRate);
        this.timeToDefaultGroup = this.lgdInputAssumptions.filter(x => x.lgdGroup === this.lgdAssumptionGroupEnum.UnsecuredRecoveriesTimeInDefault);
        this.corHighGroup = this.lgdInputAssumptions.filter(x => x.lgdGroup === this.lgdAssumptionGroupEnum.CostOfRecoveryHigh);
        this.corLowGroup = this.lgdInputAssumptions.filter(x => x.lgdGroup === this.lgdAssumptionGroupEnum.CostOfRecoveryLow);
        this.collateralGrowthGroup = this.lgdInputAssumptions.filter(x => x.lgdGroup === this.lgdAssumptionGroupEnum.CollateralGrowthBest);
        this.collateralTTRGroup = this.lgdInputAssumptions.filter(x => x.lgdGroup === this.lgdAssumptionGroupEnum.CollateralTTR);
    }

    editEcl() {
        this.notify.info('Yet to be implemented!!!');
    }

    approveEcl() {
        this.approveEclModel.show();
    }

    runEclComputation() {
        this.notify.info('Yet to be implemented!!!');
    }

    goBack() {
        this._location.back();
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
        {contractId: 'EXP OD|CONSUMER', exposurePre: 11768283635, impairmentPre: 8826212726.25, coverageRatioPre: 0.75, exposurePost: 11768283635, impairmentPost: 8826212726.25, coverageRatioPost: 0.75},
        {contractId: 'EXP CARD|CONSUMER', exposurePre: 498875934, impairmentPre: 374156950.5, coverageRatioPre: 0.75, exposurePost: 498875934, impairmentPost: 374156950.5, coverageRatioPost: 0.75},
        {contractId: '001NMLD150430001', exposurePre: 458886393, impairmentPre: 14232.4713275771, coverageRatioPre: 3.10152393809967E-05, exposurePost: 458886393, impairmentPost: 64693.0514889869, coverageRatioPost: 0.000140978360822712},
        {contractId: '001AMLD151180003', exposurePre: 239162605, impairmentPre: 0, coverageRatioPre: 0, exposurePost: 239162605, impairmentPost: 0, coverageRatioPost: 0},
        {contractId: '001ATLD152120002', exposurePre: 236295935, impairmentPre: 0, coverageRatioPre: 0, exposurePost: 236295935, impairmentPost: 0, coverageRatioPost: 0},
        {contractId: '012ABLD161120003', exposurePre: 156110269, impairmentPre: 1044248.22980634, coverageRatioPre: 0.0066891706515882, exposurePost: 156110269, impairmentPost: 902468.662894643, coverageRatioPost: 0.00578096923844672},
        {contractId: '012ABLD161120002', exposurePre: 155867838, impairmentPre: 1042610.87556199, coverageRatioPre: 0.00668906997710453, exposurePost: 155867838, impairmentPost: 901055.36965289, coverageRatioPost: 0.00578089348780786},
        {contractId: '102ABLD161970004', exposurePre: 152294379, impairmentPre: 1018684.75783881, coverageRatioPre: 0.00668891895109803, exposurePost: 152294379, impairmentPost: 880380.277559609, coverageRatioPost: 0.00578077985110408},
        {contractId: '102ABLD161450001', exposurePre: 150808347, impairmentPre: 999940.078250944, coverageRatioPre: 0.0066305353658637, exposurePost: 150808347, impairmentPost: 865164.897239087, coverageRatioPost: 0.00573685021054628},
        {contractId: '002ATLD140310002', exposurePre: 137188472, impairmentPre: 4587060.60825771, coverageRatioPre: 0.0334361957778618, exposurePost: 137188472, impairmentPost: 4682548.38951352, coverageRatioPost: 0.0341322293429547},
        {contractId: '102ABLD161130001', exposurePre: 93736340, impairmentPre: 626968.117855954, coverageRatioPre: 0.00668863450243475, exposurePost: 93736340, impairmentPost: 541849.083377946, coverageRatioPost: 0.00578056582300894},
        {contractId: '231AMLD150790001', exposurePre: 93114470, impairmentPre: 0, coverageRatioPre: 0, exposurePost: 93114470, impairmentPost: 0, coverageRatioPost: 0},
        {contractId: '002AHRL150970003', exposurePre: 92992521, impairmentPre: 0, coverageRatioPre: 0, exposurePost: 92992521, impairmentPost: 0, coverageRatioPost: 0},
        {contractId: '102ABLD163350002', exposurePre: 91553437, impairmentPre: 610339.197914643, coverageRatioPre: 0.00666648045026035, exposurePost: 91553437, impairmentPost: 527704.527704125, coverageRatioPost: 0.00576389641935698},
        {contractId: '102ABLD160980002', exposurePre: 89633360, impairmentPre: 594349.735283553, coverageRatioPre: 0.00663089875559225, exposurePost: 89633360, impairmentPost: 514237.668263681, coverageRatioPost: 0.00573712363637469},
        {contractId: '002AHRG132040001', exposurePre: 88638652, impairmentPre: 5687470.98283874, coverageRatioPre: 0.0641646827259821, exposurePost: 88638652, impairmentPost: 4974436.65057577, coverageRatioPost: 0.0561204005062686},
        {contractId: '102ABLD161730004', exposurePre: 87470684, impairmentPre: 585084.315083515, coverageRatioPre: 0.00668891894207109, exposurePost: 87470684, impairmentPost: 505648.767035378, coverageRatioPost: 0.00578077984431193},
        {contractId: 'EXP OD|2112281686', exposurePre: 77110042, impairmentPre: 16141677.5818559, coverageRatioPre: 0.209333015041749, exposurePost: 77110042, impairmentPost: 21311266.1447999, coverageRatioPost: 0.276374718416052},
        {contractId: '211AHRL130840067', exposurePre: 74959189, impairmentPre: 0, coverageRatioPre: 0, exposurePost: 74959189, impairmentPost: 0, coverageRatioPost: 0},
        {contractId: '002AMLD121250669', exposurePre: 70176989, impairmentPre: 527700.476985251, coverageRatioPre: 0.00751956566539569, exposurePost: 70176989, impairmentPost: 449865.816415641, coverageRatioPost: 0.00641044625633113},
    ];
}
