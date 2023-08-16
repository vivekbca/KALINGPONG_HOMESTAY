import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TourFeedbackAddComponent } from './tour-feedback-add.component';

describe('TourFeedbackAddComponent', () => {
  let component: TourFeedbackAddComponent;
  let fixture: ComponentFixture<TourFeedbackAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TourFeedbackAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TourFeedbackAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
