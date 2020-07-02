import { CalibrationPdCrDrServiceProxy, ResultPd12MonthsSummaryDto } from '../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'editCalibratePdCrDrSummaryModal',
    templateUrl: './edit-calibratePdCrDr-summary-modal.component.html'
})
export class EditCalibratePdCrDrSummaryModalComponent extends AppComponentBase {

    @ViewChild('editModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    resultSummary: ResultPd12MonthsSummaryDto;

    constructor(
        injector: Injector,
        private _calibrationPdCrDrServiceProxy: CalibrationPdCrDrServiceProxy
    ) {
        super(injector);
    }

    show(result: ResultPd12MonthsSummaryDto): void {

        this.resultSummary = result
        this.active = true;
        this.modal.show();
    }

    save(): void {
            this.saving = true;

            this._calibrationPdCrDrServiceProxy.updateCalibrationResultSummary(this.resultSummary)
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
