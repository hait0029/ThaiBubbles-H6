import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';  // To manage observable subscription
import { User } from '../../Models/User';
import { UserService } from '../../services/user.service';  // Import UserService
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CityService } from '../../services/city.service';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit, OnDestroy {
  userProfile: User | null = null;
  isLoading: boolean = true;
  errorMessage: string | null = null;
  cityName: string | null = null;  // Store the city name
  currentUserSubscription: Subscription = new Subscription();

  constructor(
    private authService: AuthService,
    private router: Router,
    private userService: UserService,
    private cityService: CityService  // Inject CityService
  ) {}

  ngOnInit(): void {
    this.loadUserProfile();
  }

  ngOnDestroy(): void {
    this.currentUserSubscription.unsubscribe();
  }

  loadUserProfile(): void {
    this.currentUserSubscription = this.authService.currentUser.subscribe({
      next: (currentUser) => {
        if (currentUser && currentUser.userID) {
          this.userService.getById(currentUser.userID).subscribe({
            next: (userProfileData) => {
              this.userProfile = userProfileData;
              this.isLoading = false;

              // Fetch city name if cityId exists
              if (this.userProfile.cityId) {
                this.loadCityName(this.userProfile.cityId);
              }
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

  loadCityName(cityId: number): void {
    this.cityService.getById(cityId).subscribe({
      next: (cityData) => {
        this.cityName = cityData.cityName;  // Store the city name
      },
      error: () => {
        this.cityName = 'Unknown city';  // Handle error gracefully
      }
    });
  }

}
