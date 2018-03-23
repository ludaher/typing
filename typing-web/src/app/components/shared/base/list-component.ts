import { Component, OnInit } from '@angular/core';
import { BaseComponent } from './base-component';

export class ListComponent extends BaseComponent implements OnInit {

    error = '';
    /// Propiedades generales
    viewList = true;
    /// Porpiedades para la lista
    loading = false;
    total = 0;
    page = 1;
    perPage = 10;
    totalPages = 0;
    pagesToShow = 0;
    warning: string;
    filter: string;
    orderColumn = 'FormId';
    descendingOrder = false;

    constructor() {
        super();
    }

    protected updateData() {
        alert('Updated...');
    }

    ngOnInit() {
        this.updateData();
    }

    goToPage(page): void {
        this.page = page;
        this.updateData();
    }

    showList(): void {
        this.viewList = true;
    }
    showError(error) {
        this.error = error;
    }

    changeFilter(filter) {
        this.filter = filter;
        this.updateData();
    }

    changeOrder(order) {
        this.orderColumn = order.orderColumn;
        this.descendingOrder = order.descendingOrder;
        this.updateData();
    }
}
