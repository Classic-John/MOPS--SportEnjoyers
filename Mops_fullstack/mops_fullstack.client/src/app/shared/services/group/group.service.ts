import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { GroupFilter } from '../../interfaces/groups/group-filter.interface';
import { Group } from '../../interfaces/groups/group.interface';
import { Observable } from 'rxjs';
import { CreateGroup } from '../../interfaces/groups/create-group.interface';
import { GroupJoinStatus } from '../../interfaces/requests/join-status.interface';
import { GroupJoinVerdict } from '../../interfaces/requests/join-verdict.interface';

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

  getJoinStatus(id: Number): Observable<any> {
    return this.apiService.get<GroupJoinStatus>(`${this.route}${id}/requests`);
  }

  sendJoinRequest(id: Number): Observable<any> {
    return this.apiService.post(`${this.route}${id}/requests`);
  }

  sendJoinVerdict(id: Number, verdict: GroupJoinVerdict): Observable<any> {
    return this.apiService.put(`${this.route}${id}/requests`, verdict);
  }

  leaveGroup(id: Number): Observable<any> {
    return this.apiService.delete(`${this.route}${id}/requests`);
  }

  kickFromGroup(id: Number, playerId: Number): Observable<any> {
    return this.apiService.delete(`${this.route}${id}/requests/${playerId}`);
  }

  deleteGroup(id: Number): Observable<any> {
    return this.apiService.delete(`${this.route}${id}`);
  }
}
