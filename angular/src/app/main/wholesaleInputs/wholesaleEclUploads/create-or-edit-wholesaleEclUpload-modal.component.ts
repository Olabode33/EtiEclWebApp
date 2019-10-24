import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WholesaleEclUploadsServiceProxy, CreateOrEditWholesaleEclUploadDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { WholesaleEclUploadWholesaleEclLookupTableModalComponent } from './wholesaleEclUpload-wholesaleEcl-lookup-table-modal.component';


@Component({
    selector: 'createOrEditWholesaleEclUploadModal',
    templateUrl: './create-or-edit-wholesaleEclUpload-modal.component.html'
})
export class CreateOrEditWholesaleEclUploadModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('wholesaleEclUploadWholesaleEclLookupTableModal', { static: true }) wholesaleEclUploadWholesaleEclLookupTableModal: WholesaleEclUploadWholesaleEclLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    wholesaleEclUpload: CreateOrEditWholesaleEclUploadDto = new CreateOrEditWholesaleEclUploadDto();

    wholesaleEclTenantId = '';


    constructor(
        injector: Injector,
        private _wholesaleEclUploadsServiceProxy: WholesaleEclUploadsServiceProxy
    ) {
        super(injector);
    }

    show(wholesaleEclUploadId?: string): void {

        if (!wholesaleEclUploadId) {
            this.wholesaleEclUpload = new CreateOrEditWholesaleEclUploadDto();
            this.wholesaleEclUpload.id = wholesaleEclUploadId;
            this.wholesaleEclTenantId = '';

            this.active = true;
            this.modal.show();
        } else {
            this._wholesaleEclUploadsServiceProxy.getWholesaleEclUploadForEdit(wholesaleEclUploadId).subscribe(result => {
                this.wholesaleEclUpload = result.wholesaleEclUpload;

                this.wholesaleEclTenantId = result.wholesaleEclTenantId;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._wholesaleEclUploadsServiceProxy.createOrEdit(this.wholesaleEclUpload)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectWholesaleEclModal() {
        this.wholesaleEclUploadWholesaleEclLookupTableModal.id = this.wholesaleEclUpload.wholesaleEclId;
        this.wholesaleEclUploadWholesaleEclLookupTableModal.displayName = this.wholesaleEclTenantId;
        this.wholesaleEclUploadWholesaleEclLookupTableModal.show();
    }


        setWholesaleEclIdNull() {
        this.wholesaleEclUpload.wholesaleEclId = null;
        this.wholesaleEclTenantId = '';
    }


        getNewWholesaleEclId() {
        this.wholesaleEclUpload.wholesaleEclId = this.wholesaleEclUploadWholesaleEclLookupTableModal.id;
        this.wholesaleEclTenantId = this.wholesaleEclUploadWholesaleEclLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
