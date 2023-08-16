import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HsPaymentapprovalBankComponent } from './hs-paymentapproval-bank.component';

describe('HsPaymentapprovalBankComponent', () => {
  let component: HsPaymentapprovalBankComponent;
  let fixture: ComponentFixture<HsPaymentapprovalBankComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HsPaymentapprovalBankComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HsPaymentapprovalBankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
