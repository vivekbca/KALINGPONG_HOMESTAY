import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PackagePaymentapprovalComponent } from './package-paymentapproval.component';

describe('PackagePaymentapprovalComponent', () => {
  let component: PackagePaymentapprovalComponent;
  let fixture: ComponentFixture<PackagePaymentapprovalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PackagePaymentapprovalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PackagePaymentapprovalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
