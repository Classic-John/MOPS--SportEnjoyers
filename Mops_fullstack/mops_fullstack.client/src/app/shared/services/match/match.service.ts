import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { CreateMatch } from '../../interfaces/matches/create-match.interface';
import { Observable } from 'rxjs';
import { Match } from '../../interfaces/matches/match.interface';

@Injectable({
  providedIn: 'root'
})
export class MatchService {
  private route = "match/";

  constructor(private readonly apiService: ApiService) { }

  create(match: CreateMatch): Observable<any> {
    return this.apiService.post<Match>(`${this.route}`, match);
  }
}
