import { Component, OnInit, ViewEncapsulation, ViewChild, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { WholesaleEclDataPaymentSchedulesServiceProxy, RetailEclDataPaymentSchedulesServiceProxy, ObeEclDataPaymentSchedulesServiceProxy, FrameworkEnum, TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { Location } from '@angular/common';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/primeng';
import { LazyLoadEvent } from 'primeng/api';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
    selector: 'app-view-paymentSchedule',
    templateUrl: './view-paymentSchedule.component.html',
    styleUrls: ['./view-paymentSchedule.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ViewPaymentScheduleComponent extends AppComponentBase implements OnInit {

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    _uploadId = '';
    _selectedFramework: FrameworkEnum;
    _paymentScheduleServiceProxy: any;

    advancedFiltersAreShown = false;
    filterText = '';
    contractRefNoFilter = '';
    componentFilter = '';

    constructor(
        injector: Injector,
        private _wholesaleEclPaymentScheduleServiceProxy: WholesaleEclDataPaymentSchedulesServiceProxy,
        private _retailEclPaymentScheduleServiceProxy: RetailEclDataPaymentSchedulesServiceProxy,
        private _obeEclPaymentScheduleServiceProxy: ObeEclDataPaymentSchedulesServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _router: Router,
        private _location: Location
    ) {
        super(injector);
    }

    ngOnInit() {
        this._activatedRoute.paramMap.subscribe(params => {
            this._uploadId = params.get('uploadId');
            this._selectedFramework = +params.get('framework');
            this.updateServiceProxy();
        });
    }

    updateServiceProxy(): void {
        switch (this._selectedFramework) {
            case FrameworkEnum.Wholesale:
                this._paymentScheduleServiceProxy = this._wholesaleEclPaymentScheduleServiceProxy;
                break;
            case FrameworkEnum.Retail:
                this._paymentScheduleServiceProxy = this._retailEclPaymentScheduleServiceProxy;
                break;
            case FrameworkEnum.OBE:
                this._paymentScheduleServiceProxy = this._obeEclPaymentScheduleServiceProxy;
                break;
            default:
                this._paymentScheduleServiceProxy = this._retailEclPaymentScheduleServiceProxy;
                break;
        }
    }

    getEclDataPaymentSchedules(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._paymentScheduleServiceProxy.getAll(
            this.filterText,
            this.contractRefNoFilter,
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

    goBack() {
        this._location.back();
    }
}
