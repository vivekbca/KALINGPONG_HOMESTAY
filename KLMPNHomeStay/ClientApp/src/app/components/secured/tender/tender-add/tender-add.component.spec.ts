import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TenderAddComponent } from './tender-add.component';

describe('TenderAddComponent', () => {
  let component: TenderAddComponent;
  let fixture: ComponentFixture<TenderAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TenderAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TenderAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
