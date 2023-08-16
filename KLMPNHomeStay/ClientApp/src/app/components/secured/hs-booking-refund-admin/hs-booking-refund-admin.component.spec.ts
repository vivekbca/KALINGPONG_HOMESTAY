import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HsBookingRefundAdminComponent } from './hs-booking-refund-admin.component';

describe('HsBookingRefundAdminComponent', () => {
  let component: HsBookingRefundAdminComponent;
  let fixture: ComponentFixture<HsBookingRefundAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HsBookingRefundAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HsBookingRefundAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
