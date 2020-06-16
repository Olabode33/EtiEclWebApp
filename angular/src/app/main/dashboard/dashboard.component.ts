import { AfterViewInit, Component, Injector, ViewEncapsulation } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TenantDashboardServiceProxy, SalesSummaryDatePeriod } from '@shared/service-proxies/service-proxies';
import { curveBasis } from 'd3-shape';

import * as _ from 'lodash';
import { SafeUrl, DomSanitizer } from '@angular/platform-browser';


@Component({
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.less'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class DashboardComponent extends AppComponentBase implements AfterViewInit {

    loading = false;
    powerBiUrl: SafeUrl;

    constructor(
        injector: Injector,
        private _dashboardService: TenantDashboardServiceProxy,
        private _sanitizer: DomSanitizer,
    ) {
        super(injector);
    }

    ngAfterViewInit(): void {
        this.getDashboardUrl();
    }

    getDashboardUrl(): void {
        this.loading = true;
        // this._dashboardService.getPowerBiDashboardUrl()
        //     .pipe(finalize(() => { this.loading = false; }))
        //     .subscribe(result => {
        //         this.getSafeResourceUrl(result);
        //     });
    }

    getSafeResourceUrl(urlString: any)  {
        this.powerBiUrl = this._sanitizer.bypassSecurityTrustResourceUrl(urlString);
    }
}
