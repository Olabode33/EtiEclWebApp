import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { DataTypeEnum, EadInputAssumptionDto, EadInputAssumptionGroupEnum, FrameworkEnum, EadInputAssumptionsServiceProxy, AssumptionTypeEnum } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { EditAssumptionModalComponent } from '../edit-assumption-modal/edit-assumption-modal.component';

@Component({
  selector: 'app-eadInputAssumptions',
  templateUrl: './eadInputAssumptions.component.html',
  styleUrls: ['./eadInputAssumptions.component.css']
})
export class EadInputAssumptionsComponent extends AppComponentBase {

    @ViewChild('editAssumptionModal', {static: true}) editAssumptionModal: EditAssumptionModalComponent;

    displayForm = false;

    eadInputAssumptions: EadInputAssumptionDto[] = new Array();

    ccfGroup: EadInputAssumptionDto[] = new Array();
    virGroup: EadInputAssumptionDto[] = new Array();
    exchangeRateGroup: EadInputAssumptionDto[] = new Array();

    dataTypeEnum = DataTypeEnum;
    eadAssumptionGroupEnum = EadInputAssumptionGroupEnum;

    affiliateName = '';
    affiliateFramework: FrameworkEnum;

    constructor(
        injector: Injector,
        private _eadInputServiceProxy: EadInputAssumptionsServiceProxy
    ) {
        super(injector);
    }

    load(assumptions: EadInputAssumptionDto[], affiliateName?: string, framework?: FrameworkEnum): void {
        this.eadInputAssumptions = assumptions;
        this.affiliateName = affiliateName;
        this.affiliateFramework = framework;
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

}
