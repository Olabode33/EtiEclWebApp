﻿import { ResultEadCcfSummaryDto, ResultBehaviouralTermsDto, CalibrationEadBehaviouralTermsServiceProxy, ResultPd12MonthsDto, CalibrationPdCrDrServiceProxy } from '../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'editCalibratePdCrDrModal',
    templateUrl: './edit-calibratePdCrDr-result-modal.component.html'
})
export class EditCalibratePdCrDrResultModalComponent extends AppComponentBase {

    @ViewChild('editModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    result: ResultPd12MonthsDto[];

    constructor(
        injector: Injector,
        private _calibrationPdCrDrServiceProxy: CalibrationPdCrDrServiceProxy
    ) {
        super(injector);
    }

    show(result: ResultPd12MonthsDto[]): void {

        this.result = JSON.parse(JSON.stringify(result))

        this.active = true;
        this.modal.show();
    }

    save(): void {
            this.saving = true;

            this._calibrationPdCrDrServiceProxy.updateCalibrationResult(this.result)
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
