import { filter, finalize } from 'rxjs/operators';
import { PdInputAssumptionGroupEnum, GetAllPdAssumptionsDto, PdInputAssumptionDto, PdInputAssumptionMacroeconomicInputDto, PdInputAssumptionMacroeconomicProjectionDto, PdInputAssumptionNonInternalModelDto, PdInputAssumptionNplIndexDto, PdInputSnPCummulativeDefaultRateDto, DataTypeEnum, NameValueDto, CommonLookupServiceProxy, PdInputAssumptionsServiceProxy, FrameworkEnum, AssumptionTypeEnum, PdInputSnPCummulativeDefaultRatesServiceProxy, PdInputAssumptionNonInternalModelsServiceProxy, PdInputAssumptionNplIndexesServiceProxy, PdInputAssumptionStatisticalsServiceProxy, PdInputAssumptionMacroeconomicProjectionsServiceProxy, InvestmentPdInputMacroEconomicAssumptionDto, InvestmentEclPdFitchDefaultRateDto, GetAllInvSecPdAssumptionsDto, InvSecMacroEconomicAssumptionDto, InvSecFitchCummulativeDefaultRateDto, InvSecMacroEconomicAssumptionsServiceProxy, InvSecFitchCummulativeDefaultRatesServiceProxy } from './../../../../../shared/service-proxies/service-proxies';
import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { EditAssumptionModalComponent } from '../edit-assumption-modal/edit-assumption-modal.component';
import { FileUpload } from 'primeng/primeng';
import { AppConsts } from '@shared/AppConsts';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-pdInputAssumptions',
  templateUrl: './pdInputAssumptions.component.html',
  styleUrls: ['./pdInputAssumptions.component.css']
})
export class PdInputAssumptionsComponent extends AppComponentBase {

    @ViewChild('editAssumptionModal', {static: true}) editAssumptionModal: EditAssumptionModalComponent;
    @ViewChild('snpExcelFileUpload', { static: true }) snpExcelFileUpload: FileUpload;
    @ViewChild('nimExcelFileUpload', { static: true }) nimExcelFileUpload: FileUpload;
    @ViewChild('nplExcelFileUpload', { static: true }) nplExcelFileUpload: FileUpload;

    snpUploadUrl = '';
    nimUploadUrl = '';
    nplUploadUrl = '';

    displayForm = false;
    loading = false;
    viewOnly = false;

    frameworkEnum = FrameworkEnum;

    pdAssumptions: GetAllPdAssumptionsDto = new GetAllPdAssumptionsDto();

    pdInputAssumptions: PdInputAssumptionDto[] = new Array<PdInputAssumptionDto>();
    pdInputAssumptionGroupEnum = PdInputAssumptionGroupEnum;
    //Pd Input Sub groups:
    creditPds: PdInputAssumptionDto[] = new Array();
    etiPolicy: PdInputAssumptionDto[] = new Array();
    bestFit: PdInputAssumptionDto[] = new Array();

    //S&P Mapping
    pdSnpCumulativeDefaultRate: PdInputSnPCummulativeDefaultRateDto[] = new Array<PdInputSnPCummulativeDefaultRateDto>();
    snpRatings: String[] = new Array();
    snpYears: Number[] = new Array();
    selectedRating = '';
    selectedYear = -1;
    selectedSnpCummulativeDefaultRate: PdInputSnPCummulativeDefaultRateDto[] = new Array();

    //Non-internal Model
    pdNonInternalModelAssumptions: PdInputAssumptionNonInternalModelDto[] = new Array<PdInputAssumptionNonInternalModelDto>();
    pdGroups: String[] = new Array();
    pdMonths: Number[] = new Array();
    selectedPdGroup = '';
    selectedNonInternalMonth = -1;
    selectedNonInternalModelAssumptions: PdInputAssumptionNonInternalModelDto[] = new Array();

    //Historical index & NPL
    pdNplIndexAssumptions: PdInputAssumptionNplIndexDto[] = new Array<PdInputAssumptionNplIndexDto>();

