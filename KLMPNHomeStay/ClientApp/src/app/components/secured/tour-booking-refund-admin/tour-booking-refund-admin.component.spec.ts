import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TourBookingRefundAdminComponent } from './tour-booking-refund-admin.component';

describe('TourBookingRefundAdminComponent', () => {
  let component: TourBookingRefundAdminComponent;
  let fixture: ComponentFixture<TourBookingRefundAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TourBookingRefundAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TourBookingRefundAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
