import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GuSearchlistComponent } from './gu-searchlist.component';

describe('GuSearchlistComponent', () => {
  let component: GuSearchlistComponent;
  let fixture: ComponentFixture<GuSearchlistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GuSearchlistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GuSearchlistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