    //Macroeconomics
    pdMacroeconomicInputAssumptions: PdInputAssumptionMacroeconomicInputDto[] = new Array<PdInputAssumptionMacroeconomicInputDto>();
    pdMacroeconomicProjectionsAssumptions: PdInputAssumptionMacroeconomicProjectionDto[] = new Array<PdInputAssumptionMacroeconomicProjectionDto>();
    pdMacroeconomicVariables: NameValueDto[] = new Array(); //Change to MacroVariable entity
    pdModes: String[] = new Array();
    selectedMode = '';
    selectedMacroeconomicVariable = '';
    selectedMacroeconomicInputs: PdInputAssumptionMacroeconomicInputDto[] = new Array();
    selectedMacroeconomicProjections: PdInputAssumptionMacroeconomicProjectionDto[] = new Array();

    //Investments
    pdInvestmentAssumption: GetAllInvSecPdAssumptionsDto = new GetAllInvSecPdAssumptionsDto();
    invsecPdAssumptions: PdInputAssumptionDto[] = new Array();
    invsecMacroEcoScenario: PdInputAssumptionDto[] = new Array();
    invsecMacroEcoAssumption: InvSecMacroEconomicAssumptionDto[] = new Array();
    invsecFitchRatingDefaultRate: InvSecFitchCummulativeDefaultRateDto[] = new Array();
    invsecFitchRatings: string[] = new Array();
    invsecModes: string[] = new Array();
    selectedFitchRating = '';
    selectedInvMode = '';
    selectedFitchRatingDefaultRate: InvSecFitchCummulativeDefaultRateDto[] = new Array();
    selectedInvPdAssumption: PdInputAssumptionDto[] = new Array();

    dataTypeEnum = DataTypeEnum;

    affiliateName = '';
    affiliateFramework: FrameworkEnum;
    affiliateId = -1;

    accordionList = [
        {key: 'Pd12Months', isActive: false},
        {key: 'PdInputSnPCummulativeDefaultRates', isActive: false},
        {key: 'PdInputAssumptionNonInternalModel', isActive: false},
        {key: 'PdInputAssumptionNplIndex', isActive: false},
        {key: 'PdMacroeconomicInput', isActive: false},
        {key: 'PdMacroeconomicProjection', isActive: false},
        {key: 'PdAssumptions', isActive: false},
        {key: 'MacroEconomicScenario', isActive: false},
        {key: 'InvSecMacroEconomicAssumption', isActive: false},
        {key: 'InvSecFitchCummulativeDefaultRate', isActive: false}
    ];

    constructor(
        injector: Injector,
        private _httpClient: HttpClient,
        private _commonLookupServiceProxy: CommonLookupServiceProxy,
        private _pdInputAssumptionServiceProxy: PdInputAssumptionsServiceProxy,
        private _pdSnpAssumptionServiceProxy: PdInputSnPCummulativeDefaultRatesServiceProxy,
        private _pdNonInternalServiceProxy: PdInputAssumptionNonInternalModelsServiceProxy,
        private _pdNplServiceProxy: PdInputAssumptionNplIndexesServiceProxy,
        private _pdMacroeconomicInputServiceProxy: PdInputAssumptionStatisticalsServiceProxy,
        private _pdMacroeconomicProjectionServiceProxy: PdInputAssumptionMacroeconomicProjectionsServiceProxy,
        private _invPdMacroeconomicAssumptionServiceProxy: InvSecMacroEconomicAssumptionsServiceProxy,
        private _invPdFitchRatingServiceProxy: InvSecFitchCummulativeDefaultRatesServiceProxy
    ) {
        super(injector);
        this.snpUploadUrl = AppConsts.remoteServiceBaseUrl + '/AssumptionData/ImportSnPFromExcel';
        this.nimUploadUrl = AppConsts.remoteServiceBaseUrl + '/AssumptionData/ImportNonInternalModelFromExcel';
        this.nplUploadUrl = AppConsts.remoteServiceBaseUrl + '/AssumptionData/ImportNplModelFromExcel';

        _commonLookupServiceProxy.getMacroeconomicVariableList().subscribe(result => {
            this.pdMacroeconomicVariables = result;
        });
    }

