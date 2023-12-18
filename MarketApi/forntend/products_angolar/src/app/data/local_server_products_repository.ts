
import { Product } from "../model/product";
import { ProductsRepository } from "./products_repository";
import { Injectable } from "@angular/core";
@Injectable(
    {
        providedIn:`root`
    }
)
export class ProductsService implements ProductsRepository{

    
    getAllProducts(): Product[] {
       return   products;
    }
    saveProduct(product: Product): void {
       products.push(product);
    }

}
const products:  Product[] =[
    {
nameProduct: 'IPhone 11 Pro Max',
color: 'red',
 price: 1200,
    catogery:"phone",
    photoUrl:"https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/iphone15pro-digitalmat-gallery-1-202309_GEO_US?wid=364&hei=333&fmt=png-alpha&.v=1693346851451"
    },
  {
    nameProduct: 'LapTop LG Core i51150H GTX 2080',
        color: 'black',
        price: 1000,
    catogery:"pc",
    photoUrl:"https://www.lg.com/us/images/laptops/md08000580/gallery/desktop-02.jpg"
    },
  {
    nameProduct: 'Samsong A70 Ram 8 Hard 256',
        color: 'blue',
        price: 800,
    catogery:"phone",
    photoUrl:"https://cdn.shortpixel.ai/spai/q_glossy+ret_img+to_webp/mobizil.com/wp-content/uploads/2019/04/samsung-galaxy-a70.jpg"
    },
];;
