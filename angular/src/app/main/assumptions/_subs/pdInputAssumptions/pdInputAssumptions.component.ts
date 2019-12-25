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

    pdAssumptions: GetAllPdAssumptionsDto = new GetAllPdAssumptionsDto();

    pdInputAssumptions: PdInputAssumptionDto[] = new Array<PdInputAssumptionDto>();
    //Pd Input Sub groups:
    creditPds: PdInputAssumptionDto[] = new Array();
    etiPolicy: PdInputAssumptionDto[] = new Array();
    bestFit: PdInputAssumptionDto[] = new Array();


    pdMacroeconomicInputAssumptions: PdInputAssumptionMacroeconomicInputDto[] = new Array<PdInputAssumptionMacroeconomicInputDto>();
    pdMacroeconomicProjectionsAssumptions: PdInputAssumptionMacroeconomicProjectionDto[] = new Array<PdInputAssumptionMacroeconomicProjectionDto>();
    pdNonInternalModelAssumptions: PdInputAssumptionNonInternalModelDto[] = new Array<PdInputAssumptionNonInternalModelDto>();
    pdNplIndexAssumptions: PdInputAssumptionNplIndexDto[] = new Array<PdInputAssumptionNplIndexDto>();
    pdSnpCumulativeDefaultRate: PdInputSnPCummulativeDefaultRateDto[] = new Array<PdInputSnPCummulativeDefaultRateDto>();

    dataTypeEnum = DataTypeEnum;

    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    load(assumptions: GetAllPdAssumptionsDto): void {
        this.pdAssumptions = assumptions;
        this.extractPdAssumptionGroups();
        this.displayForm = true;
    }

    extractPdAssumptionGroups(): void {
        //Extract Pd input assumption to groups
        this.pdInputAssumptions = this.pdAssumptions.pdInputAssumption;
        this.creditPds = this.pdInputAssumptions.filter(x => x.assumptionGroup === PdInputAssumptionGroupEnum.CreditPD);
        this.etiPolicy = this.pdInputAssumptions.filter(x => x.assumptionGroup === PdInputAssumptionGroupEnum.CreditEtiPolicy);
        this.bestFit = this.pdInputAssumptions.filter(x => x.assumptionGroup === PdInputAssumptionGroupEnum.CreditBestFit);

        this.pdMacroeconomicInputAssumptions = this.pdAssumptions.pdInputAssumptionMacroeconomicInput;
        this.pdMacroeconomicProjectionsAssumptions = this.pdAssumptions.pdInputAssumptionMacroeconomicProjections;
        this.pdNonInternalModelAssumptions = this.pdAssumptions.pdInputAssumptionNonInternalModels;
        this.pdNplIndexAssumptions = this.pdAssumptions.pdInputAssumptionNplIndex;
        this.pdSnpCumulativeDefaultRate = this.pdAssumptions.pdInputSnPCummulativeDefaultRate;
    }

    hide(): void {
        this.displayForm = false;
    }

}