    toggleAccordion(index) {
        // let element = event.target;
        // element.classList.toggle('active');
        let state = this.accordionList[index].isActive;
        this.accordionList = this.accordionList.map(x => { x.isActive = false; return x; } );
        this.accordionList[index].isActive = !state;
    }

    load(assumptions: any, affiliateName?: string, framework?: FrameworkEnum, viewOnly = false, affiliateId = -1): void {
        this.loading = true;
        this.displayForm = true;
        this.affiliateName = affiliateName;
        this.affiliateFramework = framework;
        this.loading = false;
        this.viewOnly = viewOnly;
        this.affiliateId = affiliateId;

        if ( framework !== null && framework === FrameworkEnum.Investments) {
            this.pdInvestmentAssumption = assumptions;
            this.extractInvestmentPdAssumptionGroups();
        } else {
            this.pdAssumptions = assumptions;
            this.extractPdAssumptionGroups();
        }
    }

    extractPdAssumptionGroups(): void {
        //Extract Pd input assumption to groups
        this.pdInputAssumptions = this.pdAssumptions.pdInputAssumption;
        this.extract12MonthPds();

        this.pdSnpCumulativeDefaultRate = this.pdAssumptions.pdInputSnPCummulativeDefaultRate;
        this.extractSnPMappingForDropdown();

        this.pdNonInternalModelAssumptions = this.pdAssumptions.pdInputAssumptionNonInternalModels;
        this.extractNonInternalModel();

        this.pdNplIndexAssumptions = this.pdAssumptions.pdInputAssumptionNplIndex;

        this.pdMacroeconomicInputAssumptions = this.pdAssumptions.pdInputAssumptionMacroeconomicInput;
        this.pdMacroeconomicProjectionsAssumptions = this.pdAssumptions.pdInputAssumptionMacroeconomicProjections;
        this.extractMacroeconomic();
    }

    extractInvestmentPdAssumptionGroups(): void {
        this.pdInputAssumptions = this.pdInvestmentAssumption.pdInputAssumption;
        this.extractInvestmentPdAssumption();

        this.invsecMacroEcoAssumption = this.pdInvestmentAssumption.pdInputAssumptionMacroeconomic;

        this.invsecFitchRatingDefaultRate = this.pdInvestmentAssumption.pdInputFitchCummulativeDefaultRate;
        //console.log(this.invsecFitchRatingDefaultRate);
        this.extractInvestmentFitchRating();
    }

    extract12MonthPds(): void {
        this.creditPds = this.pdInputAssumptions.filter(x => x.assumptionGroup === PdInputAssumptionGroupEnum.CreditPD);
        this.etiPolicy = this.pdInputAssumptions.filter(x => x.assumptionGroup === PdInputAssumptionGroupEnum.CreditEtiPolicy);
        this.bestFit = this.pdInputAssumptions.filter(x => x.assumptionGroup === PdInputAssumptionGroupEnum.CreditBestFit);
    }

    extractSnPMappingForDropdown(): void {
        this.snpRatings = Array.from(new Set(this.pdSnpCumulativeDefaultRate.map((i) => i.rating)));
        this.snpYears = Array.from(new Set(this.pdSnpCumulativeDefaultRate.map((i) => i.years)));
        this.selectedSnpCummulativeDefaultRate = this.pdSnpCumulativeDefaultRate;
    }

    extractNonInternalModel(): void {
        this.pdGroups = Array.from(new Set(this.pdNonInternalModelAssumptions.map((i) => i.pdGroup)));
        this.pdMonths = Array.from(new Set(this.pdNonInternalModelAssumptions.map((i) => i.month)));
        this.pdGroups = this.pdGroups.sort();
        this.pdMonths = this.pdMonths.sort();
        this.selectedNonInternalModelAssumptions = this.pdNonInternalModelAssumptions;
    }

    extractMacroeconomic(): void {
        this.pdModes =  Array.from(new Set(this.pdMacroeconomicInputAssumptions.map((i) => i.inputName)));
        this.selectedMacroeconomicInputs = this.pdMacroeconomicInputAssumptions;
        this.selectedMacroeconomicProjections = this.pdMacroeconomicProjectionsAssumptions;
    }

