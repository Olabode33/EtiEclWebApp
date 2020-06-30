import { Component, OnInit, ViewEncapsulation, ViewChild, Injector, ElementRef } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { CommonLookupServiceProxy } from '@shared/service-proxies/service-proxies';
import * as XLSX from 'xlsx';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
    selector: 'app-loanbook-reader',
    templateUrl: './loanbook-reader.component.html',
    styleUrls: ['./loanbook-reader.component.css'],
    encapsulation: ViewEncapsulation.None,
})
export class LoanbookReaderComponent extends AppComponentBase implements OnInit {


    @ViewChild('loanbookReaderModal', { static: true }) modal: ModalDirective;
    @ViewChild('iframe', { static: true }) iframe: ElementRef;

    active = false;
    saving = false;
    isShown = false;

    file: any;

    loading = false;
    preview = '';

    trustedHtml: SafeHtml;
    //loanBooks: EclDataLoanBookDto[] = new Array();

    constructor(
        injector: Injector,
        private _commonServiceProxy: CommonLookupServiceProxy,
        private _sanitizer: DomSanitizer
    ) {
        super(injector);
    }

    ngOnInit() {
    }

    // configure(options: IEditEclReportDateModalOptions): void {
    //     options = _.merge({}, EditEclReportDateComponent.defaultOptions, options);

    //     this.eclDto = options.eclDto;
    //     this.serviceProxy = options.serviceProxy;
    // }

    show(): void {
        // if (!this.options) {
        //     throw Error('EditEclReportDateComponentError');
        // }

        this.active = true;
        this.modal.show();
    }

    shown(): void {
        this.isShown = true;
    }

    close(): void {
        this.active = false;
        this.isShown = false;
        this.modal.hide();
    }

    fileChanged(event: any) {
        this.loading = true;
        //const target: DataTransfer = <DataTransfer>(event.target);
        this.file = event.target.files[0];
        // if (target.files.length !== 1) {
        //     throw new Error('Cannot use multiple files');
        // }
        const reader: FileReader = new FileReader();
        reader.readAsBinaryString(this.file);
        reader.onload = (e: any) => {
            /* create workbook */
            const binarystr: string = e.target.result;
            const wb: XLSX.WorkBook = XLSX.read(binarystr, { type: 'binary' });
            const preview: XLSX.WorkBook = XLSX.read(binarystr, { type: 'binary', sheetRows: 10 });

            const pre_name: string = preview.SheetNames[0];
            const pre_ws: XLSX.WorkSheet = preview.Sheets[pre_name];
            const pre_html = XLSX.utils.sheet_to_html(pre_ws);
            this.trustedHtml = this._sanitizer.bypassSecurityTrustHtml(pre_html);
            this.setIframe(pre_html);
            //console.log(pre_html);
            //this.preview = pre_html;



            /* selected the first sheet */
            const wsname: string = wb.SheetNames[0];
            const ws: XLSX.WorkSheet = wb.Sheets[wsname];

            /* save data */
            const data = XLSX.utils.sheet_to_json(ws, {raw: true}); // to get 2d array pass 2nd parameter as object {header: 1}
            //let stream = XLSX.stream.to_json(ws);
            //console.log(stream);
            //console.log(data); // Data will be logged in array format containing objects
            this.loading = false;
        };
    }

    private setIframe(content: string): void {
        const win: Window = this.iframe.nativeElement.contentWindow;
        const doc: Document = win.document;

        doc.open();
        doc.write(content);
        doc.close();
     }

}
