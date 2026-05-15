import { ComponentFixture, TestBed } from '@angular/core/testing';
import { IncidentEntry } from './incident-entry';
import { ActivatedRoute } from '@angular/router';
import { LookupsApi } from '../../services/lookups-api';
import { FieldDefinitionApi } from '../../services/field-definition-api';

describe('IncidentEntry', () => {
  let component: IncidentEntry;
  let fixture: ComponentFixture<IncidentEntry>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IncidentEntry],
      providers: [
        {
          provide: ActivatedRoute, useValue: {
            snapshot: {
              paramMap: {
                get: vi.fn((key: string) => null)
              }
            }
          }
        },
        {
          provide: LookupsApi, useValue: {
            incidentCategories: vi.fn(() => []),
            incidentSeverities: vi.fn(() => []),
            getIncidentCategories: vi.fn(),
            getIncidentSeverities: vi.fn() 
          }
        },
        {
          provide: FieldDefinitionApi, useValue: {
            fieldDefinitions: vi.fn(() => []),
            getFieldDefinitions: vi.fn()
          }
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(IncidentEntry);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
