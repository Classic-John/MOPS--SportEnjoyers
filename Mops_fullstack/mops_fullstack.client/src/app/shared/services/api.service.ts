import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = "";

  constructor(private readonly httpClient: HttpClient, @Inject('BASE_URL') baseUrl: String) {
    this.apiUrl = baseUrl + "api/";
  }

  get<T>(path: String, params = {}, headers = new HttpHeaders()): Observable<any> {
    return this.httpClient.get<T>(`${this.apiUrl}${path}`, { params, headers });
  }

  put<T>(path: String, body = {}, options = {}): Observable<any> {
    return this.httpClient.put<T>(`${this.apiUrl}${path}`, body, options);
  }

  post<T>(path: String, body = {}, options = {}): Observable<any> {
    return this.httpClient.post<T>(`${this.apiUrl}${path}`, body, options);
  }

  delete<T>(path: String): Observable<any> {
    return this.httpClient.delete(`${this.apiUrl}${path}`);
  }
}
