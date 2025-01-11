import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { map } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { LoginModel } from '../../interfaces/players/login.interface';
import { LoggedPlayer } from '../../interfaces/players/logged-player.interface';
import { RegisterModel } from '../../interfaces/players/register.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthorizationService {
  private static key = "player"
  private route = "player/"

  constructor(private readonly apiService: ApiService) { }

  register(player: RegisterModel) {
    return this.apiService.post<RegisterModel>(`${this.route}`, player);
  }

  static logout() {
    localStorage.removeItem(AuthorizationService.key);
  }

  login(player: LoginModel) {
    let headers = new HttpHeaders({
      'Accept': 'text/plain'
    });
    let options = {
      headers: headers,
      responseType: 'text'
    };

    return this.apiService.post<LoginModel>(`${this.route}login`, player, options).pipe(
      map((token) => {
        if (token) {
          localStorage.setItem(AuthorizationService.key, token);
        }
      })
    );
  }

  static getLoggedInPlayer(): LoggedPlayer | null {
    let json = localStorage.getItem(AuthorizationService.key);
    return json ? JSON.parse(json) : null;
  }

  static getToken(): string | null {
    let player = AuthorizationService.getLoggedInPlayer();
    return player ? player.token.toString() : null;
  }

  static isLoggedIn(): Boolean {
    return (localStorage.getItem(AuthorizationService.key) != null);
  }
}
