import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WholesaleEclResultSummaryTopExposuresServiceProxy, WholesaleEclResultSummaryTopExposureDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditWholesaleEclResultSummaryTopExposureModalComponent } from './create-or-edit-wholesaleEclResultSummaryTopExposure-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './wholesaleEclResultSummaryTopExposures.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class WholesaleEclResultSummaryTopExposuresComponent extends AppComponentBase {

    @ViewChild('createOrEditWholesaleEclResultSummaryTopExposureModal', { static: true }) createOrEditWholesaleEclResultSummaryTopExposureModal: CreateOrEditWholesaleEclResultSummaryTopExposureModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxPreOverrideExposureFilter : number;
		maxPreOverrideExposureFilterEmpty : number;
		minPreOverrideExposureFilter : number;
		minPreOverrideExposureFilterEmpty : number;
    maxPreOverrideImpairmentFilter : number;
		maxPreOverrideImpairmentFilterEmpty : number;
		minPreOverrideImpairmentFilter : number;
		minPreOverrideImpairmentFilterEmpty : number;
    maxPreOverrideCoverageRatioFilter : number;
		maxPreOverrideCoverageRatioFilterEmpty : number;
		minPreOverrideCoverageRatioFilter : number;
		minPreOverrideCoverageRatioFilterEmpty : number;
    maxPostOverrideExposureFilter : number;
		maxPostOverrideExposureFilterEmpty : number;
		minPostOverrideExposureFilter : number;
		minPostOverrideExposureFilterEmpty : number;
    maxPostOverrideImpairmentFilter : number;
		maxPostOverrideImpairmentFilterEmpty : number;
		minPostOverrideImpairmentFilter : number;
		minPostOverrideImpairmentFilterEmpty : number;
    maxPostOverrideCoverageRatioFilter : number;
		maxPostOverrideCoverageRatioFilterEmpty : number;
		minPostOverrideCoverageRatioFilter : number;
		minPostOverrideCoverageRatioFilterEmpty : number;
    contractIdFilter = '';
        wholesaleEclTenantIdFilter = '';
        wholesaleEclDataLoanBookCustomerNameFilter = '';




    constructor(
        injector: Injector,
        private _wholesaleEclResultSummaryTopExposuresServiceProxy: WholesaleEclResultSummaryTopExposuresServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getWholesaleEclResultSummaryTopExposures(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._wholesaleEclResultSummaryTopExposuresServiceProxy.getAll(
            this.filterText,
            this.maxPreOverrideExposureFilter == null ? this.maxPreOverrideExposureFilterEmpty: this.maxPreOverrideExposureFilter,
            this.minPreOverrideExposureFilter == null ? this.minPreOverrideExposureFilterEmpty: this.minPreOverrideExposureFilter,
            this.maxPreOverrideImpairmentFilter == null ? this.maxPreOverrideImpairmentFilterEmpty: this.maxPreOverrideImpairmentFilter,
            this.minPreOverrideImpairmentFilter == null ? this.minPreOverrideImpairmentFilterEmpty: this.minPreOverrideImpairmentFilter,
            this.maxPreOverrideCoverageRatioFilter == null ? this.maxPreOverrideCoverageRatioFilterEmpty: this.maxPreOverrideCoverageRatioFilter,
            this.minPreOverrideCoverageRatioFilter == null ? this.minPreOverrideCoverageRatioFilterEmpty: this.minPreOverrideCoverageRatioFilter,
            this.maxPostOverrideExposureFilter == null ? this.maxPostOverrideExposureFilterEmpty: this.maxPostOverrideExposureFilter,
            this.minPostOverrideExposureFilter == null ? this.minPostOverrideExposureFilterEmpty: this.minPostOverrideExposureFilter,
            this.maxPostOverrideImpairmentFilter == null ? this.maxPostOverrideImpairmentFilterEmpty: this.maxPostOverrideImpairmentFilter,
            this.minPostOverrideImpairmentFilter == null ? this.minPostOverrideImpairmentFilterEmpty: this.minPostOverrideImpairmentFilter,
            this.maxPostOverrideCoverageRatioFilter == null ? this.maxPostOverrideCoverageRatioFilterEmpty: this.maxPostOverrideCoverageRatioFilter,
            this.minPostOverrideCoverageRatioFilter == null ? this.minPostOverrideCoverageRatioFilterEmpty: this.minPostOverrideCoverageRatioFilter,
            this.contractIdFilter,
            this.wholesaleEclTenantIdFilter,
            this.wholesaleEclDataLoanBookCustomerNameFilter,
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

    createWholesaleEclResultSummaryTopExposure(): void {
        this.createOrEditWholesaleEclResultSummaryTopExposureModal.show();
    }

    deleteWholesaleEclResultSummaryTopExposure(wholesaleEclResultSummaryTopExposure: WholesaleEclResultSummaryTopExposureDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._wholesaleEclResultSummaryTopExposuresServiceProxy.delete(wholesaleEclResultSummaryTopExposure.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
