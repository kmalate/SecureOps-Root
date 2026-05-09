import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Incident } from '../../model/incident';
import { IncidentStatus } from '../../model/incidentStatus';
import { IncidentApi } from '../../services/incident-api';
import { Router } from '@angular/router';

@Component({
  selector: 'app-incident-list',
  imports: [CommonModule],
  templateUrl: './incident-list.html',
  styleUrl: './incident-list.css',
})
export class IncidentList implements OnInit {
  private router = inject(Router);
  incidentService = inject(IncidentApi);
  incidents: Incident[] = [];
  status = IncidentStatus;


  ngOnInit(): void {
    this.incidentService.getAllIncidents();
  }

  createNewIncident() {
    this.router.navigate(['/incidententry']);
  }
  
  editIncident(id:string) {
    this.router.navigate(['/incidententry', id]);
  }

  deleteIncident(id:string) {
    this.incidentService.deleteIncident(id);
  }
}
