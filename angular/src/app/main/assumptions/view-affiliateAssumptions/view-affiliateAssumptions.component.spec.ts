/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ViewAffiliateAssumptionsComponent } from './view-affiliateAssumptions.component';

describe('ViewAffiliateAssumptionsComponent', () => {
  let component: ViewAffiliateAssumptionsComponent;
  let fixture: ComponentFixture<ViewAffiliateAssumptionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewAffiliateAssumptionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewAffiliateAssumptionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
