import { ProductList } from './../Models/ProductList';
import { CartService } from './../services/cart.service';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Product } from '../Models/Product';
import { cartItems } from '../Models/CartItems';
import { environment } from '../../environments/environments';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [RouterModule,
     FormsModule,
      CommonModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit {

cartItems: cartItems[] = [];
amount: number = 1;

constructor(private CartService: CartService, private http: HttpClient) {}

ngOnInit(): void {
  this.amount = Math.floor(Math.random() * 10) + 1;
  this.CartService.currentBasket.subscribe((items) => (this.cartItems = items));
}

getBaskedTotal(): number {
  return this.cartItems.reduce((total, item) => total + item.price * item.quantity,0);
}

addToCart(product: Product): void{
  const item: cartItems = {
    productId: product.productID,
    productName: product.name,
    price: product.price,
    //description: product.description,
    quantity: 1,
  };
  this.amount = item.price * item.quantity;
  this.CartService.addToBasket(item);
}


clearCart(): void {
  this.CartService.clearBasket();
}

updateCart(): void {
  this.CartService.saveBasket(this.cartItems);
}

removeItem(item: cartItems): void {
  this.cartItems = this.cartItems.filter((x) => x.productId !== item.productId);
  this.CartService.saveBasket(this.cartItems);
}

purchase(): void {
  if(this.cartItems.length === 0){
    alert('Cart is empty');
    return;
  }
  const productList: ProductList[] = this.cartItems.map(item => ({
    productOrderListID: 0,
    quantity: item.quantity,
    productId: 0,
    orderId: 0,
  }));

  const payload = {
    productList: productList
  };

  console.log('Payload:', payload);

  this.http.post(`${environment.apiurl}productlist`, payload).subscribe({
    next: (response) => {
      alert('Purchase successful!');
      this.clearCart(); // Clear the cart after successful purchase
    },
    error: (error: HttpErrorResponse) => {
      console.error('Error during purchase:', error);
      if (error.error && error.error.errors) {
        // Log validation errors if available
        console.error('Validation errors:', error.error.errors);
        alert('Validation error during the purchase.');
      } else {
        alert('There was an error during the purchase.');
      }
    }
  });
}

}




