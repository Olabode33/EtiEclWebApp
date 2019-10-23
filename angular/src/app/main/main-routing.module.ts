import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
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
