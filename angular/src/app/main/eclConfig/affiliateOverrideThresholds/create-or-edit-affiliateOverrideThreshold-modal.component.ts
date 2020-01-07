import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { AffiliateOverrideThresholdsServiceProxy, CreateOrEditAffiliateOverrideThresholdDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { AffiliateOverrideThresholdOrganizationUnitLookupTableModalComponent } from './affiliateOverrideThreshold-organizationUnit-lookup-table-modal.component';

@Component({
    selector: 'createOrEditAffiliateOverrideThresholdModal',
    templateUrl: './create-or-edit-affiliateOverrideThreshold-modal.component.html'
})
export class CreateOrEditAffiliateOverrideThresholdModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('affiliateOverrideThresholdOrganizationUnitLookupTableModal', { static: true }) affiliateOverrideThresholdOrganizationUnitLookupTableModal: AffiliateOverrideThresholdOrganizationUnitLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    affiliateOverrideThreshold: CreateOrEditAffiliateOverrideThresholdDto = new CreateOrEditAffiliateOverrideThresholdDto();

    organizationUnitDisplayName = '';


    constructor(
        injector: Injector,
        private _affiliateOverrideThresholdsServiceProxy: AffiliateOverrideThresholdsServiceProxy
    ) {
        super(injector);
    }

    show(affiliateOverrideThresholdId?: number): void {

        if (!affiliateOverrideThresholdId) {
            this.affiliateOverrideThreshold = new CreateOrEditAffiliateOverrideThresholdDto();
            this.affiliateOverrideThreshold.id = affiliateOverrideThresholdId;
            this.organizationUnitDisplayName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._affiliateOverrideThresholdsServiceProxy.getAffiliateOverrideThresholdForEdit(affiliateOverrideThresholdId).subscribe(result => {
                this.affiliateOverrideThreshold = result.affiliateOverrideThreshold;

                this.organizationUnitDisplayName = result.organizationUnitDisplayName;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._affiliateOverrideThresholdsServiceProxy.createOrEdit(this.affiliateOverrideThreshold)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

    openSelectOrganizationUnitModal() {
        this.affiliateOverrideThresholdOrganizationUnitLookupTableModal.id = this.affiliateOverrideThreshold.organizationUnitId;
        this.affiliateOverrideThresholdOrganizationUnitLookupTableModal.displayName = this.organizationUnitDisplayName;
        this.affiliateOverrideThresholdOrganizationUnitLookupTableModal.show();
    }


    setOrganizationUnitIdNull() {
        this.affiliateOverrideThreshold.organizationUnitId = null;
        this.organizationUnitDisplayName = '';
    }


    getNewOrganizationUnitId() {
        this.affiliateOverrideThreshold.organizationUnitId = this.affiliateOverrideThresholdOrganizationUnitLookupTableModal.id;
        this.organizationUnitDisplayName = this.affiliateOverrideThresholdOrganizationUnitLookupTableModal.displayName;
    }


    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
