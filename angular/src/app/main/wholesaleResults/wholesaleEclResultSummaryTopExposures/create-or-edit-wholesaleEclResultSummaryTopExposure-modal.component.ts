import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WholesaleEclResultSummaryTopExposuresServiceProxy, CreateOrEditWholesaleEclResultSummaryTopExposureDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { WholesaleEclResultSummaryTopExposureWholesaleEclLookupTableModalComponent } from './wholesaleEclResultSummaryTopExposure-wholesaleEcl-lookup-table-modal.component';
import { WholesaleEclResultSummaryTopExposureWholesaleEclDataLoanBookLookupTableModalComponent } from './wholesaleEclResultSummaryTopExposure-wholesaleEclDataLoanBook-lookup-table-modal.component';


@Component({
    selector: 'createOrEditWholesaleEclResultSummaryTopExposureModal',
    templateUrl: './create-or-edit-wholesaleEclResultSummaryTopExposure-modal.component.html'
})
export class CreateOrEditWholesaleEclResultSummaryTopExposureModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('wholesaleEclResultSummaryTopExposureWholesaleEclLookupTableModal', { static: true }) wholesaleEclResultSummaryTopExposureWholesaleEclLookupTableModal: WholesaleEclResultSummaryTopExposureWholesaleEclLookupTableModalComponent;
    @ViewChild('wholesaleEclResultSummaryTopExposureWholesaleEclDataLoanBookLookupTableModal', { static: true }) wholesaleEclResultSummaryTopExposureWholesaleEclDataLoanBookLookupTableModal: WholesaleEclResultSummaryTopExposureWholesaleEclDataLoanBookLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    wholesaleEclResultSummaryTopExposure: CreateOrEditWholesaleEclResultSummaryTopExposureDto = new CreateOrEditWholesaleEclResultSummaryTopExposureDto();

    wholesaleEclTenantId = '';
    wholesaleEclDataLoanBookCustomerName = '';


    constructor(
        injector: Injector,
        private _wholesaleEclResultSummaryTopExposuresServiceProxy: WholesaleEclResultSummaryTopExposuresServiceProxy
    ) {
        super(injector);
    }

    show(wholesaleEclResultSummaryTopExposureId?: string): void {

        if (!wholesaleEclResultSummaryTopExposureId) {
            this.wholesaleEclResultSummaryTopExposure = new CreateOrEditWholesaleEclResultSummaryTopExposureDto();
            this.wholesaleEclResultSummaryTopExposure.id = wholesaleEclResultSummaryTopExposureId;
            this.wholesaleEclTenantId = '';
            this.wholesaleEclDataLoanBookCustomerName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._wholesaleEclResultSummaryTopExposuresServiceProxy.getWholesaleEclResultSummaryTopExposureForEdit(wholesaleEclResultSummaryTopExposureId).subscribe(result => {
                this.wholesaleEclResultSummaryTopExposure = result.wholesaleEclResultSummaryTopExposure;

                this.wholesaleEclTenantId = result.wholesaleEclTenantId;
                this.wholesaleEclDataLoanBookCustomerName = result.wholesaleEclDataLoanBookCustomerName;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._wholesaleEclResultSummaryTopExposuresServiceProxy.createOrEdit(this.wholesaleEclResultSummaryTopExposure)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectWholesaleEclModal() {
        this.wholesaleEclResultSummaryTopExposureWholesaleEclLookupTableModal.id = this.wholesaleEclResultSummaryTopExposure.wholesaleEclId;
        this.wholesaleEclResultSummaryTopExposureWholesaleEclLookupTableModal.displayName = this.wholesaleEclTenantId;
        this.wholesaleEclResultSummaryTopExposureWholesaleEclLookupTableModal.show();
    }
        openSelectWholesaleEclDataLoanBookModal() {
        this.wholesaleEclResultSummaryTopExposureWholesaleEclDataLoanBookLookupTableModal.id = this.wholesaleEclResultSummaryTopExposure.wholesaleEclDataLoanBookId;
        this.wholesaleEclResultSummaryTopExposureWholesaleEclDataLoanBookLookupTableModal.displayName = this.wholesaleEclDataLoanBookCustomerName;
        this.wholesaleEclResultSummaryTopExposureWholesaleEclDataLoanBookLookupTableModal.show();
    }


        setWholesaleEclIdNull() {
        this.wholesaleEclResultSummaryTopExposure.wholesaleEclId = null;
        this.wholesaleEclTenantId = '';
    }
        setWholesaleEclDataLoanBookIdNull() {
        this.wholesaleEclResultSummaryTopExposure.wholesaleEclDataLoanBookId = null;
        this.wholesaleEclDataLoanBookCustomerName = '';
    }


        getNewWholesaleEclId() {
        this.wholesaleEclResultSummaryTopExposure.wholesaleEclId = this.wholesaleEclResultSummaryTopExposureWholesaleEclLookupTableModal.id;
        this.wholesaleEclTenantId = this.wholesaleEclResultSummaryTopExposureWholesaleEclLookupTableModal.displayName;
    }
        getNewWholesaleEclDataLoanBookId() {
        this.wholesaleEclResultSummaryTopExposure.wholesaleEclDataLoanBookId = this.wholesaleEclResultSummaryTopExposureWholesaleEclDataLoanBookLookupTableModal.id;
        this.wholesaleEclDataLoanBookCustomerName = this.wholesaleEclResultSummaryTopExposureWholesaleEclDataLoanBookLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
