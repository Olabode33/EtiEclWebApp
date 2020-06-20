import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetAffiliateMacroEconomicVariableOffsetForViewDto, AffiliateMacroEconomicVariableOffsetDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewAffiliateMacroEconomicVariableOffsetModal',
    templateUrl: './view-affiliateMacroEconomicVariableOffset-modal.component.html'
})
export class ViewAffiliateMacroEconomicVariableOffsetModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetAffiliateMacroEconomicVariableOffsetForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetAffiliateMacroEconomicVariableOffsetForViewDto();
        this.item.affiliateMacroEconomicVariableOffset = new AffiliateMacroEconomicVariableOffsetDto();
    }

    show(item: GetAffiliateMacroEconomicVariableOffsetForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
