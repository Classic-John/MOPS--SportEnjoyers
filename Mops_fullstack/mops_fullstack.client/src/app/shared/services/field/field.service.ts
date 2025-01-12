import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { FieldFilter } from '../../interfaces/fields/field-filter.interface';
import { Observable } from 'rxjs';
import { Field } from '../../interfaces/fields/field.interface';
import { CreateField } from '../../interfaces/fields/create-field.interface';

@Injectable({
  providedIn: 'root'
})
export class FieldService {
  private route = "field/";

  constructor(private readonly apiService: ApiService) { }

  getAllThatMatch(filter: FieldFilter): Observable<any> {
    return this.apiService.get<Field[]>(`${this.route}`, filter);
  }

  create(field: CreateField): Observable<any> {
    return this.apiService.post<CreateField>(`${this.route}`, field);
  }

  get(id: Number): Observable<any> {
    return this.apiService.get<Field>(`${this.route}${id}`);
  }

  delete(id: Number): Observable<any> {
    return this.apiService.delete(`${this.route}${id}`);
  }
}
