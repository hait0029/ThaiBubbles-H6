import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environments';
import { Observable } from 'rxjs';
import { Product } from '../Models/Product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiUrl = environment.apiurl + 'Product';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl);
  }

  getById(productId: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${productId}`);
  }

  create(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }

  update(product: Product): Observable<Product> {
    return this.http.put<Product>(
      `${this.apiUrl}/${product.productID}`,
      product
    );
  }

  delete(productId: number): Observable<Product> {
    return this.http.delete<Product>(`${this.apiUrl}/${productId}`);
  }
}
