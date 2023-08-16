import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BankUserDashboardComponent } from './bank-user-dashboard.component';

describe('BankUserDashboardComponent', () => {
  let component: BankUserDashboardComponent;
  let fixture: ComponentFixture<BankUserDashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BankUserDashboardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BankUserDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
