import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ResultSummaryByStagesServiceProxy, CreateOrEditResultSummaryByStageDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditResultSummaryByStageModal',
    templateUrl: './create-or-edit-resultSummaryByStage-modal.component.html'
})
export class CreateOrEditResultSummaryByStageModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    resultSummaryByStage: CreateOrEditResultSummaryByStageDto = new CreateOrEditResultSummaryByStageDto();



    constructor(
        injector: Injector,
        private _resultSummaryByStagesServiceProxy: ResultSummaryByStagesServiceProxy
    ) {
        super(injector);
    }

    show(resultSummaryByStageId?: string): void {

        if (!resultSummaryByStageId) {
            this.resultSummaryByStage = new CreateOrEditResultSummaryByStageDto();
            this.resultSummaryByStage.id = resultSummaryByStageId;

            this.active = true;
            this.modal.show();
        } else {
            this._resultSummaryByStagesServiceProxy.getResultSummaryByStageForEdit(resultSummaryByStageId).subscribe(result => {
                this.resultSummaryByStage = result.resultSummaryByStage;


                this.active = true;
                this.modal.show();
            });
        }
        
    }

    save(): void {
            this.saving = true;

			
            this._resultSummaryByStagesServiceProxy.createOrEdit(this.resultSummaryByStage)
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
