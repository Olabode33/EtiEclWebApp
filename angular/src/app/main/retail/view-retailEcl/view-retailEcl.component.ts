import { EadInputGroupEnum, LdgInputAssumptionEnum, CreateOrEditObeEclLgdAssumptionDto } from './../../../../shared/service-proxies/service-proxies';
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

@Component({
    selector: 'app-view-retailEcl',
    templateUrl: './view-retailEcl.component.html',
    styleUrls: ['./view-retailEcl.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ViewRetailEclComponent extends AppComponentBase implements OnInit {

    _eclId = '';
    retailEclDetails: GetRetailEclForEditOutput = new GetRetailEclForEditOutput();
    retailEClDto: CreateOrEditRetailEclDto = new CreateOrEditRetailEclDto();
    frameworkAssumptions: CreateOrEditRetailEclAssumptionDto[] = new Array();
    eadInputAssumptions: CreateOrEditRetailEclEadInputAssumptionDto[] = new Array();
    lgdInputAssumptions: CreateOrEditRetailEclLgdAssumptionDto[] = new Array();

    dataTypeEnum = DataTypeEnum;
    eclStatusEnum = EclStatusEnum;
    assumptionGroupEnum = AssumptionGroupEnum;
    eadAssumptionGroupEnum = EadInputGroupEnum;
    lgdAssumptionGroupEnum = LdgInputAssumptionEnum;

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
        this.notify.info('Yet to be implemented!!!');
    }

    runEclComputation() {
        this.notify.info('Yet to be implemented!!!');
    }

    goBack() {
        this._location.back();
    }

}
