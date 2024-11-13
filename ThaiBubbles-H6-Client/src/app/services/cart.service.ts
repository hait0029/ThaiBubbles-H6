import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { cartItems } from '../Models/CartItems';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private basketName = 'bubbleteabasket';
  private memoryBasket: cartItems[] = []; // Fallback storage if localStorage is unavailable

  // A subject that expects to hold an instance of cart
  currentBasketSubject: BehaviorSubject<cartItems[]>;

  // User subscribes to our subject
  currentBasket: Observable<cartItems[]>;

  constructor() {
    this.currentBasketSubject = new BehaviorSubject<cartItems[]>(
      this.getBasketFromStorage()
    );
    this.currentBasket = this.currentBasketSubject.asObservable();
  }

  // Check if localStorage is available
  private isLocalStorageAvailable(): boolean {
    try {
      const test = 'test';
      localStorage.setItem(test, test);
      localStorage.removeItem(test);
      return true;
    } catch (e) {
      return false;
    }
  }

  // Get basket either from localStorage or from memory fallback
  private getBasketFromStorage(): cartItems[] {
    if (this.isLocalStorageAvailable()) {
      return JSON.parse(localStorage.getItem(this.basketName) || '[]');
    } else {
      return this.memoryBasket;
    }
  }

  // Save basket either to localStorage or to memory fallback
  saveBasket(basket: cartItems[]): void {
    if (this.isLocalStorageAvailable()) {
      localStorage.setItem(this.basketName, JSON.stringify(basket));
    } else {
      this.memoryBasket = basket; // Fallback to in-memory storage
    }
    this.currentBasketSubject.next(basket);
  }

  clearBasket(): void {
    let basket: cartItems[] = [];
    this.saveBasket(basket);
  }

  addToBasket(item: cartItems): void {
    let productFound = false;
    let basket = this.currentBasketValue;
    basket.forEach((basketItem) => {
      if (basketItem.productId == item.productId) {
        basketItem.quantity += item.quantity;
        productFound = true;
        if (basketItem.quantity <= 0) {
          this.removeItemFromBasket(item.productId);
        }
      }
    });

    if (!productFound) {
      basket.push(item);
    }
    this.saveBasket(basket);
  }

  removeItemFromBasket(ProductId: number): void {
    let basket = this.currentBasketValue;
    for (let i = basket.length - 1; i >= 0; i--) {
      if (basket[i].productId === ProductId) {
        basket.splice(i, 1);
      }
    }
    this.saveBasket(basket);
  }

  // Expose the current basket value
  get currentBasketValue(): cartItems[] {
    return this.currentBasketSubject.value;
  }
}
