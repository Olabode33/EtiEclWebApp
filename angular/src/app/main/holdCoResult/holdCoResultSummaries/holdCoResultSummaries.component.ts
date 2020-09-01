import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute , Router} from '@angular/router';
import { HoldCoResultSummariesServiceProxy, HoldCoResultSummaryDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditHoldCoResultSummaryModalComponent } from './create-or-edit-holdCoResultSummary-modal.component';

import { ViewHoldCoResultSummaryModalComponent } from './view-holdCoResultSummary-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './holdCoResultSummaries.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class HoldCoResultSummariesComponent extends AppComponentBase {


    @ViewChild('createOrEditHoldCoResultSummaryModal', { static: true }) createOrEditHoldCoResultSummaryModal: CreateOrEditHoldCoResultSummaryModalComponent;
    @ViewChild('viewHoldCoResultSummaryModalComponent', { static: true }) viewHoldCoResultSummaryModal: ViewHoldCoResultSummaryModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';




    constructor(
        injector: Injector,
        private _holdCoResultSummariesServiceProxy: HoldCoResultSummariesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getHoldCoResultSummaries(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._holdCoResultSummariesServiceProxy.getAll(
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

    createHoldCoResultSummary(): void {
        this.createOrEditHoldCoResultSummaryModal.show();
    }


    deleteHoldCoResultSummary(holdCoResultSummary: HoldCoResultSummaryDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._holdCoResultSummariesServiceProxy.delete(holdCoResultSummary.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {

    }
}
