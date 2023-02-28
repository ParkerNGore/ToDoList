import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import configurl from '../../assets/config/config.json';
import { CreateListItemDto } from '../models/createListItemDto';
import { ListItem } from '../models/listItem';
import { GetListDto } from '../models/getListDto';
@Injectable({
  providedIn: 'root',
})
export class ListService {
  apiUrl: string = configurl.apiServer.url;
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };
  constructor(private http: HttpClient) {}

  create(dto: CreateListItemDto): Observable<ListItem> {
    return this.http
      .post<ListItem>(
        this.apiUrl + '/list/create',
        JSON.stringify(dto),
        this.httpOptions
      )
      .pipe(retry(1), catchError(this.handleError));
  }

  update(dto: ListItem, isNewListType: boolean): Observable<ListItem> {
    return this.http
      .put<ListItem>(
        this.apiUrl + '/list/update/' + isNewListType,
        JSON.stringify(dto),
        this.httpOptions
      )
      .pipe(retry(1), catchError(this.handleError));
  }

  delete(id: string): Observable<any> {
    return this.http
      .delete<ListItem>(this.apiUrl + '/list/delete/' + id, this.httpOptions)
      .pipe(retry(1), catchError(this.handleError));
  }

  getListItem(id: string): Observable<ListItem> {
    return this.http
      .get<ListItem>(this.apiUrl + '/list/getlistitem/' + id, this.httpOptions)
      .pipe(retry(1), catchError(this.handleError));
  }

  filterListItems(
    typeName: string,
    ignoreCompleted: boolean
  ): Observable<ListItem[]> {
    return this.http
      .get<ListItem[]>(
        this.apiUrl + '/list/getlistitem/' + typeName + '/' + ignoreCompleted,
        this.httpOptions
      )
      .pipe(retry(1), catchError(this.handleError));
  }

  getAllLists(): Observable<ListItem[]> {
    return this.http
      .get<ListItem[]>(this.apiUrl + '/list/getalllists', this.httpOptions)
      .pipe(retry(1), catchError(this.handleError));
  }

  getByViewWithOptions(dto: GetListDto): Observable<ListItem[]> {
    return this.http
      .post<ListItem[]>(
        this.apiUrl + '/list/getByViewWithOptions',
        JSON.stringify(dto),
        this.httpOptions
      )
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
