import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WholesaleEclPdAssumptionMacroeconomicInputsServiceProxy, CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { WholesaleEclPdAssumptionMacroeconomicInputWholesaleEclLookupTableModalComponent } from './wholesaleEclPdAssumptionMacroeconomicInput-wholesaleEcl-lookup-table-modal.component';

@Component({
    selector: 'createOrEditWholesaleEclPdAssumptionMacroeconomicInputModal',
    templateUrl: './create-or-edit-wholesaleEclPdAssumptionMacroeconomicInput-modal.component.html'
})
export class CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('wholesaleEclPdAssumptionMacroeconomicInputWholesaleEclLookupTableModal', { static: true }) wholesaleEclPdAssumptionMacroeconomicInputWholesaleEclLookupTableModal: WholesaleEclPdAssumptionMacroeconomicInputWholesaleEclLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    wholesaleEclPdAssumptionMacroeconomicInput: CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputDto = new CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputDto();

    wholesaleEclTenantId = '';


    constructor(
        injector: Injector,
        private _wholesaleEclPdAssumptionMacroeconomicInputsServiceProxy: WholesaleEclPdAssumptionMacroeconomicInputsServiceProxy
    ) {
        super(injector);
    }

    show(wholesaleEclPdAssumptionMacroeconomicInputId?: string): void {

        if (!wholesaleEclPdAssumptionMacroeconomicInputId) {
            this.wholesaleEclPdAssumptionMacroeconomicInput = new CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputDto();
            this.wholesaleEclPdAssumptionMacroeconomicInput.id = wholesaleEclPdAssumptionMacroeconomicInputId;
            this.wholesaleEclTenantId = '';

            this.active = true;
            this.modal.show();
        } else {
            this._wholesaleEclPdAssumptionMacroeconomicInputsServiceProxy.getWholesaleEclPdAssumptionMacroeconomicInputForEdit(wholesaleEclPdAssumptionMacroeconomicInputId).subscribe(result => {
                this.wholesaleEclPdAssumptionMacroeconomicInput = result.wholesaleEclPdAssumptionMacroeconomicInput;

                this.wholesaleEclTenantId = result.wholesaleEclTenantId;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._wholesaleEclPdAssumptionMacroeconomicInputsServiceProxy.createOrEdit(this.wholesaleEclPdAssumptionMacroeconomicInput)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

    openSelectWholesaleEclModal() {
        this.wholesaleEclPdAssumptionMacroeconomicInputWholesaleEclLookupTableModal.id = this.wholesaleEclPdAssumptionMacroeconomicInput.wholesaleEclId;
        this.wholesaleEclPdAssumptionMacroeconomicInputWholesaleEclLookupTableModal.displayName = this.wholesaleEclTenantId;
        this.wholesaleEclPdAssumptionMacroeconomicInputWholesaleEclLookupTableModal.show();
    }


    setWholesaleEclIdNull() {
        this.wholesaleEclPdAssumptionMacroeconomicInput.wholesaleEclId = null;
        this.wholesaleEclTenantId = '';
    }


    getNewWholesaleEclId() {
        this.wholesaleEclPdAssumptionMacroeconomicInput.wholesaleEclId = this.wholesaleEclPdAssumptionMacroeconomicInputWholesaleEclLookupTableModal.id;
        this.wholesaleEclTenantId = this.wholesaleEclPdAssumptionMacroeconomicInputWholesaleEclLookupTableModal.displayName;
    }


    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
