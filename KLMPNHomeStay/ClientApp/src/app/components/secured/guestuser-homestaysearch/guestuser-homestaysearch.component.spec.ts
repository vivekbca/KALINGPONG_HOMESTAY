import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GuestuserHomestaysearchComponent } from './guestuser-homestaysearch.component';

describe('GuestuserHomestaysearchComponent', () => {
  let component: GuestuserHomestaysearchComponent;
  let fixture: ComponentFixture<GuestuserHomestaysearchComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GuestuserHomestaysearchComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GuestuserHomestaysearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
