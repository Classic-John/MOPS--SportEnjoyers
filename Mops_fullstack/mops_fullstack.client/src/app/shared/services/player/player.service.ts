import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { Player } from '../../interfaces/players/player.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {
  private route = 'player/'

  constructor(private readonly apiService: ApiService) { }

  getAll(): Observable<any> {
    return this.apiService.get<Player>(`${this.route}`);
  }

  isMemberOf(groupId: Number): Observable<any> {
    return this.apiService.get(`${this.route}group/${groupId}/join`);
  }

  joinGroup(groupId: Number): Observable<any> {
    return this.apiService.post(`${this.route}group/${groupId}/join`);
  }

  resolveJoin(groupId: Number, playerId: Number): Observable<any> {
    return this.apiService.put(`${this.route}group/${groupId}/join/${playerId}`);
  }
}
