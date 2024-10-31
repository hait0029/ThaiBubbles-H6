import { Product } from './Product';
import { Order } from './Order';

export interface ProductList {
  productOrderListID: number;
  quantity: number;
  productId: number;
  Product?: Product; // Should be a single object, not an array
  orderId: number;
  Order?: Order; // Should be a single object, not an array
}
