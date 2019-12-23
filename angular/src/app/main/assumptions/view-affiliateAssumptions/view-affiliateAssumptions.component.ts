import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy, EclSharedServiceProxy, CommonLookupServiceProxy, FrameworkEnum, AssumptionDto, EadInputAssumptionDto, LgdAssumptionDto, AssumptionTypeEnum, PdInputAssumptionDto, PdInputAssumptionMacroeconomicInputDto, PdInputAssumptionMacroeconomicProjectionDto, PdInputAssumptionNonInternalModelDto, PdInputAssumptionNplIndexDto, PdInputSnPCummulativeDefaultRateDto } from '@shared/service-proxies/service-proxies';
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
import { PdInputSnPCummulativeDefaultRatesComponent } from '@app/main/eclShared/pdInputSnPCummulativeDefaultRates/pdInputSnPCummulativeDefaultRates.component';

@Component({
    selector: 'app-view-affiliateAssumptions',
    templateUrl: './view-affiliateAssumptions.component.html',
    styleUrls: ['./view-affiliateAssumptions.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ViewAffiliateAssumptionsComponent extends AppComponentBase implements OnInit {

    _affiliateId = -1;

    frameworkEnum = FrameworkEnum;
    assumptionTypeEnum = AssumptionTypeEnum;
    portfolioList = [{key: FrameworkEnum.Wholesale, isActive: true}, {key: FrameworkEnum.Retail, isActive: false}, {key: FrameworkEnum.OBE, isActive: false}];
    selectedAffiliate = '';
    selectedPortfolio = '';
    selectedAssumption = '';

    frameworkAssumptions: AssumptionDto[] = new Array<AssumptionDto>();
    eadInputAssumptions: EadInputAssumptionDto[] = new Array<EadInputAssumptionDto>();
    lgdInputAssumptions: LgdAssumptionDto[] = new Array<LgdAssumptionDto>();
    pdInputAssumptions: PdInputAssumptionDto[] = new Array<PdInputAssumptionDto>();
    pdMacroeconomicInputAssumptions: PdInputAssumptionMacroeconomicInputDto[] = new Array<PdInputAssumptionMacroeconomicInputDto>();
    pdMacroeconomicProjectionsAssumptions: PdInputAssumptionMacroeconomicProjectionDto[] = new Array<PdInputAssumptionMacroeconomicProjectionDto>();
    pdNonInternalModelAssumptions: PdInputAssumptionNonInternalModelDto[] = new Array<PdInputAssumptionNonInternalModelDto>();
    pdNplIndexAssumptions: PdInputAssumptionNplIndexDto[] = new Array<PdInputAssumptionNplIndexDto>();
    pdSnpCumulativeDefaultRate: PdInputSnPCummulativeDefaultRateDto[] = new Array<PdInputSnPCummulativeDefaultRateDto>();


    constructor(
        injector: Injector,
        private _eclSharedServiceProxy: EclSharedServiceProxy,
        private _commonLookupServiceProxy: CommonLookupServiceProxy,
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
            this._affiliateId = +params.get('ouId');
            this.getOrganizationUnitName();

            //let retailEcl = new CreateOrEditRetailEclApprovalDto();
            //retailEcl.retailEclId = this._eclId;

            // this.approveEclModel.configure({
            //     title: this.l('ApproveRetailEcl'),
            //     serviceProxy: this._retailEcLsServiceProxy,
            //     dataSource: retailEcl
            // });
        });
    }

    goBack() {
        this._location.back();
    }

    selectPortfolioAssumption(portfolio: FrameworkEnum, assumption: AssumptionTypeEnum): void {
        this.selectedPortfolio = portfolio.toString();
        this.selectedAssumption = assumption.toString();
        switch (assumption) {
            case AssumptionTypeEnum.General:
                this.getAffiliateFrameworkAssumption(portfolio);
                break;
            case AssumptionTypeEnum.EadInputAssumption:
                this.getAffiliateEadAssumption(portfolio);
                break;
            case AssumptionTypeEnum.LgdInputAssumption:
                this.getAffiliateLgdAssumption(portfolio);
                break;
            default:
                this.getAffiliatePdAssumption(portfolio);
                break;
        }
    }

    editAssumption(): void {
        this.notify.info('Yet to be implemented!!!');
    }

    toggleAccordion(event, index) {
        let element = event.target;
        element.classList.toggle('active');
        this.portfolioList[index].isActive = !this.portfolioList[index].isActive;
    }

    getOrganizationUnitName(): void {
        this._commonLookupServiceProxy.getAffiliateNameFromId(this._affiliateId).subscribe(result => {
            this.selectedAffiliate = result;
        });
    }

    getAffiliateFrameworkAssumption(framework: FrameworkEnum): void {
        this.frameworkAssumptions = new Array<AssumptionDto>();
        this._eclSharedServiceProxy.getAffiliateFrameworkAssumption(this._affiliateId, framework).subscribe(result => {
            this.frameworkAssumptions = result;
        });
    }

    getAffiliateEadAssumption(framework: FrameworkEnum): void {
        this.eadInputAssumptions = new Array<EadInputAssumptionDto>();
        this._eclSharedServiceProxy.getAffiliateEadAssumption(this._affiliateId, framework).subscribe(result => {
            this.eadInputAssumptions = result;
        });
    }

    getAffiliateLgdAssumption(framework: FrameworkEnum): void {
        this.lgdInputAssumptions = new Array<LgdAssumptionDto>();
        this._eclSharedServiceProxy.getAffiliateLgdAssumption(this._affiliateId, framework).subscribe(result => {
            this.lgdInputAssumptions = result;
        });
    }

    getAffiliatePdAssumption(framework: FrameworkEnum): void {
        this.pdInputAssumptions = new Array<PdInputAssumptionDto>();
        this.pdMacroeconomicInputAssumptions = new Array<PdInputAssumptionMacroeconomicInputDto>();
        this.pdMacroeconomicProjectionsAssumptions = new Array<PdInputAssumptionMacroeconomicProjectionDto>();
        this.pdNonInternalModelAssumptions = new Array<PdInputAssumptionNonInternalModelDto>();
        this.pdNplIndexAssumptions = new Array<PdInputAssumptionNplIndexDto>();
        this.pdSnpCumulativeDefaultRate = new Array<PdInputSnPCummulativeDefaultRateDto>();

        this._eclSharedServiceProxy.getAllPdAssumptionsForAffiliate(this._affiliateId, framework).subscribe(result => {
            this.pdInputAssumptions = result.pdInputAssumption;
            this.pdMacroeconomicInputAssumptions = result.pdInputAssumptionMacroeconomicInput;
            this.pdMacroeconomicProjectionsAssumptions = result.pdInputAssumptionMacroeconomicProjections;
            this.pdNonInternalModelAssumptions = result.pdInputAssumptionNonInternalModels;
            this.pdNplIndexAssumptions = result.pdInputAssumptionNplIndex;
            this.pdSnpCumulativeDefaultRate = result.pdInputSnPCummulativeDefaultRate;
        });
    }
}
