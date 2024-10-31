import { Product } from "./Product";

export interface Category{
  "categoryID": number;
  "categoryName": string;
  product?: Product[];
}
