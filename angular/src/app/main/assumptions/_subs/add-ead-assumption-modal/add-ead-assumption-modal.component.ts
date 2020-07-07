import { Component, OnInit, ViewChild, Output, EventEmitter, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { IEditAssumptionModalOptions, EditAssumptionModalComponent } from '../edit-assumption-modal/edit-assumption-modal.component';
import { ModalDirective } from 'ngx-bootstrap';
import { GeneralStatusEnum, FrameworkEnum, AssumptionTypeEnum, DataTypeEnum, CreateOrEditEadInputAssumptionDto, EadInputAssumptionsServiceProxy, EadInputAssumptionGroupEnum } from '@shared/service-proxies/service-proxies';
import * as _ from 'lodash';

@Component({
  selector: 'app-add-ead-assumption-modal',
  templateUrl: './add-ead-assumption-modal.component.html',
  styleUrls: ['./add-ead-assumption-modal.component.css']
})
export class AddEadAssumptionModalComponent extends AppComponentBase  {

    static defaultOptions: IEditAssumptionModalOptions = {
        serviceProxy: undefined,
        dataSource: undefined,
        framework: undefined,
        affiliateName: undefined,
        assumption: undefined,
        assumptionGroup: undefined
    };

    @ViewChild('addAssumptionModal', { static: true }) modal: ModalDirective;

    @Output() saveAssumption: EventEmitter<any> = new EventEmitter<any>();
    active = false;
    saving = false;
    isShown = false;
    generalStatusEnum = GeneralStatusEnum;
    frameworkEnum = FrameworkEnum;
    assumptionTypeEnum = AssumptionTypeEnum;
    dataTypeEnum = DataTypeEnum;

    options: IEditAssumptionModalOptions = _.merge({});
    dataSource: CreateOrEditEadInputAssumptionDto = new CreateOrEditEadInputAssumptionDto();
    serviceProxy: any;
    affiliateFramework: FrameworkEnum;
    affiliateName = '';
    selectedAssumption: AssumptionTypeEnum;
    assumptionGroup = '';
    title: string;

    inputLabel = '';
    valueLabel = '';

    constructor(
        injector: Injector,
        private _eadAssumptionServiceProxy: EadInputAssumptionsServiceProxy
    ) {
        super(injector);
    }

    configure(options: IEditAssumptionModalOptions): void {
        options = _.merge({}, EditAssumptionModalComponent.defaultOptions, options);
        this.dataSource = options.dataSource;
        this.serviceProxy = options.serviceProxy;
        this.affiliateFramework = options.framework;
        this.affiliateName = options.affiliateName;
        this.selectedAssumption = options.assumption;
        this.assumptionGroup = options.assumptionGroup;
        //console.log(options);
    }

    show(): void {
        if (!this.options) {
            throw Error('AddAssumptionDateComponentError');
        }

        this.getLabel();
        this.active = true;
        this.modal.show();
    }

    shown(): void {
        this.isShown = true;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    save(): void {
        this.message.confirm('',
            (isConfirmed) => {
                this._eadAssumptionServiceProxy.createOrEdit(this.dataSource).subscribe(() => {
                    this.notify.success(this.l('SubmittedSuccessfully'));
                    this.saveAssumption.emit(this.dataSource);
                    this.close();
                });
            });
    }

    getLabel(): void {
        switch (this.dataSource.eadGroup) {
            case EadInputAssumptionGroupEnum.ExchangeRateProjections:
                this.inputLabel = 'Currency';
                this.valueLabel = 'Rate';
                break;
            case EadInputAssumptionGroupEnum.VariableInterestRateProjections:
                this.inputLabel = 'VIR Name';
                this.valueLabel = 'Value';
                break;
            default:
                break;
        }
    }

    isNumberDataType(): boolean {
        switch (this.dataSource.dataType) {
            case DataTypeEnum.Double:
            case DataTypeEnum.DoubleDropDown:
            case DataTypeEnum.DoubleMoney:
            case DataTypeEnum.DoublePercentage:
            case DataTypeEnum.Int:
            case DataTypeEnum.IntDropdown:
                //console.log(true);
                return true;
            default:
                return false;
        }
    }

    hasProp(prop: string): boolean {
        if (this.dataSource !== undefined) {
            //return Object.prototype.hasOwnProperty.call(this.dataSource, prop);
            return prop in this.dataSource;
        }
        return false;
    }

}
