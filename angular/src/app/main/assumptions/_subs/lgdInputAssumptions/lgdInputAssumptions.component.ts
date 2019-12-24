import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { LgdAssumptionDto, LdgInputAssumptionGroupEnum, DataTypeEnum } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-lgdInputAssumptions',
  templateUrl: './lgdInputAssumptions.component.html',
  styleUrls: ['./lgdInputAssumptions.component.css']
})
export class LgdInputAssumptionsComponent extends AppComponentBase {

    displayForm = false;

    lgdInputAssumptions: LgdAssumptionDto[] = new Array();

    timeToDefaultGroup: LgdAssumptionDto[] = new Array();
    cureRateGroup: LgdAssumptionDto[] = new Array();
    corHighGroup: LgdAssumptionDto[] = new Array();
    corLowGroup: LgdAssumptionDto[] = new Array();
    collateralGrowthGroup: LgdAssumptionDto[] = new Array();
    collateralTTRGroup: LgdAssumptionDto[] = new Array();

    dataTypeEnum = DataTypeEnum;
    lgdAssumptionGroupEnum = LdgInputAssumptionGroupEnum;

    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    load(assumptions: LgdAssumptionDto[]): void {
        this.lgdInputAssumptions = assumptions;
        this.extractLgdAssumptionGroups();
        this.displayForm = true;
    }

    extractLgdAssumptionGroups(): void {
        //Extract general assumption to groups
        this.cureRateGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.UnsecuredRecoveriesCureRate);
        this.timeToDefaultGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.UnsecuredRecoveriesTimeInDefault);
        this.corHighGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.CostOfRecoveryHigh);
        this.corLowGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.CostOfRecoveryLow);
        this.collateralGrowthGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.CollateralGrowthBest);
        this.collateralTTRGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.CollateralTTR);
    }

    hide(): void {
        this.displayForm = false;
    }

}
