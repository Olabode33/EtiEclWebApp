import { CreateOrEditAffiliateAssumptionsDto } from './../../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter, ViewEncapsulation } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GeneralStatusEnum, FrameworkEnum, EclSharedServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Observable } from 'rxjs';
import * as _ from 'lodash';
import * as moment from 'moment';
import { finalize } from 'rxjs/operators';

export interface IEditReportDateModalOptions {
    framework: FrameworkEnum;
    affiliateAssumption: CreateOrEditAffiliateAssumptionsDto;
    affiliateName: string;
}

@Component({
    selector: 'app-edit-portfolioReportDate',
    templateUrl: './edit-portfolioReportDate.component.html',
    styleUrls: ['./edit-portfolioReportDate.component.css']
})
export class EditPortfolioReportDateComponent extends AppComponentBase {

    static defaultOptions: IEditReportDateModalOptions = {
        framework: undefined,
        affiliateAssumption: undefined,
        affiliateName: undefined
    };

    @ViewChild('editReportDate', { static: true }) modal: ModalDirective;

    @Output() submitReportDate: EventEmitter<any> = new EventEmitter<any>();
    active = false;
    saving = false;
    isShown = false;
    frameworkEnum = FrameworkEnum;

    options: IEditReportDateModalOptions = _.merge({});
    affiliateAssumption: CreateOrEditAffiliateAssumptionsDto = new CreateOrEditAffiliateAssumptionsDto();
    affiliateFramework: FrameworkEnum;
    affiliateName = '';
    title: string;

    constructor(
        injector: Injector,
        private _eclSharedServiceProxy: EclSharedServiceProxy
    ) {
        super(injector);
        this.affiliateAssumption.lastObeReportingDate = moment().endOf('month');
        this.affiliateAssumption.lastRetailReportingDate = moment().endOf('month');
        this.affiliateAssumption.lastWholesaleReportingDate = moment().endOf('month');
        this.affiliateAssumption.lastSecuritiesReportingDate = moment().endOf('month');
    }

    configure(options: IEditReportDateModalOptions): void {
        options = _.merge({}, EditPortfolioReportDateComponent.defaultOptions, options);

        this.affiliateFramework = options.framework;
        this.affiliateName = options.affiliateName;
        this.affiliateAssumption = options.affiliateAssumption;
        console.log(options);
    }

    show(): void {
        if (!this.options) {
            throw Error('EditPortfolioReportDateComponentError');
        }

        this.modal.show();
    }

    shown(): void {
        this.isShown = true;
    }

    close(): void {
        this.isShown = false;
        this.modal.hide();
    }

    save(): void {
        this.message.confirm(
            this.l('SaveReportDateNote'),
            (isConfirmed) => {
                this.affiliateAssumption.lastAssumptionUpdate = moment();
                this._eclSharedServiceProxy.updateAffiliateAssumption(this.affiliateAssumption).subscribe(result => {
                    this.notify.success('SubmittedSuccessfully');
                    this.submitReportDate.emit(null);
                    this.close();
                });
            });
    }

}
