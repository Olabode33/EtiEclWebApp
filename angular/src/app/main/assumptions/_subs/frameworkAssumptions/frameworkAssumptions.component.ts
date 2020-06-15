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
    viewOnly = false;

    frameworkAssumptions: AssumptionDto[] = new Array();

    scenarioAssumptionGroup: AssumptionDto[] = new Array();
    absoluteCreditQualityAssumptionGroup: AssumptionDto[] = new Array();
    relativeCreditQualityAssumptionGroup: AssumptionDto[] = new Array();
    forwardTransitionsAssumptionGroup: AssumptionDto[] = new Array();
    backwardTransitionAssumptionGroup: AssumptionDto[] = new Array();
    creditRatingRankAssumptionGroup: AssumptionDto[] = new Array();

    assumptionGroupEnum = AssumptionGroupEnum;
    dataTypeEnum = DataTypeEnum;

    affiliateName = '';
    affiliateFramework: FrameworkEnum;

    accordionList = [
        {key: this.assumptionGroupEnum[this.assumptionGroupEnum.ScenarioInputs], isActive: false},
        {key: this.assumptionGroupEnum[this.assumptionGroupEnum.AbsoluteCreditQuality], isActive: false},
        {key: this.assumptionGroupEnum[this.assumptionGroupEnum.RelativeCreditQuality], isActive: false},
        {key: this.assumptionGroupEnum[this.assumptionGroupEnum.ForwardTransitions], isActive: false},
        {key: this.assumptionGroupEnum[this.assumptionGroupEnum.BackwardTransitions], isActive: false},
        {key: this.assumptionGroupEnum[this.assumptionGroupEnum.CreditRatingRank], isActive: false}
    ];

    constructor(
        injector: Injector,
        private _assumptionInputServiceProxy: AssumptionsServiceProxy
    ) {
        super(injector);
    }

    toggleAccordion(index) {
        // let element = event.target;
        // element.classList.toggle('active');
        let state = this.accordionList[index].isActive;
        this.accordionList = this.accordionList.map(x => { x.isActive = false; return x; } );
        this.accordionList[index].isActive = !state;
    }

    load(assumptions: AssumptionDto[], affiliateName?: string, framework?: FrameworkEnum, viewOnly = false): void {
        this.frameworkAssumptions = assumptions;
        this.affiliateName = affiliateName;
        this.affiliateFramework = framework;
        this.extractGeneralAssumptionGroups();
        this.displayForm = true;
        this.viewOnly = viewOnly;
    }

    extractGeneralAssumptionGroups(): void {
        //Extract general assumption to groups
        console.log(this.frameworkAssumptions);
        this.scenarioAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === AssumptionGroupEnum.ScenarioInputs);
        this.absoluteCreditQualityAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === AssumptionGroupEnum.AbsoluteCreditQuality);
        this.relativeCreditQualityAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === AssumptionGroupEnum.RelativeCreditQuality);
        this.forwardTransitionsAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === AssumptionGroupEnum.ForwardTransitions);
        this.backwardTransitionAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === AssumptionGroupEnum.BackwardTransitions);
        this.creditRatingRankAssumptionGroup = this.frameworkAssumptions.filter(x => x.assumptionGroup === AssumptionGroupEnum.CreditRatingRank);

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
