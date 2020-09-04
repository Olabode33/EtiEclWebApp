import { Component, ViewChild, Injector, Output, EventEmitter, ViewEncapsulation, Input } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GeneralStatusEnum, CalibrationStatusEnum, HoldCoRegistersServiceProxy, CreateOrEditHoldCoRegisterApprovalDto, CreateOrEditHoldCoRegisterDto, CreateOrEditReceivablesRegisterDto, ReceivablesRegistersServiceProxy } from '@shared/service-proxies/service-proxies';
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
export class ApproveIvmodelModalComponent extends AppComponentBase {

    static defaultOptions: IApprovalModalOptions = {
        serviceProxy: undefined,
        dataSource: undefined
    };

    @ViewChild('approvalModal', { static: true }) modal: ModalDirective;

    @Input() holdCoRegister: CreateOrEditHoldCoRegisterDto;
    @Input() receivablesRegister: CreateOrEditReceivablesRegisterDto;

    @Output() approved: EventEmitter<any> = new EventEmitter<any>();
    active = false;
    saving = false;
    isShown = false;
    generalStatusEnum = GeneralStatusEnum;
    rejecting = false;
    approving = false;

    options: IApprovalModalOptions = _.merge({});
    dataSource: any = {};
    serviceProxy: any;
    title: string;

    constructor(
        injector: Injector,
        private _holdCoRegistersServiceProxy: HoldCoRegistersServiceProxy,
        private _receivablesRegistersServiceProxy: ReceivablesRegistersServiceProxy

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
        this.message.confirm('', (isConfirmed) => {
            if (isConfirmed) {
                this.approving = true;
                this.dataSource.status = CalibrationStatusEnum.Approved;

                if (this.holdCoRegister) {
                    this.dataSource.registrationId = this.holdCoRegister.id;
                    this._holdCoRegistersServiceProxy
                    .approveRejectModel(this.dataSource)
                    .subscribe(() => {
                        this.notify.success(this.l("ApprovedSuccessfully"));
                        this.approved.emit(this.dataSource);
                        this.close();
                        this.approving = false;
                    });

                }
                else {
                    this.dataSource.registerId = this.receivablesRegister.id;
                    this._receivablesRegistersServiceProxy
                    .approveRejectModel(this.dataSource)
                    .subscribe(() => {
                        this.notify.success(this.l("ApprovedSuccessfully"));
                        this.approved.emit(this.dataSource);
                        this.close();
                        this.approving = false;
                    });
                }
            }
        });
    }

    reject(): void {
        this.message.confirm('', (isConfirmed) => {
            if (isConfirmed) {
                this.approving = true;
                this.dataSource.status = CalibrationStatusEnum.Rejected;
                if (this.holdCoRegister) {
                    this.dataSource.registrationId = this.holdCoRegister.id;
                    this._holdCoRegistersServiceProxy
                    .approveRejectModel(this.dataSource)
                    .subscribe(() => {
                        this.notify.success(this.l("RejectedSuccessfully"));
                        this.approved.emit(this.dataSource);
                        this.close();
                        this.approving = false;
                    });
                }
                else {
                    this.dataSource.registerId = this.receivablesRegister.id;
                    this._receivablesRegistersServiceProxy
                    .approveRejectModel(this.dataSource)
                    .subscribe(() => {
                        this.notify.success(this.l("RejectedSuccessfully"));
                        this.approved.emit(this.dataSource);
                        this.close();
                        this.approving = false;
                    });
                }

            }
        });
    }

}
