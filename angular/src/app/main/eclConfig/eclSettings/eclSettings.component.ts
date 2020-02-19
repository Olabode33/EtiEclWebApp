import { Component, Injector, ViewEncapsulation, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EclSettingsServiceProxy, EclSettingsEditDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';
import { Location } from '@angular/common';

@Component({
    templateUrl: './eclSettings.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class EclSettingsComponent extends AppComponentBase implements OnInit {

    loading = false;

    eclSettings: EclSettingsEditDto = new EclSettingsEditDto();

    constructor(
        injector: Injector,
        private _location: Location,
        private _eclSettingsServiceProxy: EclSettingsServiceProxy) {
        super(injector);
    }

    ngOnInit() {
        this.getAllEclSettings();
    }

    getAllEclSettings(): void {
        this.loading = true;
        this._eclSettingsServiceProxy.getAllSettings()
            .pipe(finalize(() => { this.loading = false; }))
            .subscribe(result => {
                this.loading = false;
                this.eclSettings = result;
                console.log(this.eclSettings);
            });
    }

    saveAllEclSettings(): void {
        this._eclSettingsServiceProxy.updateAllSettings(this.eclSettings).subscribe(() => {
            this.notify.success(this.l('SavedSuccessfully'));
        });
    }

    goBack(): void {
        this._location.back();
    }

}
