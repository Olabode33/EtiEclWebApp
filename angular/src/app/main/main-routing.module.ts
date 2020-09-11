import { MacroAnalysisComponent } from './calibration/calibrateMacroAnalysis/macroAnalysis.component';
import { ViewCalibrationEadCcfSummaryComponent } from './calibration/calibrateEadCcfSummary/view-calibrateEadCcfSummary.component';
import { ViewLoanImpairmentRegisterComponent } from './loanImpairmentsRegisters/loanImpairmentRegisters/view-loanImpairmentRegister.component';
import { LoanImpairmentRegistersComponent } from './loanImpairmentsRegisters/loanImpairmentRegisters/loanImpairmentRegisters.component';
import { CreateOrEditLoanImpairmentRegisterComponent } from './loanImpairmentsRegisters/loanImpairmentRegisters/create-or-edit-loanImpairmentRegister.component';
import { ViewReceivablesRegisterComponent } from './ivModels/receivablesRegisters/view-receivablesRegister.component';
import { ReceivablesRegistersComponent } from './ivModels/receivablesRegisters/receivablesRegisters.component';
import { CreateOrEditReceivablesRegisterComponent } from './ivModels/receivablesRegisters/create-or-edit-receivablesRegister.component';
import { ResultSummaryByStagesComponent } from './ivModels/resultSummaryByStages/resultSummaryByStages.component';
import { HoldCoRegistersComponent } from './ivModels/holdCoRegisters/holdCoRegisters.component';
import { CreateOrEditHoldCoRegisterComponent } from './ivModels/holdCoRegisters/create-or-edit-holdCoRegister.component';
import { ViewHoldCoRegisterComponent } from './ivModels/holdCoRegisters/view-holdCoRegister.component';
import { OverrideTypesComponent } from './eclConfig/overrideTypes/overrideTypes.component';
import { CalibrationEadCcfSummaryComponent } from './calibration/calibrateEadCcfSummary/calibrateEadCcfSummary.component';
import { ViewRetailEclComponent } from './retail/view-retailEcl/view-retailEcl.component';
import { NgModule } from '@angular/core';
import { CalibrationEadBehaviouralTermsComponent } from './calibration/calibrationEadBehaviouralTerms/calibrationEadBehaviouralTerms.component';
import { CreateOrEditCalibrationEadBehaviouralTermComponent } from './calibration/calibrationEadBehaviouralTerms/create-or-edit-calibrationEadBehaviouralTerm.component';
import { AffiliateMacroEconomicVariableOffsetsComponent } from './affiliateMacroeconomicVariable/affiliateMacroVariable/affiliateMacroEconomicVariableOffsets.component';
import { EclSettingsComponent } from './eclConfig/eclSettings/eclSettings.component';
import { AssumptionApprovalsComponent } from './assumptions/assumptionApprovals/assumptionApprovals.component';
import { MacroeconomicVariablesComponent } from './eclShared/macroeconomicVariables/macroeconomicVariables.component';
import { RouterModule } from '@angular/router';
import { WholesaleEclDataPaymentSchedulesComponent } from './wholesaleInputs/wholesaleEclDataPaymentSchedules/wholesaleEclDataPaymentSchedules.component';
import { WholesaleEclDataLoanBooksComponent } from './wholesaleInputs/wholesaleEclDataLoanBooks/wholesaleEclDataLoanBooks.component';
import { WholesaleEclUploadsComponent } from './wholesaleInputs/wholesaleEclUploads/wholesaleEclUploads.component';
import { OldAssumptionsComponent } from './eclShared/assumptions/assumptions.component';
import { PdInputSnPCummulativeDefaultRatesComponent } from './eclShared/pdInputSnPCummulativeDefaultRates/pdInputSnPCummulativeDefaultRates.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EclListComponent } from './ecl-list/ecl-list.component';
import { CreateEditRetailEclComponent } from './retail/createEdit-retailEcl/createEdit-retailEcl.component';
import { AffiliateAssumptionComponent } from './assumptions/affiliateAssumption/affiliateAssumption.component';
import { ViewAffiliateAssumptionsComponent } from './assumptions/view-affiliateAssumptions/view-affiliateAssumptions.component';
import { ViewLoanbookDetailsComponent } from './eclShared/view-loanbookDetails/view-loanbookDetails.component';
import { ViewPaymentScheduleComponent } from './eclShared/view-paymentSchedule/view-paymentSchedule.component';
import { ViewEclComponent } from './eclView/view-ecl/view-ecl.component';
import { AffiliateConfigurationComponent } from './eclConfig/affiliate-configuration/affiliate-configuration.component';
import { WorkspaceComponent } from './workspace/workspace.component';
import { ViewAssetBookDetailsComponent } from './eclShared/view-assetBookDetails/view-assetBookDetails.component';
import { CalibrationLgdHaircutComponent } from './calibration/calibrateLgdHairCut/calibrateLgdHaircut.component';
import { ViewCalibrationLgdHaircutComponent } from './calibration/calibrateLgdHairCut/view-calibrateLgdHaircut.component';
import { CalibrationLgdRecoveryComponent } from './calibration/calibrateLgdRecoveryRate/calibrateLgdRecovery.component';
import { ViewCalibrationLgdRecoveryComponent } from './calibration/calibrateLgdRecoveryRate/view-calibrateLgdRecovery.component';
import { CalibrationPdCrDrComponent } from './calibration/calibratePdCrDr/calibratePdCrDr.component';
import { ViewCalibrationPdCrDrComponent } from './calibration/calibratePdCrDr/view-calibratePdCrDr.component';
import { ViewMacroAnalysisComponent } from './calibration/calibrateMacroAnalysis/view-macroAnalysis.component';
import { ViewBatchEclComponent } from './eclView/view-batchEcl/view-batchEcl.component';
import { HoldCoResultSummariesComponent } from './ivModels/holdCoResultSummaries/holdCoResultSummaries.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'loanImpairmentsRegisters/loanImpairmentRegisters/view', component: ViewLoanImpairmentRegisterComponent, data: { permission: 'Pages.LoanImpairmentRegisters' }  },
                    { path: 'loanImpairmentsRegisters/loanImpairmentRegisters', component: LoanImpairmentRegistersComponent, data: { permission: 'Pages.LoanImpairmentRegisters' }  },
                    { path: 'loanImpairmentsRegisters/loanImpairmentRegisters/createOrEdit', component: CreateOrEditLoanImpairmentRegisterComponent, data: { permission: 'Pages.LoanImpairmentRegisters.Create' }  },
                    { path: 'receivablesRegisters/receivablesRegisters/view', component: ViewReceivablesRegisterComponent, data: { permission: 'Pages.ReceivablesRegisters' }  },
                    { path: 'receivablesRegisters/receivablesRegisters', component: ReceivablesRegistersComponent, data: { permission: 'Pages.ReceivablesRegisters' }  },
                    { path: 'receivablesRegisters/receivablesRegisters/createOrEdit', component: CreateOrEditReceivablesRegisterComponent, data: { permission: 'Pages.ReceivablesRegisters.Create' }  },
                    { path: 'holdCoResult/resultSummaryByStages', component: ResultSummaryByStagesComponent, data: { permission: 'Pages.ResultSummaryByStages' }  },
                    { path: 'holdCoResult/holdCoResultSummaries', component: HoldCoResultSummariesComponent, data: { permission: 'Pages.HoldCoResultSummaries' }  },
                    { path: 'ivModels/holdCoRegisters', component: HoldCoRegistersComponent, data: { permission: 'Pages.HoldCoRegisters' }  },
                    { path: 'ivModels/holdCoRegisters/createOrEdit', component: CreateOrEditHoldCoRegisterComponent, data: { permission: 'Pages.HoldCoRegisters.Create' }  },
                    { path: 'ivModels/holdCoRegisters/view', component: ViewHoldCoRegisterComponent, data: { permission: 'Pages.HoldCoRegisters' }  },
                    { path: 'eclConfig/overrideTypes', component: OverrideTypesComponent, data: { permission: 'Pages.Configurations' }  },
                    { path: 'calibration/behavioralTerms', component: CalibrationEadBehaviouralTermsComponent, data: { permission: 'Pages.Calibration' }  },
                    { path: 'calibration/behavioralTerms/view/:calibrationId', component: CreateOrEditCalibrationEadBehaviouralTermComponent, data: { permission: 'Pages.Calibration' }  },
                    { path: 'calibration/ccfSummary', component: CalibrationEadCcfSummaryComponent, data: { permission: 'Pages.Calibration' }  },
                    { path: 'calibration/ccfSummary/view/:calibrationId', component: ViewCalibrationEadCcfSummaryComponent, data: { permission: 'Pages.Calibration' }  },
                    { path: 'calibration/haircut', component: CalibrationLgdHaircutComponent, data: { permission: 'Pages.Calibration' }  },
                    { path: 'calibration/haircut/view/:calibrationId', component: ViewCalibrationLgdHaircutComponent, data: { permission: 'Pages.Calibration' }  },
                    { path: 'calibration/recovery', component: CalibrationLgdRecoveryComponent, data: { permission: 'Pages.Calibration' }  },
                    { path: 'calibration/recovery/view/:calibrationId', component: ViewCalibrationLgdRecoveryComponent, data: { permission: 'Pages.Calibration' }  },
                    { path: 'calibration/pdcrdr', component: CalibrationPdCrDrComponent, data: { permission: 'Pages.Calibration' }  },
                    { path: 'calibration/pdcrdr/view/:calibrationId', component: ViewCalibrationPdCrDrComponent, data: { permission: 'Pages.Calibration' }  },
                    { path: 'calibration/macroAnalysis', component: MacroAnalysisComponent, data: { permission: 'Pages.Calibration' }  },
                    { path: 'calibration/macroAnalysis/view/:calibrationId', component: ViewMacroAnalysisComponent, data: { permission: 'Pages.Calibration' }  },

                    { path: 'affiliateMacroEconomicVariable/affiliateMacroEconomicVariableOffsets', component: AffiliateMacroEconomicVariableOffsetsComponent, data: { permission: 'Pages.Configurations' }  },
                    { path: 'eclConfig/eclConfigurations', component: EclSettingsComponent, data: { permission: 'Pages.Configurations' }  },
                    { path: 'eclConfig/affiliates', component: AffiliateConfigurationComponent, data: { permission: 'Pages.Configurations' }  },
                    { path: 'config/macroeconomicVariables', component: MacroeconomicVariablesComponent, data: { permission: 'Pages.Configurations' }  },
                    { path: 'wholesaleInputs/wholesaleEclDataPaymentSchedules', component: WholesaleEclDataPaymentSchedulesComponent, data: { permission: 'Pages.WholesaleEclDataPaymentSchedules' }  },
                    { path: 'wholesaleInputs/wholesaleEclDataLoanBooks', component: WholesaleEclDataLoanBooksComponent, data: { permission: 'Pages.WholesaleEclDataLoanBooks' }  },
                    { path: 'wholesaleInputs/wholesaleEclUploads', component: WholesaleEclUploadsComponent, data: { permission: 'Pages.WholesaleEclUploads' }  },
                    { path: 'assumption/affiliates', component: AffiliateAssumptionComponent, data: { permission: 'Pages.Assumption.Affiliates' }  },
                    { path: 'assumption/affiliates/:filter', component: AffiliateAssumptionComponent, data: { permission: 'Pages.Assumption.Affiliates' }  },
                    { path: 'assumption/affiliates/view/:ouId', component: ViewAffiliateAssumptionsComponent, data: { permission: 'Pages.Assumption.Affiliates' }  },
                    { path: 'assumption/affiliates/approve/:ouId', component: AssumptionApprovalsComponent, data: { permission: 'Pages.Assumption.Affiliates' }  },
                    { path: 'eclShared/pdInputSnPCummulativeDefaultRates', component: PdInputSnPCummulativeDefaultRatesComponent, data: { permission: 'Pages.PdInputSnPCummulativeDefaultRates' }  },
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } },
                    { path: 'workspace', component: EclListComponent },
                    { path: 'home', component: WorkspaceComponent },
                    { path: 'ecl', component: EclListComponent,  data: { permission: 'Pages.EclView' }  },
                    { path: 'ecl/:filter', component: EclListComponent },
                    { path: 'retail/ecl/create', component: CreateEditRetailEclComponent},
                    { path: 'retail/ecl/view/:eclId', component: ViewRetailEclComponent},
                    { path: 'ecl/view/batch/:eclId', component: ViewBatchEclComponent,  data: { permission: 'Pages.EclView' } },
                    { path: 'ecl/view/:framework/:eclId', component: ViewEclComponent,  data: { permission: 'Pages.EclView' } },
                    { path: 'ecl/view/upload/loanbook/:framework/:uploadId', component: ViewLoanbookDetailsComponent},
                    { path: 'ecl/view/upload/payment/:framework/:uploadId', component: ViewPaymentScheduleComponent},
                    { path: 'ecl/view/upload/assetbook/:framework/:uploadId', component: ViewAssetBookDetailsComponent}
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
