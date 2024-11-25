import { Component, OnInit } from '@angular/core';
import { Product } from '../Models/Product';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../services/product.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { cartItems } from '../Models/CartItems';
import { CartService } from '../services/cart.service';

@Component({
  selector: 'app-search-result',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './search-result.component.html',
  styleUrl: './search-result.component.css',
})
export class SearchResultComponent implements OnInit {
  searchTerm: string = '';
  products: Product[] = [];

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private CartService: CartService
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.searchTerm = params['query'] || '';
      if (this.searchTerm) {
        this.productService
          .searchProducts(this.searchTerm)
          .subscribe((products) => (this.products = products));
      }
    });
  }
  addToCart(product: Product): void {
    // this.productService.getById(product.productId).subscribe(t => this.product = t)
    let item: cartItems = {
      productId: product.productID,
      productName: product.name,
      price: product.price,
      quantity: 1,
    };
    this.CartService.addToBasket(item);
  }
}
