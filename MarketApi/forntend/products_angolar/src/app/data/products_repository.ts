import { Observable } from "rxjs";
import { Product } from "../model/product";
export interface ProductsRepository{
 getAllProducts (): Promise<any>;
 saveProduct(product: Product):void;
}