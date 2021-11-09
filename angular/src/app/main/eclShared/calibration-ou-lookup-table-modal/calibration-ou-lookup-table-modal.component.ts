import { NameValueDtoOfInt64 } from './../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter, ViewEncapsulation} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import {AffiliateMacroEconomicVariableOffsetsServiceProxy, AffiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableDto, CommonLookupServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';

@Component({
    selector: 'calibrationOuLookupTableModal',
    styleUrls: ['./calibration-ou-lookup-table-modal.component.less'],
    encapsulation: ViewEncapsulation.None,
    templateUrl: './calibration-ou-lookup-table-modal.component.html'
})
export class CalibrationOuLookupTableModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    filterText = '';
    id: number;
    displayName: string;
    startDate:string;
    endDate:string;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    active = false;
    saving = false;

    constructor(
        injector: Injector,
        private _commonServiceProxy: CommonLookupServiceProxy
    ) {
        super(injector);
    }

    show(): void {
        this.active = true;
        this.paginator.rows = 5;
        this.getAll();
        this.modal.show();
    }

    getAll(event?: LazyLoadEvent) {
        if (!this.active) {
            return;
        }

        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._commonServiceProxy.getAllOrganizationUnitForLookupTable(
            this.filterText,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    setAndSave(organizationUnit: NameValueDtoOfInt64) {
        this.id = organizationUnit.value;
        this.displayName = organizationUnit.name;
        this.active = false;

        if(this.startDate==undefined){
            this.notify.error(this.l('StartDateIsRequired'));
            return;
        }
        
        if(this.endDate==undefined){
            this.notify.error(this.l('EndDateIsRequired'));
            return;
        }
        console.log(this.startDate);
        console.log(this.endDate);
        this.modal.hide();
        this.modalSave.emit(null);
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
