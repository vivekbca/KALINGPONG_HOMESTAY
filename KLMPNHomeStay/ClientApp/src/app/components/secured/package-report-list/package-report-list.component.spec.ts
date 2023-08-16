import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PackageReportListComponent } from './package-report-list.component';

describe('PackageReportListComponent', () => {
  let component: PackageReportListComponent;
  let fixture: ComponentFixture<PackageReportListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PackageReportListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PackageReportListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
