import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { LgdAssumptionDto, LdgInputAssumptionGroupEnum, DataTypeEnum, FrameworkEnum, LgdAssumptionUnsecuredRecoveriesServiceProxy, AssumptionTypeEnum } from '@shared/service-proxies/service-proxies';
import { EditAssumptionModalComponent } from '../edit-assumption-modal/edit-assumption-modal.component';

@Component({
  selector: 'app-lgdInputAssumptions',
  templateUrl: './lgdInputAssumptions.component.html',
  styleUrls: ['./lgdInputAssumptions.component.css']
})
export class LgdInputAssumptionsComponent extends AppComponentBase {

    @ViewChild('editAssumptionModal', {static: true}) editAssumptionModal: EditAssumptionModalComponent;

    displayForm = false;
    viewOnly = false;

    lgdInputAssumptions: LgdAssumptionDto[] = new Array();

    timeToDefaultGroup: LgdAssumptionDto[] = new Array();
    cureRateGroup: LgdAssumptionDto[] = new Array();
    corHighGroup: LgdAssumptionDto[] = new Array();
    corLowGroup: LgdAssumptionDto[] = new Array();
    collateralGrowthGroup: LgdAssumptionDto[] = new Array();
    collateralGrowthOptimisticGroup: LgdAssumptionDto[] = new Array();
    collateralGrowthDownturnGroup: LgdAssumptionDto[] = new Array();
    collateralTTRGroup: LgdAssumptionDto[] = new Array();
    investmentGroup: LgdAssumptionDto[] = new Array();
    collateralProjectionBest: LgdAssumptionDto[] = new Array();
    collateralProjectionOptimistic: LgdAssumptionDto[] = new Array();
    collateralProjectionDownturn: LgdAssumptionDto[] = new Array();

    dataTypeEnum = DataTypeEnum;
    lgdAssumptionGroupEnum = LdgInputAssumptionGroupEnum;
    frameworkEnum = FrameworkEnum;

    affiliateName = '';
    affiliateFramework: FrameworkEnum;

    accordionList = [
        {key: this.lgdAssumptionGroupEnum[this.lgdAssumptionGroupEnum.UnsecuredRecoveriesCureRate], isActive: false},
        {key: this.lgdAssumptionGroupEnum[this.lgdAssumptionGroupEnum.UnsecuredRecoveriesTimeInDefault], isActive: false},
        {key: this.lgdAssumptionGroupEnum[this.lgdAssumptionGroupEnum.CostOfRecoveryHigh], isActive: false},
        {key: this.lgdAssumptionGroupEnum[this.lgdAssumptionGroupEnum.CollateralGrowthBest], isActive: false},
        {key: this.lgdAssumptionGroupEnum[this.lgdAssumptionGroupEnum.CollateralTTR], isActive: false},
        {key: this.lgdAssumptionGroupEnum[this.lgdAssumptionGroupEnum.General], isActive: false},
        {key: this.lgdAssumptionGroupEnum[this.lgdAssumptionGroupEnum.CollateralProjectionBest], isActive: false},
        {key: this.lgdAssumptionGroupEnum[this.lgdAssumptionGroupEnum.CollateralProjectionOptimistic], isActive: false},
        {key: this.lgdAssumptionGroupEnum[this.lgdAssumptionGroupEnum.CollateralProjectionDownturn], isActive: false},
        {key: this.lgdAssumptionGroupEnum[this.lgdAssumptionGroupEnum.CostOfRecoveryLow], isActive: false},
        {key: this.lgdAssumptionGroupEnum[this.lgdAssumptionGroupEnum.CollateralGrowthOptimistic], isActive: false},
        {key: this.lgdAssumptionGroupEnum[this.lgdAssumptionGroupEnum.CollateralGrowthDownturn], isActive: false},
    ];

    constructor(
        injector: Injector,
        private _lgdInputAssumptionServiceProxy: LgdAssumptionUnsecuredRecoveriesServiceProxy
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

    load(assumptions: LgdAssumptionDto[], affiliateName?: string, framework?: FrameworkEnum, viewOnly = false): void {
        this.lgdInputAssumptions = assumptions;
        this.affiliateName = affiliateName;
        this.affiliateFramework = framework;
        this.extractLgdAssumptionGroups();
        this.displayForm = true;
        this.viewOnly = viewOnly;
        if (framework === FrameworkEnum.Investments) {
            this.accordionList.find(x => x.key === this.lgdAssumptionGroupEnum[this.lgdAssumptionGroupEnum.General]).isActive = true;
        }
    }

    extractLgdAssumptionGroups(): void {
        //Extract general assumption to groups
        this.cureRateGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.UnsecuredRecoveriesCureRate);
        this.timeToDefaultGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.UnsecuredRecoveriesTimeInDefault);
        this.corHighGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.CostOfRecoveryHigh);
        this.corLowGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.CostOfRecoveryLow);
        this.collateralGrowthGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.CollateralGrowthBest);
        this.collateralGrowthOptimisticGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.CollateralGrowthOptimistic);
        this.collateralGrowthDownturnGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.CollateralGrowthDownturn);
        this.collateralTTRGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.CollateralTTR);
        this.investmentGroup = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.General);
        this.collateralProjectionBest = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.CollateralProjectionBest);
        this.collateralProjectionOptimistic = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.CollateralProjectionOptimistic);
        this.collateralProjectionDownturn = this.lgdInputAssumptions.filter(x => x.assumptionGroup === LdgInputAssumptionGroupEnum.CollateralProjectionDownturn);
    }

    hide(): void {
        this.displayForm = false;
    }

    editAssumption(lgdInput: LgdAssumptionDto, groupKey: string): void {
        this.editAssumptionModal.configure({
            framework: this.affiliateFramework,
            affiliateName: this.affiliateName,
            dataSource: lgdInput,
            serviceProxy: this._lgdInputAssumptionServiceProxy,
            assumptionGroup: this.l(groupKey),
            assumption: AssumptionTypeEnum.LgdInputAssumption
        });
        this.editAssumptionModal.show();
    }

}
