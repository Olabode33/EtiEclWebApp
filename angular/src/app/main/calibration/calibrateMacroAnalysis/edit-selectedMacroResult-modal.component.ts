import { ResultEadCcfSummaryDto, ResultBehaviouralTermsDto, CalibrationEadBehaviouralTermsServiceProxy, MacroResultStatisticsDto, CalibrationMacroAnalysisServiceProxy, GetSelectedMacroeconomicVariableDto, OverrideMacroeconomicVariableDto, OverrideSelectedMacroeconomicVariableDto } from '../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'editSelectedMacroResultModal',
    templateUrl: './edit-selectedMacroResult-modal.component.html'
})
export class EditSelectedMacroResultModalComponent extends AppComponentBase {

    @ViewChild('editModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    results: GetSelectedMacroeconomicVariableDto[];
    macroId = -1;

    constructor(
        injector: Injector,
        private _calibrationMacroAnalysisServiceProxy: CalibrationMacroAnalysisServiceProxy
    ) {
        super(injector);
    }

    show(result: GetSelectedMacroeconomicVariableDto[], macroId: number): void {
        this.results = result;
        this.macroId = macroId;
        this.active = true;
        this.modal.show();
    }

    save(): void {
            this.saving = true;

            let overrides = new Array<OverrideSelectedMacroeconomicVariableDto>();
            this.results.forEach(e => {
                overrides.push(e.selectedMacroeconomicVariable);
            });

            let finalResult = new OverrideMacroeconomicVariableDto;
            finalResult.macroId = this.macroId;
            finalResult.macroeconomicVariables = overrides;

            this._calibrationMacroAnalysisServiceProxy.updateSelectedMacroVariablesResult(finalResult)
             .pipe(finalize(() => { this.saving = false; }))
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
