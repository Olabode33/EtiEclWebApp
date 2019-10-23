import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WholesaleEclsServiceProxy, WholesaleEclDto , EclStatusEnum } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditWholesaleEclModalComponent } from './create-or-edit-wholesaleEcl-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './wholesaleEcls.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class WholesaleEclsComponent extends AppComponentBase {

    @ViewChild('createOrEditWholesaleEclModal', { static: true }) createOrEditWholesaleEclModal: CreateOrEditWholesaleEclModalComponent;
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxReportingDateFilter : moment.Moment;
		minReportingDateFilter : moment.Moment;
    maxClosedDateFilter : moment.Moment;
		minClosedDateFilter : moment.Moment;
    isApprovedFilter = -1;
    statusFilter = -1;
        userNameFilter = '';

    eclStatusEnum = EclStatusEnum;

    _entityTypeFullName = 'TestDemo.Wholesale.WholesaleEcl';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _wholesaleEclsServiceProxy: WholesaleEclsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.entityHistoryEnabled = this.setIsEntityHistoryEnabled();
    }

    private setIsEntityHistoryEnabled(): boolean {
        let customSettings = (abp as any).custom;
        return customSettings.EntityHistory && customSettings.EntityHistory.isEnabled && _.filter(customSettings.EntityHistory.enabledEntities, entityType => entityType === this._entityTypeFullName).length === 1;
    }

    getWholesaleEcls(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._wholesaleEclsServiceProxy.getAll(
            this.filterText,
            this.maxReportingDateFilter,
            this.minReportingDateFilter,
            this.maxClosedDateFilter,
            this.minClosedDateFilter,
            this.isApprovedFilter,
            this.statusFilter,
            this.userNameFilter,
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

    createWholesaleEcl(): void {
        this.createOrEditWholesaleEclModal.show();
    }

    showHistory(wholesaleEcl: WholesaleEclDto): void {
        this.entityTypeHistoryModal.show({
            entityId: wholesaleEcl.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: ''
        });
    }

    deleteWholesaleEcl(wholesaleEcl: WholesaleEclDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._wholesaleEclsServiceProxy.delete(wholesaleEcl.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
