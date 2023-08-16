import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HsPaymentapprovalComponent } from './hs-paymentapproval.component';

describe('HsPaymentapprovalComponent', () => {
  let component: HsPaymentapprovalComponent;
  let fixture: ComponentFixture<HsPaymentapprovalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HsPaymentapprovalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HsPaymentapprovalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
