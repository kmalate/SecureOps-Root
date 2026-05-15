import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Incident } from '../model/incident';
import { environment } from '../../environments/environment.development';
import { IncidentListItem } from '../model/incidentListItem';
import { Observable, of, pipe, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class IncidentApi {
  private http = inject(HttpClient);
  private _incidentList = signal<IncidentListItem[]>([]);
  private _incident = signal<Incident | null>(null);

  incidentList = this._incidentList.asReadonly();
  incident = this._incident.asReadonly();

  getIncidentById(id: string) {
    this.http.get<Incident>(`${environment.apiUrl}/incidents/${id}`).subscribe((data) => {
      this._incident.set(data);
    });
  }

  //TODO: remove temporary method once user management is implemented and we can pull user id from auth context
  setCreateById(id: number) {
    this._incident.update(current => ({ ...current, createdById: id, reportedById: id } as Incident));
  }

  setDraftIncident(incident: Incident) {
    this._incident.set(incident);
  }
  
  getAllIncidents() {
    this.http.get<IncidentListItem[]>(`${environment.apiUrl}/incidents`)
    .subscribe((data) => { 
      this._incidentList.set(data);
    });
  }

  upsertIncident(incident: Incident | null) : Observable<Incident | void> {
    if (incident) { 
      if (incident.id) {
        return this.http.put<Incident>(`${environment.apiUrl}/incidents/${incident.id}`, incident);
      } else {
        return this.http.post<Incident>(`${environment.apiUrl}/incidents`, incident)
          .pipe(
            tap((data) => {
              this._incident.set(data);
            })
          );
      }
    }

    return of(undefined);
  }

  deleteIncident(id: string) {
    this.http.delete(`${environment.apiUrl}/incidents/${id}`).subscribe(() => {
      this.getAllIncidents();
    });
  }
}
