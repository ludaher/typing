import { Component, OnInit } from '@angular/core';
import { ProductProcess } from '../../../model/typification/product-process.model';
import { ProductsService } from '../../../services/admin/products.service';
import { Product } from '../../../model/process/product.model';
import { BaseComponent } from '../../shared/base/base-component';
import { ListComponent } from '../../shared/base/list-component';
import { CustomersService } from '../../../services/admin/customers.service';
import { Customer } from '../../../model/process/customer.model';
// import { safeSubscribe  } from '../../safe-suscribe';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent extends ListComponent implements OnInit {

  customers: any;
  products: ProductProcess[];
  orderColumn = 'FormId';

  /// Propiedades para el detalle
  ///
  product = new Product('0');

  constructor(private customerService: CustomersService,
    private service: ProductsService) {
    super();
  }

  ngOnInit() {
    this.loadCustomers();
    this.updateData();
  }

  loadCustomers(): any {
    this.customerService
      .getSelect<Customer>('CustomerId, Name', null, 'Name')
      .safeSubscribe(
        this,
        result => {
          this.customers = result.resultList;
          if (result.total === 0) {
            this.error = 'No hay clientes para crear el producto';
          }
        },
        err => this.error = err.error.message
      );
  }

  goToPage(page): void {
    this.page = page;
    this.updateData();
  }

  updateData(): void {
    this.loading = true;
    this.error = '';
    this.service
      .getList<any>(
        this.filter,
        this.orderColumn,
        this.descendingOrder,
        this.page,
        this.perPage
      ).safeSubscribe(
        this,
        result => {
          this.total = result.total;
          this.products = result.resultList;
          this.pagesToShow = Math.ceil(this.total / this.perPage);
          this.products = result.resultList;
          this.loading = false;
          if (this.total === 0) {
            this.warning = 'No hay datos para mostrar';
          }
        },
        err => {
          this.showError(err.error.message);
          this.loading = false;
        }
      );
    this.viewList = true;
  }

  showProduct(productId): void {
    this.loading = true;
    this.service
      .getById<any>(productId)
      .safeSubscribe(
        this,
        product => {
          this.loading = false;
          this.product = product;
          this.viewList = false;

          if (this.customers) {
            const customer = this.customers.filter(c => c.CustomerId === this.product.customerId);
            if (customer.length > 0) {
              this.product.customerName = customer[0].Name;
            }
          }
          // console.log(this.product);
        },
        err => {
          this.showError(err.error.message);
          this.loading = false;
        }
      );
  }

  addProduct() {
    this.product = new Product('0');
    this.viewList = false;
  }

}
