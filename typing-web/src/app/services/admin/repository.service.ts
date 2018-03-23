import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { environment } from '../../../environments/environment';

@Injectable()
export class RepositoryService {

  apiRoot = environment.repositoryOrigin;

  constructor(public http: HttpClient) { }

  upload<FileStorage>(entity: FileStorage): Observable<any> {
    const url = `${this.apiRoot}/api/Files`;
    return this.http.put<any>(url, entity);
  }

}
