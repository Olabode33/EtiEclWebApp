import { Component, OnInit, ViewEncapsulation, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { RetailEclsServiceProxy, RetailEclUploadsServiceProxy, TokenAuthServiceProxy, FrameworkEnum } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module/dist/src/notify/notify.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { Location } from '@angular/common';

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

    constructor(
        injector: Injector,
        private _retailEcLsServiceProxy: RetailEclsServiceProxy,
        private _retailEclUploadServiceProxy: RetailEclUploadsServiceProxy,
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
        });
    }

    goBack() {
        this._location.back();
    }

}
