import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { AssumptionsServiceProxy, CreateOrEditAssumptionDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditAssumptionModal',
    templateUrl: './create-or-edit-assumption-modal.component.html'
})
export class CreateOrEditAssumptionModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    assumption: CreateOrEditAssumptionDto = new CreateOrEditAssumptionDto();



    constructor(
        injector: Injector,
        private _assumptionsServiceProxy: AssumptionsServiceProxy
    ) {
        super(injector);
    }

    show(assumptionId?: string): void {

        if (!assumptionId) {
            this.assumption = new CreateOrEditAssumptionDto();
            this.assumption.id = assumptionId;

            this.active = true;
            this.modal.show();
        } else {
            this._assumptionsServiceProxy.getAssumptionForEdit(assumptionId).subscribe(result => {
                this.assumption = result.assumption;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._assumptionsServiceProxy.createOrEdit(this.assumption)
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
