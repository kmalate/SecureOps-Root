import { TestBed } from '@angular/core/testing';

import { FieldDefinitionApi } from './field-definition-api';

describe('FieldDefinitionApi', () => {
  let service: FieldDefinitionApi;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FieldDefinitionApi);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
