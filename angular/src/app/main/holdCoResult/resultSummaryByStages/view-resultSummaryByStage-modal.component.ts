import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetResultSummaryByStageForViewDto, ResultSummaryByStageDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewResultSummaryByStageModal',
    templateUrl: './view-resultSummaryByStage-modal.component.html'
})
export class ViewResultSummaryByStageModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetResultSummaryByStageForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetResultSummaryByStageForViewDto();
        this.item.resultSummaryByStage = new ResultSummaryByStageDto();
    }

    show(item: GetResultSummaryByStageForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
