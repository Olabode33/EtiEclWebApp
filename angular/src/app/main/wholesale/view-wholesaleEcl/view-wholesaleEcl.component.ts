import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EclStatusEnum, CreateOrEditWholesaleEclDto, WholesaleEclsServiceProxy } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';
import { AppConsts } from '@shared/AppConsts';
import { Location } from '@angular/common';

@Component({
  selector: 'app-view-wholesaleEcl',
  templateUrl: './view-wholesaleEcl.component.html',
  styleUrls: ['./view-wholesaleEcl.component.css'],
  encapsulation: ViewEncapsulation.None,
  animations: [appModuleAnimation()]
})
export class ViewWholesaleEclComponent extends AppComponentBase implements OnInit {

    constructor(
        injector: Injector,
        private _wholesaleEcLsServiceProxy: WholesaleEclsServiceProxy,
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
        //
    }

    editEcl() {
        this.notify.info('Yet to be implemented!!!');
    }

    approveEcl() {
        this.notify.info('Yet to be implemented!!!');
    }

    runEclComputation() {
        this.notify.info('Yet to be implemented!!!');
    }

    goBack() {
        this._location.back();
    }
}
