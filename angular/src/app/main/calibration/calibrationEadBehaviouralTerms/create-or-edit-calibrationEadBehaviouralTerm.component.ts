import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import {
    CalibrationEadBehaviouralTermsServiceProxy, CalibrationEadBehaviouralTermUserLookupTableDto, CreateOrEditCalibrationRunDto
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
    selector: 'createOrEditCalibrationEadBehaviouralTerm',
    templateUrl: './create-or-edit-calibrationEadBehaviouralTerm.component.html',
    animations: [appModuleAnimation()]
})
export class CreateOrEditCalibrationEadBehaviouralTermComponent extends AppComponentBase implements OnInit {
    active = false;
    saving = false;

    calibrationEadBehaviouralTerm: CreateOrEditCalibrationRunDto = new CreateOrEditCalibrationRunDto();

    userName = '';

    allUsers: CalibrationEadBehaviouralTermUserLookupTableDto[];

    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _calibrationEadBehaviouralTermsServiceProxy: CalibrationEadBehaviouralTermsServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    show(calibrationEadBehaviouralTermId?: string): void {

        if (!calibrationEadBehaviouralTermId) {
            this.calibrationEadBehaviouralTerm = new CreateOrEditCalibrationRunDto();
            this.calibrationEadBehaviouralTerm.id = calibrationEadBehaviouralTermId;
            this.userName = '';

            this.active = true;
        } else {
            this._calibrationEadBehaviouralTermsServiceProxy.getCalibrationForEdit(calibrationEadBehaviouralTermId).subscribe(result => {
                this.calibrationEadBehaviouralTerm = result.calibration;

                this.userName = result.userName;

                this.active = true;
            });
        }
        this._calibrationEadBehaviouralTermsServiceProxy.getAllUserForTableDropdown().subscribe(result => {
            this.allUsers = result;
        });

    }

    save(): void {
        this.saving = true;


        this._calibrationEadBehaviouralTermsServiceProxy.createOrEdit(this.calibrationEadBehaviouralTerm)
            .pipe(finalize(() => { this.saving = false; }))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
            });
    }







}
