import { ChangeDetectorRef, Component, computed, effect, inject, OnInit, Signal, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Incident } from '../../model/incident';
import { FormsModule } from '@angular/forms';
import { FieldDefinition } from '../../model/fieldDefinition';
import { LookupsApi } from '../../services/lookups-api';
import { IncidentApi } from '../../services/incident-api';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-incident-entry',
  imports: [CommonModule, FormsModule],
  templateUrl: './incident-entry.html',
  styleUrl: './incident-entry.css',
})
export class IncidentEntry implements OnInit  {
  private activateRoute = inject(ActivatedRoute);
  lookupService = inject(LookupsApi);
  incidentService = inject(IncidentApi);
  fieldDefinitions: FieldDefinition[] = [];
  narrativeWordCount: number = 0;
  incident: Signal<Incident | null> = signal<Incident | null>(null);
  incidentId: number | null = null;

  constructor() {}

  ngOnInit() {
    this.incident = this.incidentService.incident;
    this.incidentId = this.activateRoute.snapshot.paramMap.get('id') as number | null;
    this.lookupService.getIncidentCategories();
    this.lookupService.getIncidentSeverities();
    if (this.incidentId) {
      this.incidentService.getIncidentById(this.incidentId.toString());
    } else {
      this.incidentService.setDraftIncident({} as Incident);
    }
  }

  onSubmit() {
    if (this.incident) {
      //TODO: replace with actual user id
      this.incidentService.setCreateById(1);
      this.incidentService.upsertIncident(this.incident());
    }
  }  

  onMetadataChange(fieldId: number, event: any) {}

  getMetadataValue(fieldId: number) : string { return ''; }

}
