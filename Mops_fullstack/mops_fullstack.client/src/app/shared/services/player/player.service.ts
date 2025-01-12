import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { Player } from '../../interfaces/players/player.interface';
import { Observable } from 'rxjs';
import { JoinRequest } from '../../interfaces/requests/join-request.interface';
import { Group } from '../../interfaces/groups/group.interface';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {
  private route = 'player/'

  constructor(private readonly apiService: ApiService) { }

  getAll(): Observable<any> {
    return this.apiService.get<Player>(`${this.route}`);
  }

  getRequests(): Observable<any> {
    return this.apiService.get<JoinRequest>(`${this.route}requests`);
  }

  getGroupsOwned(): Observable<any> {
    return this.apiService.get<Group>(`${this.route}groups`);
  }
}
