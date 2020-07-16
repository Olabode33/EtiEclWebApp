import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { NotifyService } from '@abp/notify/notify.service';
import { TokenAuthServiceProxy, EclSharedServiceProxy, CopyAffiliateDto, NameValueDtoOfInt64, CommonLookupServiceProxy } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';
import { Location } from '@angular/common';
import { OuLookupTableModalComponent } from '@app/main/eclShared/ou-lookup-modal/ou-lookup-table-modal.component';

@Component({
    selector: 'app-affiliateAssumption',
    templateUrl: './affiliateAssumption.component.html',
    styleUrls: ['./affiliateAssumption.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class AffiliateAssumptionComponent extends AppComponentBase implements OnInit {

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    @ViewChild('ouLookupTableModal', { static: true }) ouLookupTableModal: OuLookupTableModalComponent;

    filterText = '';
    availableForReviewOnly = false;
    copying = false;
    copyFromOuId = -1;
    copyFromOuName = '';

    _affiliateId = -1;
    ouList: NameValueDtoOfInt64[] = new Array();

    constructor(
        injector: Injector,
        private _eclSharedServiceProxy: EclSharedServiceProxy,
        private _commonServiceProxy: CommonLookupServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _router: Router,
        private _location: Location
    ) {
        super(injector);
        _commonServiceProxy.getUserAffiliates().subscribe(result => {
            if (result.length > 0) {
                this._affiliateId = result[0].value;
            }
        });
        _commonServiceProxy.getAllOrganizationUnitForLookupTable('', '', 0, 100).subscribe(result => {
            this.ouList = result.items;
        });
    }

    ngOnInit() {
        this._activatedRoute.paramMap.subscribe(params => {
            let filter = params.get('filter');
            this.availableForReviewOnly = filter === 'submitted';
            this.reloadPage();
        });
    }

    getAffiliates(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._eclSharedServiceProxy.getAllAffiliateAssumption(
            this.filterText,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            if (this.availableForReviewOnly) {
                this.primengTableHelper.records = result.items.filter(x => x.hasSubmittedAssumptions === true );
            } else {
                this.primengTableHelper.records = result.items;
            }
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    navigateToViewAssumptions(ouId: number): void {
        this._router.navigate(['/app/main/assumption/affiliates/view', ouId]);
    }

    navigateToApproveAssumptions(ouId: number): void {
        this._router.navigate(['/app/main/assumption/affiliates/approve', ouId]);
    }

    goBack(): void {
        this._location.back();
    }

    selectAffiliateToCopyTo(ouId: number, ouName: string): void {
        this.copyFromOuId = ouId;
        this.copyFromOuName = ouName;
        this.ouLookupTableModal.show();
    }

    copyTo() {
        this.message.confirm(
            this.l('CopyFromAffiliateToAffiliate', this.copyFromOuName, this.ouLookupTableModal.displayName) + '?',
            (isConfirmed) => {
                if (isConfirmed) {
                    if (this.copyFromOuId !== -1) {
                        let c = new CopyAffiliateDto();
                        c.fromAffiliateId = this.copyFromOuId;
                        c.toAffiliateId = this.ouLookupTableModal.id;
                        this._eclSharedServiceProxy.copyAffiliateAssumptions(c).subscribe(() => {
                            this.notify.info(this.l('CopyAffiliateProcessStarted'));
                        });
                    }
                }
            }
        );
    }

}
