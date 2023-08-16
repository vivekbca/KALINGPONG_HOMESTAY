import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomestayApprovalComponent } from './homestay-approval.component';

describe('HomestayApprovalComponent', () => {
  let component: HomestayApprovalComponent;
  let fixture: ComponentFixture<HomestayApprovalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomestayApprovalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomestayApprovalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
