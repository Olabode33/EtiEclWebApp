import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WholesaleEclPdAssumptionMacroeconomicInputsServiceProxy, WholesaleEclPdAssumptionMacroeconomicInputDto , PdInputAssumptionMacroEconomicInputGroupEnum, GeneralStatusEnum } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputModalComponent } from './create-or-edit-wholesaleEclPdAssumptionMacroeconomicInput-modal.component';
import { ViewWholesaleEclPdAssumptionMacroeconomicInputModalComponent } from './view-wholesaleEclPdAssumptionMacroeconomicInput-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './wholesaleEclPdAssumptionMacroeconomicInputs.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class WholesaleEclPdAssumptionMacroeconomicInputsComponent extends AppComponentBase {

    @ViewChild('createOrEditWholesaleEclPdAssumptionMacroeconomicInputModal', { static: true }) createOrEditWholesaleEclPdAssumptionMacroeconomicInputModal: CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputModalComponent;
    @ViewChild('viewWholesaleEclPdAssumptionMacroeconomicInputModalComponent', { static: true }) viewWholesaleEclPdAssumptionMacroeconomicInputModal: ViewWholesaleEclPdAssumptionMacroeconomicInputModalComponent;
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
        wholesaleEclTenantIdFilter = '';

    pdInputAssumptionMacroEconomicInputGroupEnum = PdInputAssumptionMacroEconomicInputGroupEnum;
    generalStatusEnum = GeneralStatusEnum;

    _entityTypeFullName = 'TestDemo.WholesaleAssumption.WholesaleEclPdAssumptionMacroeconomicInput';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _wholesaleEclPdAssumptionMacroeconomicInputsServiceProxy: WholesaleEclPdAssumptionMacroeconomicInputsServiceProxy,
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

    getWholesaleEclPdAssumptionMacroeconomicInputs(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._wholesaleEclPdAssumptionMacroeconomicInputsServiceProxy.getAll(
            this.filterText,
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

    createWholesaleEclPdAssumptionMacroeconomicInput(): void {
        this.createOrEditWholesaleEclPdAssumptionMacroeconomicInputModal.show();
    }

    showHistory(wholesaleEclPdAssumptionMacroeconomicInput: WholesaleEclPdAssumptionMacroeconomicInputDto): void {
        this.entityTypeHistoryModal.show({
            entityId: wholesaleEclPdAssumptionMacroeconomicInput.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: ''
        });
    }

    deleteWholesaleEclPdAssumptionMacroeconomicInput(wholesaleEclPdAssumptionMacroeconomicInput: WholesaleEclPdAssumptionMacroeconomicInputDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._wholesaleEclPdAssumptionMacroeconomicInputsServiceProxy.delete(wholesaleEclPdAssumptionMacroeconomicInput.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._wholesaleEclPdAssumptionMacroeconomicInputsServiceProxy.getWholesaleEclPdAssumptionMacroeconomicInputsToExcel(
        this.filterText,
            this.wholesaleEclTenantIdFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
