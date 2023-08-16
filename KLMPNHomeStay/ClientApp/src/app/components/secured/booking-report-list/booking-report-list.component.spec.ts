import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookingReportListComponent } from './booking-report-list.component';

describe('BookingReportListComponent', () => {
  let component: BookingReportListComponent;
  let fixture: ComponentFixture<BookingReportListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookingReportListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookingReportListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
