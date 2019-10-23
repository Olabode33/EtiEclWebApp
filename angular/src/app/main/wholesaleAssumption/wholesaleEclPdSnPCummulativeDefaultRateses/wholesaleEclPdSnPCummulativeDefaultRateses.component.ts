import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WholesaleEclPdSnPCummulativeDefaultRatesesServiceProxy, WholesaleEclPdSnPCummulativeDefaultRatesDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesModalComponent } from './create-or-edit-wholesaleEclPdSnPCummulativeDefaultRates-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './wholesaleEclPdSnPCummulativeDefaultRateses.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class WholesaleEclPdSnPCummulativeDefaultRatesesComponent extends AppComponentBase {

    @ViewChild('createOrEditWholesaleEclPdSnPCummulativeDefaultRatesModal', { static: true }) createOrEditWholesaleEclPdSnPCummulativeDefaultRatesModal: CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesModalComponent;
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    keyFilter = '';
    ratingFilter = '';
    maxYearsFilter : number;
		maxYearsFilterEmpty : number;
		minYearsFilter : number;
		minYearsFilterEmpty : number;
    maxValueFilter : number;
		maxValueFilterEmpty : number;
		minValueFilter : number;
		minValueFilterEmpty : number;
        wholesaleEclTenantIdFilter = '';


    _entityTypeFullName = 'TestDemo.WholesaleAssumption.WholesaleEclPdSnPCummulativeDefaultRates';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _wholesaleEclPdSnPCummulativeDefaultRatesesServiceProxy: WholesaleEclPdSnPCummulativeDefaultRatesesServiceProxy,
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

    getWholesaleEclPdSnPCummulativeDefaultRateses(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._wholesaleEclPdSnPCummulativeDefaultRatesesServiceProxy.getAll(
            this.filterText,
            this.keyFilter,
            this.ratingFilter,
            this.maxYearsFilter == null ? this.maxYearsFilterEmpty: this.maxYearsFilter,
            this.minYearsFilter == null ? this.minYearsFilterEmpty: this.minYearsFilter,
            this.maxValueFilter == null ? this.maxValueFilterEmpty: this.maxValueFilter,
            this.minValueFilter == null ? this.minValueFilterEmpty: this.minValueFilter,
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

    createWholesaleEclPdSnPCummulativeDefaultRates(): void {
        this.createOrEditWholesaleEclPdSnPCummulativeDefaultRatesModal.show();
    }

    showHistory(wholesaleEclPdSnPCummulativeDefaultRates: WholesaleEclPdSnPCummulativeDefaultRatesDto): void {
        this.entityTypeHistoryModal.show({
            entityId: wholesaleEclPdSnPCummulativeDefaultRates.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: ''
        });
    }

    deleteWholesaleEclPdSnPCummulativeDefaultRates(wholesaleEclPdSnPCummulativeDefaultRates: WholesaleEclPdSnPCummulativeDefaultRatesDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._wholesaleEclPdSnPCummulativeDefaultRatesesServiceProxy.delete(wholesaleEclPdSnPCummulativeDefaultRates.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
