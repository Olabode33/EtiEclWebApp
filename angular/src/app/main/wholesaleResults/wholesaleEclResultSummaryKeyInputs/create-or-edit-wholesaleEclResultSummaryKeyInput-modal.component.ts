import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WholesaleEclResultSummaryKeyInputsServiceProxy, CreateOrEditWholesaleEclResultSummaryKeyInputDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { WholesaleEclResultSummaryKeyInputWholesaleEclLookupTableModalComponent } from './wholesaleEclResultSummaryKeyInput-wholesaleEcl-lookup-table-modal.component';


@Component({
    selector: 'createOrEditWholesaleEclResultSummaryKeyInputModal',
    templateUrl: './create-or-edit-wholesaleEclResultSummaryKeyInput-modal.component.html'
})
export class CreateOrEditWholesaleEclResultSummaryKeyInputModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('wholesaleEclResultSummaryKeyInputWholesaleEclLookupTableModal', { static: true }) wholesaleEclResultSummaryKeyInputWholesaleEclLookupTableModal: WholesaleEclResultSummaryKeyInputWholesaleEclLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    wholesaleEclResultSummaryKeyInput: CreateOrEditWholesaleEclResultSummaryKeyInputDto = new CreateOrEditWholesaleEclResultSummaryKeyInputDto();

    wholesaleEclTenantId = '';


    constructor(
        injector: Injector,
        private _wholesaleEclResultSummaryKeyInputsServiceProxy: WholesaleEclResultSummaryKeyInputsServiceProxy
    ) {
        super(injector);
    }

    show(wholesaleEclResultSummaryKeyInputId?: string): void {

        if (!wholesaleEclResultSummaryKeyInputId) {
            this.wholesaleEclResultSummaryKeyInput = new CreateOrEditWholesaleEclResultSummaryKeyInputDto();
            this.wholesaleEclResultSummaryKeyInput.id = wholesaleEclResultSummaryKeyInputId;
            this.wholesaleEclTenantId = '';

            this.active = true;
            this.modal.show();
        } else {
            this._wholesaleEclResultSummaryKeyInputsServiceProxy.getWholesaleEclResultSummaryKeyInputForEdit(wholesaleEclResultSummaryKeyInputId).subscribe(result => {
                this.wholesaleEclResultSummaryKeyInput = result.wholesaleEclResultSummaryKeyInput;

                this.wholesaleEclTenantId = result.wholesaleEclTenantId;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._wholesaleEclResultSummaryKeyInputsServiceProxy.createOrEdit(this.wholesaleEclResultSummaryKeyInput)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectWholesaleEclModal() {
        this.wholesaleEclResultSummaryKeyInputWholesaleEclLookupTableModal.id = this.wholesaleEclResultSummaryKeyInput.wholesaleEclId;
        this.wholesaleEclResultSummaryKeyInputWholesaleEclLookupTableModal.displayName = this.wholesaleEclTenantId;
        this.wholesaleEclResultSummaryKeyInputWholesaleEclLookupTableModal.show();
    }


        setWholesaleEclIdNull() {
        this.wholesaleEclResultSummaryKeyInput.wholesaleEclId = null;
        this.wholesaleEclTenantId = '';
    }


        getNewWholesaleEclId() {
        this.wholesaleEclResultSummaryKeyInput.wholesaleEclId = this.wholesaleEclResultSummaryKeyInputWholesaleEclLookupTableModal.id;
        this.wholesaleEclTenantId = this.wholesaleEclResultSummaryKeyInputWholesaleEclLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
