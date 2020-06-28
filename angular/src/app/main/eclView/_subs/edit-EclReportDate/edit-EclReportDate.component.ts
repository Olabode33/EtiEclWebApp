import { EclUploadDto } from './../../../../../shared/service-proxies/service-proxies';
import { CreateOrEditAffiliateAssumptionsDto, CreateOrEditEclDto } from '../../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter, ViewEncapsulation } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GeneralStatusEnum, FrameworkEnum, EclSharedServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Observable } from 'rxjs';
import * as _ from 'lodash';
import * as moment from 'moment';
import { finalize } from 'rxjs/operators';

export interface IEditEclReportDateModalOptions {
    eclDto: CreateOrEditEclDto;
    serviceProxy: any;
}

@Component({
    selector: 'app-edit-eclReportDate',
    templateUrl: './edit-EclReportDate.component.html',
    styleUrls: ['./edit-EclReportDate.component.css']
})
export class EditEclReportDateComponent extends AppComponentBase {

    static defaultOptions: IEditEclReportDateModalOptions = {
        eclDto: undefined,
        serviceProxy: undefined
    };

    @ViewChild('editReportDate', { static: true }) modal: ModalDirective;

    @Output() submitReportDate: EventEmitter<any> = new EventEmitter<any>();
    active = false;
    saving = false;
    isShown = false;
    frameworkEnum = FrameworkEnum;

    options: IEditEclReportDateModalOptions = _.merge({});
    eclDto: CreateOrEditEclDto = new CreateOrEditEclDto();
    serviceProxy: any;

    constructor(
        injector: Injector,
        private _eclSharedServiceProxy: EclSharedServiceProxy
    ) {
        super(injector);
        this.eclDto.reportingDate = moment().endOf('month');
    }

    configure(options: IEditEclReportDateModalOptions): void {
        options = _.merge({}, EditEclReportDateComponent.defaultOptions, options);

        this.eclDto = options.eclDto;
        this.serviceProxy = options.serviceProxy;
    }

    show(): void {
        if (!this.options) {
            throw Error('EditEclReportDateComponentError');
        }

        this.active = true;
        this.modal.show();
    }

    shown(): void {
        this.isShown = true;
    }

    close(): void {
        this.active = false;
        this.isShown = false;
        this.modal.hide();
    }

    save(): void {
        this.message.confirm(
            this.l('UseReportDate'),
            (isConfirmed) => {
                if (!this.eclDto.id) {
                    this.submitReportDate.emit(this.eclDto);
                } else {
                    this.serviceProxy.createOrEdit(this.eclDto).subscribe(result => {
                        this.notify.success('SubmittedSuccessfully');
                        this.submitReportDate.emit(null);
                        this.close();
                    });
                }
            });
    }

}
