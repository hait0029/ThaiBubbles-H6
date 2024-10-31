import { Category } from "./Category";
import { ProductList } from "./ProductList";

export interface Product {
  "productID": number
  "name": string
  "price": number
  "category"?: Category;
  "categoryId": number;
"Descripåtion": string;
  }
