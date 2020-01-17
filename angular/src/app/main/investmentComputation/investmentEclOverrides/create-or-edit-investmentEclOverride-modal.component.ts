import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { InvestmentEclOverridesServiceProxy, CreateOrEditInvestmentEclOverrideDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { InvestmentEclOverrideInvestmentEclSicrLookupTableModalComponent } from './investmentEclOverride-investmentEclSicr-lookup-table-modal.component';

@Component({
    selector: 'createOrEditInvestmentEclOverrideModal',
    templateUrl: './create-or-edit-investmentEclOverride-modal.component.html'
})
export class CreateOrEditInvestmentEclOverrideModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('investmentEclOverrideInvestmentEclSicrLookupTableModal', { static: true }) investmentEclOverrideInvestmentEclSicrLookupTableModal: InvestmentEclOverrideInvestmentEclSicrLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    investmentEclOverride: CreateOrEditInvestmentEclOverrideDto = new CreateOrEditInvestmentEclOverrideDto();

    investmentEclSicrAssetDescription = '';


    constructor(
        injector: Injector,
        private _investmentEclOverridesServiceProxy: InvestmentEclOverridesServiceProxy
    ) {
        super(injector);
    }

    show(investmentEclOverrideId?: string): void {

        if (!investmentEclOverrideId) {
            this.investmentEclOverride = new CreateOrEditInvestmentEclOverrideDto();
            this.investmentEclOverride.id = investmentEclOverrideId;
            this.investmentEclSicrAssetDescription = '';

            this.active = true;
            this.modal.show();
        } else {
            this._investmentEclOverridesServiceProxy.getInvestmentEclOverrideForEdit(investmentEclOverrideId).subscribe(result => {
                this.investmentEclOverride = result.investmentEclOverride;

                this.investmentEclSicrAssetDescription = result.investmentEclSicrAssetDescription;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._investmentEclOverridesServiceProxy.createOrEdit(this.investmentEclOverride)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

    openSelectInvestmentEclSicrModal() {
        this.investmentEclOverrideInvestmentEclSicrLookupTableModal.id = this.investmentEclOverride.investmentEclSicrId;
        this.investmentEclOverrideInvestmentEclSicrLookupTableModal.displayName = this.investmentEclSicrAssetDescription;
        this.investmentEclOverrideInvestmentEclSicrLookupTableModal.show();
    }


    setInvestmentEclSicrIdNull() {
        this.investmentEclOverride.investmentEclSicrId = null;
        this.investmentEclSicrAssetDescription = '';
    }


    getNewInvestmentEclSicrId() {
        this.investmentEclOverride.investmentEclSicrId = this.investmentEclOverrideInvestmentEclSicrLookupTableModal.id;
        this.investmentEclSicrAssetDescription = this.investmentEclOverrideInvestmentEclSicrLookupTableModal.displayName;
    }


    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
