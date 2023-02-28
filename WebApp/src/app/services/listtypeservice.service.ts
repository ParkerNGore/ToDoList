import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import configurl from '../../assets/config/config.json';
import { CreateListItemDto } from '../models/createListItemDto';
import { ListItem } from '../models/listItem';
import { GetListDto } from '../models/getListDto';
import { ListType } from '../models/listType';

@Injectable({
  providedIn: 'root',
})
export class ListTypeService {
  apiUrl: string = configurl.apiServer.url;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  constructor(private http: HttpClient) {}

  getListTypes(): Observable<ListType[]> {
    return this.http
      .get<ListType[]>(this.apiUrl + '/list/getListTypes', this.httpOptions)
      .pipe(retry(1), catchError(this.handleError));
  }

  handleError(error: any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(() => {
      return errorMessage;
    });
  }
}
