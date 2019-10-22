import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PdInputSnPCummulativeDefaultRatesServiceProxy, PdInputSnPCummulativeDefaultRateDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditPdInputSnPCummulativeDefaultRateModalComponent } from './create-or-edit-pdInputSnPCummulativeDefaultRate-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './pdInputSnPCummulativeDefaultRates.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class PdInputSnPCummulativeDefaultRatesComponent extends AppComponentBase {

    @ViewChild('createOrEditPdInputSnPCummulativeDefaultRateModal', { static: true }) createOrEditPdInputSnPCummulativeDefaultRateModal: CreateOrEditPdInputSnPCummulativeDefaultRateModalComponent;
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    ratingFilter = '';


    _entityTypeFullName = 'TestDemo.EclShared.PdInputSnPCummulativeDefaultRate';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _pdInputSnPCummulativeDefaultRatesServiceProxy: PdInputSnPCummulativeDefaultRatesServiceProxy,
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

    getPdInputSnPCummulativeDefaultRates(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._pdInputSnPCummulativeDefaultRatesServiceProxy.getAll(
            this.filterText,
            this.ratingFilter,
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

    createPdInputSnPCummulativeDefaultRate(): void {
        this.createOrEditPdInputSnPCummulativeDefaultRateModal.show();
    }

    showHistory(pdInputSnPCummulativeDefaultRate: PdInputSnPCummulativeDefaultRateDto): void {
        this.entityTypeHistoryModal.show({
            entityId: pdInputSnPCummulativeDefaultRate.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: ''
        });
    }

    deletePdInputSnPCummulativeDefaultRate(pdInputSnPCummulativeDefaultRate: PdInputSnPCummulativeDefaultRateDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._pdInputSnPCummulativeDefaultRatesServiceProxy.delete(pdInputSnPCummulativeDefaultRate.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
