import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetAssumptionApprovalForViewDto, AssumptionApprovalDto , FrameworkEnum, AssumptionTypeEnum, GeneralStatusEnum, AssumptionApprovalsServiceProxy} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ApprovalModalComponent } from '@app/main/eclShared/approve-ecl-modal/approve-ecl-modal.component';

@Component({
    selector: 'viewAssumptionApprovalModal',
    templateUrl: './view-assumptionApproval-modal.component.html'
})
export class ViewAssumptionApprovalModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('approvalModal', {static: true}) approvalModel: ApprovalModalComponent;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetAssumptionApprovalForViewDto;
    frameworkEnum = FrameworkEnum;
    assumptionTypeEnum = AssumptionTypeEnum;
    generalStatusEnum = GeneralStatusEnum;


    constructor(
        injector: Injector,
        private _assumptionApprovalsServiceProxy: AssumptionApprovalsServiceProxy,
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
        this.saving = false;
        this.modalSave.emit(null);
        this.modal.hide();
    }

    reviewAssumption(): void {
        this.saving = true;
        this.approvalModel.configure({
            title: this.l('ApproveAssumption'),
            serviceProxy: this._assumptionApprovalsServiceProxy,
            dataSource: this.item.assumptionApproval
        });
        this.approvalModel.show();
    }
}
