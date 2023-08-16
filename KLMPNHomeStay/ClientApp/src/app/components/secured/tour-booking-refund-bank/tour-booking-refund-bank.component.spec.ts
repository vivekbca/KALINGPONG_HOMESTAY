import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TourBookingRefundBankComponent } from './tour-booking-refund-bank.component';

describe('TourBookingRefundBankComponent', () => {
  let component: TourBookingRefundBankComponent;
  let fixture: ComponentFixture<TourBookingRefundBankComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TourBookingRefundBankComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TourBookingRefundBankComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
