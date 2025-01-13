import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { CreateMessage } from '../../interfaces/messages/create-message.interface';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private route = "message/";

  constructor(private readonly apiService: ApiService) { }

  create(message: CreateMessage): Observable<any> {
    return this.apiService.post<CreateMessage>(`${this.route}`, message);
  }

  delete(id: Number): Observable<any> {
    return this.apiService.delete(`${this.route}${id}`);
  }
}
