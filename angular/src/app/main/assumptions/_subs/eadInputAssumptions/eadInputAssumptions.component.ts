import { Component, OnInit, Injector } from '@angular/core';
import { DataTypeEnum, EadInputAssumptionDto, EadInputAssumptionGroupEnum } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-eadInputAssumptions',
  templateUrl: './eadInputAssumptions.component.html',
  styleUrls: ['./eadInputAssumptions.component.css']
})
export class EadInputAssumptionsComponent extends AppComponentBase {

    displayForm = false;

    eadInputAssumptions: EadInputAssumptionDto[] = new Array();

    ccfGroup: EadInputAssumptionDto[] = new Array();
    virGroup: EadInputAssumptionDto[] = new Array();
    exchangeRateGroup: EadInputAssumptionDto[] = new Array();

    dataTypeEnum = DataTypeEnum;
    eadAssumptionGroupEnum = EadInputAssumptionGroupEnum;

    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    load(assumptions: EadInputAssumptionDto[]): void {
        this.eadInputAssumptions = assumptions;
        this.extractEadAssumptionGroups();
        this.displayForm = true;
    }

    extractEadAssumptionGroups(): void {
        //Extract general assumption to groups
        this.ccfGroup = this.eadInputAssumptions.filter(x => x.assumptionGroup === EadInputAssumptionGroupEnum.CreditConversionFactors);
        this.virGroup = this.eadInputAssumptions.filter(x => x.assumptionGroup === EadInputAssumptionGroupEnum.VariableInterestRateProjections);
        this.exchangeRateGroup = this.eadInputAssumptions.filter(x => x.assumptionGroup === EadInputAssumptionGroupEnum.ExchangeRateProjections);
    }

    hide(): void {
        this.displayForm = false;
    }

}
