import { Component, ViewChild, Injector, Output, EventEmitter, ViewEncapsulation } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { WholesaleEclsServiceProxy, WholesaleEclUserLookupTableDto, GeneralStatusEnum } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Observable } from 'rxjs';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

export interface IApproveEclModalOptions {
    title?: string;
    serviceProxy: any;
    dataSource: any;
}

@Component({
    selector: 'app-approve-ecl-modal',
    templateUrl: './approve-ecl-modal.component.html',
    styleUrls: ['./approve-ecl-modal.component.css'],
    encapsulation: ViewEncapsulation.None,
})
export class ApproveEclModalComponent extends AppComponentBase {

    static defaultOptions: IApproveEclModalOptions = {
        serviceProxy: undefined,
        dataSource: undefined
    };

    @ViewChild('approveEclModal', { static: true }) modal: ModalDirective;

    @Output() eclApproved: EventEmitter<any> = new EventEmitter<any>();
    active = false;
    saving = false;
    isShown = false;
    generalStatusEnum = GeneralStatusEnum;

    options: IApproveEclModalOptions = _.merge({});
    dataSource: any;
    serviceProxy: any;
    title: string;

    constructor(
        injector: Injector,
        private _wholesaleEclsServiceProxy: WholesaleEclsServiceProxy
    ) {
        super(injector);
    }

    configure(options: IApproveEclModalOptions): void {
        options = _.merge({}, ApproveEclModalComponent.defaultOptions, {title: this.l('ApproveEcl')}, options);
        this.dataSource = options.dataSource;
        this.serviceProxy = options.serviceProxy;
        this.title = options.title;
        console.log(options);
    }

    show(): void {
        if (!this.options) {
            throw Error('Should call ApproveEclModalComponent.configure once before ApproveEclComponent.show!');
        }

        this.modal.show();
    }

    shown(): void {
        this.isShown = true;
    }

    close(): void {
        this.modal.hide();
    }

    approveEcl(): void {
        this.message.confirm(
            this.l('ApproveEclNote'),
            (isConfirmed) => {
                this.dataSource.status = GeneralStatusEnum.Approved;
                this.serviceProxy.approveRejectEcl(this.dataSource).subscribe(() => {
                    this.eclApproved.emit(this.dataSource);
                    this.close();
                });
            });
    }

    rejectEcl(): void {
        this.dataSource.status = GeneralStatusEnum.Rejected;
        this.serviceProxy.approveRejectEcl(this.dataSource).subscribe(() => {
            this.eclApproved.emit(this.dataSource);
            this.close();
        });
    }

}
