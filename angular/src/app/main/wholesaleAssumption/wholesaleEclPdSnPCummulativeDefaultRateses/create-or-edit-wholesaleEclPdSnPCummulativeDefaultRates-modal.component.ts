import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WholesaleEclPdSnPCummulativeDefaultRatesesServiceProxy, CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { WholesaleEclPdSnPCummulativeDefaultRatesWholesaleEclLookupTableModalComponent } from './wholesaleEclPdSnPCummulativeDefaultRates-wholesaleEcl-lookup-table-modal.component';


@Component({
    selector: 'createOrEditWholesaleEclPdSnPCummulativeDefaultRatesModal',
    templateUrl: './create-or-edit-wholesaleEclPdSnPCummulativeDefaultRates-modal.component.html'
})
export class CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('wholesaleEclPdSnPCummulativeDefaultRatesWholesaleEclLookupTableModal', { static: true }) wholesaleEclPdSnPCummulativeDefaultRatesWholesaleEclLookupTableModal: WholesaleEclPdSnPCummulativeDefaultRatesWholesaleEclLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    wholesaleEclPdSnPCummulativeDefaultRates: CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesDto = new CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesDto();

    wholesaleEclTenantId = '';


    constructor(
        injector: Injector,
        private _wholesaleEclPdSnPCummulativeDefaultRatesesServiceProxy: WholesaleEclPdSnPCummulativeDefaultRatesesServiceProxy
    ) {
        super(injector);
    }

    show(wholesaleEclPdSnPCummulativeDefaultRatesId?: string): void {

        if (!wholesaleEclPdSnPCummulativeDefaultRatesId) {
            this.wholesaleEclPdSnPCummulativeDefaultRates = new CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesDto();
            this.wholesaleEclPdSnPCummulativeDefaultRates.id = wholesaleEclPdSnPCummulativeDefaultRatesId;
            this.wholesaleEclTenantId = '';

            this.active = true;
            this.modal.show();
        } else {
            this._wholesaleEclPdSnPCummulativeDefaultRatesesServiceProxy.getWholesaleEclPdSnPCummulativeDefaultRatesForEdit(wholesaleEclPdSnPCummulativeDefaultRatesId).subscribe(result => {
                this.wholesaleEclPdSnPCummulativeDefaultRates = result.wholesaleEclPdSnPCummulativeDefaultRates;

                this.wholesaleEclTenantId = result.wholesaleEclTenantId;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._wholesaleEclPdSnPCummulativeDefaultRatesesServiceProxy.createOrEdit(this.wholesaleEclPdSnPCummulativeDefaultRates)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectWholesaleEclModal() {
        this.wholesaleEclPdSnPCummulativeDefaultRatesWholesaleEclLookupTableModal.id = this.wholesaleEclPdSnPCummulativeDefaultRates.wholesaleEclId;
        this.wholesaleEclPdSnPCummulativeDefaultRatesWholesaleEclLookupTableModal.displayName = this.wholesaleEclTenantId;
        this.wholesaleEclPdSnPCummulativeDefaultRatesWholesaleEclLookupTableModal.show();
    }


        setWholesaleEclIdNull() {
        this.wholesaleEclPdSnPCummulativeDefaultRates.wholesaleEclId = null;
        this.wholesaleEclTenantId = '';
    }


        getNewWholesaleEclId() {
        this.wholesaleEclPdSnPCummulativeDefaultRates.wholesaleEclId = this.wholesaleEclPdSnPCummulativeDefaultRatesWholesaleEclLookupTableModal.id;
        this.wholesaleEclTenantId = this.wholesaleEclPdSnPCummulativeDefaultRatesWholesaleEclLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
