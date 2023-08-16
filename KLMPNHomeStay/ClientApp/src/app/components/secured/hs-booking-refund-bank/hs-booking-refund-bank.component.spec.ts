import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HsBookingRefundBankComponent } from './hs-booking-refund-bank.component';

describe('HsBookingRefundBankComponent', () => {
  let component: HsBookingRefundBankComponent;
  let fixture: ComponentFixture<HsBookingRefundBankComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HsBookingRefundBankComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HsBookingRefundBankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
