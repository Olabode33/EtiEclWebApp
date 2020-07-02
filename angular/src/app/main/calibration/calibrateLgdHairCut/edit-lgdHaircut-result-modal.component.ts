import { ResultLgdHairCutSummaryDto, CalibrationLgdHairCutServiceProxy } from '../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'editlgdHaircutResultModal',
    templateUrl: './edit-lgdHaircut-result-modal.component.html'
})
export class EditLgdHaircutResultModalComponent extends AppComponentBase {

    @ViewChild('editModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    result: ResultLgdHairCutSummaryDto;
    tempResult: ResultLgdHairCutSummaryDto;

    constructor(
        injector: Injector,
        private _calibrationLgdHairCutServiceProxy: CalibrationLgdHairCutServiceProxy
    ) {
        super(injector);
    }

    show(result: ResultLgdHairCutSummaryDto): void {

        this.result = Object.assign({}, result);
        this.active = true;
        this.modal.show();
    }

    save(): void {
            this.saving = true;

            this._calibrationLgdHairCutServiceProxy.updateCalibrationResult(this.result)
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
