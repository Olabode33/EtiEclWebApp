import { Component, ViewChild, Injector, Output, EventEmitter, ViewEncapsulation } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GeneralStatusEnum, FrameworkEnum, AssumptionTypeEnum, DataTypeEnum } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Observable } from 'rxjs';
import * as _ from 'lodash';
import { finalize } from 'rxjs/operators';

export interface IEditAssumptionModalOptions {
    serviceProxy: any;
    dataSource: any;
    framework: FrameworkEnum;
    affiliateName: string;
    assumption: AssumptionTypeEnum;
    assumptionGroup: string;
}

@Component({
    selector: 'app-edit-assumption-modal',
    templateUrl: './edit-assumption-modal.component.html',
    styleUrls: ['./edit-assumption-modal.component.css']
})
export class EditAssumptionModalComponent extends AppComponentBase {

    static defaultOptions: IEditAssumptionModalOptions = {
        serviceProxy: undefined,
        dataSource: undefined,
        framework: undefined,
        affiliateName: undefined,
        assumption: undefined,
        assumptionGroup: undefined
    };

    @ViewChild('editAssumptionModal', { static: true }) modal: ModalDirective;

    @Output() saveAssumption: EventEmitter<any> = new EventEmitter<any>();
    active = false;
    saving = false;
    isShown = false;
    generalStatusEnum = GeneralStatusEnum;
    frameworkEnum = FrameworkEnum;
    assumptionTypeEnum = AssumptionTypeEnum;

    options: IEditAssumptionModalOptions = _.merge({});
    dataSource: any;
    serviceProxy: any;
    affiliateFramework: FrameworkEnum;
    affiliateName = '';
    selectedAssumption: AssumptionTypeEnum;
    assumptionGroup = '';
    title: string;

    constructor(
        injector: Injector
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
        console.log(options);
    }

    show(): void {
        if (!this.options) {
            throw Error('EditAssumptionDateComponentError');
        }

        this.modal.show();
    }

    shown(): void {
        this.isShown = true;
    }

    close(): void {
        this.modal.hide();
    }

    save(): void {
        this.message.confirm(
            this.l('EditAssumptionNote'),
            (isConfirmed) => {
                if (this.hasProp('status')) {
                    this.dataSource.status = GeneralStatusEnum.Submitted;
                }
                this.serviceProxy.createOrEdit(this.dataSource).subscribe(() => {
                    this.notify.success(this.l('SubmittedSuccessfully'));
                    this.saveAssumption.emit(this.dataSource);
                    this.close();
                });
            });
    }

    hasProp(prop: string): boolean {
        if (this.dataSource !== undefined) {
            //return Object.prototype.hasOwnProperty.call(this.dataSource, prop);
            return prop in this.dataSource;
        }
        return false;
    }

    isDropdownDataType(): boolean {
        if (this.hasProp('key')) {
            //console.log(this.dataSource.key);
            switch (this.dataSource.key) {
                case 'AbsoluteCreditQualityCriteria':
                case 'RelativeCreditQualityCriteria':
                case 'UseWatchlistIndicator':
                case 'UseRestructureIndicator?':
                    //console.log(true);
                    return true;
                default:
                    return false;
            }
        }
        if (this.hasProp('dataType')) {
            return this.dataSource.dataType === DataTypeEnum.StringDropdown;
        }
        return false;
    }

    isNumberDataType(): boolean {
        if (this.hasProp('dataType')) {
            //console.log(this.dataSource.dataType);
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
        if (this.hasProp('datatype')) {
            switch (this.dataSource.datatype) {
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
    }


    typeOfProp(): boolean {
        if (this.dataSource !== undefined) {
            console.log(typeof(this.dataSource.assumptionGroup));
        }
        return false;
    }

}
