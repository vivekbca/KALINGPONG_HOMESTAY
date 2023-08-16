import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PackageFeedbackComponent } from './package-feedback.component';

describe('PackageFeedbackComponent', () => {
  let component: PackageFeedbackComponent;
  let fixture: ComponentFixture<PackageFeedbackComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PackageFeedbackComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PackageFeedbackComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
