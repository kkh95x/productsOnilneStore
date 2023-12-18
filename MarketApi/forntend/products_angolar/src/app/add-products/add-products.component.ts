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
  descrption?:string;
  color?:string;
  photoUrl?:string;



 async onSubmit() {

  console.log("saveProject");
 await this.serivce.saveProduct(   {
    catogery:this.descrption??"Uncknow",
    color:this.color??"",
    nameProduct:(this.name??"unknow name"),
    photoUrl:this.photoUrl??"https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/iphone15pro-digitalmat-gallery-1-202309_GEO_US?wid=364&hei=333&fmt=png-alpha&.v=1693346851451",
	price: this.price??0,
  }).then(
    response => {
      console.log('POST success:', response);
      // Reset the form after successful submission
    },
    error => {
      console.error('POST error:', error);
    }
  );
  this.color="";
  this.descrption="";
this.name="";
this.photoUrl="";
this.price=0;

}
}
