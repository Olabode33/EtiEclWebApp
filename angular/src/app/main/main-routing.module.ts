import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { WholesaleEclPdSnPCummulativeDefaultRatesesComponent } from './wholesaleAssumption/wholesaleEclPdSnPCummulativeDefaultRateses/wholesaleEclPdSnPCummulativeDefaultRateses.component';
import { WholesaleEclPdAssumption12MonthsesComponent } from './wholesaleAssumption/wholesaleEclPdAssumption12Monthses/wholesaleEclPdAssumption12Monthses.component';
import { WholesaleEclsComponent } from './wholesale/wholesaleEcls/wholesaleEcls.component';
import { AssumptionsComponent } from './eclShared/assumptions/assumptions.component';
import { PdInputSnPCummulativeDefaultRatesComponent } from './eclShared/pdInputSnPCummulativeDefaultRates/pdInputSnPCummulativeDefaultRates.component';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'wholesaleAssumption/wholesaleEclPdSnPCummulativeDefaultRateses', component: WholesaleEclPdSnPCummulativeDefaultRatesesComponent, data: { permission: 'Pages.WholesaleEclPdSnPCummulativeDefaultRateses' }  },
                    { path: 'wholesaleAssumption/wholesaleEclPdAssumption12Monthses', component: WholesaleEclPdAssumption12MonthsesComponent, data: { permission: 'Pages.WholesaleEclPdAssumption12Monthses' }  },
                    { path: 'wholesale/wholesaleEcls', component: WholesaleEclsComponent, data: { permission: 'Pages.WholesaleEcls' }  },
                    { path: 'eclShared/assumptions', component: AssumptionsComponent, data: { permission: 'Pages.Assumptions' }  },
                    { path: 'eclShared/pdInputSnPCummulativeDefaultRates', component: PdInputSnPCummulativeDefaultRatesComponent, data: { permission: 'Pages.PdInputSnPCummulativeDefaultRates' }  },
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
