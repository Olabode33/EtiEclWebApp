import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
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

import { WholesaleEclPdSnPCummulativeDefaultRatesesComponent } from './wholesaleAssumption/wholesaleEclPdSnPCummulativeDefaultRateses/wholesaleEclPdSnPCummulativeDefaultRateses.component';
import { CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesModalComponent } from './wholesaleAssumption/wholesaleEclPdSnPCummulativeDefaultRateses/create-or-edit-wholesaleEclPdSnPCummulativeDefaultRates-modal.component';
import { WholesaleEclPdSnPCummulativeDefaultRatesWholesaleEclLookupTableModalComponent } from './wholesaleAssumption/wholesaleEclPdSnPCummulativeDefaultRateses/wholesaleEclPdSnPCummulativeDefaultRates-wholesaleEcl-lookup-table-modal.component';

import { WholesaleEclPdAssumption12MonthsesComponent } from './wholesaleAssumption/wholesaleEclPdAssumption12Monthses/wholesaleEclPdAssumption12Monthses.component';
import { CreateOrEditWholesaleEclPdAssumption12MonthsModalComponent } from './wholesaleAssumption/wholesaleEclPdAssumption12Monthses/create-or-edit-wholesaleEclPdAssumption12Months-modal.component';
import { WholesaleEclPdAssumption12MonthsWholesaleEclLookupTableModalComponent } from './wholesaleAssumption/wholesaleEclPdAssumption12Monthses/wholesaleEclPdAssumption12Months-wholesaleEcl-lookup-table-modal.component';

import { WholesaleEclsComponent } from './wholesale/wholesaleEcls/wholesaleEcls.component';
import { CreateOrEditWholesaleEclModalComponent } from './wholesale/wholesaleEcls/create-or-edit-wholesaleEcl-modal.component';
import { WholesaleEclUserLookupTableModalComponent } from './wholesale/wholesaleEcls/wholesaleEcl-user-lookup-table-modal.component';

import { AssumptionsComponent } from './eclShared/assumptions/assumptions.component';
import { CreateOrEditAssumptionModalComponent } from './eclShared/assumptions/create-or-edit-assumption-modal.component';

import { PdInputSnPCummulativeDefaultRatesComponent } from './eclShared/pdInputSnPCummulativeDefaultRates/pdInputSnPCummulativeDefaultRates.component';
import { CreateOrEditPdInputSnPCummulativeDefaultRateModalComponent } from './eclShared/pdInputSnPCummulativeDefaultRates/create-or-edit-pdInputSnPCummulativeDefaultRate-modal.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { PaginatorModule } from 'primeng/paginator';
import { EditorModule } from 'primeng/editor';
import { InputMaskModule } from 'primeng/inputmask';import { FileUploadModule } from 'primeng/fileupload';
import { TableModule } from 'primeng/table';

import { UtilsModule } from '@shared/utils/utils.module';
import { CountoModule } from 'angular2-counto';
import { ModalModule, TabsModule, TooltipModule, BsDropdownModule, PopoverModule } from 'ngx-bootstrap';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MainRoutingModule } from './main-routing.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { BsDatepickerModule, BsDatepickerConfig, BsDaterangepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';

NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();

@NgModule({
    imports: [
		FileUploadModule,
		AutoCompleteModule,
		PaginatorModule,
		EditorModule,
		InputMaskModule,		TableModule,

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
		WholesaleEclDataPaymentSchedulesComponent,
		CreateOrEditWholesaleEclDataPaymentScheduleModalComponent,
    WholesaleEclDataPaymentScheduleWholesaleEclUploadLookupTableModalComponent,
		WholesaleEclDataLoanBooksComponent,
		CreateOrEditWholesaleEclDataLoanBookModalComponent,
    WholesaleEclDataLoanBookWholesaleEclUploadLookupTableModalComponent,
		WholesaleEclUploadsComponent,
		CreateOrEditWholesaleEclUploadModalComponent,
    WholesaleEclUploadWholesaleEclLookupTableModalComponent,
		WholesaleEclPdSnPCummulativeDefaultRatesesComponent,
		CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesModalComponent,
    WholesaleEclPdSnPCummulativeDefaultRatesWholesaleEclLookupTableModalComponent,
		WholesaleEclPdAssumption12MonthsesComponent,
		CreateOrEditWholesaleEclPdAssumption12MonthsModalComponent,
    WholesaleEclPdAssumption12MonthsWholesaleEclLookupTableModalComponent,
		WholesaleEclsComponent,
		CreateOrEditWholesaleEclModalComponent,
    WholesaleEclUserLookupTableModalComponent,
		AssumptionsComponent,
		CreateOrEditAssumptionModalComponent,
		PdInputSnPCummulativeDefaultRatesComponent,
		CreateOrEditPdInputSnPCummulativeDefaultRateModalComponent,
        DashboardComponent
    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class MainModule { }
