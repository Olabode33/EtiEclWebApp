import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WholesaleEclDataLoanBooksServiceProxy, CreateOrEditWholesaleEclDataLoanBookDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { WholesaleEclDataLoanBookWholesaleEclUploadLookupTableModalComponent } from './wholesaleEclDataLoanBook-wholesaleEclUpload-lookup-table-modal.component';


@Component({
    selector: 'createOrEditWholesaleEclDataLoanBookModal',
    templateUrl: './create-or-edit-wholesaleEclDataLoanBook-modal.component.html'
})
export class CreateOrEditWholesaleEclDataLoanBookModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('wholesaleEclDataLoanBookWholesaleEclUploadLookupTableModal', { static: true }) wholesaleEclDataLoanBookWholesaleEclUploadLookupTableModal: WholesaleEclDataLoanBookWholesaleEclUploadLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    wholesaleEclDataLoanBook: CreateOrEditWholesaleEclDataLoanBookDto = new CreateOrEditWholesaleEclDataLoanBookDto();

            snapshotDate: Date;
            impairedDate: Date;
            defaultDate: Date;
            contractStartDate: Date;
            contractEndDate: Date;
            restructureStartDate: Date;
            restructureEndDate: Date;
    wholesaleEclUploadUploadComment = '';


    constructor(
        injector: Injector,
        private _wholesaleEclDataLoanBooksServiceProxy: WholesaleEclDataLoanBooksServiceProxy
    ) {
        super(injector);
    }

    show(wholesaleEclDataLoanBookId?: string): void {
this.snapshotDate = null;
this.impairedDate = null;
this.defaultDate = null;
this.contractStartDate = null;
this.contractEndDate = null;
this.restructureStartDate = null;
this.restructureEndDate = null;

        if (!wholesaleEclDataLoanBookId) {
            this.wholesaleEclDataLoanBook = new CreateOrEditWholesaleEclDataLoanBookDto();
            this.wholesaleEclDataLoanBook.id = wholesaleEclDataLoanBookId;
            this.wholesaleEclUploadUploadComment = '';

            this.active = true;
            this.modal.show();
        } else {
            this._wholesaleEclDataLoanBooksServiceProxy.getWholesaleEclDataLoanBookForEdit(wholesaleEclDataLoanBookId).subscribe(result => {
                this.wholesaleEclDataLoanBook = result.wholesaleEclDataLoanBook;

                if (this.wholesaleEclDataLoanBook.snapshotDate) {
					this.snapshotDate = this.wholesaleEclDataLoanBook.snapshotDate.toDate();
                }
                if (this.wholesaleEclDataLoanBook.impairedDate) {
					this.impairedDate = this.wholesaleEclDataLoanBook.impairedDate.toDate();
                }
                if (this.wholesaleEclDataLoanBook.defaultDate) {
					this.defaultDate = this.wholesaleEclDataLoanBook.defaultDate.toDate();
                }
                if (this.wholesaleEclDataLoanBook.contractStartDate) {
					this.contractStartDate = this.wholesaleEclDataLoanBook.contractStartDate.toDate();
                }
                if (this.wholesaleEclDataLoanBook.contractEndDate) {
					this.contractEndDate = this.wholesaleEclDataLoanBook.contractEndDate.toDate();
                }
                if (this.wholesaleEclDataLoanBook.restructureStartDate) {
					this.restructureStartDate = this.wholesaleEclDataLoanBook.restructureStartDate.toDate();
                }
                if (this.wholesaleEclDataLoanBook.restructureEndDate) {
					this.restructureEndDate = this.wholesaleEclDataLoanBook.restructureEndDate.toDate();
                }
                this.wholesaleEclUploadUploadComment = result.wholesaleEclUploadUploadComment;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
        if (this.snapshotDate) {
            if (!this.wholesaleEclDataLoanBook.snapshotDate) {
                this.wholesaleEclDataLoanBook.snapshotDate = moment(this.snapshotDate).startOf('day');
            }
            else {
                this.wholesaleEclDataLoanBook.snapshotDate = moment(this.snapshotDate);
            }
        }
        else {
            this.wholesaleEclDataLoanBook.snapshotDate = null;
        }
        if (this.impairedDate) {
            if (!this.wholesaleEclDataLoanBook.impairedDate) {
                this.wholesaleEclDataLoanBook.impairedDate = moment(this.impairedDate).startOf('day');
            }
            else {
                this.wholesaleEclDataLoanBook.impairedDate = moment(this.impairedDate);
            }
        }
        else {
            this.wholesaleEclDataLoanBook.impairedDate = null;
        }
        if (this.defaultDate) {
            if (!this.wholesaleEclDataLoanBook.defaultDate) {
                this.wholesaleEclDataLoanBook.defaultDate = moment(this.defaultDate).startOf('day');
            }
            else {
                this.wholesaleEclDataLoanBook.defaultDate = moment(this.defaultDate);
            }
        }
        else {
            this.wholesaleEclDataLoanBook.defaultDate = null;
        }
        if (this.contractStartDate) {
            if (!this.wholesaleEclDataLoanBook.contractStartDate) {
                this.wholesaleEclDataLoanBook.contractStartDate = moment(this.contractStartDate).startOf('day');
            }
            else {
                this.wholesaleEclDataLoanBook.contractStartDate = moment(this.contractStartDate);
            }
        }
        else {
            this.wholesaleEclDataLoanBook.contractStartDate = null;
        }
        if (this.contractEndDate) {
            if (!this.wholesaleEclDataLoanBook.contractEndDate) {
                this.wholesaleEclDataLoanBook.contractEndDate = moment(this.contractEndDate).startOf('day');
            }
            else {
                this.wholesaleEclDataLoanBook.contractEndDate = moment(this.contractEndDate);
            }
        }
        else {
            this.wholesaleEclDataLoanBook.contractEndDate = null;
        }
        if (this.restructureStartDate) {
            if (!this.wholesaleEclDataLoanBook.restructureStartDate) {
                this.wholesaleEclDataLoanBook.restructureStartDate = moment(this.restructureStartDate).startOf('day');
            }
            else {
                this.wholesaleEclDataLoanBook.restructureStartDate = moment(this.restructureStartDate);
            }
        }
        else {
            this.wholesaleEclDataLoanBook.restructureStartDate = null;
        }
        if (this.restructureEndDate) {
            if (!this.wholesaleEclDataLoanBook.restructureEndDate) {
                this.wholesaleEclDataLoanBook.restructureEndDate = moment(this.restructureEndDate).startOf('day');
            }
            else {
                this.wholesaleEclDataLoanBook.restructureEndDate = moment(this.restructureEndDate);
            }
        }
        else {
            this.wholesaleEclDataLoanBook.restructureEndDate = null;
        }
            this._wholesaleEclDataLoanBooksServiceProxy.createOrEdit(this.wholesaleEclDataLoanBook)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectWholesaleEclUploadModal() {
        this.wholesaleEclDataLoanBookWholesaleEclUploadLookupTableModal.id = this.wholesaleEclDataLoanBook.wholesaleEclUploadId;
        this.wholesaleEclDataLoanBookWholesaleEclUploadLookupTableModal.displayName = this.wholesaleEclUploadUploadComment;
        this.wholesaleEclDataLoanBookWholesaleEclUploadLookupTableModal.show();
    }


        setWholesaleEclUploadIdNull() {
        this.wholesaleEclDataLoanBook.wholesaleEclUploadId = null;
        this.wholesaleEclUploadUploadComment = '';
    }


        getNewWholesaleEclUploadId() {
        this.wholesaleEclDataLoanBook.wholesaleEclUploadId = this.wholesaleEclDataLoanBookWholesaleEclUploadLookupTableModal.id;
        this.wholesaleEclUploadUploadComment = this.wholesaleEclDataLoanBookWholesaleEclUploadLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
