import { CalibrationStatusEnum, CreateOrEditCalibrationRunDto, CommonLookupServiceProxy, NameValueDtoOfInt64, CalibrationEadCcfSummaryServiceProxy, CalibrationLgdHairCutServiceProxy, CalibrationLgdRecoveryRateServiceProxy, CalibrationPdCrDrServiceProxy, FrameworkEnum } from '../../../../shared/service-proxies/service-proxies';
import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CalibrationEadBehaviouralTermsServiceProxy, GeneralStatusEnum, CalibrationRunDto } from '@shared/service-proxies/service-proxies';
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
import { OuLookupTableModalComponent } from '@app/main/eclShared/ou-lookup-modal/ou-lookup-table-modal.component';


@Component({
    templateUrl: './calibratePdCrDr.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class CalibrationPdCrDrComponent extends AppComponentBase implements OnInit {


    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('ouLookupTableModal', { static: true }) ouLookupTableModal: OuLookupTableModalComponent;


    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    statusFilter = -1;
    affiliateFilter = -1;

    generalStatusEnum = CalibrationStatusEnum;
    frameworkEnum = FrameworkEnum;

    selectFramework=FrameworkEnum.All;

    _entityTypeFullName = 'TestDemo.Calibration.CalibrationEadBehaviouralTerm';
    entityHistoryEnabled = false;

    _affiliateId = -1;

    ouList: NameValueDtoOfInt64[] = new Array();

    constructor(
        injector: Injector,
        private _calibrationServiceProxy: CalibrationPdCrDrServiceProxy,
        private _commonServiceProxy: CommonLookupServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _router: Router
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

    ngOnInit(): void {
        this.entityHistoryEnabled = this.setIsEntityHistoryEnabled();
    }

    private setIsEntityHistoryEnabled(): boolean {
        let customSettings = (abp as any).custom;
        return this.isGrantedAny('Pages.Administration.AuditLogs') && customSettings.EntityHistory && customSettings.EntityHistory.isEnabled && _.filter(customSettings.EntityHistory.enabledEntities, entityType => entityType === this._entityTypeFullName).length === 1;
    }

    getAll(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._calibrationServiceProxy.getAll(
            this.filterText,
            this.statusFilter,
            this.affiliateFilter,
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

    create(framework: FrameworkEnum): void {
        this.selectFramework=framework;
        if (this._affiliateId === -1) {
            this.ouLookupTableModal.show();
        } else {
            let c = new CreateOrEditCalibrationRunDto();
            c.modelType=framework;
            this._calibrationServiceProxy.createOrEdit(c).subscribe(result => {
                this.reloadPage();
                this.notify.success(this.l('CalibrationSuccessfullyCreated'));
            });
        }
        //this._router.navigate(['/app/main/calibration/calibrationEadBehaviouralTerms/createOrEdit']);
    }

    view(id: string): void {
        this._router.navigate(['/app/main/calibration/pdcrdr/view/', id]);
    }

    createForAffiliate() {
        let c = new CreateOrEditCalibrationRunDto();
        c.affiliateId = this.ouLookupTableModal.id;
        c.modelType=this.selectFramework;
        this._calibrationServiceProxy.createOrEdit(c).subscribe(result => {
            this.reloadPage();
            this.notify.success(this.l('CalibrationSuccessfullyCreated'));
        });
    }


    showHistory(calibration: CalibrationRunDto): void {
        this.entityTypeHistoryModal.show({
            entityId: calibration.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: ''
        });
    }

    delete(calibration: CalibrationRunDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._calibrationServiceProxy.delete(calibration.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    getStatusLabelClass(uploadStatus: CalibrationStatusEnum): string {
        switch (uploadStatus) {
            case CalibrationStatusEnum.Draft:
                return 'primary';
            case CalibrationStatusEnum.Submitted:
            case CalibrationStatusEnum.Processing:
            case CalibrationStatusEnum.AwaitngAdditionApproval:
                return 'warning';
            case CalibrationStatusEnum.Completed:
            case CalibrationStatusEnum.Approved:
                return 'success';
            case CalibrationStatusEnum.Rejected:
            case CalibrationStatusEnum.Failed:
                return 'danger';
            default:
                return 'dark';
        }
    }
}