    extractInvestmentPdAssumption(): void {
        this.invsecPdAssumptions = this.pdInputAssumptions.filter(x => x.assumptionGroup === PdInputAssumptionGroupEnum.InvestmentAssumption);
        this.invsecMacroEcoScenario = this.pdInputAssumptions.filter(x => x.assumptionGroup === PdInputAssumptionGroupEnum.InvestmentMacroeconomicScenario);
        this.invsecModes = Array.from(new Set(this.invsecPdAssumptions.map((i) => i.inputName)));
        this.selectedInvPdAssumption = this.invsecPdAssumptions;
    }

    extractInvestmentFitchRating(): void {
        this.invsecFitchRatings = Array.from(new Set(this.invsecFitchRatingDefaultRate.map((i) => i.rating)));
        this.selectedFitchRatingDefaultRate = this.invsecFitchRatingDefaultRate;
    }

    filterSnPCummulativeDefaultRate(): void {
        //console.log('Rating:' + this.selectedRating + ' Year: ' + this.selectedYear);
        if (this.selectedRating !== '' && this.selectedYear !== -1) {
            this.selectedSnpCummulativeDefaultRate = this.pdSnpCumulativeDefaultRate.filter(x => x.rating === this.selectedRating && x.years == this.selectedYear);
        } else if (this.selectedRating === '' && this.selectedYear !== -1) {
            this.selectedSnpCummulativeDefaultRate = this.pdSnpCumulativeDefaultRate.filter(x => x.years == this.selectedYear);
        } else if (this.selectedRating !== '' && this.selectedYear === -1) {
            this.selectedSnpCummulativeDefaultRate = this.pdSnpCumulativeDefaultRate.filter(x => x.rating === this.selectedRating);
        } else {
            this.selectedSnpCummulativeDefaultRate = this.pdSnpCumulativeDefaultRate;
        }
    }

    filterNonInternalModel(): void {
        if (this.selectedPdGroup !== '' && this.selectedNonInternalMonth !== -1) {
            this.selectedNonInternalModelAssumptions = this.pdNonInternalModelAssumptions.filter(x => x.pdGroup === this.selectedPdGroup && x.month == this.selectedNonInternalMonth);
        } else if (this.selectedPdGroup === '' && this.selectedNonInternalMonth !== -1) {
            this.selectedNonInternalModelAssumptions = this.pdNonInternalModelAssumptions.filter(x => x.month == this.selectedNonInternalMonth);
        } else if (this.selectedPdGroup !== '' && this.selectedNonInternalMonth === -1) {
            this.selectedNonInternalModelAssumptions = this.pdNonInternalModelAssumptions.filter(x => x.pdGroup === this.selectedPdGroup);
        } else {
            this.selectedNonInternalModelAssumptions = this.pdNonInternalModelAssumptions;
        }
    }

    filterMacroeconomicProjection(): void {
        if (this.selectedMacroeconomicVariable !== '') {
            this.selectedMacroeconomicProjections = this.pdMacroeconomicProjectionsAssumptions.filter(x => x.assumptionGroup.toString() === this.selectedMacroeconomicVariable);
            //this.selectedMacroeconomicInputs = this.pdMacroeconomicInputAssumptions.filter(x => x.inputName === this.selectedMacroeconomicVariable);
        } else {
            this.selectedMacroeconomicProjections = this.pdMacroeconomicProjectionsAssumptions;
            //this.selectedMacroeconomicInputs = this.pdMacroeconomicInputAssumptions;
        }
    }

