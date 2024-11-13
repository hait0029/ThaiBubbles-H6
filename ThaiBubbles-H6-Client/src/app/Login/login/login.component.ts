import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule, FormsModule,CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = ''; // For displaying error messages

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.authService.login(this.email, this.password).subscribe({
      next: () => {
        // Navigate to home or dashboard on successful login
        this.router.navigate(['/']);
      },
      error: (error) => {
        // Handle errors here, e.g., invalid credentials
        if (error.status === 401) {
          // Unauthorized error (Invalid credentials)
          this.errorMessage = 'An error occurred. Please try again later.';
          alert(this.errorMessage);  // Displaying a pop-up message
        } else {
          // General error message
          this.errorMessage = 'Invalid email or password. Please try again.';
          alert(this.errorMessage);  // Displaying a pop-up message
        }
        console.error(error);
      }
    });
  }
  closePopup() {
    this.errorMessage = '';  // Reset error message and hide the modal
  }
  
}
