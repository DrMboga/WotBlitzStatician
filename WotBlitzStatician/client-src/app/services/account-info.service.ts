import { Injectable, Inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { HttpClient, HttpParams, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, filter, catchError, mergeMap } from 'rxjs/operators';
// import { throwError } from 'rxjs';

import { AccountInfo } from '../model/account-info';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json'
  })
};

@Injectable()
export class AccountInfoService {

    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private datePipe: DatePipe) {
    }
  
  getAccounts(): Observable<AccountInfo[]> {
    return this.http.get<AccountInfo[]>(`${this.baseUrl}api/AccountInfo`);
  }

  getAccount(accountId: number): Observable<AccountInfo> {
    return this.http.get<AccountInfo>(`${this.baseUrl}api/AccountInfo/${accountId}`);
  }

  getDataByQuery(dataQuery: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}${dataQuery}`);
  }

  getAccountStatHistory(accountId: number, dateFrom: Date): Observable<any> {
        let datefromString = this.datePipe.transform(dateFrom, 'yyyy-MM-dd');
        let params = new HttpParams()
          .set('dateFrom', datefromString)
        return this.http.get<any>(`${this.baseUrl}api/AccountInfo/AccountStatHistory/${accountId}`, { params });
  }

  getTanksByAchievement(accountId: number, achievementId: string): Observable<any> {
    let params = new HttpParams()
      .set('achievementId', achievementId)
    return this.http.get<any>(`${this.baseUrl}api/TanksStat/TanksByAchievement/${accountId}`, { params });
  }

  getTanksByMastery(accountId: number, rankOfMastery: string): Observable<any> {
    let params = new HttpParams()
      .set('markOfMastery', rankOfMastery)
    return this.http.get<any>(`${this.baseUrl}api/TanksStat/TanksByMastery/${accountId}`, { params });
  }

// api/AccountInfo/ShortAccountInfo/46512100
  getShortAccountInfo(accountId: number) : Observable<AccountInfo> {
    return this.http.get<AccountInfo>(`${this.baseUrl}api/AccountInfo/ShortAccountInfo/${accountId}`);
  }

  // api/WgRequests/Authentication
  getAuthenticationRequest(redirectUrl: string) : Observable<string> {
    let params = new HttpParams()
      .set('redirectUrl', redirectUrl);
    return this.http.get<string>(`${this.baseUrl}api/WgRequests/Authentication`, { params: params });
  }

  putNewAccountInfo(accountInfo: AccountInfo) : Observable<{}>{
    return this.http.put<AccountInfo>(`${this.baseUrl}api/AccountInfo/${accountInfo.accountId}`, 
                                accountInfo, httpOptions);
                                // .pipe(
                                //   catchError(this.handleError('putNewAccountInfo', accountInfo))
                                //);
  }

  downloadDictionariesAndImages() : Observable<{}> {
    return this.http.get(`${this.baseUrl}api/Dictionaries/LoadDictionariesAndPicturesIfNeeded`)
  }

  saveAllAccountInfo(accountId: number): Observable<{}> {
    return this.http.get(`${this.baseUrl}api/StatisticsCollector/${accountId}`);
  }

/*   private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    // return an observable with a user-facing error message
    return throwError(
      'Something bad happened; please try again later.');
  };
 */}
