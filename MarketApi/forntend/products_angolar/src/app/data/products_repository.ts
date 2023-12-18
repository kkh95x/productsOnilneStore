import { Product } from "../model/product";
export interface ProductsRepository{
 getAllProducts ():Product[];
 saveProduct(product: Product):void;
}