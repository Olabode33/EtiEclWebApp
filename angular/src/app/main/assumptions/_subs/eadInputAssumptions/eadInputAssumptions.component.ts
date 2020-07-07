import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { DataTypeEnum, EadInputAssumptionDto, EadInputAssumptionGroupEnum, FrameworkEnum, EadInputAssumptionsServiceProxy, AssumptionTypeEnum, CreateOrEditEadInputAssumptionDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { EditAssumptionModalComponent } from '../edit-assumption-modal/edit-assumption-modal.component';
import { AddEadAssumptionModalComponent } from '../add-ead-assumption-modal/add-ead-assumption-modal.component';

@Component({
  selector: 'app-eadInputAssumptions',
  templateUrl: './eadInputAssumptions.component.html',
  styleUrls: ['./eadInputAssumptions.component.css']
})
export class EadInputAssumptionsComponent extends AppComponentBase {

    @ViewChild('editAssumptionModal', {static: true}) editAssumptionModal: EditAssumptionModalComponent;
    @ViewChild('addAssumptionModal', {static: true}) addAssumptionModal: AddEadAssumptionModalComponent;

    displayForm = false;
    viewOnly = true;

    eadInputAssumptions: EadInputAssumptionDto[] = new Array();

    eadGroup: EadInputAssumptionDto[] = new Array();
    ccfGroup: EadInputAssumptionDto[] = new Array();
    virGroup: EadInputAssumptionDto[] = new Array();
    exchangeRateGroup: EadInputAssumptionDto[] = new Array();

    dataTypeEnum = DataTypeEnum;
    eadAssumptionGroupEnum = EadInputAssumptionGroupEnum;
    frameworkEnum = FrameworkEnum;

    affiliateName = '';
    affiliateFramework: FrameworkEnum;

    accordionList = [
        {key: this.eadAssumptionGroupEnum[this.eadAssumptionGroupEnum.CreditConversionFactors], isActive: false},
        {key: this.eadAssumptionGroupEnum[this.eadAssumptionGroupEnum.VariableInterestRateProjections], isActive: false},
        {key: this.eadAssumptionGroupEnum[this.eadAssumptionGroupEnum.ExchangeRateProjections], isActive: false},
        {key: this.eadAssumptionGroupEnum[this.eadAssumptionGroupEnum.General], isActive: false}
    ];

    constructor(
        injector: Injector,
        private _eadInputServiceProxy: EadInputAssumptionsServiceProxy
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

    load(assumptions: EadInputAssumptionDto[], affiliateName?: string, framework?: FrameworkEnum, viewOnly = false): void {
        this.eadInputAssumptions = assumptions;
        this.affiliateName = affiliateName;
        this.affiliateFramework = framework;
        this.extractEadAssumptionGroups();
        this.displayForm = true;
        this.viewOnly = viewOnly;
        if (framework === FrameworkEnum.Investments) {
            this.accordionList.find(x => x.key === this.eadAssumptionGroupEnum[this.eadAssumptionGroupEnum.General]).isActive = true;
        }
    }

    extractEadAssumptionGroups(): void {
        //Extract general assumption to groups
        this.eadGroup = this.eadInputAssumptions.filter(x => x.assumptionGroup === EadInputAssumptionGroupEnum.General || x.assumptionGroup === EadInputAssumptionGroupEnum.BehaviouralLife);
        this.ccfGroup = this.eadInputAssumptions.filter(x => x.assumptionGroup === EadInputAssumptionGroupEnum.CreditConversionFactors);
        this.virGroup = this.eadInputAssumptions.filter(x => x.assumptionGroup === EadInputAssumptionGroupEnum.VariableInterestRateProjections);
        this.exchangeRateGroup = this.eadInputAssumptions.filter(x => x.assumptionGroup === EadInputAssumptionGroupEnum.ExchangeRateProjections);
    }

    hide(): void {
        this.displayForm = false;
    }

    editAssumption(eadInput: EadInputAssumptionDto, groupKey: string): void {
        this.editAssumptionModal.configure({
            framework: this.affiliateFramework,
            affiliateName: this.affiliateName,
            dataSource: eadInput,
            serviceProxy: this._eadInputServiceProxy,
            assumptionGroup: this.l(groupKey),
            assumption: AssumptionTypeEnum.EadInputAssumption
        });
        this.editAssumptionModal.show();
    }

    addAssumption(eadGroup: EadInputAssumptionGroupEnum, groupKey: string): void {
        let dto = new CreateOrEditEadInputAssumptionDto();

        let ead = this.eadInputAssumptions.filter(x => x.assumptionGroup === eadGroup);

        dto.framework = this.affiliateFramework;
        dto.eadGroup = eadGroup;
        dto.dataType = ead[0].dataType;
        dto.organizationUnitId = ead[0].organizationUnitId;
        dto.inputName = '';
        dto.value = '';

        this.addAssumptionModal.configure({
            framework: this.affiliateFramework,
            affiliateName: this.affiliateName,
            dataSource: dto,
            serviceProxy: this._eadInputServiceProxy,
            assumptionGroup: this.l(groupKey),
            assumption: AssumptionTypeEnum.EadInputAssumption
        });
        this.addAssumptionModal.show();
    }

}
