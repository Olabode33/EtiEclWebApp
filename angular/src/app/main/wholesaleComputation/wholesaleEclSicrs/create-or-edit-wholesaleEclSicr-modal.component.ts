import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WholesaleEclSicrsServiceProxy, CreateOrEditWholesaleEclSicrDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { WholesaleEclSicrWholesaleEclDataLoanBookLookupTableModalComponent } from './wholesaleEclSicr-wholesaleEclDataLoanBook-lookup-table-modal.component';


@Component({
    selector: 'createOrEditWholesaleEclSicrModal',
    templateUrl: './create-or-edit-wholesaleEclSicr-modal.component.html'
})
export class CreateOrEditWholesaleEclSicrModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('wholesaleEclSicrWholesaleEclDataLoanBookLookupTableModal', { static: true }) wholesaleEclSicrWholesaleEclDataLoanBookLookupTableModal: WholesaleEclSicrWholesaleEclDataLoanBookLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    wholesaleEclSicr: CreateOrEditWholesaleEclSicrDto = new CreateOrEditWholesaleEclSicrDto();

    wholesaleEclDataLoanBookContractNo = '';


    constructor(
        injector: Injector,
        private _wholesaleEclSicrsServiceProxy: WholesaleEclSicrsServiceProxy
    ) {
        super(injector);
    }

    show(wholesaleEclSicrId?: string): void {

        if (!wholesaleEclSicrId) {
            this.wholesaleEclSicr = new CreateOrEditWholesaleEclSicrDto();
            this.wholesaleEclSicr.id = wholesaleEclSicrId;
            this.wholesaleEclDataLoanBookContractNo = '';

            this.active = true;
            this.modal.show();
        } else {
            this._wholesaleEclSicrsServiceProxy.getWholesaleEclSicrForEdit(wholesaleEclSicrId).subscribe(result => {
                this.wholesaleEclSicr = result.wholesaleEclSicr;

                this.wholesaleEclDataLoanBookContractNo = result.wholesaleEclDataLoanBookContractNo;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._wholesaleEclSicrsServiceProxy.createOrEdit(this.wholesaleEclSicr)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectWholesaleEclDataLoanBookModal() {
        this.wholesaleEclSicrWholesaleEclDataLoanBookLookupTableModal.id = this.wholesaleEclSicr.wholesaleEclDataLoanBookId;
        this.wholesaleEclSicrWholesaleEclDataLoanBookLookupTableModal.displayName = this.wholesaleEclDataLoanBookContractNo;
        this.wholesaleEclSicrWholesaleEclDataLoanBookLookupTableModal.show();
    }


        setWholesaleEclDataLoanBookIdNull() {
        this.wholesaleEclSicr.wholesaleEclDataLoanBookId = null;
        this.wholesaleEclDataLoanBookContractNo = '';
    }


        getNewWholesaleEclDataLoanBookId() {
        this.wholesaleEclSicr.wholesaleEclDataLoanBookId = this.wholesaleEclSicrWholesaleEclDataLoanBookLookupTableModal.id;
        this.wholesaleEclDataLoanBookContractNo = this.wholesaleEclSicrWholesaleEclDataLoanBookLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
