import { filter } from 'rxjs/operators';
import { PdInputAssumptionGroupEnum, GetAllPdAssumptionsDto, PdInputAssumptionDto, PdInputAssumptionMacroeconomicInputDto, PdInputAssumptionMacroeconomicProjectionDto, PdInputAssumptionNonInternalModelDto, PdInputAssumptionNplIndexDto, PdInputSnPCummulativeDefaultRateDto, DataTypeEnum } from './../../../../../shared/service-proxies/service-proxies';
import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-pdInputAssumptions',
  templateUrl: './pdInputAssumptions.component.html',
  styleUrls: ['./pdInputAssumptions.component.css']
})
export class PdInputAssumptionsComponent extends AppComponentBase {

    displayForm = false;
    loading = false;

    pdAssumptions: GetAllPdAssumptionsDto = new GetAllPdAssumptionsDto();

    pdInputAssumptions: PdInputAssumptionDto[] = new Array<PdInputAssumptionDto>();
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



    pdMacroeconomicInputAssumptions: PdInputAssumptionMacroeconomicInputDto[] = new Array<PdInputAssumptionMacroeconomicInputDto>();
    pdMacroeconomicProjectionsAssumptions: PdInputAssumptionMacroeconomicProjectionDto[] = new Array<PdInputAssumptionMacroeconomicProjectionDto>();
    pdNplIndexAssumptions: PdInputAssumptionNplIndexDto[] = new Array<PdInputAssumptionNplIndexDto>();

    dataTypeEnum = DataTypeEnum;

    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    load(assumptions: GetAllPdAssumptionsDto): void {
        this.loading = true;
        this.displayForm = true;
        this.pdAssumptions = assumptions;
        this.extractPdAssumptionGroups();
        this.loading = false;
    }

    extractPdAssumptionGroups(): void {
        //Extract Pd input assumption to groups
        this.pdInputAssumptions = this.pdAssumptions.pdInputAssumption;
        this.extract12MonthPds();

        this.pdSnpCumulativeDefaultRate = this.pdAssumptions.pdInputSnPCummulativeDefaultRate;
        this.extractSnPMappingForDropdown();

        this.pdNonInternalModelAssumptions = this.pdAssumptions.pdInputAssumptionNonInternalModels;
        this.extractNonInternalModel();

        this.pdMacroeconomicInputAssumptions = this.pdAssumptions.pdInputAssumptionMacroeconomicInput;
        this.pdMacroeconomicProjectionsAssumptions = this.pdAssumptions.pdInputAssumptionMacroeconomicProjections;
        this.pdNplIndexAssumptions = this.pdAssumptions.pdInputAssumptionNplIndex;
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
        console.log(this.selectedNonInternalMonth);
        if (this.selectedPdGroup !== '' && this.selectedNonInternalMonth !== -1) {
            console.log('Both');
            this.selectedNonInternalModelAssumptions = this.pdNonInternalModelAssumptions.filter(x => x.pdGroup === this.selectedPdGroup && x.month == this.selectedNonInternalMonth);
        } else if (this.selectedPdGroup === '' && this.selectedNonInternalMonth !== -1) {
            console.log('Month');
            this.selectedNonInternalModelAssumptions = this.pdNonInternalModelAssumptions.filter(x => x.month == this.selectedNonInternalMonth);
        } else if (this.selectedPdGroup !== '' && this.selectedNonInternalMonth === -1) {
            console.log('PdGroup');
            this.selectedNonInternalModelAssumptions = this.pdNonInternalModelAssumptions.filter(x => x.pdGroup === this.selectedPdGroup);
        } else {
            this.selectedNonInternalModelAssumptions = this.pdNonInternalModelAssumptions;
        }
    }

    hide(): void {
        this.displayForm = false;
    }

}
