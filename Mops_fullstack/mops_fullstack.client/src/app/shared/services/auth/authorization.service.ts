import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { map } from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import { LoginModel } from '../../interfaces/players/login.interface';
import { LoggedPlayer } from '../../interfaces/players/logged-player.interface';
import { RegisterModel } from '../../interfaces/players/register.interface';
import { VerifyEmail } from '../../interfaces/players/verify.interface';
import { GoogleAuth } from '../../interfaces/players/google-auth.interface';

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

  verify(verifyEmail: VerifyEmail) {
    return this.apiService.put<VerifyEmail>(`${this.route}verify`, verifyEmail);
  }

  static logout() {
    localStorage.removeItem(AuthorizationService.key);
  }

  login(player: LoginModel) {
    return this.apiService.post<LoginModel>(`${this.route}login`, player).pipe(
      map((player: LoggedPlayer) => {
        this.setPlayer(player);
      })
    );
  }

  googleAuth(player: GoogleAuth) {
    return this.apiService.put<GoogleAuth>(`${this.route}googleAuth`, player).pipe(
      map((player: LoggedPlayer) => {
        this.setPlayer(player);
      })
    )
  }

  setPlayer(player: LoggedPlayer) {
    localStorage.setItem(AuthorizationService.key, JSON.stringify(player));
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
