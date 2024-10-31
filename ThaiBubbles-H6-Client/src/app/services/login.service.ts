import { Injectable } from '@angular/core';
import { environment } from '../../environments/environments';
import { Observable } from 'rxjs';
import { Login } from '../Models/Login';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private apiUrl = environment.apiurl + 'Login';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Login[]> {
    return this.http.get<Login[]>(this.apiUrl);
  }
  getById(loginId: number): Observable<Login> {
    return this.http.get<Login>(`${this.apiUrl}/${loginId}`);
  }
  create(login: Login): Observable<Login> {
    return this.http.post<Login>(this.apiUrl, login);
  }

  update(login: Login): Observable<Login> {
    return this.http.put<Login>(`${this.apiUrl}/${login.loginID}`, login);
  }

  delete(loginId: number): Observable<Login> {
    return this.http.delete<Login>(`${this.apiUrl}/${loginId}`);
  }
}
