import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WholesaleEclResultSummariesServiceProxy, WholesaleEclResultSummaryDto , ResultSummaryTypeEnum } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditWholesaleEclResultSummaryModalComponent } from './create-or-edit-wholesaleEclResultSummary-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './wholesaleEclResultSummaries.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class WholesaleEclResultSummariesComponent extends AppComponentBase {

    @ViewChild('createOrEditWholesaleEclResultSummaryModal', { static: true }) createOrEditWholesaleEclResultSummaryModal: CreateOrEditWholesaleEclResultSummaryModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    summaryTypeFilter = -1;
    titleFilter = '';
        wholesaleEclTenantIdFilter = '';

    resultSummaryTypeEnum = ResultSummaryTypeEnum;



    constructor(
        injector: Injector,
        private _wholesaleEclResultSummariesServiceProxy: WholesaleEclResultSummariesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getWholesaleEclResultSummaries(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._wholesaleEclResultSummariesServiceProxy.getAll(
            this.filterText,
            this.summaryTypeFilter,
            this.titleFilter,
            this.wholesaleEclTenantIdFilter,
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

    createWholesaleEclResultSummary(): void {
        this.createOrEditWholesaleEclResultSummaryModal.show();
    }

    deleteWholesaleEclResultSummary(wholesaleEclResultSummary: WholesaleEclResultSummaryDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._wholesaleEclResultSummariesServiceProxy.delete(wholesaleEclResultSummary.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
