import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { WholesaleEclDataLoanBooksServiceProxy, WholesaleEclDataLoanBookDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditWholesaleEclDataLoanBookModalComponent } from './create-or-edit-wholesaleEclDataLoanBook-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './wholesaleEclDataLoanBooks.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class WholesaleEclDataLoanBooksComponent extends AppComponentBase {

    @ViewChild('createOrEditWholesaleEclDataLoanBookModal', { static: true }) createOrEditWholesaleEclDataLoanBookModal: CreateOrEditWholesaleEclDataLoanBookModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    customerNoFilter = '';
    accountNoFilter = '';
    contractNoFilter = '';
    customerNameFilter = '';
    currencyFilter = '';
    productTypeFilter = '';
    productMappingFilter = '';
    specialisedLendingFilter = '';
    ratingModelFilter = '';
    maxOriginalRatingFilter : number;
		maxOriginalRatingFilterEmpty : number;
		minOriginalRatingFilter : number;
		minOriginalRatingFilterEmpty : number;
    maxCurrentRatingFilter : number;
		maxCurrentRatingFilterEmpty : number;
		minCurrentRatingFilter : number;
		minCurrentRatingFilterEmpty : number;
    maxLifetimePDFilter : number;
		maxLifetimePDFilterEmpty : number;
		minLifetimePDFilter : number;
		minLifetimePDFilterEmpty : number;
    maxMonth12PDFilter : number;
		maxMonth12PDFilterEmpty : number;
		minMonth12PDFilter : number;
		minMonth12PDFilterEmpty : number;
    maxDaysPastDueFilter : number;
		maxDaysPastDueFilterEmpty : number;
		minDaysPastDueFilter : number;
		minDaysPastDueFilterEmpty : number;
    watchlistIndicatorFilter = -1;
    classificationFilter = '';
    maxImpairedDateFilter : moment.Moment;
		minImpairedDateFilter : moment.Moment;
    maxDefaultDateFilter : moment.Moment;
		minDefaultDateFilter : moment.Moment;
    maxCreditLimitFilter : number;
		maxCreditLimitFilterEmpty : number;
		minCreditLimitFilter : number;
		minCreditLimitFilterEmpty : number;
    maxOriginalBalanceLCYFilter : number;
		maxOriginalBalanceLCYFilterEmpty : number;
		minOriginalBalanceLCYFilter : number;
		minOriginalBalanceLCYFilterEmpty : number;
    maxOutstandingBalanceLCYFilter : number;
		maxOutstandingBalanceLCYFilterEmpty : number;
		minOutstandingBalanceLCYFilter : number;
		minOutstandingBalanceLCYFilterEmpty : number;
    maxOutstandingBalanceACYFilter : number;
		maxOutstandingBalanceACYFilterEmpty : number;
		minOutstandingBalanceACYFilter : number;
		minOutstandingBalanceACYFilterEmpty : number;
    maxContractStartDateFilter : moment.Moment;
		minContractStartDateFilter : moment.Moment;
    maxContractEndDateFilter : moment.Moment;
		minContractEndDateFilter : moment.Moment;
    restructureIndicatorFilter = -1;
    restructureRiskFilter = '';
    restructureTypeFilter = '';
    maxRestructureStartDateFilter : moment.Moment;
		minRestructureStartDateFilter : moment.Moment;
    maxRestructureEndDateFilter : moment.Moment;
		minRestructureEndDateFilter : moment.Moment;
    principalPaymentTermsOriginationFilter = '';
    maxPPTOPeriodFilter : number;
		maxPPTOPeriodFilterEmpty : number;
		minPPTOPeriodFilter : number;
		minPPTOPeriodFilterEmpty : number;
    interestPaymentTermsOriginationFilter = '';
    maxIPTOPeriodFilter : number;
		maxIPTOPeriodFilterEmpty : number;
		minIPTOPeriodFilter : number;
		minIPTOPeriodFilterEmpty : number;
    principalPaymentStructureFilter = '';
    interestPaymentStructureFilter = '';
    interestRateTypeFilter = '';
    baseRateFilter = '';
    originationContractualInterestRateFilter = '';
    maxIntroductoryPeriodFilter : number;
		maxIntroductoryPeriodFilterEmpty : number;
		minIntroductoryPeriodFilter : number;
		minIntroductoryPeriodFilterEmpty : number;
    maxPostIPContractualInterestRateFilter : number;
		maxPostIPContractualInterestRateFilterEmpty : number;
		minPostIPContractualInterestRateFilter : number;
		minPostIPContractualInterestRateFilterEmpty : number;
    maxCurrentContractualInterestRateFilter : number;
		maxCurrentContractualInterestRateFilterEmpty : number;
		minCurrentContractualInterestRateFilter : number;
		minCurrentContractualInterestRateFilterEmpty : number;
    maxEIRFilter : number;
		maxEIRFilterEmpty : number;
		minEIRFilter : number;
		minEIRFilterEmpty : number;
    maxDebentureOMVFilter : number;
		maxDebentureOMVFilterEmpty : number;
		minDebentureOMVFilter : number;
		minDebentureOMVFilterEmpty : number;
    maxDebentureFSVFilter : number;
		maxDebentureFSVFilterEmpty : number;
		minDebentureFSVFilter : number;
		minDebentureFSVFilterEmpty : number;
    maxCashOMVFilter : number;
		maxCashOMVFilterEmpty : number;
		minCashOMVFilter : number;
		minCashOMVFilterEmpty : number;
    maxCashFSVFilter : number;
		maxCashFSVFilterEmpty : number;
		minCashFSVFilter : number;
		minCashFSVFilterEmpty : number;
    maxInventoryOMVFilter : number;
		maxInventoryOMVFilterEmpty : number;
		minInventoryOMVFilter : number;
		minInventoryOMVFilterEmpty : number;
    maxInventoryFSVFilter : number;
		maxInventoryFSVFilterEmpty : number;
		minInventoryFSVFilter : number;
		minInventoryFSVFilterEmpty : number;
    maxPlantEquipmentOMVFilter : number;
		maxPlantEquipmentOMVFilterEmpty : number;
		minPlantEquipmentOMVFilter : number;
		minPlantEquipmentOMVFilterEmpty : number;
    maxPlantEquipmentFSVFilter : number;
		maxPlantEquipmentFSVFilterEmpty : number;
		minPlantEquipmentFSVFilter : number;
		minPlantEquipmentFSVFilterEmpty : number;
    maxResidentialPropertyOMVFilter : number;
		maxResidentialPropertyOMVFilterEmpty : number;
		minResidentialPropertyOMVFilter : number;
		minResidentialPropertyOMVFilterEmpty : number;
    maxResidentialPropertyFSVFilter : number;
		maxResidentialPropertyFSVFilterEmpty : number;
		minResidentialPropertyFSVFilter : number;
		minResidentialPropertyFSVFilterEmpty : number;
    maxCommercialPropertyOMVFilter : number;
		maxCommercialPropertyOMVFilterEmpty : number;
		minCommercialPropertyOMVFilter : number;
		minCommercialPropertyOMVFilterEmpty : number;
    maxCommercialPropertyFilter : number;
		maxCommercialPropertyFilterEmpty : number;
		minCommercialPropertyFilter : number;
		minCommercialPropertyFilterEmpty : number;
    maxReceivablesOMVFilter : number;
		maxReceivablesOMVFilterEmpty : number;
		minReceivablesOMVFilter : number;
		minReceivablesOMVFilterEmpty : number;
    maxReceivablesFSVFilter : number;
		maxReceivablesFSVFilterEmpty : number;
		minReceivablesFSVFilter : number;
		minReceivablesFSVFilterEmpty : number;
    maxSharesOMVFilter : number;
		maxSharesOMVFilterEmpty : number;
		minSharesOMVFilter : number;
		minSharesOMVFilterEmpty : number;
    maxSharesFSVFilter : number;
		maxSharesFSVFilterEmpty : number;
		minSharesFSVFilter : number;
		minSharesFSVFilterEmpty : number;
    maxVehicleOMVFilter : number;
		maxVehicleOMVFilterEmpty : number;
		minVehicleOMVFilter : number;
		minVehicleOMVFilterEmpty : number;
    maxVehicleFSVFilter : number;
		maxVehicleFSVFilterEmpty : number;
		minVehicleFSVFilter : number;
		minVehicleFSVFilterEmpty : number;
    maxCureRateFilter : number;
		maxCureRateFilterEmpty : number;
		minCureRateFilter : number;
		minCureRateFilterEmpty : number;
    guaranteeIndicatorFilter = -1;
    guarantorPDFilter = '';
    guarantorLGDFilter = '';
    maxGuaranteeValueFilter : number;
		maxGuaranteeValueFilterEmpty : number;
		minGuaranteeValueFilter : number;
		minGuaranteeValueFilterEmpty : number;
    maxGuaranteeLevelFilter : number;
		maxGuaranteeLevelFilterEmpty : number;
		minGuaranteeLevelFilter : number;
		minGuaranteeLevelFilterEmpty : number;
    contractIdFilter = '';
        wholesaleEclUploadUploadCommentFilter = '';




    constructor(
        injector: Injector,
        private _wholesaleEclDataLoanBooksServiceProxy: WholesaleEclDataLoanBooksServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getWholesaleEclDataLoanBooks(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._wholesaleEclDataLoanBooksServiceProxy.getAll(
            this.filterText,
            this.customerNoFilter,
            this.accountNoFilter,
            this.contractNoFilter,
            this.customerNameFilter,
            this.currencyFilter,
            this.productTypeFilter,
            this.productMappingFilter,
            this.specialisedLendingFilter,
            this.ratingModelFilter,
            this.maxOriginalRatingFilter == null ? this.maxOriginalRatingFilterEmpty: this.maxOriginalRatingFilter,
            this.minOriginalRatingFilter == null ? this.minOriginalRatingFilterEmpty: this.minOriginalRatingFilter,
            this.maxCurrentRatingFilter == null ? this.maxCurrentRatingFilterEmpty: this.maxCurrentRatingFilter,
            this.minCurrentRatingFilter == null ? this.minCurrentRatingFilterEmpty: this.minCurrentRatingFilter,
            this.maxLifetimePDFilter == null ? this.maxLifetimePDFilterEmpty: this.maxLifetimePDFilter,
            this.minLifetimePDFilter == null ? this.minLifetimePDFilterEmpty: this.minLifetimePDFilter,
            this.maxMonth12PDFilter == null ? this.maxMonth12PDFilterEmpty: this.maxMonth12PDFilter,
            this.minMonth12PDFilter == null ? this.minMonth12PDFilterEmpty: this.minMonth12PDFilter,
            this.maxDaysPastDueFilter == null ? this.maxDaysPastDueFilterEmpty: this.maxDaysPastDueFilter,
            this.minDaysPastDueFilter == null ? this.minDaysPastDueFilterEmpty: this.minDaysPastDueFilter,
            this.watchlistIndicatorFilter,
            this.classificationFilter,
            this.maxImpairedDateFilter,
            this.minImpairedDateFilter,
            this.maxDefaultDateFilter,
            this.minDefaultDateFilter,
            this.maxCreditLimitFilter == null ? this.maxCreditLimitFilterEmpty: this.maxCreditLimitFilter,
            this.minCreditLimitFilter == null ? this.minCreditLimitFilterEmpty: this.minCreditLimitFilter,
            this.maxOriginalBalanceLCYFilter == null ? this.maxOriginalBalanceLCYFilterEmpty: this.maxOriginalBalanceLCYFilter,
            this.minOriginalBalanceLCYFilter == null ? this.minOriginalBalanceLCYFilterEmpty: this.minOriginalBalanceLCYFilter,
            this.maxOutstandingBalanceLCYFilter == null ? this.maxOutstandingBalanceLCYFilterEmpty: this.maxOutstandingBalanceLCYFilter,
            this.minOutstandingBalanceLCYFilter == null ? this.minOutstandingBalanceLCYFilterEmpty: this.minOutstandingBalanceLCYFilter,
            this.maxOutstandingBalanceACYFilter == null ? this.maxOutstandingBalanceACYFilterEmpty: this.maxOutstandingBalanceACYFilter,
            this.minOutstandingBalanceACYFilter == null ? this.minOutstandingBalanceACYFilterEmpty: this.minOutstandingBalanceACYFilter,
            this.maxContractStartDateFilter,
            this.minContractStartDateFilter,
            this.maxContractEndDateFilter,
            this.minContractEndDateFilter,
            this.restructureIndicatorFilter,
            this.restructureRiskFilter,
            this.restructureTypeFilter,
            this.maxRestructureStartDateFilter,
            this.minRestructureStartDateFilter,
            this.maxRestructureEndDateFilter,
            this.minRestructureEndDateFilter,
            this.principalPaymentTermsOriginationFilter,
            this.maxPPTOPeriodFilter == null ? this.maxPPTOPeriodFilterEmpty: this.maxPPTOPeriodFilter,
            this.minPPTOPeriodFilter == null ? this.minPPTOPeriodFilterEmpty: this.minPPTOPeriodFilter,
            this.interestPaymentTermsOriginationFilter,
            this.maxIPTOPeriodFilter == null ? this.maxIPTOPeriodFilterEmpty: this.maxIPTOPeriodFilter,
            this.minIPTOPeriodFilter == null ? this.minIPTOPeriodFilterEmpty: this.minIPTOPeriodFilter,
            this.principalPaymentStructureFilter,
            this.interestPaymentStructureFilter,
            this.interestRateTypeFilter,
            this.baseRateFilter,
            this.originationContractualInterestRateFilter,
            this.maxIntroductoryPeriodFilter == null ? this.maxIntroductoryPeriodFilterEmpty: this.maxIntroductoryPeriodFilter,
            this.minIntroductoryPeriodFilter == null ? this.minIntroductoryPeriodFilterEmpty: this.minIntroductoryPeriodFilter,
            this.maxPostIPContractualInterestRateFilter == null ? this.maxPostIPContractualInterestRateFilterEmpty: this.maxPostIPContractualInterestRateFilter,
            this.minPostIPContractualInterestRateFilter == null ? this.minPostIPContractualInterestRateFilterEmpty: this.minPostIPContractualInterestRateFilter,
            this.maxCurrentContractualInterestRateFilter == null ? this.maxCurrentContractualInterestRateFilterEmpty: this.maxCurrentContractualInterestRateFilter,
            this.minCurrentContractualInterestRateFilter == null ? this.minCurrentContractualInterestRateFilterEmpty: this.minCurrentContractualInterestRateFilter,
            this.maxEIRFilter == null ? this.maxEIRFilterEmpty: this.maxEIRFilter,
            this.minEIRFilter == null ? this.minEIRFilterEmpty: this.minEIRFilter,
            this.maxDebentureOMVFilter == null ? this.maxDebentureOMVFilterEmpty: this.maxDebentureOMVFilter,
            this.minDebentureOMVFilter == null ? this.minDebentureOMVFilterEmpty: this.minDebentureOMVFilter,
            this.maxDebentureFSVFilter == null ? this.maxDebentureFSVFilterEmpty: this.maxDebentureFSVFilter,
            this.minDebentureFSVFilter == null ? this.minDebentureFSVFilterEmpty: this.minDebentureFSVFilter,
            this.maxCashOMVFilter == null ? this.maxCashOMVFilterEmpty: this.maxCashOMVFilter,
            this.minCashOMVFilter == null ? this.minCashOMVFilterEmpty: this.minCashOMVFilter,
            this.maxCashFSVFilter == null ? this.maxCashFSVFilterEmpty: this.maxCashFSVFilter,
            this.minCashFSVFilter == null ? this.minCashFSVFilterEmpty: this.minCashFSVFilter,
            this.maxInventoryOMVFilter == null ? this.maxInventoryOMVFilterEmpty: this.maxInventoryOMVFilter,
            this.minInventoryOMVFilter == null ? this.minInventoryOMVFilterEmpty: this.minInventoryOMVFilter,
            this.maxInventoryFSVFilter == null ? this.maxInventoryFSVFilterEmpty: this.maxInventoryFSVFilter,
            this.minInventoryFSVFilter == null ? this.minInventoryFSVFilterEmpty: this.minInventoryFSVFilter,
            this.maxPlantEquipmentOMVFilter == null ? this.maxPlantEquipmentOMVFilterEmpty: this.maxPlantEquipmentOMVFilter,
            this.minPlantEquipmentOMVFilter == null ? this.minPlantEquipmentOMVFilterEmpty: this.minPlantEquipmentOMVFilter,
            this.maxPlantEquipmentFSVFilter == null ? this.maxPlantEquipmentFSVFilterEmpty: this.maxPlantEquipmentFSVFilter,
            this.minPlantEquipmentFSVFilter == null ? this.minPlantEquipmentFSVFilterEmpty: this.minPlantEquipmentFSVFilter,
            this.maxResidentialPropertyOMVFilter == null ? this.maxResidentialPropertyOMVFilterEmpty: this.maxResidentialPropertyOMVFilter,
            this.minResidentialPropertyOMVFilter == null ? this.minResidentialPropertyOMVFilterEmpty: this.minResidentialPropertyOMVFilter,
            this.maxResidentialPropertyFSVFilter == null ? this.maxResidentialPropertyFSVFilterEmpty: this.maxResidentialPropertyFSVFilter,
            this.minResidentialPropertyFSVFilter == null ? this.minResidentialPropertyFSVFilterEmpty: this.minResidentialPropertyFSVFilter,
            this.maxCommercialPropertyOMVFilter == null ? this.maxCommercialPropertyOMVFilterEmpty: this.maxCommercialPropertyOMVFilter,
            this.minCommercialPropertyOMVFilter == null ? this.minCommercialPropertyOMVFilterEmpty: this.minCommercialPropertyOMVFilter,
            this.maxCommercialPropertyFilter == null ? this.maxCommercialPropertyFilterEmpty: this.maxCommercialPropertyFilter,
            this.minCommercialPropertyFilter == null ? this.minCommercialPropertyFilterEmpty: this.minCommercialPropertyFilter,
            this.maxReceivablesOMVFilter == null ? this.maxReceivablesOMVFilterEmpty: this.maxReceivablesOMVFilter,
            this.minReceivablesOMVFilter == null ? this.minReceivablesOMVFilterEmpty: this.minReceivablesOMVFilter,
            this.maxReceivablesFSVFilter == null ? this.maxReceivablesFSVFilterEmpty: this.maxReceivablesFSVFilter,
            this.minReceivablesFSVFilter == null ? this.minReceivablesFSVFilterEmpty: this.minReceivablesFSVFilter,
            this.maxSharesOMVFilter == null ? this.maxSharesOMVFilterEmpty: this.maxSharesOMVFilter,
            this.minSharesOMVFilter == null ? this.minSharesOMVFilterEmpty: this.minSharesOMVFilter,
            this.maxSharesFSVFilter == null ? this.maxSharesFSVFilterEmpty: this.maxSharesFSVFilter,
            this.minSharesFSVFilter == null ? this.minSharesFSVFilterEmpty: this.minSharesFSVFilter,
            this.maxVehicleOMVFilter == null ? this.maxVehicleOMVFilterEmpty: this.maxVehicleOMVFilter,
            this.minVehicleOMVFilter == null ? this.minVehicleOMVFilterEmpty: this.minVehicleOMVFilter,
            this.maxVehicleFSVFilter == null ? this.maxVehicleFSVFilterEmpty: this.maxVehicleFSVFilter,
            this.minVehicleFSVFilter == null ? this.minVehicleFSVFilterEmpty: this.minVehicleFSVFilter,
            this.maxCureRateFilter == null ? this.maxCureRateFilterEmpty: this.maxCureRateFilter,
            this.minCureRateFilter == null ? this.minCureRateFilterEmpty: this.minCureRateFilter,
            this.guaranteeIndicatorFilter,
            this.guarantorPDFilter,
            this.guarantorLGDFilter,
            this.maxGuaranteeValueFilter == null ? this.maxGuaranteeValueFilterEmpty: this.maxGuaranteeValueFilter,
            this.minGuaranteeValueFilter == null ? this.minGuaranteeValueFilterEmpty: this.minGuaranteeValueFilter,
            this.maxGuaranteeLevelFilter == null ? this.maxGuaranteeLevelFilterEmpty: this.maxGuaranteeLevelFilter,
            this.minGuaranteeLevelFilter == null ? this.minGuaranteeLevelFilterEmpty: this.minGuaranteeLevelFilter,
            this.contractIdFilter,
            this.wholesaleEclUploadUploadCommentFilter,
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

    createWholesaleEclDataLoanBook(): void {
        this.createOrEditWholesaleEclDataLoanBookModal.show();
    }

    deleteWholesaleEclDataLoanBook(wholesaleEclDataLoanBook: WholesaleEclDataLoanBookDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._wholesaleEclDataLoanBooksServiceProxy.delete(wholesaleEclDataLoanBook.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
