import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { NotifyService } from '@abp/notify/notify.service';
import { TokenAuthServiceProxy, EclSharedServiceProxy } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';
import { Location } from '@angular/common';

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

    filterText = '';
    availableForReviewOnly = false;

    constructor(
        injector: Injector,
        private _eclSharedServiceProxy: EclSharedServiceProxy,
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

}
