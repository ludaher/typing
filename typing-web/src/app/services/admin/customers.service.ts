import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { ProductProcess } from '../../model/typification/product-process.model';
import { environment } from '../../../environments/environment';
import { Product } from '../../model/process/product.model';
import { PagingUtil } from '../utilities/pagingUtil';
import { GenericService } from '../utilities/GenericService';

@Injectable()
export class CustomersService extends GenericService {
  apiRoot = environment.origin;

  constructor(public http: HttpClient, protected paging: PagingUtil) {
    super(http, paging, 'api/customers');
  }
}
