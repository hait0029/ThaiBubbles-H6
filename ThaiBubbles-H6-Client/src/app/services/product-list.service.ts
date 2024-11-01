import { Injectable } from '@angular/core';
import { environment } from '../../environments/environments';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductList } from '../Models/ProductList';

@Injectable({
  providedIn: 'root',
})
export class ProductListService {
  private apiUrl = environment.apiurl + 'ProductList';

  constructor(private http: HttpClient) {}

  getAll(): Observable<ProductList[]> {
    return this.http.get<ProductList[]>(this.apiUrl);
  }
  getById(productListId: number): Observable<ProductList> {
    return this.http.get<ProductList>(`${this.apiUrl}/${productListId}`);
  }
  create(productList: ProductList): Observable<ProductList> {
    return this.http.post<ProductList>(this.apiUrl, productList);
  }

  update(productList: ProductList): Observable<ProductList> {
    return this.http.put<ProductList>(
      `${this.apiUrl}/${productList.productOrderListID}`,
      productList
    );
  }

  delete(productListId: number): Observable<ProductList> {
    return this.http.delete<ProductList>(`${this.apiUrl}/${productListId}`);
  }
}
