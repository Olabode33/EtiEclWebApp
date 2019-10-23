import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WholesaleEclsServiceProxy, CreateOrEditWholesaleEclDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { WholesaleEclUserLookupTableModalComponent } from './wholesaleEcl-user-lookup-table-modal.component';


@Component({
    selector: 'createOrEditWholesaleEclModal',
    templateUrl: './create-or-edit-wholesaleEcl-modal.component.html'
})
export class CreateOrEditWholesaleEclModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('wholesaleEclUserLookupTableModal', { static: true }) wholesaleEclUserLookupTableModal: WholesaleEclUserLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    wholesaleEcl: CreateOrEditWholesaleEclDto = new CreateOrEditWholesaleEclDto();

            closedDate: Date;
    userName = '';


    constructor(
        injector: Injector,
        private _wholesaleEclsServiceProxy: WholesaleEclsServiceProxy
    ) {
        super(injector);
    }

    show(wholesaleEclId?: string): void {
this.closedDate = null;

        if (!wholesaleEclId) {
            this.wholesaleEcl = new CreateOrEditWholesaleEclDto();
            this.wholesaleEcl.id = wholesaleEclId;
            this.wholesaleEcl.reportingDate = moment().startOf('day');
            this.userName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._wholesaleEclsServiceProxy.getWholesaleEclForEdit(wholesaleEclId).subscribe(result => {
                this.wholesaleEcl = result.wholesaleEcl;

                if (this.wholesaleEcl.closedDate) {
					this.closedDate = this.wholesaleEcl.closedDate.toDate();
                }
                this.userName = result.userName;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
        if (this.closedDate) {
            if (!this.wholesaleEcl.closedDate) {
                this.wholesaleEcl.closedDate = moment(this.closedDate).startOf('day');
            }
            else {
                this.wholesaleEcl.closedDate = moment(this.closedDate);
            }
        }
        else {
            this.wholesaleEcl.closedDate = null;
        }
            this._wholesaleEclsServiceProxy.createOrEdit(this.wholesaleEcl)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectUserModal() {
        this.wholesaleEclUserLookupTableModal.id = this.wholesaleEcl.closedByUserId;
        this.wholesaleEclUserLookupTableModal.displayName = this.userName;
        this.wholesaleEclUserLookupTableModal.show();
    }


        setClosedByUserIdNull() {
        this.wholesaleEcl.closedByUserId = null;
        this.userName = '';
    }


        getNewClosedByUserId() {
        this.wholesaleEcl.closedByUserId = this.wholesaleEclUserLookupTableModal.id;
        this.userName = this.wholesaleEclUserLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
