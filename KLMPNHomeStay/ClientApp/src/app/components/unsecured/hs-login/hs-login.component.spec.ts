import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HsLoginComponent } from './hs-login.component';

describe('HsLoginComponent', () => {
  let component: HsLoginComponent;
  let fixture: ComponentFixture<HsLoginComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HsLoginComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HsLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
