import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/productType';
import { IPagination } from '../shared/models/Pagination';
import { map } from 'rxjs/operators';
import { shopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) { }

  getProducts(shopParams : shopParams){
    let params = new HttpParams();

    if(shopParams.brandId !=0){
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if(shopParams.typeId != 0){
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if(shopParams.search ){
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('PageSize', shopParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl+'products', {observe: 'response',params})
    .pipe(
      map(Response=> {
        return Response.body;
      })
    );
  }

  getBrands(){
    return this.http.get<IBrand[]>(this.baseUrl+'products/Brands')
  }

  getTypes(){
    return this.http.get<IType[]>(this.baseUrl+'products/Types')
  }

}
