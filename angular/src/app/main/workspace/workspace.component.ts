import { GetAllEclForWorkspaceSummaryDto, GetWorkspaceImpairmentSummaryDto } from './../../../shared/service-proxies/service-proxies';
import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, ViewEncapsulation, Injector, AfterContentInit, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { EclStatusEnum, EclSharedServiceProxy, GetWorkspaceSummaryDataOutput, FrameworkEnum } from '@shared/service-proxies/service-proxies';

@Component({
    selector: 'app-workspace',
    templateUrl: './workspace.component.html',
    styleUrls: ['./workspace.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class WorkspaceComponent extends AppComponentBase implements OnInit, AfterViewInit {

    pageHeader = '';
    eclStatusEnum = EclStatusEnum;
    workspaceSummaryData: GetWorkspaceSummaryDataOutput = new GetWorkspaceSummaryDataOutput();
    recentEclSummary: GetAllEclForWorkspaceSummaryDto[] = new Array();
    impairmentSummary: GetWorkspaceImpairmentSummaryDto = new GetWorkspaceImpairmentSummaryDto();

    frameworkEnum = FrameworkEnum;

    constructor(
        injector: Injector,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _eclSharedAppService: EclSharedServiceProxy
    ) {
        super(injector);
     }

    ngOnInit() {
        this.pageHeader = this.l('WorkspaceWelcome', this.appSession.user.surname + ' ' + this.appSession.user.name);
    }

    ngAfterViewInit() {
        this.getWorkspaceData();
        this.getRecentEclSummaryData();
        this.getImpairmentSummaryData();
    }

    getWorkspaceData(): void {
        this._eclSharedAppService.getWorkspaceSummaryData().subscribe(result => {
            this.workspaceSummaryData = result;
        });
    }

    getRecentEclSummaryData(): void {
        this._eclSharedAppService.getAllEclSummaryForWorkspace('', -1, -1, -1, '', 0, 10).subscribe(result => {
            this.recentEclSummary = result;
        });
    }

    getImpairmentSummaryData(): void {
        this._eclSharedAppService.getWorkspaceImpairmentSummary().subscribe(result => {
            this.impairmentSummary = result;
        });
    }

    navigateToAffiliateAssumptionPage(filter?: string): void {
        if (filter === null || filter === undefined) {
            this._router.navigate(['/app/main/assumption/affiliates']);
        } else {
            this._router.navigate(['/app/main/assumption/affiliates', filter]);
        }
    }

    navigateToEclWorkspace(status?: EclStatusEnum): void {
        if (status === null || status === undefined) {
            this._router.navigate(['/app/main/ecl']);
        } else {
            this._router.navigate(['/app/main/ecl', status]);
        }
    }

    navigateToViewEcl(framework: FrameworkEnum, eclId: string): void {
        this._router.navigate(['/app/main/ecl/view/', framework.toString(), eclId]);
    }
}
