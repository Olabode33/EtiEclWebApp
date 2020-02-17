import { Component, OnInit, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { InvestmentEclResultsServiceProxy, FrameworkEnum, GeneralStatusEnum, ViewEclResultSummaryDto, ViewEclResultDetailsDto, EclResultOverrideFigures, EclStatusEnum } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-ecl-results',
  templateUrl: './ecl-results.component.html',
  styleUrls: ['./ecl-results.component.css']
})
export class EclResultsComponent extends AppComponentBase {

    show = false;

    _serviceProxy: any;
    _eclId = '';
    _eclFramework: FrameworkEnum;
    _eclStatus: EclStatusEnum;

    resultSummary: ViewEclResultSummaryDto;
    top20Exposures: ViewEclResultDetailsDto[];

    eclStatusEnum = EclStatusEnum;
    frameworkEnum = FrameworkEnum;
    showFakeTopExposure = false;

    constructor(
        injector: Injector,
        private _investmentEclResultServiceProxy: InvestmentEclResultsServiceProxy
    ) {
        super(injector);
        this.resultSummary = new ViewEclResultSummaryDto();
        this.top20Exposures = new Array<ViewEclResultDetailsDto>();
    }

    load(eclId: string, framework: FrameworkEnum): void {
        this._eclId = eclId;
        this._eclFramework = framework;
        this.show = false;

        this.configureServiceProxy();
        this.getResultSummary();
        this.getTop20Exposure();
    }

    displayResult(eclStatus: EclStatusEnum): void {
        this._eclStatus = eclStatus;
        switch (eclStatus) {
            case EclStatusEnum.PreOverrideComplete:
            case EclStatusEnum.PostOverrideComplete:
            case EclStatusEnum.Completed:
                this.show = true;
                break;
            default:
                this.show = false;
                break;
        }
    }

    configureServiceProxy(): void {
        switch (this._eclFramework) {
            case FrameworkEnum.Investments:
                this._serviceProxy = this._investmentEclResultServiceProxy;
                break;
            default:
                //throw Error('FrameworkDoesNotExistError');
                this._serviceProxy = this._investmentEclResultServiceProxy;
                break;
        }
    }

    getResultSummary(): void {
        this._serviceProxy.getResultSummary(this._eclId).subscribe(result => {
            this.resultSummary = result;
        });
    }

    getTop20Exposure(): void {
        this._serviceProxy.getTop20Exposure(this._eclId).subscribe(result => {
            this.top20Exposures = result;
        });
    }
}