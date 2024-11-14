import { CanActivateFn } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service'; // Ensure this path is correct
import { Router } from '@angular/router';

export const adminGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService); // Inject the AuthService to check user role
  const router = inject(Router); // Inject Router for redirection

  // Get the user's role using a method from AuthService
  const userRole = authService.getCurrentUserRole(); // Ensure this method exists in AuthService

  if (userRole === 'Admin') {
    return true; // Allow access if the user is an admin
  } else {
    router.navigate(['/']); // Redirect to the home page or any other page if not an admin
    return false; // Block navigation
  }
};
