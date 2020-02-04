/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EclListComponent } from './ecl-list.component';

describe('WorkspaceComponent', () => {
  let component: EclListComponent;
  let fixture: ComponentFixture<EclListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EclListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EclListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
