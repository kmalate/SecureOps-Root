import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { IncidentCategory } from '../model/incidentCategory';
import { IncidentSeverity } from '../model/incidentSeverity';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class LookupsApi {
  private http = inject(HttpClient);
  private _incidentCategories = signal<IncidentCategory[]>([]);
  private _incidentSeverities = signal<IncidentSeverity[]>([]);

  incidentCategories = this._incidentCategories.asReadonly();
  incidentSeverities = this._incidentSeverities.asReadonly();
  
  getIncidentCategories() {
    if (this._incidentCategories().length > 0) {
      return;
    }
    
    this.http.get<IncidentCategory[]>(`${environment.apiUrl}/lookups/incident-categories`)
    .subscribe((data) => { 
      this._incidentCategories.set(data);
    });
  }

  getIncidentSeverities() {
    if (this._incidentSeverities().length > 0) {
      return;
    }

    this.http.get<IncidentSeverity[]>(`${environment.apiUrl}/lookups/incident-severities`)
    .subscribe((data) => { 
      this._incidentSeverities.set(data);
    });
  }
}
