import { Component, inject, OnInit, Signal, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Incident } from '../../model/incident';
import { FormsModule } from '@angular/forms';
import { LookupsApi } from '../../services/lookups-api';
import { IncidentApi } from '../../services/incident-api';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Toast } from '../../services/toast';
import { FieldDefinitionApi } from '../../services/field-definition-api';

@Component({
  selector: 'app-incident-entry',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './incident-entry.html',
  styleUrl: './incident-entry.css',
})
export class IncidentEntry implements OnInit  {
  private activateRoute = inject(ActivatedRoute);
  private router = inject(Router);
  private toast = inject(Toast);
  lookupService = inject(LookupsApi);
  incidentService = inject(IncidentApi);
  fieldDefinitionService = inject(FieldDefinitionApi);
  narrativeWordCount: number = 0;
  incident: Signal<Incident | null> = signal<Incident | null>(null);
  incidentId: number | null = null;

  constructor() {}

  ngOnInit() {
    this.incident = this.incidentService.incident;
    this.incidentId = this.activateRoute.snapshot.paramMap.get('id') as number | null;
    this.lookupService.getIncidentCategories();
    this.lookupService.getIncidentSeverities();
    this.fieldDefinitionService.getFieldDefinitions();
    if (this.incidentId) {
      this.incidentService.getIncidentById(this.incidentId.toString());
    } else {
      this.incidentService.setDraftIncident({ metadata: {}} as Incident);
    }
  }

  onSubmit() {
    if (this.incident) {
      //TODO: replace with actual user id
      this.incidentService.setCreateById(1);
      this.incidentService.upsertIncident(this.incident()).subscribe(() => {
        this.router.navigate(['/incidents']);
      });
    }
  }  

  onSave() {
    if (this.incident) {  
      //TODO: replace with actual user id
      this.incidentService.setCreateById(1);
      this.incidentService.upsertIncident(this.incident()).subscribe(() => {
        this.toast.show('Incident saved successfully', { classname: 'bg-success text-light', delay: 3000 });
      });
    }
  }

  onMetadataChange(fieldId: number, newValue: string) {
    const incident = this.incident();
    if (!incident) {
      return;
    }

    incident.metadata[fieldId] = newValue;
  }

  getMetadataValue(fieldId: number) : string { 
    let entry = '';
    if (this.incident()?.metadata) {
      entry = this.incident()!.metadata[fieldId] || '';
    }
    return entry;
  }

}
