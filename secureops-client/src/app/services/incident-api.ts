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
  
  getAllIncidents() {
    this.http.get<Incident[]>(`${environment.apiUrl}/incidents`)
    .subscribe((data) => { 
      this._incidents.set(data);
    });
  }

  upsertIncident(incident: Incident | null) {
    if (incident) { 
      if (incident.id) {
        this.http.put<Incident>(`${environment.apiUrl}/incidents/${incident.id}`, incident)
          .subscribe((data) => {
            this._incident.set(data);
          });
      } else {
        this.http.post<Incident>(`${environment.apiUrl}/incidents`, incident)
          .subscribe((data) => {
            this._incident.set(data);
          });
      }
    }
  }
}
