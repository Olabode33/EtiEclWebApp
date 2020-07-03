import { MacroResultStatisticsDto, CalibrationMacroAnalysisServiceProxy, MacroResultPrincipalComponentSummaryDto } from '../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'editPrincipalSummaryResultModal',
    templateUrl: './edit-principalSummary-result-modal.component.html'
})
export class EditPrincipalSummaryResultModalComponent extends AppComponentBase {

    @ViewChild('editModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    presult: MacroResultPrincipalComponentSummaryDto[];

    constructor(
        injector: Injector,
        private _calibrationMacroAnalysisServiceProxy: CalibrationMacroAnalysisServiceProxy
    ) {
        super(injector);
    }

    show(result: MacroResultPrincipalComponentSummaryDto[]): void {

        this.presult = JSON.parse(JSON.stringify(result))
        this.active = true;
        this.modal.show();
    }

    save(): void {
            this.saving = true;

            this._calibrationMacroAnalysisServiceProxy.updatePrincipalSummaryResult(this.presult)
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
