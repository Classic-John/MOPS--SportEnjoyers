import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';
import { Observable } from 'rxjs';
import { Thread } from '../../interfaces/threads/thread.interface';
import { CreateThread } from '../../interfaces/threads/create-thread.interface';

@Injectable({
  providedIn: 'root'
})
export class ThreadService {
  private route = 'thread/';

  constructor(private readonly apiService: ApiService) { }

  get(id: Number): Observable<any> {
    return this.apiService.get<Thread>(`${this.route}${id}`);
  }

  create(thread: CreateThread): Observable<any> {
    return this.apiService.post<CreateThread>(`${this.route}`, thread);
  }

  delete(id: Number): Observable<any> {
    return this.apiService.delete(`${this.route}${id}`);
  }
}
