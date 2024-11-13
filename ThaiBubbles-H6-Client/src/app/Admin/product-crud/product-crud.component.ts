import { ProductService } from './../../services/product.service';
import { Component } from '@angular/core';
import { Product } from '../../Models/Product';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-crud',
  standalone: true,
  imports:[RouterModule, FormsModule, CommonModule],
  templateUrl: './product-crud.component.html',
  styleUrl: './product-crud.component.css'
})
export class ProductCrudComponent {

  products: Product[] = [];
  product: Product = { productID: 0, name: '', description: '', price: 0, categoryId: 0, };
  productCopy: Product = { ...this.product }; // Temporary object for form data

   constructor(private productService: ProductService){ }

  ngOnInit(): void{
    this.productService.getAll().subscribe(x => this.products = x);
   }

  edit(selectedProduct: Product): void {
    this.product = selectedProduct;
    this.productCopy = { ...selectedProduct }; // Create a copy to prevent direct binding
  }

  delete(selectedProduct: Product): void {
    if (confirm('Are you sure you want to delete this product?')) {
      this.productService.delete(selectedProduct.productID).subscribe({
        next: () => {
          this.products = this.products.filter((product) => product.productID !== selectedProduct.productID);
        }
      });
    }
  }

  save(): void {
    if (this.productCopy.productID === 0) {
      this.productService.create(this.productCopy).subscribe({
        next: (newProduct) => {
          this.products.push(newProduct);
          this.resetForm();
        },
        error: (err) => {
          console.log(Object.keys(err.error.errors).join(', '));
        }
      });
    } else {
      this.productService.update(this.productCopy).subscribe({
        next: () => {
          const index = this.products.findIndex(u => u.productID === this.productCopy.productID);
          if (index !== -1) {
            this.products[index] = { ...this.productCopy }; // Update list with saved data
          }
          this.resetForm();
        },
        error: (err) => {
          console.log(Object.keys(err.error.errors).join(', '));
        }
      });
    }
  }

  resetForm(): void {
    this.product = { productID: 0, name: '', description: '', price: 0, categoryId: 0, };
    this.productCopy = { ...this.product };
  }

}
