import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HsMemberRoomAddComponent } from './hs-member-room-add.component';

describe('HsMemberRoomAddComponent', () => {
  let component: HsMemberRoomAddComponent;
  let fixture: ComponentFixture<HsMemberRoomAddComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HsMemberRoomAddComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HsMemberRoomAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
