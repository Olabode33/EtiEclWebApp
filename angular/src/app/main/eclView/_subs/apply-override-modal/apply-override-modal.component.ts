import { GetRecordForOverrideInputDto, ReviewEclOverrideInputDto, InvestmentEclOverrideApprovalsServiceProxy, EclAuditInfoDto, EclApprovalAuditInfoDto } from './../../../../../shared/service-proxies/service-proxies';
import { Component, OnInit, ViewEncapsulation, ViewChild, Output, EventEmitter, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { GeneralStatusEnum, NameValueDto, InvestmentEclOverridesServiceProxy } from '@shared/service-proxies/service-proxies';
import * as _ from 'lodash';
import { ApprovalModalComponent } from '@app/main/eclShared/approve-ecl-modal/approve-ecl-modal.component';

export interface IApplyOverrideModalOptions {
    title?: string;
    serviceProxy: any;
    selectedEclId: string;
}

@Component({
    selector: 'app-apply-override-modal',
    templateUrl: './apply-override-modal.component.html',
    styleUrls: ['./apply-override-modal.component.css'],
    encapsulation: ViewEncapsulation.None,
})
export class ApplyOverrideModalComponent extends AppComponentBase {

    static defaultOptions: IApplyOverrideModalOptions = {
        serviceProxy: undefined,
        selectedEclId: undefined
    };

    @ViewChild('applyOverrideModal', { static: true }) modal: ModalDirective;
    @ViewChild('approvalModal', { static: true }) approvalModal: ApprovalModalComponent;
    @Output() applied: EventEmitter<any> = new EventEmitter<any>();

    _eclId = '';

    active = false;
    saving = false;
    isShown = false;
    reviewMode = false;
    viewOnlyMode = false;
    generalStatusEnum = GeneralStatusEnum;

    options: IApplyOverrideModalOptions = _.merge({});
    dataSource: any;
    serviceProxy: any;
    title: string;
    eclOverride: any;

    filteredAccounts: NameValueDto[] = new Array();
    selectedAccount: any;

    auditInfo: EclAuditInfoDto;
    approvalsAuditInfo: EclApprovalAuditInfoDto[];
    genStatusEnum = GeneralStatusEnum;

    constructor(
        injector: Injector,
        private _invSecOverrideServiceProxy: InvestmentEclOverridesServiceProxy,
        private _invSecOverrideApprovalServiceProxy: InvestmentEclOverrideApprovalsServiceProxy
    ) {
        super(injector);
    }

    configure(options: IApplyOverrideModalOptions): void {
        options = _.merge({}, ApplyOverrideModalComponent.defaultOptions, {title: this.l('ApplyOverride')}, options);
        this.serviceProxy = options.serviceProxy;
        this._eclId = options.selectedEclId;
        this.title = options.title;
        console.log(options);
    }

    show(): void {
        if (!this.options) {
            throw Error('Should call ApplyOverrideModalComponent.configure once before ApproveEclComponent.show!');
        }

        this.title = this.l('ApplyOverride');
        this.reviewMode = false;
        this.viewOnlyMode = false;
        this.dataSource = null;
        this.eclOverride = null;
        this.modal.show();
    }

    showInReviewMode(sicrId: string): void {
        if (!this.options) {
            throw Error('Should call ApplyOverrideModalComponent.configure once before ApproveEclComponent.show!');
        }

        this.title = this.l('ReviewOverride');
        this.reviewMode = true;
        this.viewOnlyMode = false;
        this.getRecordDetails(sicrId);
        this.modal.show();
    }

    showInViewOnlyMode(sicrId: string): void {
        if (!this.options) {
            throw Error('Should call ApplyOverrideModalComponent.configure once before ApproveEclComponent.show!');
        }

        this.title = this.l('ViewOverride');
        this.reviewMode = false;
        this.viewOnlyMode = true;
        this.getRecordDetails(sicrId);
        this.modal.show();
    }

    shown(): void {
        this.isShown = true;
    }

    close(): void {
        this.applied.emit(null);
        this.modal.hide();
    }

    searchBooks(event): void {
        if (this._eclId === '') {
            throw Error('!Component not configured correctly!');
        }

        let input = new GetRecordForOverrideInputDto();
        input.eclId = this._eclId;
        input.searchTerm = event.query;
        this._invSecOverrideServiceProxy.searchResult(input).subscribe(result => {
            this.filteredAccounts = result;
        });
    }

    getRecordDetails(selectedAccountId?: string): void {
        //console.log(this.selectedAccount);
        if (!selectedAccountId) {
            this.serviceProxy.getEclRecordDetails(this.selectedAccount.value).subscribe(result => {
                this.dataSource = result;
                this.eclOverride = result.eclOverrides;
                if (this.eclOverrideHasProp('id') && this.eclOverride.id) {
                    this.getOverrideAuditTrail();
                }
            });
        } else {
            this.serviceProxy.getEclRecordDetails(selectedAccountId).subscribe(result => {
                this.dataSource = result;
                this.eclOverride = result.eclOverrides;
                if (this.eclOverrideHasProp('id')) {
                    this.getOverrideAuditTrail();
                }
            });
        }
    }

    configureApprovalModal(title: string): void {
        let approvalDto = new ReviewEclOverrideInputDto();
        approvalDto.overrideRecordId = this.eclOverride.id;
        approvalDto.reviewComment = '';
        approvalDto.status = GeneralStatusEnum.Submitted;

        this.approvalModal.configure({
            title: title,
            serviceProxy: this.serviceProxy,
            dataSource: approvalDto
        });
    }

    hasProp(prop: string): boolean {
        if (this.dataSource !== undefined) {
            //return Object.prototype.hasOwnProperty.call(this.dataSource, prop);
            return prop in this.dataSource;
        }
        return false;
    }

    eclOverrideHasProp(prop: string): boolean {
        if (this.eclOverride !== undefined) {
            return prop in this.eclOverride;
        }
        return false;
    }

    objectHasProp(prop: string, obj: any): boolean {
        if (obj !== undefined) {
            //return Object.prototype.hasOwnProperty.call(this.dataSource, prop);
            return prop in obj;
        }
        return false;
    }

    apply(): void {
        console.log(this.eclOverride);
        this.message.confirm(
            this.l('SubmitForApproval'),
            (isConfirmed) => {
                if (isConfirmed) {
                    if (this.hasProp('status')) {
                        this.dataSource.status = GeneralStatusEnum.Submitted;
                    }
                    this.serviceProxy.createOrEdit(this.eclOverride).subscribe(() => {
                        this.notify.success(this.l('OverrideSubmittedSuccessfully'));
                        this.applied.emit(this.dataSource);
                        this.close();
                    });
                }
            });
    }

    reviewOverride(): void {
        this.configureApprovalModal(this.l('ApproveOverrideRecord'));
        this.approvalModal.show();
    }

    getOverrideAuditTrail(): void {
        this._invSecOverrideApprovalServiceProxy.getEclAudit(this.eclOverride.id).subscribe(result => {
            this.auditInfo = result;
            //console.log(this.auditInfo);
            this.approvalsAuditInfo = result.approvals;
        });
    }

}
