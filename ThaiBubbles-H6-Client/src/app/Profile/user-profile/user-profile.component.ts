import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';  // To manage observable subscription
import { User } from '../../Models/User';
import { UserService } from '../../services/user.service';  // Import UserService
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit, OnDestroy {
  userProfile: User | null = null;  // User profile information
  isLoading: boolean = true;
  errorMessage: string | null = null;
  currentUserSubscription: Subscription = new Subscription();  // Store the subscription

  constructor(
    private authService: AuthService,
    private router: Router,
    private userService: UserService  // Inject UserService
  ) {}

  ngOnInit(): void {
    this.loadUserProfile();
  }

  ngOnDestroy(): void {
    this.currentUserSubscription.unsubscribe();
  }

  loadUserProfile(): void {
    // Fetch the current user (from AuthService) to get their ID or necessary data
    this.currentUserSubscription = this.authService.currentUser.subscribe({
      next: (currentUser) => {
        if (currentUser && currentUser.userID) {
          // Now, use the UserService to fetch the full user profile from the backend
          this.userService.getById(currentUser.userID).subscribe({
            next: (userProfileData) => {
              this.userProfile = userProfileData;  // Set the user profile with additional data
              this.isLoading = false;  // Set loading state to false when data is available
            },
            error: () => {
              this.errorMessage = 'Failed to load user profile. Please try again later.';
              this.isLoading = false;
            }
          });
        } else {
          this.errorMessage = 'No valid user found. Please log in again.';
          this.isLoading = false;
        }
      },
      error: () => {
        this.errorMessage = 'Failed to load current user data. Please try again later.';
        this.isLoading = false;
      }
    });
  }
  
}
