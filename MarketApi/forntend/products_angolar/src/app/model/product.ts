export class Product {
    constructor(
        nameProduct: string,
	price: number,
  color:string,
	catogery: string,
	photoUrl: string,
    ){
        this.nameProduct=nameProduct;
        this.catogery=catogery;
        this.color=color;
        this.price=price;
        this.photoUrl=photoUrl;
    }
	nameProduct: string|string;
	price: number;
  color:string
	catogery: string;
	photoUrl: string;
}