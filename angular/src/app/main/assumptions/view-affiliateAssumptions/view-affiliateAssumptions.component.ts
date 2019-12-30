import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy, EclSharedServiceProxy, CommonLookupServiceProxy, FrameworkEnum, AssumptionDto, EadInputAssumptionDto, LgdAssumptionDto, AssumptionTypeEnum, PdInputAssumptionDto, PdInputAssumptionMacroeconomicInputDto, PdInputAssumptionMacroeconomicProjectionDto, PdInputAssumptionNonInternalModelDto, PdInputAssumptionNplIndexDto, PdInputSnPCummulativeDefaultRateDto, CreateOrEditAffiliateAssumptionsDto } from '@shared/service-proxies/service-proxies';
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
import { FrameworkAssumptionsComponent } from '../_subs/frameworkAssumptions/frameworkAssumptions.component';
import { EadInputAssumptionsComponent } from '../_subs/eadInputAssumptions/eadInputAssumptions.component';
import { LgdInputAssumptionsComponent } from '../_subs/lgdInputAssumptions/lgdInputAssumptions.component';
import { PdInputAssumptionsComponent } from '../_subs/pdInputAssumptions/pdInputAssumptions.component';
import { EditPortfolioReportDateComponent } from '../_subs/edit-portfolioReportDate/edit-portfolioReportDate.component';
import { EditAssumptionModalComponent } from '../_subs/edit-assumption-modal/edit-assumption-modal.component';

@Component({
    selector: 'app-view-affiliateAssumptions',
    templateUrl: './view-affiliateAssumptions.component.html',
    styleUrls: ['./view-affiliateAssumptions.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ViewAffiliateAssumptionsComponent extends AppComponentBase implements OnInit {

    @ViewChild('frameworkAssumptionTag', {static: true}) frameworkAssumptionTag: FrameworkAssumptionsComponent;
    @ViewChild('eadInputAssumptionTag', {static: true}) eadInputAssumptionTag: EadInputAssumptionsComponent;
    @ViewChild('lgdInputAssumptionTag', {static: true}) lgdInputAssumptionTag: LgdInputAssumptionsComponent;
    @ViewChild('pdInputAssumptionTag', {static: true}) pdInputAssumptionTag: PdInputAssumptionsComponent;
    @ViewChild('editReportDateModal', {static: true}) editReportDateModal: EditPortfolioReportDateComponent;

    _affiliateId = -1;

    frameworkEnum = FrameworkEnum;
    assumptionTypeEnum = AssumptionTypeEnum;
    portfolioList = [
        {key: FrameworkEnum.Wholesale, isActive: true, reportDate: moment().endOf('month')},
        {key: FrameworkEnum.Retail, isActive: false, reportDate: moment().endOf('month')},
        {key: FrameworkEnum.OBE, isActive: false, reportDate: moment().endOf('month')}
    ];
    selectedAffiliate = '';
    selectedPortfolio = '';
    selectedAssumption = '';
    reportDate = moment().endOf('month');
    affiliateAssumption: CreateOrEditAffiliateAssumptionsDto = new CreateOrEditAffiliateAssumptionsDto();


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
            this.getAffiliateAssumptionSummary();

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
        this.portfolioList = this.portfolioList.map(x => { x.isActive = false; return x; } );
        this.portfolioList[index].isActive = !this.portfolioList[index].isActive;
    }

    getOrganizationUnitName(): void {
        this._commonLookupServiceProxy.getAffiliateNameFromId(this._affiliateId).subscribe(result => {
            this.selectedAffiliate = result;
        });
    }

    getAffiliateAssumptionSummary(): void {
        this._eclSharedServiceProxy.getAffiliateAssumptionForEdit(this._affiliateId).subscribe(result => {
            this.affiliateAssumption = result;
            this.portfolioList.find(x => x.key === FrameworkEnum.Wholesale).reportDate = result.lastWholesaleReportingDate;
            this.portfolioList.find(x => x.key === FrameworkEnum.Retail).reportDate = result.lastRetailReportingDate;
            this.portfolioList.find(x => x.key === FrameworkEnum.OBE).reportDate = result.lastObeReportingDate;
        });
    }

    getAffiliateFrameworkAssumption(framework: FrameworkEnum): void {
        this._eclSharedServiceProxy.getAffiliateFrameworkAssumption(this._affiliateId, framework).subscribe(result => {
            this.frameworkAssumptionTag.load(result);
            //hide others
            this.eadInputAssumptionTag.hide();
            this.lgdInputAssumptionTag.hide();
            this.pdInputAssumptionTag.hide();
        });
    }

    getAffiliateEadAssumption(framework: FrameworkEnum): void {
        this._eclSharedServiceProxy.getAffiliateEadAssumption(this._affiliateId, framework).subscribe(result => {
            this.eadInputAssumptionTag.load(result, this.selectedAffiliate, framework);
            //hide others
            this.frameworkAssumptionTag.hide();
            this.lgdInputAssumptionTag.hide();
            this.pdInputAssumptionTag.hide();
        });
    }

    getAffiliateLgdAssumption(framework: FrameworkEnum): void {
        this._eclSharedServiceProxy.getAffiliateLgdAssumption(this._affiliateId, framework).subscribe(result => {
            this.lgdInputAssumptionTag.load(result);
            //hide others
            this.frameworkAssumptionTag.hide();
            this.eadInputAssumptionTag.hide();
            this.pdInputAssumptionTag.hide();
        });
    }

    getAffiliatePdAssumption(framework: FrameworkEnum): void {
        this._eclSharedServiceProxy.getAllPdAssumptionsForAffiliate(this._affiliateId, framework).subscribe(result => {
            this.pdInputAssumptionTag.load(result);
            //hide others
            this.frameworkAssumptionTag.hide();
            this.eadInputAssumptionTag.hide();
            this.lgdInputAssumptionTag.hide();

        });
    }

    editReportDate(framework: FrameworkEnum): void {
        this.editReportDateModal.configure({
            framework: framework,
            affiliateAssumption: this.affiliateAssumption,
            affiliateName: this.selectedAffiliate
        });
        this.editReportDateModal.show();
    }
}
