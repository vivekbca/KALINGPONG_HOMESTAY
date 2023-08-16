import { TestBed } from '@angular/core/testing';

import { MemberWithUSService } from './member-with-us.service';

describe('MemberWithUSService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MemberWithUSService = TestBed.get(MemberWithUSService);
    expect(service).toBeTruthy();
  });
});
