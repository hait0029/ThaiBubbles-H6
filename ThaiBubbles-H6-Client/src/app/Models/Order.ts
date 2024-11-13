import { ProductList } from "./ProductList"
import { User } from "./User"

export interface Order {
  "orderID": number;
  "orderDate": Date;

  productList: ProductList[];
}
