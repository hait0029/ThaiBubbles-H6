import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { environment } from '../../../environments/environments';
import { City } from '../../Models/City';
import { EncryptionService } from '../../services/encryption.service'; // Import EncryptionService

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  email: string = '';
  password: string = '';
  fName: string = '';
  lName: string = '';
  phoneNr: string = '';
  address: string = '';
  cityId: number = 0;  // The ID of the selected city
  cityName: string = ''; // The name of the selected city
  cities: City[] = [];  // Array to hold the cities
  city: City = {         // City object, initially empty
    cityID: 0,
    cityName: '',
    zipCode: 0
  };

  constructor(
    private http: HttpClient, 
    private router: Router,
    private encryptionService: EncryptionService  // Inject EncryptionService
  ) { }

  ngOnInit() {
    // Fetch the list of cities from the backend
    this.http.get<City[]>(`${environment.apiurl}City`)
      .subscribe(data => {
        this.cities = data;
      });
  }

  register() {
    // Encrypt sensitive fields using EncryptionService
    const encryptedUser = {
      email: this.encryptionService.encrypt(this.email),  // Encrypt email
      password: this.password,  // Password should be handled by backend (hashed)
      fName: this.encryptionService.encrypt(this.fName),  // Encrypt first name
      lName: this.encryptionService.encrypt(this.lName),  // Encrypt last name
      phoneNr: this.encryptionService.encrypt(this.phoneNr),  // Encrypt phone number
      address: this.encryptionService.encrypt(this.address),  // Encrypt address
      cityId: this.cityId,  // Use the city ID without encryption (foreign key)
      cityName: this.cityName,  // Include the city name for display or further use
      roleType: 'Customer'  // Default role
    };

    // Make the HTTP POST request to register the user
    this.http.post(`${environment.apiurl}user/register`, encryptedUser)
      .subscribe({
        next: () => {
          this.router.navigate(['/login']);  // Redirect to login page after registration
        },
        error: (err) => {
          console.error('Error registering user:', err);
        }
      });
  }

  // Update cityName based on the selected cityId
  onCityChange() {
    const selectedCity = this.cities.find(city => city.cityID === this.cityId);
    if (selectedCity) {
      this.cityName = selectedCity.cityName;
      this.city = selectedCity; // Update the city object
    }
  }
}
