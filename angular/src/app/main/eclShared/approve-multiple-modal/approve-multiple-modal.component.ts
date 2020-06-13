import { Component, ViewChild, Injector, Output, EventEmitter, ViewEncapsulation } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GeneralStatusEnum } from '@shared/service-proxies/service-proxies';
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
    selector: 'app-approve-multiple-modal',
    templateUrl: './approve-multiple-modal.component.html',
    styleUrls: ['./approve-multiple-modal.component.css'],
    encapsulation: ViewEncapsulation.None,
})
export class ApprovalMultipleModalComponent extends AppComponentBase {

    static defaultOptions: IApprovalModalOptions = {
        serviceProxy: undefined,
        dataSource: undefined
    };

    @ViewChild('approvalModal', { static: true }) modal: ModalDirective;

    @Output() approved: EventEmitter<any> = new EventEmitter<any>();
    active = false;
    saving = false;
    isShown = false;
    generalStatusEnum = GeneralStatusEnum;

    options: IApprovalModalOptions = _.merge({});
    dataSource: any;
    serviceProxy: any;
    title: string;
    totalSelected = 0;
    items: any;

    constructor(
        injector: Injector,
    ) {
        super(injector);
    }

    configure(options: IApprovalModalOptions): void {
        options = _.merge({}, ApprovalMultipleModalComponent.defaultOptions, {title: this.l('ApproveEcl')}, options);
        this.dataSource = options.dataSource;
        this.serviceProxy = options.serviceProxy;
        this.title = options.title;
        console.log(options);
    }

    show(): void {
        if (!this.options) {
            throw Error('Should call ApprovalModalComponent.configure once before ApproveEclComponent.show!');
        }
        console.log(this.dataSource);
        if ( this.hasProp('items') ) {
            console.log(Array.isArray(this.dataSource.items));
            if (Array.isArray(this.dataSource.items)) {
                this.totalSelected = this.dataSource.items.length;
                this.items = this.dataSource.items;
            }
        }
        this.modal.show();
    }

    shown(): void {
        this.isShown = true;
    }

    close(): void {
        this.modal.hide();
    }

    hasProp(prop: string): boolean {
        if (this.dataSource !== undefined) {
            //return Object.prototype.hasOwnProperty.call(this.dataSource, prop);
            return prop in this.dataSource;
        }
        return false;
    }

    approve(): void {
        //TODO: update approval note to come from configuration...
        this.message.confirm(
            this.l('ApprovalNote'),
            (isConfirmed) => {
                if (isConfirmed) {
                    console.log(this.dataSource);
                    this.dataSource.items = this.items;
                    this.serviceProxy.approveMultiple(this.dataSource).subscribe(() => {
                        this.notify.success(this.l('ApprovedSuccessfully'));
                        this.approved.emit(this.dataSource);
                        this.close();
                    });
                }
            });
    }

    reject(): void {
        this.message.confirm(
            this.l('RejectNote'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.dataSource.items = this.items;
                    this.serviceProxy.rejectMultiple(this.dataSource).subscribe(() => {
                        this.notify.success(this.l('RejectedSuccessfully'));
                        this.approved.emit(this.dataSource);
                        this.close();
                    });
                }
            });
    }

}
