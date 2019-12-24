import { AppComponentBase } from '@shared/common/app-component-base';
import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AssumptionDto, AssumptionGroupEnum, DataTypeEnum } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-frameworkAssumptions',
  templateUrl: './frameworkAssumptions.component.html',
  styleUrls: ['./frameworkAssumptions.component.css']
})
export class FrameworkAssumptionsComponent extends AppComponentBase {

    displayForm = false;

    frameworkAssumptions: AssumptionDto[] = new Array();

    scenarioAssumptionGroup: AssumptionDto[] = new Array();
    absoluteCreditQualityAssumptionGroup: AssumptionDto[] = new Array();
    relativeCreditQualityAssumptionGroup: AssumptionDto[] = new Array();
    forwardTransitionsAssumptionGroup: AssumptionDto[] = new Array();
    backwardTransitionAssumptionGroup: AssumptionDto[] = new Array();

    assumptionGroupEnum = AssumptionGroupEnum;
    dataTypeEnum = DataTypeEnum;

    constructor(
        injector: Injector
    ) {
        super(injector);
    }

    load(assumptions: AssumptionDto[]): void {
        this.frameworkAssumptions = assumptions;
        this.extractGeneralAssumptionGroups();
        this.displayForm = true;
    }

    extractGeneralAssumptionGroups(): void {
        //Extract general assumption to groups
        console.log(this.frameworkAssumptions);
        this.scenarioAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === AssumptionGroupEnum.ScenarioInputs);
        this.absoluteCreditQualityAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === AssumptionGroupEnum.AbsoluteCreditQuality);
        this.relativeCreditQualityAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === AssumptionGroupEnum.RelativeCreditQuality);
        this.forwardTransitionsAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === AssumptionGroupEnum.ForwardTransitions);
        this.backwardTransitionAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === AssumptionGroupEnum.BackwardTransitions);
    }

    hide(): void {
        this.displayForm = false;
    }

}
