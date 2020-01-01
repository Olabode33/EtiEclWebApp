import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetAssumptionApprovalForViewDto, AssumptionApprovalDto , FrameworkEnum, AssumptionTypeEnum, GeneralStatusEnum} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewAssumptionApprovalModal',
    templateUrl: './view-assumptionApproval-modal.component.html'
})
export class ViewAssumptionApprovalModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetAssumptionApprovalForViewDto;
    frameworkEnum = FrameworkEnum;
    assumptionTypeEnum = AssumptionTypeEnum;
    generalStatusEnum = GeneralStatusEnum;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetAssumptionApprovalForViewDto();
        this.item.assumptionApproval = new AssumptionApprovalDto();
    }

    show(item: GetAssumptionApprovalForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
