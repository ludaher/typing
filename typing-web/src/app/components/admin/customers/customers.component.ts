import { Component, OnInit } from '@angular/core';
import { Customer } from '../../../model/process/customer.model';
import { CustomersService } from '../../../services/admin/customers.service';
import { BaseComponent } from '../../shared/base/base-component';
import { ListComponent } from '../../shared/base/list-component';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent extends ListComponent {

  listOfData: Customer[];
  orderColumn = 'CustomerId';
  descendingOrder = false;

  /// Propiedades para el detalle
  ///
  customer = new Customer('0');

  constructor(private service: CustomersService) {
    super();
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
          this.listOfData = result.resultList;
          this.pagesToShow = Math.ceil(this.total / this.perPage);
          this.loading = false;
          if (this.total === 0) {
            this.warning = 'No hay datos para mostrar';
          }
        },
        error => this.showError(error.error.message)
      );
    this.viewList = true;
  }

  showCustomer(customerId): void {
    this.loading = true;
    this.service
      .getById<Customer>(customerId)
      .safeSubscribe(
        this,
        customer => {
          this.loading = false;
          this.customer = customer;
          this.viewList = false;
          // console.log(this.product);
        },
        error => {
          this.showError(error.error.message);
          this.loading = false;
        }
      );
  }

  addProduct() {
    this.customer = new Customer('0');
    this.viewList = false;
  }
}
