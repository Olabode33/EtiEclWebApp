import { EclApprovalAuditInfoDto, EclAuditInfoDto } from './../../../../../shared/service-proxies/service-proxies';
import { Component, OnInit, Injector } from '@angular/core';
import { InvestmentEclApprovalsServiceProxy, FrameworkEnum, EclStatusEnum, ViewEclResultSummaryDto, ViewEclResultDetailsDto, GeneralStatusEnum } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-ecl-audit-info',
  templateUrl: './ecl-audit-info.component.html',
  styleUrls: ['./ecl-audit-info.component.css']
})
export class EclAuditInfoComponent extends AppComponentBase {

    show = false;

    _serviceProxy: any;
    _eclId = '';
    _eclFramework: FrameworkEnum;
    _eclStatus: EclStatusEnum;

    auditInfo: EclAuditInfoDto;
    approvalsAuditInfo: EclApprovalAuditInfoDto[];

    eclStatusEnum = EclStatusEnum;
    genStatusEnum = GeneralStatusEnum;
    frameworkEnum = FrameworkEnum;
    showFakeTopExposure = false;

    constructor(
        injector: Injector,
        private _investmentEclApprovalServiceProxy: InvestmentEclApprovalsServiceProxy
    ) {
        super(injector);
        this.auditInfo = new EclAuditInfoDto();
        this.approvalsAuditInfo = new Array<EclApprovalAuditInfoDto>();
    }

    load(eclId: string, framework: FrameworkEnum): void {
        this._eclId = eclId;
        this._eclFramework = framework;
        this.show = false;

        this.configureServiceProxy();
        this.getApprovalAuditInformation();
    }

    configureServiceProxy(): void {
        //TODO: Update service proxy for the different portfolio
        switch (this._eclFramework) {
            case FrameworkEnum.Wholesale:
                this._serviceProxy = this._investmentEclApprovalServiceProxy;
                break;
            case FrameworkEnum.Retail:
                this._serviceProxy = this._investmentEclApprovalServiceProxy;
                break;
            case FrameworkEnum.OBE:
                this._serviceProxy = this._investmentEclApprovalServiceProxy;
                break;
            case FrameworkEnum.Investments:
                this._serviceProxy = this._investmentEclApprovalServiceProxy;
                break;
            default:
                //throw Error('FrameworkDoesNotExistError');
                this._serviceProxy = this._investmentEclApprovalServiceProxy;
                break;
        }
    }

    getApprovalAuditInformation(): void {
        this._investmentEclApprovalServiceProxy.getEclAudit(this._eclId).subscribe(result => {
            this.auditInfo = result;
            this.approvalsAuditInfo = result.approvals;
        });
    }

    hasProp(prop: string, obj: any): boolean {
        if (obj !== undefined) {
            //return Object.prototype.hasOwnProperty.call(this.dataSource, prop);
            return prop in obj;
        }
        return false;
    }
}
