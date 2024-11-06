// auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import {jwtDecode} from 'jwt-decode'; // Ensure jwtDecode import without curly braces
import { environment } from '../../environments/environments';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiurl;
  private token: string = ''; // Initialize token as a string, not null

  private currentUserSubject: BehaviorSubject<any> = new BehaviorSubject(null);
  public currentUser = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) {
    this.initialize(); // Initialize token and user info on service load
  }

  login(email: string, password: string) {
    return this.http.post<any>(`${this.apiUrl}/login`, { email, password })
      .subscribe(response => {
        this.token = response.token || ''; // Set token from response or fallback to empty string
        localStorage.setItem('token', this.token); // Save token in local storage
        this.decodeToken();
        this.router.navigate(['/']);
      });
  }

  logout() {
    this.token = ''; // Clear token
    localStorage.removeItem('token');
    this.currentUserSubject.next(null);
    this.router.navigate(['/login']);
  }

  private decodeToken() {
    const token = this.token || localStorage.getItem('token'); // Retrieve or default to stored token
    if (token) {
      const decoded: any = jwtDecode(token); // Safely decode
      this.currentUserSubject.next(decoded); // Update current user observable with decoded data
    }
  }

  isLoggedIn(): boolean {
    return !!this.token; // Check if token exists
  }

  hasRole(role: string): boolean {
    const user = this.currentUserSubject.value;
    return user && user.role === role;
  }

  initialize() {
    this.token = localStorage.getItem('token') || ''; // Ensure token is a string
    if (this.token) {
      this.decodeToken(); // Decode if token exists
    }
  }
}
