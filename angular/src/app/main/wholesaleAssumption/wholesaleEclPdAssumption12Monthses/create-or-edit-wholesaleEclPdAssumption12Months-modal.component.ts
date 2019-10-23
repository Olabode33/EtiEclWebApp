import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WholesaleEclPdAssumption12MonthsesServiceProxy, CreateOrEditWholesaleEclPdAssumption12MonthsDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { WholesaleEclPdAssumption12MonthsWholesaleEclLookupTableModalComponent } from './wholesaleEclPdAssumption12Months-wholesaleEcl-lookup-table-modal.component';


@Component({
    selector: 'createOrEditWholesaleEclPdAssumption12MonthsModal',
    templateUrl: './create-or-edit-wholesaleEclPdAssumption12Months-modal.component.html'
})
export class CreateOrEditWholesaleEclPdAssumption12MonthsModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('wholesaleEclPdAssumption12MonthsWholesaleEclLookupTableModal', { static: true }) wholesaleEclPdAssumption12MonthsWholesaleEclLookupTableModal: WholesaleEclPdAssumption12MonthsWholesaleEclLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    wholesaleEclPdAssumption12Months: CreateOrEditWholesaleEclPdAssumption12MonthsDto = new CreateOrEditWholesaleEclPdAssumption12MonthsDto();

    wholesaleEclTenantId = '';


    constructor(
        injector: Injector,
        private _wholesaleEclPdAssumption12MonthsesServiceProxy: WholesaleEclPdAssumption12MonthsesServiceProxy
    ) {
        super(injector);
    }

    show(wholesaleEclPdAssumption12MonthsId?: string): void {

        if (!wholesaleEclPdAssumption12MonthsId) {
            this.wholesaleEclPdAssumption12Months = new CreateOrEditWholesaleEclPdAssumption12MonthsDto();
            this.wholesaleEclPdAssumption12Months.id = wholesaleEclPdAssumption12MonthsId;
            this.wholesaleEclTenantId = '';

            this.active = true;
            this.modal.show();
        } else {
            this._wholesaleEclPdAssumption12MonthsesServiceProxy.getWholesaleEclPdAssumption12MonthsForEdit(wholesaleEclPdAssumption12MonthsId).subscribe(result => {
                this.wholesaleEclPdAssumption12Months = result.wholesaleEclPdAssumption12Months;

                this.wholesaleEclTenantId = result.wholesaleEclTenantId;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._wholesaleEclPdAssumption12MonthsesServiceProxy.createOrEdit(this.wholesaleEclPdAssumption12Months)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectWholesaleEclModal() {
        this.wholesaleEclPdAssumption12MonthsWholesaleEclLookupTableModal.id = this.wholesaleEclPdAssumption12Months.wholesaleEclId;
        this.wholesaleEclPdAssumption12MonthsWholesaleEclLookupTableModal.displayName = this.wholesaleEclTenantId;
        this.wholesaleEclPdAssumption12MonthsWholesaleEclLookupTableModal.show();
    }


        setWholesaleEclIdNull() {
        this.wholesaleEclPdAssumption12Months.wholesaleEclId = null;
        this.wholesaleEclTenantId = '';
    }


        getNewWholesaleEclId() {
        this.wholesaleEclPdAssumption12Months.wholesaleEclId = this.wholesaleEclPdAssumption12MonthsWholesaleEclLookupTableModal.id;
        this.wholesaleEclTenantId = this.wholesaleEclPdAssumption12MonthsWholesaleEclLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
