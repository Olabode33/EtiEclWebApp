import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ReceivablesRegistersServiceProxy, CreateOrEditReceivablesRegisterDto, CalibrationStatusEnum, CreateOrEditReceivablesInputDto, CreateOrEditCurrentPeriodDateDto, CreateOrEditReceivablesForecastDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Observable } from "@node_modules/rxjs";
import { Location } from '@angular/common';
import * as XLSX from 'xlsx';


@Component({
    templateUrl: './create-or-edit-receivablesRegister.component.html',
    animations: [appModuleAnimation()]
})
export class CreateOrEditReceivablesRegisterComponent extends AppComponentBase implements OnInit {
    active = false;
    saving = false;

    receivablesRegister: CreateOrEditReceivablesRegisterDto = new CreateOrEditReceivablesRegisterDto();
    showForecastCard = true;
    showInputCard = true;
    showCurrentPeriodDataCard = true;
    statusEnum = CalibrationStatusEnum;
    inputParameter = new CreateOrEditReceivablesInputDto();
    currentPeriodData = new Array<CreateOrEditCurrentPeriodDateDto>();
    forecastData = new Array<CreateOrEditReceivablesForecastDto>();

    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _receivablesRegistersServiceProxy: ReceivablesRegistersServiceProxy,
        private _router: Router,
        private _location: Location
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    show(receivablesRegisterId?: string): void {

        if (!receivablesRegisterId) {
            this.receivablesRegister = new CreateOrEditReceivablesRegisterDto();
            this.receivablesRegister.id = receivablesRegisterId;
            this.receivablesRegister.status = this.statusEnum.Draft;
            this.active = true;
        } else {
            this._receivablesRegistersServiceProxy.getReceivablesRegisterForEdit(receivablesRegisterId).subscribe(result => {
                this.receivablesRegister = result.receivablesRegister;


                this.active = true;
            });
        }

    }

    save() {
        this.message.confirm(
            'Submit for approval?',
            this.l('AreYouSure'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.saving = true;

                    this.inputParameter.reportingDate = moment(this.inputParameter.reportingDate).add(1, 'hour');

                    this.receivablesRegister.inputParameter = this.inputParameter;
                    this.receivablesRegister.currentPeriodData = this.currentPeriodData;
                    this.receivablesRegister.forecastData = this.forecastData;

                    return this._receivablesRegistersServiceProxy.createOrEdit(this.receivablesRegister)
                        .pipe(finalize(() => {
                            this.saving = false;
                        })).subscribe(x => {
                            this.notify.info(this.l('SavedSuccessfully'));
                            this._router.navigate(['/app/main/receivablesRegisters/receivablesRegisters']);
                        });
                }
            }, true
        );
    }

    goBack(): void {
        this._location.back();
    }

    getStatusLabelClass(uploadStatus: CalibrationStatusEnum): string {
        switch (uploadStatus) {
            case CalibrationStatusEnum.Draft:
                return 'primary';
            case CalibrationStatusEnum.Submitted:
            case CalibrationStatusEnum.Processing:
            case CalibrationStatusEnum.AwaitngAdditionApproval:
                return 'warning';
            case CalibrationStatusEnum.Completed:
            case CalibrationStatusEnum.Approved:
                return 'success';
            case CalibrationStatusEnum.Rejected:
            case CalibrationStatusEnum.Failed:
                return 'danger';
            default:
                return 'dark';
        }
    }

    onUploadExcelError(): void {
        this.notify.error(this.l('ImportCalibrationDataFailed'));
    }

    uploadData(data: { files: File }, type): void {
        const file = data.files[0];
        const reader: FileReader = new FileReader();
        reader.readAsBinaryString(file);
        reader.onload = (e: any) => {
            const binarystr: string = e.target.result;
            const wb: XLSX.WorkBook = XLSX.read(binarystr, { type: 'binary' });

            const wsname: string = wb.SheetNames[0];
            const ws: XLSX.WorkSheet = wb.Sheets[wsname];

            const data = XLSX.utils.sheet_to_json(ws);

            if (type == 'CurrentPeriodData') {
                this.currentPeriodData = [];
                data.forEach(r => {
                    let cpd = new CreateOrEditCurrentPeriodDateDto();
                    cpd.account = r['Account'];
                    cpd.zeroTo90 = r['0-90 Days'];
                    cpd.ninetyOneTo180 = r['91-180 Days'];
                    cpd.oneEightyOneTo365 = r['180-365 Days'];
                    cpd.over365 = r['> 365 Days'];

                    this.currentPeriodData.push(cpd);
                });
            }

            else if (type == 'ForecastData') {
                this.forecastData = [];
                data.forEach(r => {
                    let cpd = new CreateOrEditReceivablesForecastDto();
                    cpd.period = r['Period'];
                    cpd.optimistic = r['Optimistic'];
                    cpd.base = r['Base'];
                    cpd.downturn = r['Downturn'];

                    this.forecastData.push(cpd);
                });
            }

        }
    }

}
