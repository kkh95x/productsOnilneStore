import { Component } from '@angular/core';
import { ProductsService } from '../data/local_server_products_repository';
import { Product } from '../model/product';
@Component({
  selector: 'app-add-products',
  templateUrl: './add-products.component.html',
  styleUrls: ['./add-products.component.css']
})
export class AddProductsComponent {
  serivce:  ProductsService ;
  constructor(private productService: ProductsService){
this.serivce=productService;

  }
  name?:string;
  price?:number;
  photoUrl?:string;


catogerys=[
  'phone',
  'pc',
  'clothes',
  'food'
];
saveProject(){

  this.serivce.saveProduct(   {
    catogery:'clothes',
    color:'red',
    nameProduct:(this.name??"unknow name"),
    photoUrl:this.photoUrl??"https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/iphone15pro-digitalmat-gallery-1-202309_GEO_US?wid=364&hei=333&fmt=png-alpha&.v=1693346851451",
	price: this.price??0,
  })
}
}
