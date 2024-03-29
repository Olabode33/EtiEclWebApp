import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EclStatusEnum, CreateOrEditRetailEclDto, RetailEclsServiceProxy, GeneralStatusEnum, EclSharedServiceProxy, AssumptionDto, EadInputAssumptionDto, LgdAssumptionDto, FrameworkEnum } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import * as _ from 'lodash';
import * as moment from 'moment';
import { AppConsts } from '@shared/AppConsts';

@Component({
    selector: 'app-createEdit-retailEcl',
    templateUrl: './createEdit-retailEcl.component.html',
    styleUrls: ['./createEdit-retailEcl.component.css'],
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})

export class CreateEditRetailEclComponent extends AppComponentBase implements OnInit {

    uploadUrl: string;
    uploadedLoanbookFiles: any[] = [];
    uploadedPaymentScheduleFiles: any[] = [];
    paymentScheduleFiles: any[] = [];
    loanbookFiles: any[] = [];

    //retailEclAndAssumption: CreateRetailEclAndAssumptions = new CreateRetailEclAndAssumptions();
    retailECL: CreateOrEditRetailEclDto = new CreateOrEditRetailEclDto();
    frameworkAssumptions: AssumptionDto[] = new Array();
    eadInputAssumptions: EadInputAssumptionDto[] = new Array();
    lgdInputAssumptions: LgdAssumptionDto[] = new Array();

    wizardStep1Completed = 0;
    wizardStep2Completed = 0;
    wizardStep3Completed = 0;
    wizardStep4Completed = 0;
    wizardStep5Completed = 0;

    wizardCurrentStep = 1;
    wizardLastStep = 5;

    fake_be_scenario = 0;
    fake_o_scenario = 0;
    fake_d_scenario = 1 - (this.fake_be_scenario - this.fake_o_scenario);

    constructor(
        injector: Injector,
        private _retailEcLsServiceProxy: RetailEclsServiceProxy,
        private _eclSharedServiceProxy: EclSharedServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _router: Router
    ) {
        super(injector);
        this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/UploadFiles';
        this.loadFrameworkAssumptions();
        this.loadEadInputAssumptions();
        this.loadLgdInputAssumptions();
    }

    ngOnInit() {
        this.retailECL = new CreateOrEditRetailEclDto();
        this.retailECL.reportingDate = moment().endOf('month');
    }

    goTo(number: number): void {
        //Skip if question is already displayed
        if (number === this.wizardCurrentStep) {
            return;
        }

        //Validate
        if (!this.isValid()) {
            this.notify.error('Please select a response');
        }

        //Save
        //this.saveQuestionResponse();

        //Set next question
        this.wizardCurrentStep = number;

        //Update UI
        //this.displayQuestion();

    }

    isValid(): boolean {
        let status = true;

        switch (this.wizardCurrentStep) {
            case 1:
                this.wizardStep1Completed = 1;
                this.frameworkAssumptions.length > 0 ? status = true : status = false;
                break;
            case 2:
                this.wizardStep2Completed = 1;
                this.eadInputAssumptions.length > 0 ? status = true : status = false;
                break;
            case 3:
                this.wizardStep3Completed = 1;
                this.lgdInputAssumptions.length > 0 ? status = true : status = false;
                break;
            case 4:
                this.wizardStep4Completed = 1;
                break;
            default:
                this.wizardStep5Completed = 1;
                break;
        }
        return status;
    }

    isFirstQuestion(): boolean {
        return this.wizardCurrentStep === 1;
    }

    isLastQuestion(): boolean {
        return this.wizardCurrentStep === this.wizardLastStep;
    }

    getNextStep(): number {
        if (this.wizardLastStep >= (this.wizardCurrentStep + 1)) {
            return this.wizardCurrentStep + 1;
        } else {
            return this.wizardLastStep;
        }
    }

    getPrevStep(): number {
        if ((this.wizardCurrentStep - 1) >= 1) {
            return this.wizardCurrentStep - 1;
        } else {
            return 1;
        }
    }

    goNext() {
        return this.goTo(this.getNextStep());
    }

    goBack() {
        return this.goTo(this.getPrevStep());
    }

    finish(): void {
        if (this.isValid()) {
            this.saveRetailEcl();
            return;
        }
        this.notify.error('Please select a response');
    }

    navigateToWorkSpace(): void {
        this._router.navigate(['app/main/workspace']);
    }

    // upload completed event
    onUploadLoanbook(event): void {
        for (const file of event.files) {
            this.uploadedLoanbookFiles.push(file);
            console.log(this.uploadedLoanbookFiles);
        }
    }
    onUploadPaymentSchedule(event): void {
        for (const file of event.files) {
            this.uploadedPaymentScheduleFiles.push(file);
            console.log(this.uploadedPaymentScheduleFiles);
        }
    }

    onBeforeSend(event): void {
        event.xhr.setRequestHeader('Authorization', 'Bearer ' + abp.auth.getToken());
    }

    saveRetailEcl(): void {
        this.message.confirm(
            this.l('SubmitOrSaveAsDraft'),
            (isConfirmed) => {
                if (isConfirmed) {
                    this.retailECL.status = EclStatusEnum.Submitted;
                } else {
                    this.retailECL.status = EclStatusEnum.Draft;
                }

                // this.retailEclAndAssumption = new CreateRetailEclAndAssumptions();
                // this.retailEclAndAssumption.retailEcl = this.retailECL;
                // this.retailEclAndAssumption.frameworkAssumptions = this.frameworkAssumptions;
                // this.retailEclAndAssumption.eadInputAssumptionDtos = this.eadInputAssumptions;
                // this.retailEclAndAssumption.lgdInputAssumptionDtos = this.lgdInputAssumptions;

                // this._retailEcLsServiceProxy.createEclAndAssumption(this.retailEclAndAssumption)
                //     .subscribe(() => {
                //         this.navigateToWorkSpace();
                //         this.message.success('RetailEclSuccessfullyCreated');
                //     });
            }
        );
    }

    loadFrameworkAssumptions(): void {
        this._eclSharedServiceProxy.getFrameworkAssumptionSnapshot(FrameworkEnum.Retail).subscribe(result => {
            this.frameworkAssumptions = result;
        });
    }

    loadEadInputAssumptions(): void {
        this._eclSharedServiceProxy.getEadInputAssumptionSnapshot(FrameworkEnum.Retail).subscribe(result => {
            this.eadInputAssumptions = result;
            console.log(this.eadInputAssumptions);
        });
    }

    loadLgdInputAssumptions(): void {
        this._eclSharedServiceProxy.getLgdInputAssumptionSnapshot(FrameworkEnum.Retail).subscribe(result => {
            this.lgdInputAssumptions = result;
        });
    }
}


