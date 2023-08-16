import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PackageDateAddComponent } from './package-date-add.component';

describe('PackageDateAddComponent', () => {
  let component: PackageDateAddComponent;
  let fixture: ComponentFixture<PackageDateAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PackageDateAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PackageDateAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
