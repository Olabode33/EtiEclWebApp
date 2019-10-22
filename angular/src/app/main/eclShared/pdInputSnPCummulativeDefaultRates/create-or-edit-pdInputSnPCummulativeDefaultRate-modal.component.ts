import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PdInputSnPCummulativeDefaultRatesServiceProxy, CreateOrEditPdInputSnPCummulativeDefaultRateDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditPdInputSnPCummulativeDefaultRateModal',
    templateUrl: './create-or-edit-pdInputSnPCummulativeDefaultRate-modal.component.html'
})
export class CreateOrEditPdInputSnPCummulativeDefaultRateModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    pdInputSnPCummulativeDefaultRate: CreateOrEditPdInputSnPCummulativeDefaultRateDto = new CreateOrEditPdInputSnPCummulativeDefaultRateDto();



    constructor(
        injector: Injector,
        private _pdInputSnPCummulativeDefaultRatesServiceProxy: PdInputSnPCummulativeDefaultRatesServiceProxy
    ) {
        super(injector);
    }

    show(pdInputSnPCummulativeDefaultRateId?: string): void {

        if (!pdInputSnPCummulativeDefaultRateId) {
            this.pdInputSnPCummulativeDefaultRate = new CreateOrEditPdInputSnPCummulativeDefaultRateDto();
            this.pdInputSnPCummulativeDefaultRate.id = pdInputSnPCummulativeDefaultRateId;

            this.active = true;
            this.modal.show();
        } else {
            this._pdInputSnPCummulativeDefaultRatesServiceProxy.getPdInputSnPCummulativeDefaultRateForEdit(pdInputSnPCummulativeDefaultRateId).subscribe(result => {
                this.pdInputSnPCummulativeDefaultRate = result.pdInputSnPCummulativeDefaultRate;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._pdInputSnPCummulativeDefaultRatesServiceProxy.createOrEdit(this.pdInputSnPCummulativeDefaultRate)
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
