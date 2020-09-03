import { AssetBooksServiceProxy, HoldCoInterCompanyResultsServiceProxy, HoldCoResultSummariesServiceProxy, CreateOrEditHoldCoInterCompanyResultDto, CreateOrEditHoldCoResultSummaryDto, CreateOrEditResultSummaryByStageDto, HoldCoApprovalsServiceProxy, GetHoldCoApprovalForViewDto, CreateOrEditHoldCoApprovalDto, HoldCoApprovalAuditInfoDto, HoldCoAuditInfoDto } from './../../../../shared/service-proxies/service-proxies';
import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { HoldCoRegistersServiceProxy, GetHoldCoRegisterForViewDto, HoldCoRegisterDto, CalibrationStatusEnum, CreateOrEditHoldCoRegisterDto, CreateOrEditHoldCoInputParameterDto, CreateOrEditMacroEconomicCreditIndexDto, CreateOrEditAssetBookDto, EntityDtoOfGuid, MacroEconomicCreditIndicesServiceProxy, ResultSummaryByStagesServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Location } from '@angular/common';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { ApprovalModalComponent } from '@app/main/eclShared/approve-ecl-modal/approve-ecl-modal.component';

@Component({
    templateUrl: './view-holdCoRegister.component.html',
    animations: [appModuleAnimation()]
})
export class ViewHoldCoRegisterComponent extends AppComponentBase implements OnInit {
    @ViewChild('approvalModal', { static: true }) approvalModal: ApprovalModalComponent;

    active = false;
    saving = false;

    item: GetHoldCoRegisterForViewDto;
    calibrationStatusEnum = CalibrationStatusEnum;
    showResultSummaryByStageCard = true;
    showResultSummaryCard = true;
    showResultCard = true;
    showAssetCard = true;
    showIntercompanyCard = true;
    showMacroCard = true;
    holdCoRegister: CreateOrEditHoldCoRegisterDto = new CreateOrEditHoldCoRegisterDto();
    totalUploads = 1;
    statusEnum = CalibrationStatusEnum;
    inputParameter = new CreateOrEditHoldCoInputParameterDto();
    macroEconomicCreditIndex = new Array<CreateOrEditMacroEconomicCreditIndexDto>();
    registrationId: any;
    assetBook = new Array<CreateOrEditAssetBookDto>();
    holdCoInterCompanyResults = new Array<CreateOrEditHoldCoInterCompanyResultDto>();
    holdCoResultSummaries = new Array<CreateOrEditHoldCoResultSummaryDto>();
    holdCoResultSummariesByStage = new Array<CreateOrEditResultSummaryByStageDto>();
    auditInfo = new HoldCoAuditInfoDto();

    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
        private _location: Location,
        private _fileDownloadService: FileDownloadService,
        private _macroEconomicCreditIndices: MacroEconomicCreditIndicesServiceProxy,
        private _resultSummaryByStagesServiceProxy: ResultSummaryByStagesServiceProxy,
        private _holdCoInterCompanyResultsServiceProxy: HoldCoInterCompanyResultsServiceProxy,
        private _holdCoResultSummariesServiceProxy: HoldCoResultSummariesServiceProxy,
        private _holdCoApprovalsServiceProxy: HoldCoApprovalsServiceProxy,

        private _assetBookServiceProxy: AssetBooksServiceProxy,
        private _holdCoRegistersServiceProxy: HoldCoRegistersServiceProxy
    ) {
        super(injector);
        this.item = new GetHoldCoRegisterForViewDto();
        this.item.holdCoRegister = new HoldCoRegisterDto();
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    reloadPage(): void {
        this.show(this.holdCoRegister.id);
    }

    show(holdCoRegisterId: string): void {

        this._holdCoRegistersServiceProxy.getHoldCoRegisterForEdit(holdCoRegisterId).subscribe(result => {
            this.holdCoRegister = result.holdCoRegister;
            this.inputParameter = this.holdCoRegister.inputParameter;
            this.inputParameter.valuationDate = this.inputParameter.valuationDate.toDate() as any;
            this.inputParameter.assumedMaturityDate = this.inputParameter.assumedMaturityDate.toDate() as any;
            this.inputParameter.assumedStartDate = this.inputParameter.assumedStartDate.toDate() as any;
            this.macroEconomicCreditIndex = this.holdCoRegister.macroEconomicCreditIndex;
            this.assetBook = this.holdCoRegister.assetBook;
            this.active = true;
            if (this.holdCoRegister.status == CalibrationStatusEnum.Completed) {
                this._resultSummaryByStagesServiceProxy.getResults(this.holdCoRegister.id).subscribe(res => {
                    this.holdCoResultSummariesByStage = res;
                });
                this._holdCoInterCompanyResultsServiceProxy.getResults(this.holdCoRegister.id).subscribe(res => {
                    this.holdCoInterCompanyResults = res;
                });
                this._holdCoResultSummariesServiceProxy.getResults(this.holdCoRegister.id).subscribe(res => {
                    this.holdCoResultSummaries = res;
                });
            }

            this._holdCoApprovalsServiceProxy.getHoldCoApprovals(holdCoRegisterId).subscribe(result => {
                this.auditInfo = result;
            });
        });
    }

    goBack(): void {
        this._location.back();
    }

    approve() {
        this.approvalModal.show();
    }

    exportResultSummaryByStageToExcel(): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this.holdCoRegister.id;
        this._resultSummaryByStagesServiceProxy.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    exportResultSummaryToExcel(): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this.holdCoRegister.id;
        this._holdCoResultSummariesServiceProxy.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    exportResultToExcel(): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this.holdCoRegister.id;
        this._holdCoInterCompanyResultsServiceProxy.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    exportMacroToExcel(): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this.holdCoRegister.id;
        this._macroEconomicCreditIndices.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    exportAssetBookToExcel(): void {
        let dto = new EntityDtoOfGuid();
        dto.id = this.holdCoRegister.id;
        this._assetBookServiceProxy.exportToExcel(dto).subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
        });
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
}
