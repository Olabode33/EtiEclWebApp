import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { HoldCoRegistersServiceProxy, GetHoldCoRegisterForViewDto, HoldCoRegisterDto , CalibrationStatusEnum} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ActivatedRoute } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';

@Component({
    templateUrl: './view-holdCoRegister.component.html',
    animations: [appModuleAnimation()]
})
export class ViewHoldCoRegisterComponent extends AppComponentBase implements OnInit {

    active = false;
    saving = false;

    item: GetHoldCoRegisterForViewDto;
    calibrationStatusEnum = CalibrationStatusEnum;


    constructor(
        injector: Injector,
        private _activatedRoute: ActivatedRoute,
         private _holdCoRegistersServiceProxy: HoldCoRegistersServiceProxy
    ) {
        super(injector);
        this.item = new GetHoldCoRegisterForViewDto();
        this.item.holdCoRegister = new HoldCoRegisterDto();        
    }

    ngOnInit(): void {
        this.show(this._activatedRoute.snapshot.queryParams['id']);
    }

    show(holdCoRegisterId: string): void {
      this._holdCoRegistersServiceProxy.getHoldCoRegisterForView(holdCoRegisterId).subscribe(result => {      
                 this.item = result;
                this.active = true;
            });       
    }
}
