import { MacroAnalysisComponent } from './calibration/calibrateMacroAnalysis/macroAnalysis.component';
import { ViewCalibrationEadCcfSummaryComponent } from './calibration/calibrateEadCcfSummary/view-calibrateEadCcfSummary.component';
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

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
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