    filterMacroeconomic(): void {
        if (this.selectedMode !== '' && this.selectedMacroeconomicVariable !== '') {
            //this.selectedMacroeconomicProjections = this.pdMacroeconomicProjectionsAssumptions.filter(x => x.inputName === this.selectedMacroeconomicVariable);
            this.selectedMacroeconomicInputs = this.pdMacroeconomicInputAssumptions.filter(x => x.inputName === this.selectedMode && x.assumptionGroup.toString() === this.selectedMacroeconomicVariable);
        } else if (this.selectedMode !== '' && this.selectedMacroeconomicVariable === '') {
            this.selectedMacroeconomicInputs = this.pdMacroeconomicInputAssumptions.filter(x => x.inputName === this.selectedMode);
        } else if (this.selectedMode === '' && this.selectedMacroeconomicVariable !== '') {
            this.selectedMacroeconomicInputs = this.pdMacroeconomicInputAssumptions.filter(x => x.assumptionGroup.toString() === this.selectedMacroeconomicVariable);
        } else {
            //this.selectedMacroeconomicProjections = this.pdMacroeconomicProjectionsAssumptions;
            this.selectedMacroeconomicInputs = this.pdMacroeconomicInputAssumptions;
        }
    }

    filterFitchRatingRates(): void {
        if (this.selectedFitchRating !== '') {
            this.selectedFitchRatingDefaultRate = this.invsecFitchRatingDefaultRate.filter(x => x.rating === this.selectedFitchRating);
        } else {
            this.selectedFitchRatingDefaultRate = this.invsecFitchRatingDefaultRate;
        }
    }

    filterInvPdAssumption(): void {
        if (this.selectedInvMode !== '') {
            this.selectedInvPdAssumption = this.invsecPdAssumptions.filter(x => x.inputName === this.selectedInvMode);
        } else {
            this.selectedInvPdAssumption = this.invsecPdAssumptions;
        }
    }

    hide(): void {
        this.displayForm = false;
    }

    editPdAssumption(pdInput: PdInputAssumptionDto, locKey = 'SnPMappingEtiCreditPolicy'): void {
        this.editAssumptionModal.configure({
            framework: this.affiliateFramework,
            affiliateName: this.affiliateName,
            dataSource: pdInput,
            serviceProxy: this._pdInputAssumptionServiceProxy,
            assumptionGroup: this.l(locKey),
            assumption: AssumptionTypeEnum.PdInputAssumption
        });
        this.editAssumptionModal.show();
    }

    editSnPAssumption(pdInput: PdInputSnPCummulativeDefaultRateDto): void {
        this.editAssumptionModal.configure({
            framework: this.affiliateFramework,
            affiliateName: this.affiliateName,
            dataSource: pdInput,
            serviceProxy: this._pdSnpAssumptionServiceProxy,
            assumptionGroup: this.l('PdInputSnPCummulativeDefaultRates'),
            assumption: AssumptionTypeEnum.PdInputAssumption
        });
        this.editAssumptionModal.show();
    }

    editNonInternalAssumption(pdInput: PdInputAssumptionNonInternalModelDto): void {
        this.editAssumptionModal.configure({
            framework: this.affiliateFramework,
            affiliateName: this.affiliateName,
            dataSource: pdInput,
            serviceProxy: this._pdNonInternalServiceProxy,
            assumptionGroup: this.l('PdInputAssumptionNonInternalModel'),
            assumption: AssumptionTypeEnum.PdInputAssumption
        });
        this.editAssumptionModal.show();
    }

    editNplAssumption(pdInput: PdInputAssumptionNplIndexDto): void {
        this.editAssumptionModal.configure({
            framework: this.affiliateFramework,
            affiliateName: this.affiliateName,
            dataSource: pdInput,
            serviceProxy: this._pdNplServiceProxy,
            assumptionGroup: this.l('PdInputAssumptionNplIndex'),
            assumption: AssumptionTypeEnum.PdInputAssumption
        });
        this.editAssumptionModal.show();
    }

    editMacroInputAssumption(pdInput: PdInputAssumptionMacroeconomicInputDto): void {
        this.editAssumptionModal.configure({
            framework: this.affiliateFramework,
            affiliateName: this.affiliateName,
            dataSource: pdInput,
            serviceProxy: this._pdMacroeconomicInputServiceProxy,
            assumptionGroup: this.l('PdMacroeconomicInput'),
            assumption: AssumptionTypeEnum.PdInputAssumption
        });
        this.editAssumptionModal.show();
    }

