import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WholesaleEclResultSummariesServiceProxy, CreateOrEditWholesaleEclResultSummaryDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { WholesaleEclResultSummaryWholesaleEclLookupTableModalComponent } from './wholesaleEclResultSummary-wholesaleEcl-lookup-table-modal.component';


@Component({
    selector: 'createOrEditWholesaleEclResultSummaryModal',
    templateUrl: './create-or-edit-wholesaleEclResultSummary-modal.component.html'
})
export class CreateOrEditWholesaleEclResultSummaryModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('wholesaleEclResultSummaryWholesaleEclLookupTableModal', { static: true }) wholesaleEclResultSummaryWholesaleEclLookupTableModal: WholesaleEclResultSummaryWholesaleEclLookupTableModalComponent;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    wholesaleEclResultSummary: CreateOrEditWholesaleEclResultSummaryDto = new CreateOrEditWholesaleEclResultSummaryDto();

    wholesaleEclTenantId = '';


    constructor(
        injector: Injector,
        private _wholesaleEclResultSummariesServiceProxy: WholesaleEclResultSummariesServiceProxy
    ) {
        super(injector);
    }

    show(wholesaleEclResultSummaryId?: string): void {

        if (!wholesaleEclResultSummaryId) {
            this.wholesaleEclResultSummary = new CreateOrEditWholesaleEclResultSummaryDto();
            this.wholesaleEclResultSummary.id = wholesaleEclResultSummaryId;
            this.wholesaleEclTenantId = '';

            this.active = true;
            this.modal.show();
        } else {
            this._wholesaleEclResultSummariesServiceProxy.getWholesaleEclResultSummaryForEdit(wholesaleEclResultSummaryId).subscribe(result => {
                this.wholesaleEclResultSummary = result.wholesaleEclResultSummary;

                this.wholesaleEclTenantId = result.wholesaleEclTenantId;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._wholesaleEclResultSummariesServiceProxy.createOrEdit(this.wholesaleEclResultSummary)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

        openSelectWholesaleEclModal() {
        this.wholesaleEclResultSummaryWholesaleEclLookupTableModal.id = this.wholesaleEclResultSummary.wholesaleEclId;
        this.wholesaleEclResultSummaryWholesaleEclLookupTableModal.displayName = this.wholesaleEclTenantId;
        this.wholesaleEclResultSummaryWholesaleEclLookupTableModal.show();
    }


        setWholesaleEclIdNull() {
        this.wholesaleEclResultSummary.wholesaleEclId = null;
        this.wholesaleEclTenantId = '';
    }


        getNewWholesaleEclId() {
        this.wholesaleEclResultSummary.wholesaleEclId = this.wholesaleEclResultSummaryWholesaleEclLookupTableModal.id;
        this.wholesaleEclTenantId = this.wholesaleEclResultSummaryWholesaleEclLookupTableModal.displayName;
    }


    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
