import { Component, ViewChild, Injector, Output, EventEmitter, OnInit} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { HoldCoRegistersServiceProxy, CreateOrEditHoldCoRegisterDto, EntityDtoOfGuid, CreateOrEditCalibrationRunDto, CalibrationStatusEnum } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {Observable} from "@node_modules/rxjs";


@Component({
    templateUrl: './create-or-edit-holdCoRegister.component.html',
    animations: [appModuleAnimation()]
})
export class CreateOrEditHoldCoRegisterComponent extends AppComponentBase implements OnInit {
    active = false;
    saving = false;
    showAssetCard = true;
    showIntercompanyCard = true;
    showMacroCard = true;
    holdCoRegister: CreateOrEditHoldCoRegisterDto = new CreateOrEditHoldCoRegisterDto();
    totalUploads = 1;
    calibration: CreateOrEditCalibrationRunDto = new CreateOrEditCalibrationRunDto();
    calibrationStatusEnum = CalibrationStatusEnum;



    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _holdCoRegistersServiceProxy: HoldCoRegistersServiceProxy,
        private _router: Router
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    show(holdCoRegisterId?: string): void {

        if (!holdCoRegisterId) {
            this.holdCoRegister = new CreateOrEditHoldCoRegisterDto();
            this.holdCoRegister.id = holdCoRegisterId;

            this.active = true;
        } else {
            this._holdCoRegistersServiceProxy.getHoldCoRegisterForEdit(holdCoRegisterId).subscribe(result => {
                this.holdCoRegister = result.holdCoRegister;


                this.active = true;
            });
        }

    }

    uploadData(data: { files: File }): void {
        const formData: FormData = new FormData();
        const file = data.files[0];
        formData.append('file', file, file.name);
        // formData.append('calibrationId', this._calibrationId);

        // this.message.confirm(
        //     this.l('ExistingDataWouldBeReplaced'),
        //     (isConfirmed) => {
        //         if (isConfirmed) {
        //             this._httpClient
        //                 .post<any>(this.uploadUrl, formData)
        //                 .pipe(finalize(() => this.excelFileUpload.clear()))
        //                 .subscribe(response => {
        //                     if (response.success) {
        //                         this.notify.success(this.l('ImportCalibrationDataProcessStart'));
        //                         this.autoReloadUploadSummary();
        //                         setTimeout(() => this.getUploadSummary(), 5000);
        //                     } else if (response.error != null) {
        //                         this.notify.error(this.l('ImportCalibrationDataUploadFailed'));
        //                     }
        //                 });
        //         }
        //     }
        // );
    }

    onUploadExcelError(): void {
        this.notify.error(this.l('ImportCalibrationDataFailed'));
    }

    private saveInternal(): Observable<void> {
            this.saving = true;


        return this._holdCoRegistersServiceProxy.createOrEdit(this.holdCoRegister)
         .pipe(finalize(() => {
            this.saving = false;
            this.notify.info(this.l('SavedSuccessfully'));
         }));
    }

    save(): void {
        this.saveInternal().subscribe(x => {
             this._router.navigate( ['/app/main/ivModels/holdCoRegisters']);
        })
    }

    saveAndNew(): void {
        this.saveInternal().subscribe(x => {
            this.holdCoRegister = new CreateOrEditHoldCoRegisterDto();
        })
    }


    exportToExcel(): void {
        // let dto = new EntityDtoOfGuid();
        // dto.id = this._calibrationId;
        // this._calibrationServiceProxy.exportToExcel(dto).subscribe(result => {
        //     this._fileDownloadService.downloadTempFile(result);
        // });
    }



}
