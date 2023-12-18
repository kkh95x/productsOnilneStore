
import { Product } from "../model/product";
import { ProductsRepository } from "./products_repository";
import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable(
    {
        providedIn:`root`
    }
)
export class ProductsService implements ProductsRepository{
    private apiUrlp = 'https://localhost:7265/api/Products'; // Replace with your API endpoint
    private apiUrla = 'https://localhost:7265/a'; // Replace with your API endpoint

    constructor(private http: HttpClient) {}

     getAllProducts(): Promise<any> {
      return this.http.get<any>(this.apiUrlp).toPromise();
       
    ;
    }
   async saveProduct(product: Product) {
        await this.http.post<any>(this.apiUrla,{
            createdAt: "2023-12-18T13:40:48.495Z",
            "name": product.nameProduct,
            "description": product.catogery,
            "price": product.price,
            "photoUrl": product.photoUrl
        }).toPromise()
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
