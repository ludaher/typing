import { Component, OnInit, Input, EventEmitter, Output, ViewChild, ElementRef, HostListener } from '@angular/core';
import { Product } from '../../../../model/process/product.model';
import { DocumentalTypesService } from '../../../../services/admin/documental-types.service';
import { ProductsService } from '../../../../services/admin/products.service';
import { BaseComponent } from '../../../shared/base/base-component';
import { CustomersService } from '../../../../services/admin/customers.service';
import { RepositoryService } from '../../../../services/admin/repository.service';
import { FileStorage } from '../../../../model/shared/FileStorage';
import { Customer } from '../../../../model/process/customer.model';
import { AuthService } from '../../../../services/authentication/auth.service';
import { environment } from '../../../../../environments/environment';


@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent extends BaseComponent {

  pdfSrc: any;
  pdfLoaded = true;
  fileName: string;
  scrollTop = 0;
  target: any;
  captures = ['1', '2'];

  @Input() Product: Product;
  @Input() customers: any;

  _product: Product;
  get product(): Product {
    return this._product;
  }

  @Input('product')
  set product(value: Product) {
    this._product = value;
    if (value.templatePath !== '') {
      const url = `${environment.repositoryOrigin}/api/files/download/${value.templatePath}`;
      this.pdfLoaded = false;
      this.pdfSrc = {
        httpHeaders: {
          'Authorization': this.authService.getAuthorizationHeaderValue()
        },
        url: url
      };
    }
  }

  @Output() cancel = new EventEmitter<number>();
  @Output() saveComplete = new EventEmitter();
  @Output() error = new EventEmitter<string>();

  @ViewChild('fileInput') fileInput: ElementRef;

  @ViewChild('pdfContainer') pdfContainer: ElementRef;

  @ViewChild('pdfContainer') pdfViewer: ElementRef;

  fileData: any;
  loading = false;

  documentalTypes: any[];

  constructor(
    private authService: AuthService,
    private repositoryService: RepositoryService,
    private productService: ProductsService,
    private documentalTypesService: DocumentalTypesService) {
    super();
  }

  onSubmit() {
    this.loading = true;
    if (this.fileData == null) {
      this.saveProduct();
    } else {
      this.saveFile();
    }
  }

  saveFile() {
    if (this.fileData != null) {
      const entity = new FileStorage('', this.fileName, this.fileData);
      this.repositoryService
        .upload(entity)
        .safeSubscribe(
          this,
          result => {
            this.product.templatePath = result;
            this.saveProduct();
          },
          err => {
            this.loading = false;
            this.error.emit('Ha ocurrido un error almacenando el archivo');
          });
    } else {
      this.saveProduct();
    }
  }

  saveProduct() {
    if (this.product.templatePath === '') {
      this.error.emit('Debe cargar un archivo de ejemplo.');
    }
    if (this.product.formId === 0) {
      this.productService
        .add<Product>(this.product)
        .safeSubscribe(
          this,
          product => {
            this.loading = false;
            this.product = product;
          },
          err => {
            this.error.emit(err.error.message);
          }
        );
    } else {
      this.productService
        .update<Product>(this.product)
        .safeSubscribe(
          this,
          result => {
            this.loading = false;
            this.saveComplete.emit();
          },
          error => {
            this.error.emit('Ha ocurrido un error con la aplicación');
          }
        );
    }
  }
  cancelClick(): void {
    this.cancel.emit();
  }


  loadDocumentalTypes(formId): void {
    if (!formId) {
      return;
    }
    this.loading = true;
    this.documentalTypes = undefined;
    this.documentalTypesService
      .getList<any>(`formId,13,${formId}`, 'Value', true)
      .safeSubscribe(
        this,
        result => {
          this.loading = false;
          this.documentalTypes = result.resultList;
        },
        error => {
          this.loading = true;
          this.error.emit('Ha ocurrido un error con la aplicación');
        }
      );

  }

  onFileChange(event) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length > 0) {
      const file = event.target.files[0];
      reader.onload = () => {
        this.pdfLoaded = false;
        const results = reader.result.split(',');
        this.fileData = results[1];
        console.log(results[1]);
        console.log(this.fileData);
        this.fileName = this.fileInput.nativeElement.value;
        this.pdfSrc = reader.result;
      };
      reader.readAsDataURL(file);
    }
  }

  clearFile() {
    this.fileData = null;
    this.fileInput.nativeElement.value = '';
    this.fileName = '';
  }

  onLoadPdfComplete($event) {
    this.pdfLoaded = true;
    this.product.templateHeight = this.pdfContainer.nativeElement.scrollHeight;
  }

  pageRendered($event) {
    this.pdfLoaded = true;
  }

  uploadFile() {

  }

  onScroll($event) {
    this.scrollTop = $event.target.scrollTop;
    this.target = $event.target;
  }

  // scrollTop_(elem): any {
  //   if (typeof elem.scrollTop === 'function') {
  //     return elem.scrolltop();
  //   } else if (elem.constructor === Array && typeof elem[0].scrolltop !== 'undefined') {
  //     return elem[0].scrolltop;
  //   }
  //   const comp = (window.pageYOffset !== undefined) ?
  //     window.pageYOffset :
  //     (document.documentElement || document.body.parentNode || document.body);
  //   return comp.scrollTop;
  // }
  onSelectCustomer($event) {
    this.product.customerId = $event.item.CustomerId;
  }

  OnCustomersNoResult($event) {
    this.product.customerId = 0;
  }

  OnCapturesNoResult($event) {
    this.product.requiredCaptures = '';
  }
}
