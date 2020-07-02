import { AffiliateConfigurationComponent } from './eclConfig/affiliate-configuration/affiliate-configuration.component';
import { FrameworkAssumptionsComponent } from './assumptions/_subs/frameworkAssumptions/frameworkAssumptions.component';
import { ViewAffiliateAssumptionsComponent } from './assumptions/view-affiliateAssumptions/view-affiliateAssumptions.component';
import { CommonModule } from '@angular/common';
import { CalibrationEadBehaviouralTermsComponent } from './calibration/calibrationEadBehaviouralTerms/calibrationEadBehaviouralTerms.component';
import { CreateOrEditCalibrationEadBehaviouralTermComponent } from './calibration/calibrationEadBehaviouralTerms/create-or-edit-calibrationEadBehaviouralTerm.component';

import { AffiliateMacroEconomicVariableOffsetsComponent } from './affiliateMacroeconomicVariable/affiliateMacroVariable/affiliateMacroEconomicVariableOffsets.component';
import { ViewAffiliateMacroEconomicVariableOffsetModalComponent } from './affiliateMacroeconomicVariable/affiliateMacroVariable/view-affiliateMacroEconomicVariableOffset-modal.component';
import { CreateOrEditAffiliateMacroEconomicVariableOffsetModalComponent } from './affiliateMacroeconomicVariable/affiliateMacroVariable/create-or-edit-affiliateMacroEconomicVariableOffset-modal.component';
import { AffiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableModalComponent } from './affiliateMacroeconomicVariable/affiliateMacroVariable/affiliateMacroEconomicVariableOffset-organizationUnit-lookup-table-modal.component';
import { AffiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableModalComponent } from './affiliateMacroeconomicVariable/affiliateMacroVariable/affiliateMacroEconomicVariableOffset-macroeconomicVariable-lookup-table-modal.component';

import { NgModule } from '@angular/core';
import { EclSettingsComponent } from './eclConfig/eclSettings/eclSettings.component';

import { AssumptionApprovalsComponent } from './assumptions/assumptionApprovals/assumptionApprovals.component';
import { ViewAssumptionApprovalModalComponent } from './assumptions/assumptionApprovals/view-assumptionApproval-modal.component';

import { MacroeconomicVariablesComponent } from './eclShared/macroeconomicVariables/macroeconomicVariables.component';
import { CreateOrEditMacroeconomicVariableModalComponent } from './eclShared/macroeconomicVariables/create-or-edit-macroeconomicVariable-modal.component';

import { FormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';

import { WholesaleEclDataPaymentSchedulesComponent } from './wholesaleInputs/wholesaleEclDataPaymentSchedules/wholesaleEclDataPaymentSchedules.component';
import { CreateOrEditWholesaleEclDataPaymentScheduleModalComponent } from './wholesaleInputs/wholesaleEclDataPaymentSchedules/create-or-edit-wholesaleEclDataPaymentSchedule-modal.component';
import { WholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableModalComponent } from './wholesaleInputs/wholesaleEclDataPaymentSchedules/wholesaleEclDataPaymentSchedule-wholesaleEclUpload-lookup-table-modal.component';

import { WholesaleEclDataLoanBooksComponent } from './wholesaleInputs/wholesaleEclDataLoanBooks/wholesaleEclDataLoanBooks.component';
import { CreateOrEditWholesaleEclDataLoanBookModalComponent } from './wholesaleInputs/wholesaleEclDataLoanBooks/create-or-edit-wholesaleEclDataLoanBook-modal.component';
import { WholesaleEclDataLoanBookWholesaleEclUploadLookupTableModalComponent } from './wholesaleInputs/wholesaleEclDataLoanBooks/wholesaleEclDataLoanBook-wholesaleEclUpload-lookup-table-modal.component';

import { WholesaleEclUploadsComponent } from './wholesaleInputs/wholesaleEclUploads/wholesaleEclUploads.component';
import { CreateOrEditWholesaleEclUploadModalComponent } from './wholesaleInputs/wholesaleEclUploads/create-or-edit-wholesaleEclUpload-modal.component';
import { WholesaleEclUploadWholesaleEclLookupTableModalComponent } from './wholesaleInputs/wholesaleEclUploads/wholesaleEclUpload-wholesaleEcl-lookup-table-modal.component';

import { OldAssumptionsComponent } from './eclShared/assumptions/assumptions.component';
import { CreateOrEditAssumptionModalComponent } from './eclShared/assumptions/create-or-edit-assumption-modal.component';

import { PdInputSnPCummulativeDefaultRatesComponent } from './eclShared/pdInputSnPCummulativeDefaultRates/pdInputSnPCummulativeDefaultRates.component';
import { CreateOrEditPdInputSnPCummulativeDefaultRateModalComponent } from './eclShared/pdInputSnPCummulativeDefaultRates/create-or-edit-pdInputSnPCummulativeDefaultRate-modal.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { PaginatorModule } from 'primeng/paginator';
import { EditorModule } from 'primeng/editor';
import { InputMaskModule } from 'primeng/inputmask'; import { FileUploadModule } from 'primeng/fileupload';
import { TableModule } from 'primeng/table';

import { UtilsModule } from '@shared/utils/utils.module';
import { CountoModule } from 'angular2-counto';
import { ModalModule, TabsModule, TooltipModule, BsDropdownModule, PopoverModule } from 'ngx-bootstrap';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MainRoutingModule } from './main-routing.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { BsDatepickerModule, BsDatepickerConfig, BsDaterangepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';
import { EclListComponent } from './ecl-list/ecl-list.component';
import { CreateEditRetailEclComponent } from './retail/createEdit-retailEcl/createEdit-retailEcl.component';
import { ViewRetailEclComponent } from './retail/view-retailEcl/view-retailEcl.component';
import { ApprovalModalComponent } from './eclShared/approve-ecl-modal/approve-ecl-modal.component';
import { AffiliateAssumptionComponent } from './assumptions/affiliateAssumption/affiliateAssumption.component';
import { EadInputAssumptionsComponent } from './assumptions/_subs/eadInputAssumptions/eadInputAssumptions.component';
import { LgdInputAssumptionsComponent } from './assumptions/_subs/lgdInputAssumptions/lgdInputAssumptions.component';
import { PdInputAssumptionsComponent } from './assumptions/_subs/pdInputAssumptions/pdInputAssumptions.component';
import { EditPortfolioReportDateComponent } from './assumptions/_subs/edit-portfolioReportDate/edit-portfolioReportDate.component';
import { EditAssumptionModalComponent } from './assumptions/_subs/edit-assumption-modal/edit-assumption-modal.component';
import { ViewLoanbookDetailsComponent } from './eclShared/view-loanbookDetails/view-loanbookDetails.component';
import { ViewPaymentScheduleComponent } from './eclShared/view-paymentSchedule/view-paymentSchedule.component';
import { ViewEclComponent } from './eclView/view-ecl/view-ecl.component';
import { EclOverrideComponent } from './eclView/_subs/ecl-override/ecl-override.component';
import { ApplyOverrideModalComponent } from './eclView/_subs/apply-override-modal/apply-override-modal.component';
import { WorkspaceComponent } from './workspace/workspace.component';
import { EclResultsComponent } from './eclView/_subs/ecl-results/ecl-results.component';
import { EclAuditInfoComponent } from './eclView/_subs/ecl-audit-info/ecl-audit-info.component';
import { ViewAssetBookDetailsComponent } from './eclShared/view-assetBookDetails/view-assetBookDetails.component';
import { OuLookupTableModalComponent } from './eclShared/ou-lookup-modal/ou-lookup-table-modal.component';
import { CalibrationEadCcfSummaryComponent } from './calibration/calibrateEadCcfSummary/calibrateEadCcfSummary.component';
import { ViewCalibrationEadCcfSummaryComponent } from './calibration/calibrateEadCcfSummary/view-calibrateEadCcfSummary.component';
import { CalibrationLgdHaircutComponent } from './calibration/calibrateLgdHairCut/calibrateLgdHaircut.component';
import { ViewCalibrationLgdHaircutComponent } from './calibration/calibrateLgdHairCut/view-calibrateLgdHaircut.component';
import { CalibrationLgdRecoveryComponent } from './calibration/calibrateLgdRecoveryRate/calibrateLgdRecovery.component';
import { ViewCalibrationLgdRecoveryComponent } from './calibration/calibrateLgdRecoveryRate/view-calibrateLgdRecovery.component';
import { CalibrationPdCrDrComponent } from './calibration/calibratePdCrDr/calibratePdCrDr.component';
import { ViewCalibrationPdCrDrComponent } from './calibration/calibratePdCrDr/view-calibratePdCrDr.component';
import { ApprovalMultipleModalComponent } from './eclShared/approve-multiple-modal/approve-multiple-modal.component';
import { MacroAnalysisComponent } from './calibration/calibrateMacroAnalysis/macroAnalysis.component';
import { ViewMacroAnalysisComponent } from './calibration/calibrateMacroAnalysis/view-macroAnalysis.component';
import { EditEclReportDateComponent } from './eclView/_subs/edit-EclReportDate/edit-EclReportDate.component';
import { LoanbookReaderComponent } from './eclView/_subs/loanbook-reader/loanbook-reader.component';
import { EditEadCcfResultModalComponent } from './calibration/calibrateEadCcfSummary/edit-eadccf-result-modal.component';
import { EditEadBehaviouralTermsResultModalComponent } from './calibration/calibrationEadBehaviouralTerms/edit-eadBehaviouralTerms-result-modal.component';
import { EditLgdHaircutResultModalComponent } from './calibration/calibrateLgdHairCut/edit-lgdHaircut-result-modal.component';
import { EditLgdRecoveryResultModalComponent } from './calibration/calibrateLgdRecoveryRate/edit-recoveryRate-result-modal.component';
import { EditCalibratePdCrDrResultModalComponent } from './calibration/calibratePdCrDr/edit-calibratePdCrDr-result-modal.component';
import { EditCalibratePdCrDrSummaryModalComponent } from './calibration/calibratePdCrDr/edit-calibratePdCrDr-summary-modal.component';

NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();

@NgModule({
    imports: [
        FileUploadModule,
        AutoCompleteModule,
        PaginatorModule,
        EditorModule,
        InputMaskModule, TableModule,

        CommonModule,
        FormsModule,
        ModalModule,
        TabsModule,
        TooltipModule,
        AppCommonModule,
        UtilsModule,
        MainRoutingModule,
        CountoModule,
        NgxChartsModule,
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot(),
        PopoverModule.forRoot()
    ],
    declarations: [
        LoanbookReaderComponent,
        EditEclReportDateComponent,
        MacroAnalysisComponent,
        ViewMacroAnalysisComponent,
        ApprovalMultipleModalComponent,
        ViewCalibrationPdCrDrComponent,
        CalibrationPdCrDrComponent,
        ViewCalibrationLgdRecoveryComponent,
        CalibrationLgdRecoveryComponent,
        ViewCalibrationLgdHaircutComponent,
        CalibrationLgdHaircutComponent,
        ViewCalibrationEadCcfSummaryComponent,
        CalibrationEadCcfSummaryComponent,
        OuLookupTableModalComponent,
        CalibrationEadBehaviouralTermsComponent,
        CreateOrEditCalibrationEadBehaviouralTermComponent,
        AffiliateMacroEconomicVariableOffsetsComponent,
        ViewAffiliateMacroEconomicVariableOffsetModalComponent, CreateOrEditAffiliateMacroEconomicVariableOffsetModalComponent,
        AffiliateMacroEconomicVariableOffsetOrganizationUnitLookupTableModalComponent,
        AffiliateMacroEconomicVariableOffsetMacroeconomicVariableLookupTableModalComponent,
        ViewAssetBookDetailsComponent,
        EclAuditInfoComponent,
        EclResultsComponent,
        WorkspaceComponent,
        AffiliateConfigurationComponent,
        ApplyOverrideModalComponent,
        EclOverrideComponent,
        ViewEclComponent,
        EclSettingsComponent,
        ViewPaymentScheduleComponent,
        ViewLoanbookDetailsComponent,
        AssumptionApprovalsComponent,
        ViewAssumptionApprovalModalComponent,
        EditAssumptionModalComponent,
        EditPortfolioReportDateComponent,
        MacroeconomicVariablesComponent,
        CreateOrEditMacroeconomicVariableModalComponent,
        PdInputAssumptionsComponent,
        LgdInputAssumptionsComponent,
        EadInputAssumptionsComponent,
        FrameworkAssumptionsComponent,
        ViewAffiliateAssumptionsComponent,
        AffiliateAssumptionComponent,
        ApprovalModalComponent,
        ViewRetailEclComponent,
        CreateEditRetailEclComponent,
        EclListComponent,
        WholesaleEclDataPaymentSchedulesComponent,
        CreateOrEditWholesaleEclDataPaymentScheduleModalComponent,
        WholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableModalComponent,
        WholesaleEclDataLoanBooksComponent,
        CreateOrEditWholesaleEclDataLoanBookModalComponent,
        WholesaleEclDataLoanBookWholesaleEclUploadLookupTableModalComponent,
        WholesaleEclUploadsComponent,
        CreateOrEditWholesaleEclUploadModalComponent,
        WholesaleEclUploadWholesaleEclLookupTableModalComponent,
        OldAssumptionsComponent,
        CreateOrEditAssumptionModalComponent,
        PdInputSnPCummulativeDefaultRatesComponent,
        CreateOrEditPdInputSnPCummulativeDefaultRateModalComponent,
        DashboardComponent,
        EditEadCcfResultModalComponent,
        EditEadBehaviouralTermsResultModalComponent,
        EditLgdHaircutResultModalComponent,
        EditLgdRecoveryResultModalComponent,
        EditCalibratePdCrDrResultModalComponent,
        EditCalibratePdCrDrSummaryModalComponent
    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class MainModule { }
