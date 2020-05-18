import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AffiliateMacroEconomicVariableOffsetsServiceProxy, AffiliateMacroEconomicVariableOffsetDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditAffiliateMacroEconomicVariableOffsetModalComponent } from './create-or-edit-affiliateMacroEconomicVariableOffset-modal.component';
import { ViewAffiliateMacroEconomicVariableOffsetModalComponent } from './view-affiliateMacroEconomicVariableOffset-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './affiliateMacroEconomicVariableOffsets.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class AffiliateMacroEconomicVariableOffsetsComponent extends AppComponentBase {

    @ViewChild('createOrEditAffiliateMacroEconomicVariableOffsetModal', { static: true }) createOrEditAffiliateMacroEconomicVariableOffsetModal: CreateOrEditAffiliateMacroEconomicVariableOffsetModalComponent;
    @ViewChild('viewAffiliateMacroEconomicVariableOffsetModalComponent', { static: true }) viewAffiliateMacroEconomicVariableOffsetModal: ViewAffiliateMacroEconomicVariableOffsetModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxBackwardOffsetFilter : number;
		maxBackwardOffsetFilterEmpty : number;
		minBackwardOffsetFilter : number;
		minBackwardOffsetFilterEmpty : number;
        organizationUnitDisplayNameFilter = '';
        macroeconomicVariableNameFilter = '';




    constructor(
        injector: Injector,
        private _affiliateMacroEconomicVariableOffsetsServiceProxy: AffiliateMacroEconomicVariableOffsetsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getAffiliateMacroEconomicVariableOffsets(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._affiliateMacroEconomicVariableOffsetsServiceProxy.getAll(
            this.filterText,
            this.maxBackwardOffsetFilter == null ? this.maxBackwardOffsetFilterEmpty: this.maxBackwardOffsetFilter,
            this.minBackwardOffsetFilter == null ? this.minBackwardOffsetFilterEmpty: this.minBackwardOffsetFilter,
            this.organizationUnitDisplayNameFilter,
            this.macroeconomicVariableNameFilter,
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

    createAffiliateMacroEconomicVariableOffset(): void {
        this.createOrEditAffiliateMacroEconomicVariableOffsetModal.show();
    }

    deleteAffiliateMacroEconomicVariableOffset(affiliateMacroEconomicVariableOffset: AffiliateMacroEconomicVariableOffsetDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._affiliateMacroEconomicVariableOffsetsServiceProxy.delete(affiliateMacroEconomicVariableOffset.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._affiliateMacroEconomicVariableOffsetsServiceProxy.getAffiliateMacroEconomicVariableOffsetsToExcel(
        this.filterText,
            this.maxBackwardOffsetFilter == null ? this.maxBackwardOffsetFilterEmpty: this.maxBackwardOffsetFilter,
            this.minBackwardOffsetFilter == null ? this.minBackwardOffsetFilterEmpty: this.minBackwardOffsetFilter,
            this.organizationUnitDisplayNameFilter,
            this.macroeconomicVariableNameFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
