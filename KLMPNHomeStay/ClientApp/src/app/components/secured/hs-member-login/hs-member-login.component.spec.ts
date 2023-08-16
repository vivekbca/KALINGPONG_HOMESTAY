import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HsMemberLoginComponent } from './hs-member-login.component';

describe('HsMemberLoginComponent', () => {
  let component: HsMemberLoginComponent;
  let fixture: ComponentFixture<HsMemberLoginComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HsMemberLoginComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HsMemberLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
