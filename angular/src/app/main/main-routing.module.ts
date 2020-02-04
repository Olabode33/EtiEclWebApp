import { ViewRetailEclComponent } from './retail/view-retailEcl/view-retailEcl.component';
import { NgModule } from '@angular/core';
import { InvestmentEclOverridesComponent } from './investmentComputation/investmentEclOverrides/investmentEclOverrides.component';
import { EclConfigurationsComponent } from './eclConfig/eclConfigurations/eclConfigurations.component';
import { AssumptionApprovalsComponent } from './assumptions/assumptionApprovals/assumptionApprovals.component';
import { MacroeconomicVariablesComponent } from './eclShared/macroeconomicVariables/macroeconomicVariables.component';
import { RouterModule } from '@angular/router';
import { WholesaleEclResultSummaryTopExposuresComponent } from './wholesaleResults/wholesaleEclResultSummaryTopExposures/wholesaleEclResultSummaryTopExposures.component';
import { WholesaleEclResultSummaryKeyInputsComponent } from './wholesaleResults/wholesaleEclResultSummaryKeyInputs/wholesaleEclResultSummaryKeyInputs.component';
import { WholesaleEclResultSummariesComponent } from './wholesaleResult/wholesaleEclResultSummaries/wholesaleEclResultSummaries.component';
import { WholesaleEclSicrsComponent } from './wholesaleComputation/wholesaleEclSicrs/wholesaleEclSicrs.component';
import { WholesaleEclDataPaymentSchedulesComponent } from './wholesaleInputs/wholesaleEclDataPaymentSchedules/wholesaleEclDataPaymentSchedules.component';
import { WholesaleEclDataLoanBooksComponent } from './wholesaleInputs/wholesaleEclDataLoanBooks/wholesaleEclDataLoanBooks.component';
import { WholesaleEclUploadsComponent } from './wholesaleInputs/wholesaleEclUploads/wholesaleEclUploads.component';
import { WholesaleEclPdSnPCummulativeDefaultRatesesComponent } from './wholesaleAssumption/wholesaleEclPdSnPCummulativeDefaultRateses/wholesaleEclPdSnPCummulativeDefaultRateses.component';
import { WholesaleEclPdAssumption12MonthsesComponent } from './wholesaleAssumption/wholesaleEclPdAssumption12Monthses/wholesaleEclPdAssumption12Monthses.component';
import { WholesaleEclsComponent } from './wholesale/wholesaleEcls/wholesaleEcls.component';
import { OldAssumptionsComponent } from './eclShared/assumptions/assumptions.component';
import { PdInputSnPCummulativeDefaultRatesComponent } from './eclShared/pdInputSnPCummulativeDefaultRates/pdInputSnPCummulativeDefaultRates.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EclListComponent } from './ecl-list/ecl-list.component';
import { CreateEditWholesaleEclComponent } from './wholesale/createEdit-wholesaleEcl/createEdit-wholesaleEcl.component';
import { ViewWholesaleEclComponent } from './wholesale/view-wholesaleEcl/view-wholesaleEcl.component';
import { CreateEditRetailEclComponent } from './retail/createEdit-retailEcl/createEdit-retailEcl.component';
import { AffiliateAssumptionComponent } from './assumptions/affiliateAssumption/affiliateAssumption.component';
import { ViewAffiliateAssumptionsComponent } from './assumptions/view-affiliateAssumptions/view-affiliateAssumptions.component';
import { ViewLoanbookDetailsComponent } from './eclShared/view-loanbookDetails/view-loanbookDetails.component';
import { ViewPaymentScheduleComponent } from './eclShared/view-paymentSchedule/view-paymentSchedule.component';
import { ViewEclComponent } from './eclView/view-ecl/view-ecl.component';
import { AffiliateConfigurationComponent } from './eclConfig/affiliate-configuration/affiliate-configuration.component';
import { WorkspaceComponent } from './workspace/workspace.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'investmentComputation/investmentEclOverrides', component: InvestmentEclOverridesComponent, data: { permission: 'Pages.InvestmentEclOverrides' }  },
                    { path: 'eclConfig/eclConfigurations', component: EclConfigurationsComponent, data: { permission: 'Pages.EclConfigurations' }  },
                    { path: 'eclConfig/affiliates', component: AffiliateConfigurationComponent, data: { permission: 'Pages.EclConfigurations' }  },
                    { path: 'config/macroeconomicVariables', component: MacroeconomicVariablesComponent, data: { permission: 'Pages.MacroeconomicVariables' }  },
                    { path: 'wholesaleResults/wholesaleEclResultSummaryTopExposures', component: WholesaleEclResultSummaryTopExposuresComponent, data: { permission: 'Pages.WholesaleEclResultSummaryTopExposures' }  },
                    { path: 'wholesaleResults/wholesaleEclResultSummaryKeyInputs', component: WholesaleEclResultSummaryKeyInputsComponent, data: { permission: 'Pages.WholesaleEclResultSummaryKeyInputs' }  },
                    { path: 'wholesaleResult/wholesaleEclResultSummaries', component: WholesaleEclResultSummariesComponent, data: { permission: 'Pages.WholesaleEclResultSummaries' }  },
                    { path: 'wholesaleComputation/wholesaleEclSicrs', component: WholesaleEclSicrsComponent, data: { permission: 'Pages.WholesaleEclSicrs' }  },
                    { path: 'wholesaleInputs/wholesaleEclDataPaymentSchedules', component: WholesaleEclDataPaymentSchedulesComponent, data: { permission: 'Pages.WholesaleEclDataPaymentSchedules' }  },
                    { path: 'wholesaleInputs/wholesaleEclDataLoanBooks', component: WholesaleEclDataLoanBooksComponent, data: { permission: 'Pages.WholesaleEclDataLoanBooks' }  },
                    { path: 'wholesaleInputs/wholesaleEclUploads', component: WholesaleEclUploadsComponent, data: { permission: 'Pages.WholesaleEclUploads' }  },
                    { path: 'wholesaleAssumption/wholesaleEclPdSnPCummulativeDefaultRateses', component: WholesaleEclPdSnPCummulativeDefaultRatesesComponent, data: { permission: 'Pages.WholesaleEclPdSnPCummulativeDefaultRateses' }  },
                    { path: 'wholesaleAssumption/wholesaleEclPdAssumption12Monthses', component: WholesaleEclPdAssumption12MonthsesComponent, data: { permission: 'Pages.WholesaleEclPdAssumption12Monthses' }  },
                    { path: 'wholesale/wholesaleEcls', component: WholesaleEclsComponent, data: { permission: 'Pages.WholesaleEcls' }  },
                    { path: 'assumption/affiliates', component: AffiliateAssumptionComponent, data: { permission: 'Pages.Assumption.Affiliates' }  },
                    { path: 'assumption/affiliates/view/:ouId', component: ViewAffiliateAssumptionsComponent, data: { permission: 'Pages.Assumption.Affiliates' }  },
                    { path: 'assumption/affiliates/approve/:ouId', component: AssumptionApprovalsComponent, data: { permission: 'Pages.Assumption.Affiliates' }  },
                    { path: 'eclShared/pdInputSnPCummulativeDefaultRates', component: PdInputSnPCummulativeDefaultRatesComponent, data: { permission: 'Pages.PdInputSnPCummulativeDefaultRates' }  },
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } },
                    { path: 'workspace', component: EclListComponent },
                    { path: 'home', component: WorkspaceComponent },
                    { path: 'wholesale/ecl/create', component: CreateEditWholesaleEclComponent},
                    { path: 'wholesale/ecl/view/:eclId', component: ViewWholesaleEclComponent},
                    { path: 'retail/ecl/create', component: CreateEditRetailEclComponent},
                    { path: 'retail/ecl/view/:eclId', component: ViewRetailEclComponent},
                    { path: 'ecl/view/:framework/:eclId', component: ViewEclComponent},
                    { path: 'ecl/view/upload/loanbook/:framework/:uploadId', component: ViewLoanbookDetailsComponent},
                    { path: 'ecl/view/upload/payment/:framework/:uploadId', component: ViewPaymentScheduleComponent}
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
