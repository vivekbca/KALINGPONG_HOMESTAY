import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PackagePaymentapprovalBankComponent } from './package-paymentapproval-bank.component';

describe('PackagePaymentapprovalBankComponent', () => {
  let component: PackagePaymentapprovalBankComponent;
  let fixture: ComponentFixture<PackagePaymentapprovalBankComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PackagePaymentapprovalBankComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PackagePaymentapprovalBankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
