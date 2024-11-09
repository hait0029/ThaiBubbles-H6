import { cartItems } from './../../Models/CartItems';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CartService } from '../../services/cart.service';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';
import { ProductService } from '../../services/product.service';
import { Product } from '../../Models/Product';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    RouterModule,
    FormsModule,
    CommonModule
  ],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit, OnDestroy {
  searchTerm: string = '';  // Define searchTerm here
  searchResults: Product[] = [];  // Define searchResults here to store the products from search
  basket: cartItems[] = [];
  isLoggedIn: boolean = false;
  isAdmin: boolean = false;
  private authSubscription: Subscription| null = null; // To manage the subscription

  constructor(private cartService: CartService, private router: Router, private authService: AuthService, private productService: ProductService) { }

  ngOnInit(): void {
    // Subscribe to cart changes
    this.cartService.currentBasket.subscribe(x => {
      this.basket = x;
    });

    // Subscribe to the login status observable and update the UI accordingly
    this.authSubscription = this.authService.isLoggedIn$.subscribe(loggedIn => {
      this.isLoggedIn = loggedIn;
      if (this.isLoggedIn) {
        this.isAdmin = this.authService.isAdmin(); // Check if the user is an admin
      } else {
        this.isAdmin = false; // Reset to false if not logged in
      }
    });
  }

  logout() {
    this.authService.logout(); // Trigger logout when clicked
    this.router.navigate(['/']); // Redirect to home or login page after logout
  }

  ngOnDestroy(): void {
    // Unsubscribe from the observable to avoid memory leaks
    if (this.authSubscription) {
      this.authSubscription.unsubscribe();
    }
  }

  onSearch(): void {
    if (this.searchTerm.trim()) {
      this.productService.searchProducts(this.searchTerm)
        .subscribe(products => {
          this.searchResults = products;
          // Navigate to a search results page, or display results in a dropdown
          this.router.navigate(['/search'], { queryParams: { query: this.searchTerm } });
        });
    }
  }
}
