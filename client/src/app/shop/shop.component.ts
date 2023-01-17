import { Component, OnInit } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/Pagination';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  brandIdSelected = 0 ;
  typeIdSelected:number = 0 ;
  sortSelected = "noOption";
  sortOptions = [
    {name:"Alphabetical", value:"noOption"},
    {name:"Price: Low to High", value:"priceAsc"},
    {name:"Price: High to Low", value:"priceDesc"}
  ];

  constructor(private shopService: ShopService){}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.brandIdSelected, this.typeIdSelected, this.sortSelected).subscribe((response : IPagination)=>{
      this.products = response.data;
    }, error=>{
      console.log(error);
    });
  }

  getBrands(){
    this.shopService.getBrands().subscribe( response =>{
      this.brands = [{id:0, name: "ALL"}, ... response];
    }, error=>{
      console.log(error);
    });
  }

  getTypes(){
    this.shopService.getTypes().subscribe( response =>{
      this.types = [{id:0, name: "ALL"}, ... response];
    }, error=>{
      console.log(error);
    });
  }

  onBrandSelected(brandId: number){
    this.brandIdSelected = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number){
    this.typeIdSelected = typeId;
    this.getProducts();
  }

  onSortSelected(sort:string){
    this.sortSelected = sort
    this.getProducts();
  }
}
