// auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { BehaviorSubject, catchError, map, Observable, throwError } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { environment } from '../../environments/environments';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiurl;
  private token: string = ''; // Always a string, empty by default

  // BehaviorSubject to store current user info
  private currentUserSubject: BehaviorSubject<any> = new BehaviorSubject(null);
  public currentUser = this.currentUserSubject.asObservable();

  // BehaviorSubject to track login state
  private isLoggedInSubject: BehaviorSubject<boolean> = new BehaviorSubject(false);
  public isLoggedIn$ = this.isLoggedInSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) {
    this.initialize(); // Load any token on service load
  }

  // Public getter to access currentUserSubject value
  get currentUserValue(): any {
    return this.currentUserSubject.value;
  }

  // Public method to access the current user's role
  getCurrentUserRole(): string | null {
    const user = this.currentUserSubject.value;
    return user ? user.role : null; // Return the role of the current user, or null if not found
  }

  // Login to obtain a token, decode it, and navigate to the home page
  login(email: string, password: string): Observable<void> {
    return this.http.post<{ token?: string }>(`${this.apiUrl}user/login`, { email, password }).pipe(
      map(response => {
        if (response && response.token) {
          this.token = response.token;
          localStorage.setItem('token', this.token);
          this.decodeToken();
          this.isLoggedInSubject.next(true); // Set login status to true
        } else {
          throw new Error('Token not found in response'); // Throw error if token is missing
        }
      }),
      catchError(error => {
        console.error('Login error:', error);
        return throwError(() => new Error('Login failed. Please try again.'));
      })
    );
  }

  // Clear the token and reset user data on logout
  logout() {
    this.token = ''; // Reset token
    localStorage.removeItem('token'); // Remove from local storage
    this.currentUserSubject.next(null); // Notify observers that user is logged out
    this.isLoggedInSubject.next(false); // Update login status
    this.router.navigate(['/login']); // Redirect to login
  }

  // Decode token to update current user data
  private decodeToken() {
    if (this.token) {
      const decoded: any = jwtDecode(this.token);
      console.log('Decoded Token:', decoded); // Add this to see the decoded content

      // Ensure that decoded contains userID and other fields
      if (decoded && decoded.userID) {
        this.currentUserSubject.next(decoded);
      } else {
        console.error('Decoded token does not contain userID');
      }
    }
  }



  // Get user role from decoded token
  getUserRole(): string {
    const user = this.currentUserSubject.value;
    return user?.role || ''; // Return user role (admin/user)
  }

  // Verify if user is logged in by checking token presence
  isLoggedIn(): boolean {
    return this.token !== ''; // True if token is a non-empty string
  }

  // Verify user role by matching with decoded data
  hasRole(role: string): boolean {
    const user = this.currentUserSubject.value;
    return user && user.role === role;
  }

  // Check if the current user is an admin
  isAdmin(): boolean {
    return this.getUserRole() === 'Admin';
  }

  // Initialize service by retrieving token from local storage and decoding it
  initialize() {
    const storedToken = localStorage.getItem('token') || '';
    if (storedToken) {
      this.token = storedToken;
      console.log('Token from localStorage:', storedToken); // Debugging the token here
      this.decodeToken();
      this.isLoggedInSubject.next(true);
    } else {
      console.log('No token found in localStorage. User is not logged in.');
    }
  }

}
