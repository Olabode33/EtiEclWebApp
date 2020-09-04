import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ReceivablesResultsServiceProxy, CreateOrEditReceivablesResultDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditReceivablesResultModal',
    templateUrl: './create-or-edit-receivablesResult-modal.component.html'
})
export class CreateOrEditReceivablesResultModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    receivablesResult: CreateOrEditReceivablesResultDto = new CreateOrEditReceivablesResultDto();



    constructor(
        injector: Injector,
        private _receivablesResultsServiceProxy: ReceivablesResultsServiceProxy
    ) {
        super(injector);
    }

    show(receivablesResultId?: string): void {

        if (!receivablesResultId) {
            this.receivablesResult = new CreateOrEditReceivablesResultDto();
            this.receivablesResult.id = receivablesResultId;

            this.active = true;
            this.modal.show();
        } else {
            this._receivablesResultsServiceProxy.getReceivablesResultForEdit(receivablesResultId).subscribe(result => {
                this.receivablesResult = result.receivablesResult;


                this.active = true;
                this.modal.show();
            });
        }
        
    }

    save(): void {
            this.saving = true;

			
            this._receivablesResultsServiceProxy.createOrEdit(this.receivablesResult)
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
