import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WholesaleEclResultSummaryKeyInputsServiceProxy, WholesaleEclResultSummaryKeyInputDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditWholesaleEclResultSummaryKeyInputModalComponent } from './create-or-edit-wholesaleEclResultSummaryKeyInput-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './wholesaleEclResultSummaryKeyInputs.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class WholesaleEclResultSummaryKeyInputsComponent extends AppComponentBase {

    @ViewChild('createOrEditWholesaleEclResultSummaryKeyInputModal', { static: true }) createOrEditWholesaleEclResultSummaryKeyInputModal: CreateOrEditWholesaleEclResultSummaryKeyInputModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    pdGroupingFilter = '';
    maxExposureFilter : number;
		maxExposureFilterEmpty : number;
		minExposureFilter : number;
		minExposureFilterEmpty : number;
    maxCollateralFilter : number;
		maxCollateralFilterEmpty : number;
		minCollateralFilter : number;
		minCollateralFilterEmpty : number;
    maxUnsecuredPercentageFilter : number;
		maxUnsecuredPercentageFilterEmpty : number;
		minUnsecuredPercentageFilter : number;
		minUnsecuredPercentageFilterEmpty : number;
    maxPercentageOfBookFilter : number;
		maxPercentageOfBookFilterEmpty : number;
		minPercentageOfBookFilter : number;
		minPercentageOfBookFilterEmpty : number;
    maxMonths6CummulativeBestPDsFilter : number;
		maxMonths6CummulativeBestPDsFilterEmpty : number;
		minMonths6CummulativeBestPDsFilter : number;
		minMonths6CummulativeBestPDsFilterEmpty : number;
    maxMonths12CummulativeBestPDsFilter : number;
		maxMonths12CummulativeBestPDsFilterEmpty : number;
		minMonths12CummulativeBestPDsFilter : number;
		minMonths12CummulativeBestPDsFilterEmpty : number;
    maxMonths24CummulativeBestPDsFilter : number;
		maxMonths24CummulativeBestPDsFilterEmpty : number;
		minMonths24CummulativeBestPDsFilter : number;
		minMonths24CummulativeBestPDsFilterEmpty : number;
        wholesaleEclTenantIdFilter = '';




    constructor(
        injector: Injector,
        private _wholesaleEclResultSummaryKeyInputsServiceProxy: WholesaleEclResultSummaryKeyInputsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getWholesaleEclResultSummaryKeyInputs(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._wholesaleEclResultSummaryKeyInputsServiceProxy.getAll(
            this.filterText,
            this.pdGroupingFilter,
            this.maxExposureFilter == null ? this.maxExposureFilterEmpty: this.maxExposureFilter,
            this.minExposureFilter == null ? this.minExposureFilterEmpty: this.minExposureFilter,
            this.maxCollateralFilter == null ? this.maxCollateralFilterEmpty: this.maxCollateralFilter,
            this.minCollateralFilter == null ? this.minCollateralFilterEmpty: this.minCollateralFilter,
            this.maxUnsecuredPercentageFilter == null ? this.maxUnsecuredPercentageFilterEmpty: this.maxUnsecuredPercentageFilter,
            this.minUnsecuredPercentageFilter == null ? this.minUnsecuredPercentageFilterEmpty: this.minUnsecuredPercentageFilter,
            this.maxPercentageOfBookFilter == null ? this.maxPercentageOfBookFilterEmpty: this.maxPercentageOfBookFilter,
            this.minPercentageOfBookFilter == null ? this.minPercentageOfBookFilterEmpty: this.minPercentageOfBookFilter,
            this.maxMonths6CummulativeBestPDsFilter == null ? this.maxMonths6CummulativeBestPDsFilterEmpty: this.maxMonths6CummulativeBestPDsFilter,
            this.minMonths6CummulativeBestPDsFilter == null ? this.minMonths6CummulativeBestPDsFilterEmpty: this.minMonths6CummulativeBestPDsFilter,
            this.maxMonths12CummulativeBestPDsFilter == null ? this.maxMonths12CummulativeBestPDsFilterEmpty: this.maxMonths12CummulativeBestPDsFilter,
            this.minMonths12CummulativeBestPDsFilter == null ? this.minMonths12CummulativeBestPDsFilterEmpty: this.minMonths12CummulativeBestPDsFilter,
            this.maxMonths24CummulativeBestPDsFilter == null ? this.maxMonths24CummulativeBestPDsFilterEmpty: this.maxMonths24CummulativeBestPDsFilter,
            this.minMonths24CummulativeBestPDsFilter == null ? this.minMonths24CummulativeBestPDsFilterEmpty: this.minMonths24CummulativeBestPDsFilter,
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

    createWholesaleEclResultSummaryKeyInput(): void {
        this.createOrEditWholesaleEclResultSummaryKeyInputModal.show();
    }

    deleteWholesaleEclResultSummaryKeyInput(wholesaleEclResultSummaryKeyInput: WholesaleEclResultSummaryKeyInputDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._wholesaleEclResultSummaryKeyInputsServiceProxy.delete(wholesaleEclResultSummaryKeyInput.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
