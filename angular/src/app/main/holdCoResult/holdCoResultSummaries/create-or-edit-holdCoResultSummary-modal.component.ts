import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { HoldCoResultSummariesServiceProxy, CreateOrEditHoldCoResultSummaryDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditHoldCoResultSummaryModal',
    templateUrl: './create-or-edit-holdCoResultSummary-modal.component.html'
})
export class CreateOrEditHoldCoResultSummaryModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    holdCoResultSummary: CreateOrEditHoldCoResultSummaryDto = new CreateOrEditHoldCoResultSummaryDto();



    constructor(
        injector: Injector,
        private _holdCoResultSummariesServiceProxy: HoldCoResultSummariesServiceProxy
    ) {
        super(injector);
    }

    show(holdCoResultSummaryId?: string): void {

        if (!holdCoResultSummaryId) {
            this.holdCoResultSummary = new CreateOrEditHoldCoResultSummaryDto();
            this.holdCoResultSummary.id = holdCoResultSummaryId;

            this.active = true;
            this.modal.show();
        } else {
            this._holdCoResultSummariesServiceProxy.getHoldCoResultSummaryForEdit(holdCoResultSummaryId).subscribe(result => {
                this.holdCoResultSummary = result.holdCoResultSummary;


                this.active = true;
                this.modal.show();
            });
        }
        
    }

    save(): void {
            this.saving = true;

			
            this._holdCoResultSummariesServiceProxy.createOrEdit(this.holdCoResultSummary)
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
