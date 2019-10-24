import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WholesaleEclDataPaymentSchedulesServiceProxy, CreateOrEditWholesaleEclDataPaymentScheduleDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { WholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableModalComponent } from './wholesaleEclDataPaymentSchedule-wholesaleEclUpload-lookup-table-modal.component';


@Component({
    selector: 'createOrEditWholesaleEclDataPaymentScheduleModal',
    templateUrl: './create-or-edit-wholesaleEclDataPaymentSchedule-modal.component.html'
})
export class CreateOrEditWholesaleEclDataPaymentScheduleModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('wholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableModal', { static: true }) wholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableModal: WholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    wholesaleEclDataPaymentSchedule: CreateOrEditWholesaleEclDataPaymentScheduleDto = new CreateOrEditWholesaleEclDataPaymentScheduleDto();

            startDate: Date;
    wholesaleEclUploadUploadComment = '';


    constructor(
        injector: Injector,
        private _wholesaleEclDataPaymentSchedulesServiceProxy: WholesaleEclDataPaymentSchedulesServiceProxy
    ) {
        super(injector);
    }

    show(wholesaleEclDataPaymentScheduleId?: string): void {
this.startDate = null;

        if (!wholesaleEclDataPaymentScheduleId) {
            this.wholesaleEclDataPaymentSchedule = new CreateOrEditWholesaleEclDataPaymentScheduleDto();
            this.wholesaleEclDataPaymentSchedule.id = wholesaleEclDataPaymentScheduleId;
            this.wholesaleEclUploadUploadComment = '';

            this.active = true;
            this.modal.show();
        } else {
            this._wholesaleEclDataPaymentSchedulesServiceProxy.getWholesaleEclDataPaymentScheduleForEdit(wholesaleEclDataPaymentScheduleId).subscribe(result => {
                this.wholesaleEclDataPaymentSchedule = result.wholesaleEclDataPaymentSchedule;

                if (this.wholesaleEclDataPaymentSchedule.startDate) {
					this.startDate = this.wholesaleEclDataPaymentSchedule.startDate.toDate();
                }
                this.wholesaleEclUploadUploadComment = result.wholesaleEclUploadUploadComment;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
        if (this.startDate) {
            if (!this.wholesaleEclDataPaymentSchedule.startDate) {
                this.wholesaleEclDataPaymentSchedule.startDate = moment(this.startDate).startOf('day');
            }
            else {
                this.wholesaleEclDataPaymentSchedule.startDate = moment(this.startDate);
            }
        }
        else {
            this.wholesaleEclDataPaymentSchedule.startDate = null;
        }
            this._wholesaleEclDataPaymentSchedulesServiceProxy.createOrEdit(this.wholesaleEclDataPaymentSchedule)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectWholesaleEclUploadModal() {
        this.wholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableModal.id = this.wholesaleEclDataPaymentSchedule.wholesaleEclUploadId;
        this.wholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableModal.displayName = this.wholesaleEclUploadUploadComment;
        this.wholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableModal.show();
    }


        setWholesaleEclUploadIdNull() {
        this.wholesaleEclDataPaymentSchedule.wholesaleEclUploadId = null;
        this.wholesaleEclUploadUploadComment = '';
    }


        getNewWholesaleEclUploadId() {
        this.wholesaleEclDataPaymentSchedule.wholesaleEclUploadId = this.wholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableModal.id;
        this.wholesaleEclUploadUploadComment = this.wholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
