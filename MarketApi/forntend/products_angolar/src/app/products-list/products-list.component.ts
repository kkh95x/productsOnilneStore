import { Component } from '@angular/core';
import { Product } from '../model/product';
import { ProductsService } from '../data/local_server_products_repository';
// import { ProductsService } from '../data/local_server_products_repository';
@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css'],
})
export class ProductsListComponent {
serivce:  ProductsService ;
  constructor(private productService: ProductsService){
this.serivce=productService;
this.products=productService.getAllProducts();
this.count=productService.getAllProducts().length;
  }

	products :Product[];
  count:number;
refresh():void{
  this.products=[];
  // this.products=this.serivce.getAllProducts();
  console.log(this.products.length);
  this.count=this.serivce.getAllProducts().length;
}

}
