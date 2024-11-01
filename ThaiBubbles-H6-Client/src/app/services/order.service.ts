import { Injectable } from '@angular/core';
import { environment } from '../../environments/environments';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Order } from '../Models/Order';
import { ProductList } from '../Models/ProductList';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private apiUrl = environment.apiurl + 'Order';
  private productListUrl = environment.apiurl + 'ProductLists'; // Add URL for ProductLists
  constructor(private http: HttpClient) {}

  getAll(): Observable<Order[]> {
    return this.http.get<Order[]>(this.apiUrl);
  }
  getById(orderId: number): Observable<Order> {
    return this.http.get<Order>(`${this.apiUrl}/${orderId}`);
  }
  create(order: Order): Observable<Order> {
    return this.http.post<Order>(this.apiUrl, order);
  }
  createProductList(productList: ProductList): Observable<ProductList> {
    return this.http.post<ProductList>(this.productListUrl, productList);
  }
  update(order: Order): Observable<Order> {
    return this.http.put<Order>(`${this.apiUrl}/${order.orderID}`, order);
  }

  delete(orderId: number): Observable<Order> {
    return this.http.delete<Order>(`${this.apiUrl}/${orderId}`);
  }
}
