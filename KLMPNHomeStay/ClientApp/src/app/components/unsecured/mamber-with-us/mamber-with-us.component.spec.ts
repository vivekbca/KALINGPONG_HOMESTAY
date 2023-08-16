import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MamberWithUsComponent } from './mamber-with-us.component';

describe('MamberWithUsComponent', () => {
  let component: MamberWithUsComponent;
  let fixture: ComponentFixture<MamberWithUsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MamberWithUsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MamberWithUsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
