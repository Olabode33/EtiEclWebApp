import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { OverrideTypesServiceProxy, CreateOrEditOverrideTypeDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditOverrideTypeModal',
    templateUrl: './create-or-edit-overrideType-modal.component.html'
})
export class CreateOrEditOverrideTypeModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    overrideType: CreateOrEditOverrideTypeDto = new CreateOrEditOverrideTypeDto();



    constructor(
        injector: Injector,
        private _overrideTypesServiceProxy: OverrideTypesServiceProxy
    ) {
        super(injector);
    }

    show(overrideTypeId?: number): void {

        if (!overrideTypeId) {
            this.overrideType = new CreateOrEditOverrideTypeDto();
            this.overrideType.id = overrideTypeId;

            this.active = true;
            this.modal.show();
        } else {
            this._overrideTypesServiceProxy.getOverrideTypeForEdit(overrideTypeId).subscribe(result => {
                this.overrideType = result.overrideType;


                this.active = true;
                this.modal.show();
            });
        }
        
    }

    save(): void {
            this.saving = true;

			
            this._overrideTypesServiceProxy.createOrEdit(this.overrideType)
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
