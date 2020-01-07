import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { EclConfigurationsServiceProxy, CreateOrEditEclConfigurationDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditEclConfigurationModal',
    templateUrl: './create-or-edit-eclConfiguration-modal.component.html'
})
export class CreateOrEditEclConfigurationModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    eclConfiguration: CreateOrEditEclConfigurationDto = new CreateOrEditEclConfigurationDto();



    constructor(
        injector: Injector,
        private _eclConfigurationsServiceProxy: EclConfigurationsServiceProxy
    ) {
        super(injector);
    }

    show(eclConfigurationId?: number): void {

        if (!eclConfigurationId) {
            this.eclConfiguration = new CreateOrEditEclConfigurationDto();
            this.eclConfiguration.id = eclConfigurationId;

            this.active = true;
            this.modal.show();
        } else {
            this._eclConfigurationsServiceProxy.getEclConfigurationForEdit(eclConfigurationId).subscribe(result => {
                this.eclConfiguration = result.eclConfiguration;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._eclConfigurationsServiceProxy.createOrEdit(this.eclConfiguration)
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
