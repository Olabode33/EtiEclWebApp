/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EclResultsComponent } from './ecl-results.component';

describe('EclResultsComponent', () => {
  let component: EclResultsComponent;
  let fixture: ComponentFixture<EclResultsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EclResultsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EclResultsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
