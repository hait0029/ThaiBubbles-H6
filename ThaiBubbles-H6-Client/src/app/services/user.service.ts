import { User } from './../Models/User';
import { environment } from '../../environments/environments';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl = environment.apiurl + 'User';

  constructor(private http: HttpClient) {}

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }
  getById(userId: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${userId}`);
  }
  create(user: User): Observable<User> {
    return this.http.post<User>(this.apiUrl, user);
  }

  update(user: User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/${user.userID}`, user);
  }

  delete(userId: number): Observable<User> {
    return this.http.delete<User>(`${this.apiUrl}/${userId}`);
  }
}
