import { MacroResultStatisticsDto, CalibrationMacroAnalysisServiceProxy, MacroResultPrincipalComponentSummaryDto, MacroResultPrincipalComponentDto } from '../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { CSVConverter } from '@app/main/eclView/_subs/ecl-override/csv-converter';
import * as XLSX from 'xlsx';

@Component({
    selector: 'editPrincipalComponentResultModal',
    templateUrl: './edit-principalComponent-result-modal.component.html'
})
export class EditPrincipalComponentResultModalComponent extends AppComponentBase {

    @ViewChild('editModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    presult: MacroResultPrincipalComponentDto[];

    constructor(
        injector: Injector,
        private _calibrationMacroAnalysisServiceProxy: CalibrationMacroAnalysisServiceProxy
    ) {
        super(injector);
    }

    show(result: MacroResultPrincipalComponentDto[]): void {

        this.presult = JSON.parse(JSON.stringify(result));
        this.active = true;
        this.modal.show();
    }

    save(): void {
            this.saving = true;

            this._calibrationMacroAnalysisServiceProxy.updatePrincipalComponentResult(this.presult)
             .pipe(finalize(() => { this.saving = false; }))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

    download() {
        let data = this.presult;
        let csvData = CSVConverter.ConvertToCSV(data);
        let a = document.createElement('a');
        a.setAttribute('style', 'display:none;');
        document.body.appendChild(a);
        let blob = new Blob([csvData], { type: 'text/csv' });
        let url = window.URL.createObjectURL(blob);
        a.href = url;
        a.download = 'PrincipalComponentResult.csv';
        a.click();
    }

    uploadExcel(data: { files: File }): void {
        this.primengTableHelper.isLoading = true;
        const file = data.files[0];
        const reader: FileReader = new FileReader();
        reader.readAsBinaryString(file);
        reader.onload = (e: any) => {
            const binarystr: string = e.target.result;
            const wb: XLSX.WorkBook = XLSX.read(binarystr, { type: 'binary' });

            const wsname: string = wb.SheetNames[0];
            const ws: XLSX.WorkSheet = wb.Sheets[wsname];

            const data = XLSX.utils.sheet_to_json(ws);

            let finalList = new Array<MacroResultPrincipalComponentDto>();
            let result = data as MacroResultPrincipalComponentDto[];


            let invalids = new Array<MacroResultPrincipalComponentDto>();
            result.forEach(e => {
                let noInOriginal = this.presult.find(x => x.id === e.id && x.macroId === e.macroId);
                if (!noInOriginal){
                    invalids.push(e);
                }
            });


            if (invalids.length > 0) {
                this.message.error('Id or MarcroId Mismatch');
            } else {
                this.message.confirm(
                    this.l('AreYouSure'),
                    (isConfirmed) => {
                        if (isConfirmed) {
                            this.presult = result;
                            this.save();
                        }
                    }
                );
            }
        };
    }






    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
