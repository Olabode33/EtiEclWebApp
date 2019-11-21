import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EclStatusEnum, CreateOrEditRetailEclDto, RetailEclsServiceProxy, GeneralStatusEnum } from '@shared/service-proxies/service-proxies';
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

export class CreateEditRetailEclComponent  extends AppComponentBase implements OnInit {

  uploadUrl: string;
  uploadedFiles: any[] = [];

  retailECL: CreateOrEditRetailEclDto = new CreateOrEditRetailEclDto();

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
    private _notifyService: NotifyService,
    private _tokenAuth: TokenAuthServiceProxy,
    private _activatedRoute: ActivatedRoute,
    private _fileDownloadService: FileDownloadService,
    private _router: Router
  ) { 
    super(injector);
    this.uploadUrl = AppConsts.remoteServiceBaseUrl + '/DemoUiComponents/UploadFiles';
   
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
  switch (this.wizardCurrentStep) {
      case 1:
          this.wizardStep1Completed = 1;
          break;
      case 2:
          this.wizardStep2Completed = 1;
          break;
      case 3:
          this.wizardStep3Completed = 1;
          break;
      case 4:
          this.wizardStep4Completed = 1;
          break;
      default:
          this.wizardStep5Completed = 1;
          break;
  }
  return true;
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
onUpload(event): void {
  for (const file of event.files) {
      this.uploadedFiles.push(file);
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
              this.retailECL.status =  GeneralStatusEnum.Submitted;
          } else {
              this.retailECL.status = GeneralStatusEnum.Draft;
          }
          this._retailEcLsServiceProxy.createOrEdit(this.retailECL)
                  .subscribe(() => {
                      this.navigateToWorkSpace();
                      this.message.success('RetailEclSuccessfullyCreated');
                  });
      }
  );
}

}


