import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { WholesaleEclsServiceProxy, WholesaleEclDto, EclStatusEnum, EclSharedServiceProxy, FrameworkEnum, RetailEclsServiceProxy, InvestmentEclsServiceProxy, EntityDtoOfGuid, ObeEclsServiceProxy, NameValueDtoOfInt64, CommonLookupServiceProxy } from '@shared/service-proxies/service-proxies';
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
import { Location } from '@angular/common';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'app-workspace',
    templateUrl: './ecl-list.component.html',
    styleUrls: ['./ecl-list.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class EclListComponent extends AppComponentBase implements OnInit {

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    loadingEclAssumption = false;

    eclStatusEnum = EclStatusEnum;
    frameworkEnum = FrameworkEnum;
    pageHeader = '';
    filterText = '';
    ouFilter = -1;
    statusFilter = -1;
    frameworkFilter = -1;
    _affiliateId = -1;

    ouList: NameValueDtoOfInt64[] = new Array();

    constructor(
        injector: Injector,
        private _eclSharedServiceProxy: EclSharedServiceProxy,
        private _retailEclServiceProxy: RetailEclsServiceProxy,
        private _investmentEclServiceProxy: InvestmentEclsServiceProxy,
        private _wholesaleEclServiceProxy: WholesaleEclsServiceProxy,
        private _obeEclServiceProxy: ObeEclsServiceProxy,
        private _commonServiceProxy: CommonLookupServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _router: Router,
        private _location: Location
    ) {
        super(injector);
        _commonServiceProxy.getUserAffiliates().subscribe(result => {
            if (result.length > 0) {
                this._affiliateId = result[0].value;
            }
        });
        _commonServiceProxy.getAllOrganizationUnitForLookupTable('', '', 0, 100).subscribe(result => {
            this.ouList = result.items;
        });
    }

    ngOnInit() {
        this.pageHeader = this.l('WorkspaceWelcome', this.appSession.user.surname + ' ' + this.appSession.user.name);
        if (this._activatedRoute.snapshot.params['filter']) {
            this._activatedRoute.paramMap.subscribe(params => {
                this.statusFilter = +params.get('filter');
            });
        }
    }

    getWorkspaceEcls(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._eclSharedServiceProxy.getAllEclForWorkspace(
            this.filterText,
            this.ouFilter,
            this.frameworkFilter,
            this.statusFilter,
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

    navigateToDashboard(): void {
        this._router.navigate(['../dashboard'], { relativeTo: this._activatedRoute});
    }

    viewEcl(framework: FrameworkEnum, eclId: string): void {
        this._router.navigate(['/app/main/ecl/view/', framework.toString(), eclId]);
    }

    createRetailEcl(): void {
        this.loadingEclAssumption = true;
        this._retailEclServiceProxy.createEclAndAssumption()
            .pipe(finalize(() => this.loadingEclAssumption = false))
            .subscribe(result => {
                this.notify.success('EclSuccessfullyCreated');
                this.viewEcl(FrameworkEnum.Retail, result);
            });
    }

    createWholesaleEcl(): void {
        this.loadingEclAssumption = true;
        this._wholesaleEclServiceProxy.createEclAndAssumption()
        .pipe(finalize(() => this.loadingEclAssumption = false))
        .subscribe(result => {
            this.notify.success('EclSuccessfullyCreated');
            this.viewEcl(FrameworkEnum.Wholesale, result);
        });
    }

    createObeEcl(): void {
        this.loadingEclAssumption = true;
        this._obeEclServiceProxy.createEclAndAssumption()
        .pipe(finalize(() => this.loadingEclAssumption = false))
        .subscribe(result => {
            this.notify.success('EclSuccessfullyCreated');
            this.viewEcl(FrameworkEnum.OBE, result);
        });
    }

    createInvestmentEcl(): void {
        this.loadingEclAssumption = true;
        this._investmentEclServiceProxy.createEclAndAssumption()
        .pipe(finalize(() => this.loadingEclAssumption = false))
        .subscribe(result => {
            this.notify.success('EclSuccessfullyCreated');
            this.viewEcl(FrameworkEnum.Investments, result);
        });
    }

    goBack(): void {
        this._location.back();
    }

    reOpenEcl(framework: FrameworkEnum, eclId: string): void {
        switch (framework) {
            case FrameworkEnum.Investments:
                this.reopenInvestmentEcl(eclId);
                break;
            case FrameworkEnum.Wholesale:
                this.reopenWholesalesEcl(eclId);
                break;
            case FrameworkEnum.Retail:
                this.reopenRetailEcl(eclId);
                break;
            case FrameworkEnum.OBE:
                this.reopenObeEcl(eclId);
                break;

            default:
                break;
        }
    }

    reopenWholesalesEcl(eclId: string) {
        this.message.confirm(
            this.l('ReopenEclInfo'),
            (isConfirmed) => {
                if (isConfirmed) {
                    let dto = new EntityDtoOfGuid();
                    dto.id = eclId;
                    this._wholesaleEclServiceProxy.reopenEcl(dto)
                        .subscribe(() => {
                            //this.reloadPage();
                            this.notify.success(this.l('EclReopenStartedNotification'));
                        });
                }
            }
        );
    }

    reopenRetailEcl(eclId: string) {
        this.message.confirm(
            this.l('ReopenEclInfo'),
            (isConfirmed) => {
                if (isConfirmed) {
                    let dto = new EntityDtoOfGuid();
                    dto.id = eclId;
                    this._retailEclServiceProxy.reopenEcl(dto)
                        .subscribe(() => {
                            //this.reloadPage();
                            this.notify.success(this.l('EclReopenStartedNotification'));
                        });
                }
            }
        );
    }

    reopenObeEcl(eclId: string) {
        this.message.confirm(
            this.l('ReopenEclInfo'),
            (isConfirmed) => {
                if (isConfirmed) {
                    let dto = new EntityDtoOfGuid();
                    dto.id = eclId;
                    this._obeEclServiceProxy.reopenEcl(dto)
                        .subscribe(() => {
                            //this.reloadPage();
                            this.notify.success(this.l('EclReopenStartedNotification'));
                        });
                }
            }
        );
    }

    reopenInvestmentEcl(eclId: string) {
        this.message.confirm(
            this.l('ReopenEclInfo'),
            (isConfirmed) => {
                if (isConfirmed) {
                    let dto = new EntityDtoOfGuid();
                    dto.id = eclId;
                    this._investmentEclServiceProxy.reopenEcl(dto)
                        .subscribe(() => {
                            //this.reloadPage();
                            this.notify.success(this.l('EclReopenStartedNotification'));
                        });
                }
            }
        );
    }

    generateReport(framework: FrameworkEnum, eclId: string): void {
        switch (framework) {
            case FrameworkEnum.Investments:
                this.generateInvestmentReport(eclId);
                break;
            case FrameworkEnum.Wholesale:
                this.generateWholesaleReport(eclId);
                break;
            case FrameworkEnum.Retail:
                this.generateRetailReport(eclId);
                break;
            case FrameworkEnum.OBE:
                this.generateObeReport(eclId);
                break;
            default:
                break;
        }
    }

    generateWholesaleReport(eclId: string): void {
        let dto = new EntityDtoOfGuid();
        dto.id = eclId;
        this._wholesaleEclServiceProxy.generateReport(dto).subscribe(() => {
            this.message.success(this.l('EclReportGenerationStartedNotification'));
        });
    }

    generateRetailReport(eclId: string): void {
        let dto = new EntityDtoOfGuid();
        dto.id = eclId;
        this._retailEclServiceProxy.generateReport(dto).subscribe(() => {
            this.message.success(this.l('EclReportGenerationStartedNotification'));
        });
    }

    generateObeReport(eclId: string): void {
        let dto = new EntityDtoOfGuid();
        dto.id = eclId;
        this._obeEclServiceProxy.generateReport(dto).subscribe(() => {
            this.message.success(this.l('EclReportGenerationStartedNotification'));
        });
    }

    generateInvestmentReport(eclId: string): void {
        let dto = new EntityDtoOfGuid();
        dto.id = eclId;
        this._investmentEclServiceProxy.generateReport(dto).subscribe(() => {
            this.message.success(this.l('EclReportGenerationStartedNotification'));
        });
    }

}
