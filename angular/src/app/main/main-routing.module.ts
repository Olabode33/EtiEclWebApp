import { NgModule } from '@angular/core';
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
import { AssumptionsComponent } from './eclShared/assumptions/assumptions.component';
import { PdInputSnPCummulativeDefaultRatesComponent } from './eclShared/pdInputSnPCummulativeDefaultRates/pdInputSnPCummulativeDefaultRates.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { WorkspaceComponent } from './workspace/workspace.component';
import { CreateEditWholesaleEclComponent } from './wholesale/createEdit-wholesaleEcl/createEdit-wholesaleEcl.component';
import { ViewWholesaleEclComponent } from './wholesale/view-wholesaleEcl/view-wholesaleEcl.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
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
                    { path: 'eclShared/assumptions', component: AssumptionsComponent, data: { permission: 'Pages.Assumptions' }  },
                    { path: 'eclShared/pdInputSnPCummulativeDefaultRates', component: PdInputSnPCummulativeDefaultRatesComponent, data: { permission: 'Pages.PdInputSnPCummulativeDefaultRates' }  },
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } },
                    { path: 'workspace', component: WorkspaceComponent },
                    { path: 'wholesale/ecl/create', component: CreateEditWholesaleEclComponent},
                    { path: 'wholesale/ecl/view/:eclId', component: ViewWholesaleEclComponent}
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
