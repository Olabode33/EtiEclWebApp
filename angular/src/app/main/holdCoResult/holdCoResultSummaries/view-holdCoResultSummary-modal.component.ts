import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetHoldCoResultSummaryForViewDto, HoldCoResultSummaryDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewHoldCoResultSummaryModal',
    templateUrl: './view-holdCoResultSummary-modal.component.html'
})
export class ViewHoldCoResultSummaryModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetHoldCoResultSummaryForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetHoldCoResultSummaryForViewDto();
        this.item.holdCoResultSummary = new HoldCoResultSummaryDto();
    }

    show(item: GetHoldCoResultSummaryForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
