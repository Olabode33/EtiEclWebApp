import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AssumptionApprovalsServiceProxy, AssumptionApprovalDto, FrameworkEnum, AssumptionTypeEnum, GeneralStatusEnum, CommonLookupServiceProxy } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { ViewAssumptionApprovalModalComponent } from './view-assumptionApproval-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';
import { Location } from '@angular/common';
import { ApprovalModalComponent } from '../../eclShared/approve-ecl-modal/approve-ecl-modal.component';

@Component({
    templateUrl: './assumptionApprovals.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class AssumptionApprovalsComponent extends AppComponentBase implements OnInit {

    @ViewChild('viewAssumptionApprovalModalComponent', { static: true }) viewAssumptionApprovalModal: ViewAssumptionApprovalModalComponent;
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    @ViewChild('approvalModal', {static: true}) approvalModel: ApprovalModalComponent;

    advancedFiltersAreShown = false;
    filterText = '';
    organizationUnitIdFilterEmpty: number;
    frameworkFilter = -1;
    assumptionTypeFilter = -1;
    statusFilter = 1;
    assumptionGroupFilter = '';
    userNameFilter = '';

    frameworkEnum = FrameworkEnum;
    assumptionTypeEnum = AssumptionTypeEnum;
    generalStatusEnum = GeneralStatusEnum;

    _entityTypeFullName = 'TestDemo.EclShared.AssumptionApproval';
    entityHistoryEnabled = false;

    _affiliateId = -1;
    selectedAffiliate = '';

    selectedRecords: AssumptionApprovalDto[] = new Array();

    constructor(
        injector: Injector,
        private _assumptionApprovalsServiceProxy: AssumptionApprovalsServiceProxy,
        private _commonLookupServiceProxy: CommonLookupServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _location: Location
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this._activatedRoute.paramMap.subscribe(params => {
            this._affiliateId = +params.get('ouId');
            this.getOrganizationUnitName();
        });
        this.entityHistoryEnabled = this.setIsEntityHistoryEnabled();
    }

    private setIsEntityHistoryEnabled(): boolean {
        let customSettings = (abp as any).custom;
        return customSettings.EntityHistory && customSettings.EntityHistory.isEnabled && _.filter(customSettings.EntityHistory.enabledEntities, entityType => entityType === this._entityTypeFullName).length === 1;
    }

    getOrganizationUnitName(): void {
        this._commonLookupServiceProxy.getAffiliateNameFromId(this._affiliateId).subscribe(result => {
            this.selectedAffiliate = result;
        });
    }

    getAssumptionApprovals(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._assumptionApprovalsServiceProxy.getAll(
            this.filterText,
            this._affiliateId == null ? this.organizationUnitIdFilterEmpty : this._affiliateId,
            this.frameworkFilter,
            this.assumptionTypeFilter,
            this.statusFilter,
            this.assumptionGroupFilter,
            this.userNameFilter,
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

    showHistory(assumptionApproval: AssumptionApprovalDto): void {
        this.entityTypeHistoryModal.show({
            entityId: assumptionApproval.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: ''
        });
    }

    goBack() {
        this._location.back();
    }

    changeAffiliate(): void {
        this.notify.error('Yet to be implemented!');
    }

    deleteAssumptionApproval(assumptionApproval: AssumptionApprovalDto): void {
        this.message.confirm(
            '',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._assumptionApprovalsServiceProxy.delete(assumptionApproval.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    approveAssumption(assumption: AssumptionApprovalDto): void {
        this.approvalModel.configure({
            title: this.l('ApproveAssumption'),
            serviceProxy: this._assumptionApprovalsServiceProxy,
            dataSource: assumption
        });
        this.approvalModel.show();
    }
}
