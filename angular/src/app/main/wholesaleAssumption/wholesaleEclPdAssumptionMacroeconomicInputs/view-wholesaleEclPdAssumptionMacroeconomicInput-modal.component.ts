import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetWholesaleEclPdAssumptionMacroeconomicInputForViewDto, WholesaleEclPdAssumptionMacroeconomicInputDto , PdInputAssumptionMacroEconomicInputGroupEnum, GeneralStatusEnum} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewWholesaleEclPdAssumptionMacroeconomicInputModal',
    templateUrl: './view-wholesaleEclPdAssumptionMacroeconomicInput-modal.component.html'
})
export class ViewWholesaleEclPdAssumptionMacroeconomicInputModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetWholesaleEclPdAssumptionMacroeconomicInputForViewDto;
    pdInputAssumptionMacroEconomicInputGroupEnum = PdInputAssumptionMacroEconomicInputGroupEnum;
    generalStatusEnum = GeneralStatusEnum;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetWholesaleEclPdAssumptionMacroeconomicInputForViewDto();
        this.item.wholesaleEclPdAssumptionMacroeconomicInput = new WholesaleEclPdAssumptionMacroeconomicInputDto();
    }

    show(item: GetWholesaleEclPdAssumptionMacroeconomicInputForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
