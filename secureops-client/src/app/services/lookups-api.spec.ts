import { TestBed } from '@angular/core/testing';

import { LookupsApi } from './lookups-api';

describe('LookupsApi', () => {
  let service: LookupsApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LookupsApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
