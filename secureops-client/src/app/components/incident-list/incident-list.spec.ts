import { ComponentFixture, TestBed } from '@angular/core/testing';
import { IncidentList } from './incident-list';
import { IncidentApi } from '../../services/incident-api';

describe('IncidentList', () => {
  let component: IncidentList;
  let fixture: ComponentFixture<IncidentList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [IncidentList],
      providers: [
        { 
          provide: IncidentApi, useValue: { 
            incidents: vi.fn(() => []) ,
            getAllIncidents: vi.fn(() => Promise.resolve([]))
          } 
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(IncidentList);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
