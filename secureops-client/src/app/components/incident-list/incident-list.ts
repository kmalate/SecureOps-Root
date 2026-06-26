import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Incident } from '../../model/incident';
import { IncidentStatus } from '../../model/incidentStatus';
import { IncidentApi } from '../../services/incident-api';
import { Router } from '@angular/router';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog';

@Component({
  selector: 'app-incident-list',
  imports: [CommonModule, ConfirmDialogComponent],
  templateUrl: './incident-list.html',
  styleUrl: './incident-list.css',
})
export class IncidentList implements OnInit {
  private router = inject(Router);
  incidentService = inject(IncidentApi);
  incidents: Incident[] = [];
  status = IncidentStatus;
  @ViewChild('deleteDialog') deleteDialog!: ConfirmDialogComponent;
  private deleteId!: string;


  ngOnInit(): void {
    this.incidentService.getAllIncidents();
  }

  createNewIncident() {
    this.router.navigate(['/incidententry']);
  }
  
  editIncident(id:string) {
    this.router.navigate(['/incidententry', id]);
  }

  openDelete(id: string) {
    this.deleteId = id;
    this.deleteDialog.open();
  }

  deleteIncident() {
    this.incidentService.deleteIncident(this.deleteId);
  }
}
