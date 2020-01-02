import { filter } from 'rxjs/operators';
import { PdInputAssumptionGroupEnum, GetAllPdAssumptionsDto, PdInputAssumptionDto, PdInputAssumptionMacroeconomicInputDto, PdInputAssumptionMacroeconomicProjectionDto, PdInputAssumptionNonInternalModelDto, PdInputAssumptionNplIndexDto, PdInputSnPCummulativeDefaultRateDto, DataTypeEnum, NameValueDto, CommonLookupServiceProxy, PdInputAssumptionsServiceProxy, FrameworkEnum, AssumptionTypeEnum, PdInputSnPCummulativeDefaultRatesServiceProxy, PdInputAssumptionNonInternalModelsServiceProxy, PdInputAssumptionNplIndexesServiceProxy, PdInputAssumptionStatisticalsServiceProxy, PdInputAssumptionMacroeconomicProjectionsServiceProxy } from './../../../../../shared/service-proxies/service-proxies';
import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { EditAssumptionModalComponent } from '../edit-assumption-modal/edit-assumption-modal.component';

@Component({
  selector: 'app-pdInputAssumptions',
  templateUrl: './pdInputAssumptions.component.html',
  styleUrls: ['./pdInputAssumptions.component.css']
})
export class PdInputAssumptionsComponent extends AppComponentBase {

    @ViewChild('editAssumptionModal', {static: true}) editAssumptionModal: EditAssumptionModalComponent;

    displayForm = false;
    loading = false;
    viewOnly = false;

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

    dataTypeEnum = DataTypeEnum;

    affiliateName = '';
    affiliateFramework: FrameworkEnum;

    constructor(
        injector: Injector,
        private _commonLookupServiceProxy: CommonLookupServiceProxy,
        private _pdInputAssumptionServiceProxy: PdInputAssumptionsServiceProxy,
        private _pdSnpAssumptionServiceProxy: PdInputSnPCummulativeDefaultRatesServiceProxy,
        private _pdNonInternalServiceProxy: PdInputAssumptionNonInternalModelsServiceProxy,
        private _pdNplServiceProxy: PdInputAssumptionNplIndexesServiceProxy,
        private _pdMacroeconomicInputServiceProxy: PdInputAssumptionStatisticalsServiceProxy,
        private _pdMacroeconomicProjectionServiceProxy: PdInputAssumptionMacroeconomicProjectionsServiceProxy
    ) {
        super(injector);
        _commonLookupServiceProxy.getMacroeconomicVariableList().subscribe(result => {
            this.pdMacroeconomicVariables = result;
        });
    }

    load(assumptions: GetAllPdAssumptionsDto, affiliateName?: string, framework?: FrameworkEnum, viewOnly = false): void {
        this.loading = true;
        this.displayForm = true;
        this.pdAssumptions = assumptions;
        this.affiliateName = affiliateName;
        this.affiliateFramework = framework;
        this.extractPdAssumptionGroups();
        this.loading = false;
        this.viewOnly = viewOnly;
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
        this.selectedNonInternalModelAssumptions = this.pdNonInternalModelAssumptions;
    }

    extractMacroeconomic(): void {
        this.pdModes =  Array.from(new Set(this.pdMacroeconomicInputAssumptions.map((i) => i.inputName)));
        this.selectedMacroeconomicInputs = this.pdMacroeconomicInputAssumptions;
        this.selectedMacroeconomicProjections = this.pdMacroeconomicProjectionsAssumptions;
    }

    filterSnPCummulativeDefaultRate(): void {
        console.log('Rating:' + this.selectedRating + ' Year: ' + this.selectedYear);
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

    hide(): void {
        this.displayForm = false;
    }

    editPdAssumption(pdInput: PdInputAssumptionDto): void {
        this.editAssumptionModal.configure({
            framework: this.affiliateFramework,
            affiliateName: this.affiliateName,
            dataSource: pdInput,
            serviceProxy: this._pdInputAssumptionServiceProxy,
            assumptionGroup: this.l('SnPMappingEtiCreditPolicy'),
            assumption: AssumptionTypeEnum.PdInputAssumption
        });
        this.editAssumptionModal.show();
    }

    uploadSnP(): void {
        this.notify.error('Yet to be implemented!');
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

}
