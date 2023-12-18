import { Component, OnInit } from '@angular/core';
import { Product } from '../model/product';
import { ProductsService } from '../data/local_server_products_repository';
// import { ProductsService } from '../data/local_server_products_repository';
@Component({
  selector: 'app-products-list',
  templateUrl: './products-list.component.html',
  styleUrls: ['./products-list.component.css'],
})
export class ProductsListComponent  implements OnInit {
  constructor(private productService: ProductsService){

  }
  ngOnInit(): void {
    this.productService.getAllProducts().then((result: any[]) => {
      result.forEach((element: any) => {
        this.products.push({
          catogery: element.description,
          color: element.color,
          nameProduct: element.name,
          photoUrl: element.photoUrl, // You may need to set the actual URL for the photo
          price: element.price,
        });
      })});
  }

	products :Product[]=[];
  count:number=0;
refresh():void{
  this.products=[];
  // this.products=this.serivce.getAllProducts();
  // console.log(this.products.length);
  // this.count=this.serivce.getAllProducts();
}

}
