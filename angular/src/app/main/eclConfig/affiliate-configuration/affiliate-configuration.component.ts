import { CreateOrEditAffiliateDto, AffiliateConfigurationDto } from './../../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { NotifyService } from '@abp/notify/notify.service';
import { TokenAuthServiceProxy, EclSharedServiceProxy, AffiliateConfigurationServiceProxy, GetAffiliateConfigurationForViewDto } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';
import { OuLookupTableModalComponent } from '@app/main/eclShared/ou-lookup-modal/ou-lookup-table-modal.component';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-affiliate-configuration',
  templateUrl: './affiliate-configuration.component.html',
  styleUrls: ['./affiliate-configuration.component.css'],
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()]
})
export class AffiliateConfigurationComponent extends AppComponentBase implements OnInit {

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    filterText = '';

    selectedAffiliate: GetAffiliateConfigurationForViewDto;
    menuItem: MenuItem[];

    fromAffiliateId = -1;
    fromAffiliateName = '';

    sourceAffiliateId:number;

    constructor(
        injector: Injector,
        private _affiliateConfigServiceProxy: AffiliateConfigurationServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _router: Router
    ) {
        super(injector);
        this.fromAffiliateId = -1;
    }

    ngOnInit() {
        this.fromAffiliateId = -1;
        this.menuItem = [
            // { label: 'View', icon: 'fa fa-search', command: (event) => this.viewAffiliate(this.selectedAffiliate) },
            { label: this.l('CopyToNew'), icon: 'fa fa-plus', command: (event) => this.newAffiliate(this.selectedAffiliate) }
        ];
    }

    newAffiliate(fromAffiliate: GetAffiliateConfigurationForViewDto): void {

        this.sourceAffiliateId= fromAffiliate.affiliateConfiguration.id;
        this.selectedAffiliate = new GetAffiliateConfigurationForViewDto();
        this.selectedAffiliate.affiliateConfiguration = new AffiliateConfigurationDto();
        this.selectedAffiliate.affiliateConfiguration.overrideThreshold = fromAffiliate.affiliateConfiguration.overrideThreshold;
        this.selectedAffiliate.affiliateConfiguration.id = null;
        this.fromAffiliateId = fromAffiliate.affiliateConfiguration.id;
        this.fromAffiliateName = fromAffiliate.affiliateConfiguration.affiliateName;
    }

    viewAffiliate(affiliate: GetAffiliateConfigurationForViewDto): void {
        this.selectedAffiliate = affiliate;
    }

    applyOverride() {
        if (this.selectedAffiliate) {
            if (this.selectedAffiliate.affiliateConfiguration) {
                let c = new CreateOrEditAffiliateDto();
                c.currency = this.selectedAffiliate.affiliateConfiguration.currency;
                c.overrideThreshold = this.selectedAffiliate.affiliateConfiguration.overrideThreshold;
                c.displayName = this.selectedAffiliate.affiliateConfiguration.affiliateName;
                c.id = this.selectedAffiliate.affiliateConfiguration.id;

                if ( this.fromAffiliateId > 0) {
                    c.parentId = this.fromAffiliateId;
                }

                this._affiliateConfigServiceProxy.createOrEdit(c).subscribe(() => {
                    this.notify.success(this.l('SavedSuccessfully'));
                });
            }
        }
    }

    getAffiliates(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._affiliateConfigServiceProxy.getAll(
            this.filterText,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

}
