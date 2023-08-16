import { TestBed } from '@angular/core/testing';

import { HsLoginService } from './hs-login.service';

describe('HsLoginService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: HsLoginService = TestBed.get(HsLoginService);
    expect(service).toBeTruthy();
  });
});
