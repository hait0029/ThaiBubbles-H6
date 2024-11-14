import { CartService } from './../../services/cart.service';
import { CategoryService } from './../../services/category.service';
import { ProductService } from './../../services/product.service';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Category } from '../../Models/Category';
import { Product } from '../../Models/Product';
import { cartItems } from '../../Models/CartItems';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [RouterModule,
    FormsModule,
    CommonModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent {

categories: Category[] = []
category: Category = {
  categoryID: 0, categoryName: '', product: []
}
product: Product = {
  productID: 0, name: '', price: 0,description: '', categoryId: 0,
  category: {
    categoryID: 0, categoryName: "",
  },
};

constructor(private ProductService:ProductService, private CategoryService:CategoryService, private CartService:CartService, private route: ActivatedRoute,) { }

ngOnInit(): void {
  this.CategoryService.getAll()
  .subscribe(x => this.categories = x);

console.warn("category id is", this.route.snapshot.paramMap.get('categoryID'));
this.route.params.subscribe((params) => {

  this.CategoryService.getById(parseInt(params["categoryID"])).subscribe(x => this.category = x);
})
}

addToCart(product: Product): void {

  // this.productService.getById(product.productId).subscribe(t => this.product = t)
  let item: cartItems = {
    productId: product.productID,
    productName: product.name,
    price: product.price,
    quantity: 1
  };
  this.CartService.addToBasket(item);
}


}

