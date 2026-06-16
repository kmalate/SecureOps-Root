import { Component, effect, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Incident } from '../../model/incident';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LookupsApi } from '../../services/lookups-api';
import { IncidentApi } from '../../services/incident-api';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Toast } from '../../services/toast';
import { FieldDefinitionApi } from '../../services/field-definition-api';
import { IncidentMetadata } from '../../model/incidentMetadata';
import { form, required, FormField, maxLength, min } from '@angular/forms/signals';

interface IncidentEntryModel {
  id: string,
  incidentCategoryId: string,
  incidentSeverityId: string,
  narrative: string,
  caseNumber: string,
  metadata: IncidentMetadata;
}

@Component({
  selector: 'app-incident-entry',
  imports: [CommonModule, FormsModule, RouterLink, FormField, ReactiveFormsModule],
  templateUrl: './incident-entry.html',
  styleUrl: './incident-entry.css',
})
export class IncidentEntry implements OnInit {
  private activateRoute = inject(ActivatedRoute);
  private router = inject(Router);
  private toast = inject(Toast);
  
  lookupService = inject(LookupsApi);
  incidentService = inject(IncidentApi);
  fieldDefinitionService = inject(FieldDefinitionApi);
  narrativeWordCount: number = 0;
  incidentId: number | null = null;
  incident = signal<IncidentEntryModel>(this.initialIncident());
  incidentForm = form(this.incident, (schemaPath) => {
    required(schemaPath.narrative, {message: 'Narrative is required'}),
    maxLength(schemaPath.narrative, 2000, {message: 'Narrative must be at most 2000 characters'}),
    min(schemaPath.incidentCategoryId, 1, {message: 'Incident category is required'}),
    min(schemaPath.incidentSeverityId, 1, {message: 'Incident severity is required'})
  });

  private initialIncident(): IncidentEntryModel {
    return {
      id: '',
      incidentCategoryId: '',
      incidentSeverityId: '',
      narrative: '',
      caseNumber: '',
      metadata: {}
    }
  }

  private incidentEntryToIncident(): Incident {
    const incidentEntry = this.incident();
    return {
      id: incidentEntry.id,
      incidentCategoryId: Number(incidentEntry.incidentCategoryId),
      incidentSeverityId: Number(incidentEntry.incidentSeverityId),
      narrative: incidentEntry.narrative,
      caseNumber: incidentEntry.caseNumber,
       //TODO: replace with actual user id
      createdById: 1,
       //TODO: replace with actual user id
      reportedById: 1,
      metadata: incidentEntry.metadata
    } as Incident;
  }

  constructor() {
    effect(() => {
      const serviceIncident = this.incidentService.incident();
      if (serviceIncident) {
        let incidentMetadata = {} as IncidentMetadata;
        this.fieldDefinitionService.fieldDefinitions().forEach(field => {
            incidentMetadata[field.id] = serviceIncident.metadata[field.id] ?? '';
        });
        this.incident.set({
          id: serviceIncident.id,
          incidentCategoryId: String(serviceIncident.incidentCategoryId),
          incidentSeverityId: String(serviceIncident.incidentSeverityId),
          narrative: serviceIncident.narrative,
          caseNumber: serviceIncident.caseNumber,
          metadata: incidentMetadata
        });
      }
    });
  }

  ngOnInit() {
    this.incidentId = this.activateRoute.snapshot.paramMap.get('id') as number | null;
    this.lookupService.getIncidentCategories();
    this.lookupService.getIncidentSeverities();
    this.fieldDefinitionService.getFieldDefinitions();
    if (this.incidentId) {
      this.incidentService.getIncidentById(this.incidentId.toString());
    } 
    else {
      this.incidentService.setDraftIncident();
    }
  }

  onSubmit(event: Event) {
      event.preventDefault();
      this.incidentService.upsertIncident(this.incidentEntryToIncident()).subscribe(() => {
        this.router.navigate(['/incidents']);
      });
  }

  onSave() {
      this.incidentService.upsertIncident(this.incidentEntryToIncident()).subscribe(() => {
        this.toast.show('Incident saved successfully', { classname: 'bg-success text-light', delay: 3000 });
      });
  }
}

