import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { AffiliateMacroEconomicVariableOffsetsServiceProxy, CreateOrEditAffiliateMacroEconomicVariableOffsetDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { AffiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableModalComponent } from './affiliateMacroEconomicVariableOffset-organizationUnit-lookup-table-modal.component';
import { AffiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableModalComponent } from './affiliateMacroEconomicVariableOffset-macroeconomicVariable-lookup-table-modal.component';

@Component({
    selector: 'createOrEditAffiliateMacroEconomicVariableOffsetModal',
    templateUrl: './create-or-edit-affiliateMacroEconomicVariableOffset-modal.component.html'
})
export class CreateOrEditAffiliateMacroEconomicVariableOffsetModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('affiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableModal', { static: true }) affiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableModal: AffiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableModalComponent;
    @ViewChild('affiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableModal', { static: true }) affiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableModal: AffiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableModalComponent;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    affiliateMacroEconomicVariableOffset: CreateOrEditAffiliateMacroEconomicVariableOffsetDto = new CreateOrEditAffiliateMacroEconomicVariableOffsetDto();

    organizationUnitDisplayName = '';
    macroeconomicVariableName = '';


    constructor(
        injector: Injector,
        private _affiliateMacroEconomicVariableOffsetsServiceProxy: AffiliateMacroEconomicVariableOffsetsServiceProxy
    ) {
        super(injector);
    }

    show(affiliateMacroEconomicVariableOffsetId?: number): void {

        if (!affiliateMacroEconomicVariableOffsetId) {
            this.affiliateMacroEconomicVariableOffset = new CreateOrEditAffiliateMacroEconomicVariableOffsetDto();
            this.affiliateMacroEconomicVariableOffset.id = affiliateMacroEconomicVariableOffsetId;
            this.organizationUnitDisplayName = '';
            this.macroeconomicVariableName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._affiliateMacroEconomicVariableOffsetsServiceProxy.getAffiliateMacroEconomicVariableOffsetForEdit(affiliateMacroEconomicVariableOffsetId).subscribe(result => {
                this.affiliateMacroEconomicVariableOffset = result.affiliateMacroEconomicVariableOffset;

                this.organizationUnitDisplayName = result.organizationUnitDisplayName;
                this.macroeconomicVariableName = result.macroeconomicVariableName;

                this.active = true;
                this.modal.show();
            });
        }
        
    }

    save(): void {
            this.saving = true;

			
            this._affiliateMacroEconomicVariableOffsetsServiceProxy.createOrEdit(this.affiliateMacroEconomicVariableOffset)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

    openSelectOrganizationUnitModal() {
        this.affiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableModal.id = this.affiliateMacroEconomicVariableOffset.affiliateId;
        this.affiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableModal.displayName = this.organizationUnitDisplayName;
        this.affiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableModal.show();
    }
    openSelectMacroeconomicVariableModal() {
        this.affiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableModal.id = this.affiliateMacroEconomicVariableOffset.macroeconomicVariableId;
        this.affiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableModal.displayName = this.macroeconomicVariableName;
        this.affiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableModal.show();
    }


    setAffiliateIdNull() {
        this.affiliateMacroEconomicVariableOffset.affiliateId = null;
        this.organizationUnitDisplayName = '';
    }
    setMacroeconomicVariableIdNull() {
        this.affiliateMacroEconomicVariableOffset.macroeconomicVariableId = null;
        this.macroeconomicVariableName = '';
    }


    getNewAffiliateId() {
        this.affiliateMacroEconomicVariableOffset.affiliateId = this.affiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableModal.id;
        this.organizationUnitDisplayName = this.affiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableModal.displayName;
    }
    getNewMacroeconomicVariableId() {
        this.affiliateMacroEconomicVariableOffset.macroeconomicVariableId = this.affiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableModal.id;
        this.macroeconomicVariableName = this.affiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableModal.displayName;
    }


    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
