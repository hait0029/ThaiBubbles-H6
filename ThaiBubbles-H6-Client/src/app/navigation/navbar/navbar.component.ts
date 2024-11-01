import { cartItems } from './../../Models/CartItems';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FormsModule } from '@angular/forms';
import { CartService } from '../../services/cart.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    RouterModule,
    FormsModule
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
basket: cartItems[] = [];

constructor(private cartService: CartService){}

ngOnInit(): void {
  console.log("test");
  this.cartService.currentBasket.subscribe(x => {
    this.basket = x;
  });
}


}
