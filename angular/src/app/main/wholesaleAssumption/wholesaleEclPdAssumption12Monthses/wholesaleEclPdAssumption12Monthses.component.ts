import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WholesaleEclPdAssumption12MonthsesServiceProxy, WholesaleEclPdAssumption12MonthsDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditWholesaleEclPdAssumption12MonthsModalComponent } from './create-or-edit-wholesaleEclPdAssumption12Months-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './wholesaleEclPdAssumption12Monthses.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class WholesaleEclPdAssumption12MonthsesComponent extends AppComponentBase {

    @ViewChild('createOrEditWholesaleEclPdAssumption12MonthsModal', { static: true }) createOrEditWholesaleEclPdAssumption12MonthsModal: CreateOrEditWholesaleEclPdAssumption12MonthsModalComponent;
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxCreditFilter : number;
		maxCreditFilterEmpty : number;
		minCreditFilter : number;
		minCreditFilterEmpty : number;
    maxPDFilter : number;
		maxPDFilterEmpty : number;
		minPDFilter : number;
		minPDFilterEmpty : number;
    snPMappingEtiCreditPolicyFilter = '';
    snPMappingBestFitFilter = '';
        wholesaleEclTenantIdFilter = '';


    _entityTypeFullName = 'TestDemo.WholesaleAssumption.WholesaleEclPdAssumption12Months';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _wholesaleEclPdAssumption12MonthsesServiceProxy: WholesaleEclPdAssumption12MonthsesServiceProxy,
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

    getWholesaleEclPdAssumption12Monthses(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._wholesaleEclPdAssumption12MonthsesServiceProxy.getAll(
            this.filterText,
            this.maxCreditFilter == null ? this.maxCreditFilterEmpty: this.maxCreditFilter,
            this.minCreditFilter == null ? this.minCreditFilterEmpty: this.minCreditFilter,
            this.maxPDFilter == null ? this.maxPDFilterEmpty: this.maxPDFilter,
            this.minPDFilter == null ? this.minPDFilterEmpty: this.minPDFilter,
            this.snPMappingEtiCreditPolicyFilter,
            this.snPMappingBestFitFilter,
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

    createWholesaleEclPdAssumption12Months(): void {
        this.createOrEditWholesaleEclPdAssumption12MonthsModal.show();
    }

    showHistory(wholesaleEclPdAssumption12Months: WholesaleEclPdAssumption12MonthsDto): void {
        this.entityTypeHistoryModal.show({
            entityId: wholesaleEclPdAssumption12Months.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: ''
        });
    }

    deleteWholesaleEclPdAssumption12Months(wholesaleEclPdAssumption12Months: WholesaleEclPdAssumption12MonthsDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._wholesaleEclPdAssumption12MonthsesServiceProxy.delete(wholesaleEclPdAssumption12Months.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