    editMacroProjectionAssumption(pdInput: PdInputAssumptionMacroeconomicProjectionDto): void {
        this.editAssumptionModal.configure({
            framework: this.affiliateFramework,
            affiliateName: this.affiliateName,
            dataSource: pdInput,
            serviceProxy: this._pdMacroeconomicProjectionServiceProxy,
            assumptionGroup: this.l('PdMacroeconomicProjection'),
            assumption: AssumptionTypeEnum.PdInputAssumption
        });
        this.editAssumptionModal.show();
    }

    editInvSecMacroAssumption(pdInput: InvSecMacroEconomicAssumptionDto): void {
        this.editAssumptionModal.configure({
            framework: this.affiliateFramework,
            affiliateName: this.affiliateName,
            dataSource: pdInput,
            serviceProxy: this._invPdMacroeconomicAssumptionServiceProxy,
            assumptionGroup: this.l('InvSecMacroEconomicAssumption'),
            assumption: AssumptionTypeEnum.PdInputAssumption
        });
        this.editAssumptionModal.show();
    }

    editInvSecFitchAssumption(pdInput: InvSecMacroEconomicAssumptionDto): void {
        this.editAssumptionModal.configure({
            framework: this.affiliateFramework,
            affiliateName: this.affiliateName,
            dataSource: pdInput,
            serviceProxy: this._invPdFitchRatingServiceProxy,
            assumptionGroup: this.l('InvSecFitchCummulativeDefaultRate'),
            assumption: AssumptionTypeEnum.PdInputAssumption
        });
        this.editAssumptionModal.show();
    }


    uploadSnpData(data: { files: File }): void {
        const formData: FormData = new FormData();
        const file = data.files[0];
        formData.append('file', file, file.name);
        formData.append('framework', this.affiliateFramework.toString());
        formData.append('affiliateId', this.affiliateId.toString());

        this.message.confirm(
            this.l('ExistingDataWouldBeReplaced'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._httpClient
                        .post<any>(this.snpUploadUrl, formData)
                        .pipe(finalize(() => this.snpExcelFileUpload.clear()))
                        .subscribe(response => {
                            if (response.success) {
                                this.notify.success(this.l('ImportDataProcessStart'));
                                //this.autoReloadUploadSummary();
                            } else if (response.error != null) {
                                this.notify.error(this.l('ImportDataUploadFailed'));
                            }
                        });
                }
            }
        );
    }

    uploadNimData(data: { files: File }): void {
        const formData: FormData = new FormData();
        const file = data.files[0];
        formData.append('file', file, file.name);
        formData.append('framework', this.affiliateFramework.toString());
        formData.append('affiliateId', this.affiliateId.toString());

        this.message.confirm(
            this.l('ExistingDataWouldBeReplaced'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._httpClient
                        .post<any>(this.nimUploadUrl, formData)
                        .pipe(finalize(() => this.nimExcelFileUpload.clear()))
                        .subscribe(response => {
                            if (response.success) {
                                this.notify.success(this.l('ImportDataProcessStart'));
                                //this.autoReloadUploadSummary();
                            } else if (response.error != null) {
                                this.notify.error(this.l('ImportDataUploadFailed'));
                            }
                        });
                }
            }
        );
    }

    uploadNplData(data: { files: File }): void {
        const formData: FormData = new FormData();
        const file = data.files[0];
        formData.append('file', file, file.name);
        formData.append('framework', this.affiliateFramework.toString());
        formData.append('affiliateId', this.affiliateId.toString());

        this.message.confirm(
            this.l('ExistingDataWouldBeReplaced'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._httpClient
                        .post<any>(this.nplUploadUrl, formData)
                        .pipe(finalize(() => this.nplExcelFileUpload.clear()))
                        .subscribe(response => {
                            if (response.success) {
                                this.notify.success(this.l('ImportDataProcessStart'));
                                //this.autoReloadUploadSummary();
                            } else if (response.error != null) {
                                this.notify.error(this.l('ImportDataUploadFailed'));
                            }
                        });
                }
            }
        );
    }

    onUploadExcelError(): void {
        this.notify.error(this.l('ImportDataUploadFailed'));
    }

}
