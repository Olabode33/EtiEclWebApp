import { AppComponentBase } from '@shared/common/app-component-base';
import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AssumptionDto, AssumptionGroupEnum, DataTypeEnum, FrameworkEnum, AssumptionTypeEnum, AssumptionsServiceProxy } from '@shared/service-proxies/service-proxies';
import { EditAssumptionModalComponent } from '../edit-assumption-modal/edit-assumption-modal.component';

@Component({
  selector: 'app-frameworkAssumptions',
  templateUrl: './frameworkAssumptions.component.html',
  styleUrls: ['./frameworkAssumptions.component.css']
})
export class FrameworkAssumptionsComponent extends AppComponentBase {

    @ViewChild('editAssumptionModal', {static: true}) editAssumptionModal: EditAssumptionModalComponent;

    displayForm = false;

    frameworkAssumptions: AssumptionDto[] = new Array();

    scenarioAssumptionGroup: AssumptionDto[] = new Array();
    absoluteCreditQualityAssumptionGroup: AssumptionDto[] = new Array();
    relativeCreditQualityAssumptionGroup: AssumptionDto[] = new Array();
    forwardTransitionsAssumptionGroup: AssumptionDto[] = new Array();
    backwardTransitionAssumptionGroup: AssumptionDto[] = new Array();

    assumptionGroupEnum = AssumptionGroupEnum;
    dataTypeEnum = DataTypeEnum;

    affiliateName = '';
    affiliateFramework: FrameworkEnum;

    constructor(
        injector: Injector,
        private _assumptionInputServiceProxy: AssumptionsServiceProxy
    ) {
        super(injector);
    }

    load(assumptions: AssumptionDto[], affiliateName?: string, framework?: FrameworkEnum): void {
        this.frameworkAssumptions = assumptions;
        this.affiliateName = affiliateName;
        this.affiliateFramework = framework;
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

    editAssumption(frameworkInput: AssumptionDto, groupKey: string): void {
        this.editAssumptionModal.configure({
            framework: this.affiliateFramework,
            affiliateName: this.affiliateName,
            dataSource: frameworkInput,
            serviceProxy: this._assumptionInputServiceProxy,
            assumptionGroup: this.l(groupKey),
            assumption: AssumptionTypeEnum.General
        });
        this.editAssumptionModal.show();
    }

}
