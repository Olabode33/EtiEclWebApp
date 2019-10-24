import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WholesaleEclDataPaymentSchedulesServiceProxy, WholesaleEclDataPaymentScheduleDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditWholesaleEclDataPaymentScheduleModalComponent } from './create-or-edit-wholesaleEclDataPaymentSchedule-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './wholesaleEclDataPaymentSchedules.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class WholesaleEclDataPaymentSchedulesComponent extends AppComponentBase {

    @ViewChild('createOrEditWholesaleEclDataPaymentScheduleModal', { static: true }) createOrEditWholesaleEclDataPaymentScheduleModal: CreateOrEditWholesaleEclDataPaymentScheduleModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    contractRefNoFilter = '';
    maxStartDateFilter : moment.Moment;
		minStartDateFilter : moment.Moment;
    componentFilter = '';
    maxNoOfSchedulesFilter : number;
		maxNoOfSchedulesFilterEmpty : number;
		minNoOfSchedulesFilter : number;
		minNoOfSchedulesFilterEmpty : number;
    frequencyFilter = '';
    maxAmountFilter : number;
		maxAmountFilterEmpty : number;
		minAmountFilter : number;
		minAmountFilterEmpty : number;
        wholesaleEclUploadUploadCommentFilter = '';




    constructor(
        injector: Injector,
        private _wholesaleEclDataPaymentSchedulesServiceProxy: WholesaleEclDataPaymentSchedulesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getWholesaleEclDataPaymentSchedules(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._wholesaleEclDataPaymentSchedulesServiceProxy.getAll(
            this.filterText,
            this.contractRefNoFilter,
            this.maxStartDateFilter,
            this.minStartDateFilter,
            this.componentFilter,
            this.maxNoOfSchedulesFilter == null ? this.maxNoOfSchedulesFilterEmpty: this.maxNoOfSchedulesFilter,
            this.minNoOfSchedulesFilter == null ? this.minNoOfSchedulesFilterEmpty: this.minNoOfSchedulesFilter,
            this.frequencyFilter,
            this.maxAmountFilter == null ? this.maxAmountFilterEmpty: this.maxAmountFilter,
            this.minAmountFilter == null ? this.minAmountFilterEmpty: this.minAmountFilter,
            this.wholesaleEclUploadUploadCommentFilter,
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

    createWholesaleEclDataPaymentSchedule(): void {
        this.createOrEditWholesaleEclDataPaymentScheduleModal.show();
    }

    deleteWholesaleEclDataPaymentSchedule(wholesaleEclDataPaymentSchedule: WholesaleEclDataPaymentScheduleDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._wholesaleEclDataPaymentSchedulesServiceProxy.delete(wholesaleEclDataPaymentSchedule.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
