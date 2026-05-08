import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Incident } from '../model/incident';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class IncidentApi {
  private http = inject(HttpClient);
  private _incidents = signal<Incident[]>([]);
  private _incident = signal<Incident | null>(null);

  incidents = this._incidents.asReadonly();
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
    this.http.get<Incident[]>(`${environment.apiUrl}/incidents`)
    .subscribe((data) => { 
      this._incidents.set(data);
    });
  }

  upsertIncident(incident: Incident | null) {
    if (incident) { 
      if (incident.id) {
        this.http.put<Incident>(`${environment.apiUrl}/incidents/${incident.id}`, incident).subscribe();
      } else {
        this.http.post<Incident>(`${environment.apiUrl}/incidents`, incident)
          .subscribe((data) => {
            this._incident.set(data);
          });
      }
    }
  }
}
