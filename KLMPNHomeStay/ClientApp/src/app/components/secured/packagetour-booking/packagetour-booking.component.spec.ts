import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PackagetourBookingComponent } from './packagetour-booking.component';

describe('PackagetourBookingComponent', () => {
  let component: PackagetourBookingComponent;
  let fixture: ComponentFixture<PackagetourBookingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PackagetourBookingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PackagetourBookingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
