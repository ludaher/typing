import { Component, OnInit, Output, Input, EventEmitter, ElementRef, ViewChild } from '@angular/core';
import { Customer } from '../../../../model/process/customer.model';
import { CustomersService } from '../../../../services/admin/customers.service';
import { BaseComponent } from '../../../shared/base/base-component';


@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent extends BaseComponent implements OnInit {

  _customer: Customer;
  get customer(): Customer {
    return this._customer;
  }

  @Input('customer')
  set customer(value: Customer) {
    this._customer = value;
  }

  @Output() cancel = new EventEmitter<number>();
  @Output() saveComplete = new EventEmitter();
  @Output() error = new EventEmitter<string>();

  @ViewChild('fileInput') fileInput: ElementRef;

  fileData: any;
  loading = false;

  constructor(private customerService: CustomersService) {
    super();
  }

  ngOnInit() {
  }

  onSubmit() {
    this.loading = true;
    if (this.customer.createOn === '') {
      this.customerService
        .add<Customer>(this.customer)
        .safeSubscribe(
          this,
          customer => {
            this.loading = false;
            this.saveComplete.emit();
          },
          err => {
            this.loading = false;
            this.error.emit(err.error.message);
          }
        );
    } else {
      this.customerService
        .update<Customer>(this.customer)
        .safeSubscribe(
          this,
          result => {
            this.loading = false;
            this.saveComplete.emit();
          },
          err => {
            this.loading = false;
            this.error.emit(err.error.message);
          }
        );
    }
  }

  cancelClick(): void {
    this.cancel.emit();
  }

}
