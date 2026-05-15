import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { FieldDefinitionList } from '../model/fieldDefinitionList';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class FieldDefinitionApi {
  private http = inject(HttpClient);
  private _fieldDefinitions = signal<FieldDefinitionList[]>([]);

  fieldDefinitions = this._fieldDefinitions.asReadonly();

  getFieldDefinitions() {
    this.http.get<FieldDefinitionList[]>(`${environment.apiUrl}/fielddefinition`)
    .subscribe((data) => { 
      this._fieldDefinitions.set(data);
    });
  }
}
