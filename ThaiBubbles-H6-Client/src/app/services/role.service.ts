import { Injectable } from '@angular/core';
import { environment } from '../../environments/environments';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Role } from '../Models/Role';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  private apiUrl = environment.apiurl + 'Role';
  constructor(private http: HttpClient) {}

  getAll(): Observable<Role[]> {
    return this.http.get<Role[]>(this.apiUrl);
  }

  getById(roleId: number): Observable<Role> {
    return this.http.get<Role>(`${this.apiUrl}/${roleId}`);
  }
  create(role: Role): Observable<Role> {
    return this.http.post<Role>(this.apiUrl, role);
  }

  update(role: Role): Observable<Role> {
    return this.http.put<Role>(`${this.apiUrl}/${role.roleID}`, role);
  }

  delete(roleId: number): Observable<Role> {
    return this.http.delete<Role>(`${this.apiUrl}/${roleId}`);
  }
}
