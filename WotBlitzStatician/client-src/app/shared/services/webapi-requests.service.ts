import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient, HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

@Injectable()
export class WebapiRequestsService {

  constructor(
    protected http: HttpClient,
    protected baseUrl: string
  ) { }

  protected httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  protected handleError(error: HttpErrorResponse) {
    let errorMessage: string;
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${error.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${error.status}, ` + `body was: ${error.error}`;
    }
    console.error(errorMessage);
    // return an observable with a user-facing error message
    return throwError(`Can not load data from backend; please try again later. Error: ${errorMessage}`);
  }

}
