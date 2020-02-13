import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { FrameworkEnum, InvestmentEclOverridesServiceProxy, GeneralStatusEnum } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { ApplyOverrideModalComponent } from '../apply-override-modal/apply-override-modal.component';

@Component({
    selector: 'app-ecl-override',
    templateUrl: './ecl-override.component.html',
    styleUrls: ['./ecl-override.component.css']
})
export class EclOverrideComponent extends AppComponentBase {

    @ViewChild('applyOverrideModal', {static: true}) applyOverrideModal: ApplyOverrideModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    //TODO: Refresh table on modal close
    //TODO: Hide action buttons if ECL is post-override or completed

    show = false;

    _serviceProxy: any;
    _eclId = '';
    _eclFramework: FrameworkEnum;

    advancedFiltersAreShown = false;
    filterText = '';
    statusFilter = -1;
    contractNameFilter = '';

    frameworkEnum = FrameworkEnum;
    generalStatusEnum = GeneralStatusEnum;

    constructor(
        injector: Injector,
        private _investmentOverrideServiceProxy: InvestmentEclOverridesServiceProxy
    ) {
        super(injector);
    }

    load(eclId: string, framework: FrameworkEnum, showOverride = false): void {
        this._eclId = eclId;
        this._eclFramework = framework;
        this.show = showOverride;
        console.log(this._eclFramework);
        this.configureServiceProxy();
        this.configureApplyOverrideModal();
    }

    display(showOverride: boolean): void {
        this.show = showOverride;
    }

    configureServiceProxy(): void {
        switch (this._eclFramework) {
            case FrameworkEnum.Investments:
                this._serviceProxy = this._investmentOverrideServiceProxy;
                break;
            default:
                this._serviceProxy = this._investmentOverrideServiceProxy;
                //throw Error('FrameworkDoesNotExistError');
                break;
        }
    }

    configureApplyOverrideModal(): void {
        this.applyOverrideModal.configure({
            selectedEclId: this._eclId,
            serviceProxy: this._serviceProxy,
        });
    }

    getEclOverrides(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._serviceProxy.getAll(
            this._eclId,
            this.filterText,
            this.statusFilter,
            this.contractNameFilter,
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

    applyOverride(): void {
        this.applyOverrideModal.show();
    }

    reviewOverride(sicrId: string): void {
        this.applyOverrideModal.showInReviewMode(sicrId);
    }

    getStatusLabelClass(uploadStatus: GeneralStatusEnum): string {
        switch (uploadStatus) {
            case GeneralStatusEnum.Submitted:
                return 'warning';
            case GeneralStatusEnum.Approved:
                return 'success';
            case GeneralStatusEnum.Rejected:
                return 'danger';
            default:
                return 'dark';
        }
    }
}
