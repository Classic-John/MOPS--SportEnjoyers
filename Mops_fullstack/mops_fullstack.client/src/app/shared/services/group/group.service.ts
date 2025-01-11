import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { GroupFilter } from '../../interfaces/groups/group-filter.interface';
import { Group } from '../../interfaces/groups/group.interface';
import { Observable } from 'rxjs';
import { CreateGroup } from '../../interfaces/groups/create-group.interface';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  private route = "group/"

  constructor(private readonly apiService: ApiService) { }

  getAllThatMatch(filter: GroupFilter): Observable<any> {
    return this.apiService.get<Group[]>(`${this.route}`, filter);
  }

  create(group: CreateGroup): Observable<any> {
    return this.apiService.post<CreateGroup>(`${this.route}`, group);
  }

  get(id: Number): Observable<any> {
    return this.apiService.get<Group>(`${this.route}${id}`);
  }
}
