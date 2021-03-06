import { Component, OnInit, ViewEncapsulation, Injector, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { RetailEclsServiceProxy, RetailEclUploadsServiceProxy, TokenAuthServiceProxy, FrameworkEnum, WholesaleEclDataLoanBooksServiceProxy, RetailEclDataLoanBooksServiceProxy, ObeEclDataLoanBooksServiceProxy } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { Location } from '@angular/common';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/primeng';
import { LazyLoadEvent } from 'primeng/api';

@Component({
    selector: 'app-view-loanbookDetails',
    templateUrl: './view-loanbookDetails.component.html',
    styleUrls: ['./view-loanbookDetails.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ViewLoanbookDetailsComponent extends AppComponentBase implements OnInit {

    _uploadId = '';
    _selectedFramework: FrameworkEnum;
    _loanbookServiceProxy: any;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    customerNoFilter = '';
    accountNoFilter = '';
    contractNoFilter = '';
    customerNameFilter = '';

    constructor(
        injector: Injector,
        private _wholesaleEclLoanbookServiceProxy: WholesaleEclDataLoanBooksServiceProxy,
        private _retailEclLoanbookServiceProxy: RetailEclDataLoanBooksServiceProxy,
        private _obeEclLoanbookServiceProxy: ObeEclDataLoanBooksServiceProxy,
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
                this._loanbookServiceProxy = this._wholesaleEclLoanbookServiceProxy;
                break;
            case FrameworkEnum.Retail:
                this._loanbookServiceProxy = this._retailEclLoanbookServiceProxy;
                break;
            case FrameworkEnum.OBE:
                this._loanbookServiceProxy = this._obeEclLoanbookServiceProxy;
                break;
            default:
                this._loanbookServiceProxy = this._retailEclLoanbookServiceProxy;
                break;
        }
    }

    getLoanBookDetails(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._loanbookServiceProxy.getAll(
            this.filterText,
            this.customerNoFilter,
            this.accountNoFilter,
            this.contractNoFilter,
            this.customerNameFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            console.log(result);
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
