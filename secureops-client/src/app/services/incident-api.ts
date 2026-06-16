import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Incident } from '../model/incident';
import { environment } from '../../environments/environment';
import { IncidentListItem } from '../model/incidentListItem';
import { map, Observable, tap } from 'rxjs';
import { IncidentMetadata } from '../model/incidentMetadata';

@Injectable({
  providedIn: 'root',
})
export class IncidentApi {
  private http = inject(HttpClient);
  private _incidentList = signal<IncidentListItem[]>([]);
  private _incident = signal(this.incidentInitialValue());

  private incidentInitialValue(): Incident {
    return {
      incidentCategoryId: 0,
      incidentSeverityId: 0,
      narrative: '',
      caseNumber: '',
      metadata: {} as IncidentMetadata,
    } as Incident;
  }

  incidentList = this._incidentList.asReadonly();
  incident = this._incident.asReadonly();

  getIncidentById(id: string) {
    this.http.get<Incident>(`${environment.apiUrl}/incidents/${id}`).subscribe((data) => {
      this._incident.set(data);
    });
  }

  setDraftIncident() {
    this._incident.set(this.incidentInitialValue());
  }

  getAllIncidents() {
    this.http.get<IncidentListItem[]>(`${environment.apiUrl}/incidents`)
      .subscribe((data) => {
        this._incidentList.set(data);
      });
  }

  upsertIncident(incident: Incident): Observable<void> {
    const request$ = incident.id
      ? this.http.put<Incident>(`${environment.apiUrl}/incidents/${incident.id}`, incident)
      : this.http.post<Incident>(`${environment.apiUrl}/incidents`, incident);

    return request$.pipe(
      tap(result => this._incident.set(result)), 
      map(() => void 0)
    );
  }

  deleteIncident(id: string) {
    this.http.delete(`${environment.apiUrl}/incidents/${id}`).subscribe(() => {
      this.getAllIncidents();
    });
  }
}
