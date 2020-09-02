import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveIvmodelModalComponent } from './approve-ivmodel-modal.component';

describe('ApproveIvmodelModalComponent', () => {
  let component: ApproveIvmodelModalComponent;
  let fixture: ComponentFixture<ApproveIvmodelModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApproveIvmodelModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApproveIvmodelModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
