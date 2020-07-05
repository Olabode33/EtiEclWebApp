import { FacilityStageTrackerOutputDto, CommonLookupServiceProxy, CreateOrEditEclOverrideNewDto } from './../../../../../shared/service-proxies/service-proxies';
import { Component, OnInit, Injector, ViewChild, Input } from '@angular/core';
import { FrameworkEnum, InvestmentEclOverridesServiceProxy, GeneralStatusEnum, WholesaleEclOverridesServiceProxy, RetailEclOverridesServiceProxy, ObeEclOverridesServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { ApplyOverrideModalComponent } from '../apply-override-modal/apply-override-modal.component';
import * as XLSX from 'xlsx';
import { CSVConverter } from './csv-converter';
@Component({
    selector: 'app-ecl-override',
    templateUrl: './ecl-override.component.html',
    styleUrls: ['./ecl-override.component.css']
})
export class EclOverrideComponent extends AppComponentBase {

    @ViewChild('applyOverrideModal', { static: true }) applyOverrideModal: ApplyOverrideModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    //TODO: Refresh table on modal close
    //TODO: Hide action buttons if ECL is post-override or completed

    show = false;
    canApplyOverride = true;

    _serviceProxy: any;
    _eclId = '';
    _eclFramework: FrameworkEnum;

    advancedFiltersAreShown = false;
    filterText = '';
    statusFilter = -1;
    contractNameFilter = '';

    frameworkEnum = FrameworkEnum;
    generalStatusEnum = GeneralStatusEnum;

    constructor(
        injector: Injector,
        private _investmentOverrideServiceProxy: InvestmentEclOverridesServiceProxy,
        private _wholesaleOverrideServiceProxy: WholesaleEclOverridesServiceProxy,
        private _retailOverrideServiceProxy: RetailEclOverridesServiceProxy,
        private _obeOverrideServiceProxy: ObeEclOverridesServiceProxy
    ) {
        super(injector);
    }

    load(eclId: string, framework: FrameworkEnum, showOverride = false, canApplyOverride = true): void {
        this._eclId = eclId;
        this._eclFramework = framework;
        this.show = showOverride;
        this.canApplyOverride = canApplyOverride;
        console.log(this._eclFramework);
        this.configureServiceProxy();
        this.configureApplyOverrideModal();
    }

    display(showOverride: boolean): void {
        this.show = showOverride;
    }

    disableApplyOverride(canApplyOverride: boolean): void {
        this.canApplyOverride = canApplyOverride;
    }

    configureServiceProxy(): void {
        switch (this._eclFramework) {
            case FrameworkEnum.Investments:
                this._serviceProxy = this._investmentOverrideServiceProxy;
                break;
            case FrameworkEnum.Retail:
                this._serviceProxy = this._retailOverrideServiceProxy;
                break;
            case FrameworkEnum.Wholesale:
                this._serviceProxy = this._wholesaleOverrideServiceProxy;
                break;
            case FrameworkEnum.OBE:
                this._serviceProxy = this._obeOverrideServiceProxy;
                break;
            default:
                throw Error('FrameworkDoesNotExistError');
                break;
        }
    }

    configureApplyOverrideModal(): void {
        this.applyOverrideModal.configure({
            selectedEclId: this._eclId,
            serviceProxy: this._serviceProxy,
            selectedFrameWork: this._eclFramework
        });
    }

    getEclOverrides(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._serviceProxy.getAll(
            this._eclId,
            this.filterText,
            this.statusFilter,
            this.contractNameFilter,
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

    applyOverride(): void {
        this.applyOverrideModal.show();
    }

    reviewOverride(sicrId: string): void {
        this.applyOverrideModal.showInReviewMode(sicrId);
    }

    viewOverride(sicrId: string): void {
        this.applyOverrideModal.showInViewOnlyMode(sicrId);
    }

    getStatusLabelClass(uploadStatus: GeneralStatusEnum): string {
        switch (uploadStatus) {
            case GeneralStatusEnum.Submitted:
            case GeneralStatusEnum.AwaitngAdditionApproval:
                return 'warning';
            case GeneralStatusEnum.Approved:
                return 'success';
            case GeneralStatusEnum.Rejected:
                return 'danger';
            default:
                return 'dark';
        }
    }

    download() {
        var data = [{}];
        if(this._eclFramework == FrameworkEnum.Investments) {
             data = [{ 'Company Name*': "", "Currency*": "", "Current Assets": "", "Average Fixed Assets": "", "Total Assets": "", "Average Inventory": "", "Cash": "", "Cash Equivalent": "", "Average Account Receivables": "", "Inventories": "", "Current Liabilities": "", "Contingent Liabilities": "", "Short Term Debts": "", "Total Debts": "", "Total Outside Liabilities": "", "Total Equity": "", "Net Worth": "", "Long Term Surplus Fund": "", "Retained Earnings": "", "Net Working Capital": "", "Average Working Capital": "", "Net Cash Accruals": "", "Other Income": "", "Sales": "", "Net Sales": "", "EBIT": "", "Net Credit Sales": "", "Revenue": "", "Annual Interest & Principal Repayments": "", "Other Finance Charges On Payment": "", "Interest": "", "Operating Profit": "", "Net Profit": "", "Cash Flow From Operating Activities": ""}];
        }
        else {
             data = [{ 'ContractId*': "", "Reason": "", "Stage": "", "Ttr Years": "", "FSV Cash": "", "FSV Commercial Property": "", "FSV Debenture": "", "FSV Inventory": "", "FSV Plant And Equipment": "", "FSV Receivables": "", "FSV Residential Property": "", "FSV Shares": "", "FSV Vehicle": "", "Overlays Percentage": "", "Comment": "", "Override Type": ""}];
        }
        var csvData = CSVConverter.ConvertToCSV(data);
        var a = document.createElement("a");
        a.setAttribute('style', 'display:none;');
        document.body.appendChild(a);
        var blob = new Blob([csvData], { type: 'text/csv' });
        var url = window.URL.createObjectURL(blob);
        a.href = url;
        a.download = `${FrameworkEnum[FrameworkEnum.Investments]}SampleFile.csv`;
        a.click();
      }

      uploadExcel(data: { files: File }): void {
        this.primengTableHelper.isLoading = true;
        const file = data.files[0];
        const reader: FileReader = new FileReader();
        reader.readAsBinaryString(file);
        reader.onload = (e: any) => {
          const binarystr: string = e.target.result;
          const wb: XLSX.WorkBook = XLSX.read(binarystr, { type: 'binary' });

          const wsname: string = wb.SheetNames[0];
          const ws: XLSX.WorkSheet = wb.Sheets[wsname];

          const data = XLSX.utils.sheet_to_json(ws);
          var finalList = new Array<CreateOrEditEclOverrideNewDto>();
          var result = data as CreateOrEditEclOverrideNewDto[];
        result.forEach(r => {
            var obj = new CreateOrEditEclOverrideNewDto();
            obj.contractId = r["ContractId*"] as string;
            obj.fsV_Cash = r["FSV Cash"] as number;
            obj.fsV_CommercialProperty = r["FSV Commercial Property"] as number;
            obj.fsV_Debenture = r["FSV Debenture"] as number;
            obj.fsV_Inventory = r["FSV Inventory"] as number;
            obj.fsV_PlantAndEquipment = r["FSV Plant And Equipment"] as number;
            obj.fsV_Receivables = r["FSV Receivables"] as number;
            obj.fsV_ResidentialProperty = r["FSV Residential Property"] as number;
            obj.fsV_Shares = r["FSV Shares"] as number;
            obj.fsV_Vehicle = r["FSV Vehicle"] as number;
            obj.overlaysPercentage = r["Overlays Percentage"] as number;
            obj.overrideComment = r["Comment"] == undefined ? '' : r["Comment"] as string;
            obj.stage = r["Stage"] as number;
            obj.ttrYears = r["Ttr Years"] as number;
            obj.overrideType = r["Override Type"] == undefined ? '' : r["Override Type"] as string;
            finalList.push(obj);
        });

        var invalids = finalList.filter(r => r.contractId == '' || r.contractId == null);
        if(invalids.length > 0) {
            this.primengTableHelper.isLoading = false;
            this.message.error('One or more required fields are empty');
        }
        else {
                this._serviceProxy.uploadBulkOveride(finalList, this._eclId).subscribe(result => {
                    this.primengTableHelper.isLoading = false;
                    this.getEclOverrides();
                    this.notify.success(this.l('SubmittedSuccessfully'));
                });
    }

    }
}

    onUploadExcelError(): void {
        this.notify.error(this.l('ImportUsersUploadFailed'));
    }
}
