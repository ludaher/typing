import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Customer } from '../../../../model/process/customer.model';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {

  @Input() listOfData: Customer[];

  @Output() clickItem = new EventEmitter<any>();

  @Output() changeFilter = new EventEmitter<string>();

  @Output() changeOrder = new EventEmitter<any>();

  descendingOrder = false;
  orderColumn = 'CustomerId';

  idFilter: string;
  nameFilter: string;
  typificationsFilter: string;
  activeFilter: string;

  constructor() { }

  ngOnInit() {
  }


  clickRow(productId: number): void {
    this.clickItem.emit(productId);
  }

  filter() {
    let filter = '';
    if (this.idFilter && this.idFilter !== '') {
      filter += '|CustomerId,13,' + this.idFilter;
    }
    if (this.nameFilter && this.nameFilter !== '') {
      filter += '|Name,0,' + this.nameFilter;
    }
    this.changeFilter.emit(filter);
  }

  order(orderColumn) {
    this.orderColumn = orderColumn;
    this.descendingOrder = !this.descendingOrder;
    this.changeOrder.emit({ orderColumn: orderColumn, descendingOrder: this.descendingOrder });
  }
}
