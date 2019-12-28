import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { MacroeconomicVariablesServiceProxy, CreateOrEditMacroeconomicVariableDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditMacroeconomicVariableModal',
    templateUrl: './create-or-edit-macroeconomicVariable-modal.component.html'
})
export class CreateOrEditMacroeconomicVariableModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    macroeconomicVariable: CreateOrEditMacroeconomicVariableDto = new CreateOrEditMacroeconomicVariableDto();



    constructor(
        injector: Injector,
        private _macroeconomicVariablesServiceProxy: MacroeconomicVariablesServiceProxy
    ) {
        super(injector);
    }

    show(macroeconomicVariableId?: number): void {

        if (!macroeconomicVariableId) {
            this.macroeconomicVariable = new CreateOrEditMacroeconomicVariableDto();
            this.macroeconomicVariable.id = macroeconomicVariableId;

            this.active = true;
            this.modal.show();
        } else {
            this._macroeconomicVariablesServiceProxy.getMacroeconomicVariableForEdit(macroeconomicVariableId).subscribe(result => {
                this.macroeconomicVariable = result.macroeconomicVariable;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._macroeconomicVariablesServiceProxy.createOrEdit(this.macroeconomicVariable)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }







    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
