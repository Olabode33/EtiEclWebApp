import { Component, ViewChild, Injector, Output, EventEmitter, ViewEncapsulation, Input } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GeneralStatusEnum, CalibrationStatusEnum, HoldCoRegistersServiceProxy, CreateOrEditHoldCoRegisterApprovalDto, CreateOrEditHoldCoRegisterDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Observable } from 'rxjs';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

export interface IApprovalModalOptions {
    title?: string;
    serviceProxy: any;
    dataSource: any;
}
@Component({
  selector: 'app-approve-ivmodel-modal',
  templateUrl: './approve-ivmodel-modal.component.html',
  styleUrls: ['./approve-ivmodel-modal.component.css']
})
export class ApproveIvmodelModalComponent  extends AppComponentBase {

    static defaultOptions: IApprovalModalOptions = {
        serviceProxy: undefined,
        dataSource: undefined
    };

    @ViewChild('approvalModal', { static: true }) modal: ModalDirective;

    @Input() holdCoRegister: CreateOrEditHoldCoRegisterDto;
    @Output() approved: EventEmitter<any> = new EventEmitter<any>();
    active = false;
    saving = false;
    isShown = false;
    generalStatusEnum = GeneralStatusEnum;
    rejecting = false;
    approving = false;

    options: IApprovalModalOptions = _.merge({});
    dataSource = new CreateOrEditHoldCoRegisterApprovalDto();
    serviceProxy: any;
    title: string;

    constructor(
        injector: Injector,
        private _holdCoRegistersServiceProxy: HoldCoRegistersServiceProxy
    ) {
        super(injector);
    }

    show(): void {
        this.modal.show();
    }

    shown(): void {
        this.isShown = true;
    }

    close(): void {
        this.modal.hide();
    }

    approve(): void {
        this.message.confirm(this.l("ApprovalNote"), (isConfirmed) => {
            if (isConfirmed) {
                this.approving = true;
                this.dataSource.registrationId = this.holdCoRegister.id;
                    this.dataSource.status = CalibrationStatusEnum.Approved;
                    this._holdCoRegistersServiceProxy
                        .approveRejectModel(this.dataSource)
                        .subscribe(() => {
                            this.notify.success(this.l("ApprovedSuccessfully"));
                            this.approved.emit(this.dataSource);
                            this.close();
                            this.approving = false;
                        });
                }
        });
    }

    reject(): void {
        this.message.confirm(this.l("RejectNote"), (isConfirmed) => {
            if (isConfirmed) {
                this.approving = true;
                this.dataSource.registrationId = this.holdCoRegister.id;
                    this.dataSource.status = CalibrationStatusEnum.Rejected;
                    this._holdCoRegistersServiceProxy
                        .approveRejectModel(this.dataSource)
                        .subscribe(() => {
                            this.notify.success(this.l("RejectedSuccessfully"));
                            this.approved.emit(this.dataSource);
                            this.close();
                            this.approving = false;
                        });
                }
        });
    }

}
