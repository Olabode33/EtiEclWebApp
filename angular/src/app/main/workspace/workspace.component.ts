import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { WholesaleEclsServiceProxy, WholesaleEclDto, EclStatusEnum, EclSharedServiceProxy, FrameworkEnum, RetailEclsServiceProxy, InvestmentEclsServiceProxy } from '@shared/service-proxies/service-proxies';
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

@Component({
    selector: 'app-workspace',
    templateUrl: './workspace.component.html',
    styleUrls: ['./workspace.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class WorkspaceComponent extends AppComponentBase implements OnInit {

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    eclStatusEnum = EclStatusEnum;
    frameworkEnum = FrameworkEnum;
    pageHeader = '';
    filterText = '';
    ouFilter = -1;
    statusFilter = -1;
    frameworkFilter = -1;

    constructor(
        injector: Injector,
        private _eclSharedServiceProxy: EclSharedServiceProxy,
        private _retailEclServiceProxy: RetailEclsServiceProxy,
        private _investmentEclServiceProxy: InvestmentEclsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _router: Router
    ) {
        super(injector);
    }

    ngOnInit() {
        this.pageHeader = this.l('WorkspaceWelcome', this.appSession.user.surname + ' ' + this.appSession.user.name);
    }

    getWorkspaceEcls(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._eclSharedServiceProxy.getAllEclForWorkspace(
            this.filterText,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            console.log(result.items);
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    navigateToCreateWholesaleEcl(): void {
        this._router.navigate(['../wholesale/ecl/create'], { relativeTo: this._activatedRoute});
    }

    navigateToViewWholesaleEcl(eclId: string): void {
        console.log(eclId);
        this._router.navigate(['../wholesale/ecl/view', eclId], { relativeTo: this._activatedRoute});
    }

    navigateToCreateRetailEcl(): void {
        this._router.navigate(['../retail/ecl/create'], { relativeTo: this._activatedRoute});
    }

    navigateToViewRetailEcl(eclId: string): void {
        console.log(eclId);
        this._router.navigate(['../retail/ecl/view', eclId], { relativeTo: this._activatedRoute});
    }

    navigateToCreateObeEcl(): void {
        this._router.navigate(['../wholesale/ecl/create'], { relativeTo: this._activatedRoute});
    }

    navigateToDashboard(): void {
        this._router.navigate(['../dashboard'], { relativeTo: this._activatedRoute});
    }

    viewEcl(framework: FrameworkEnum, eclId: string): void {
        this._router.navigate(['/app/main/ecl/view/', framework.toString(), eclId]);
        // switch (framework) {
        //     case FrameworkEnum.OBE:
        //         this.navigateToDashboard();
        //         break;
        //     case FrameworkEnum.Retail:
        //         this.navigateToViewRetailEcl(eclId);
        //         break;
        //     case FrameworkEnum.Wholesale:
        //         this.navigateToViewWholesaleEcl(eclId);
        //         break;
        // }
    }

    createRetailEcl(): void {
        this._retailEclServiceProxy.createEclAndAssumption().subscribe(result => {
            this.notify.success('EclSuccessfullyCreated');
            this.viewEcl(FrameworkEnum.Retail, result);
        });
    }

    createInvestmentEcl(): void {
        this._investmentEclServiceProxy.createEclAndAssumption().subscribe(result => {
            this.notify.success('EclSuccessfullyCreated');
            this.viewEcl(FrameworkEnum.Investments, result);
        });
    }

}
