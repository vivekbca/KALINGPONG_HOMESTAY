import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HomestayapprovalDetailComponent } from './homestayapproval-detail.component';

describe('HomestayapprovalDetailComponent', () => {
  let component: HomestayapprovalDetailComponent;
  let fixture: ComponentFixture<HomestayapprovalDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HomestayapprovalDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomestayapprovalDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
