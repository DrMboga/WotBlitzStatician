import { Injectable, Inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { HttpClient, HttpParams, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

import { AccountInfo } from '../model/account-info';
import { AccountInfoDto } from '../model/account-info-dto';

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
  
  getAccount(accountId: number): Observable<AccountInfoDto> {
    return this.http.get<AccountInfoDto>(`${this.baseUrl}api/AccountInfo/${accountId}`)
    .pipe(catchError(this.handleError));
  }

  getDataByQuery(dataQuery: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}${dataQuery}`)
    .pipe(catchError(this.handleError));
  }

  getAccountStatHistory(accountId: number, dateFrom: Date): Observable<any> {
        let datefromString = this.datePipe.transform(dateFrom, 'yyyy-MM-dd');
        let params = new HttpParams()
          .set('dateFrom', datefromString)
        return this.http.get<any>(`${this.baseUrl}api/AccountInfo/AccountStatHistory/${accountId}`, { params })
        .pipe(catchError(this.handleError));
  }

  getTanksByAchievement(accountId: number, achievementId: string): Observable<any> {
    let params = new HttpParams()
      .set('achievementId', achievementId)
    return this.http.get<any>(`${this.baseUrl}api/TanksStat/TanksByAchievement/${accountId}`, { params })
    .pipe(catchError(this.handleError));
  }

  getTanksByMastery(accountId: number, rankOfMastery: string): Observable<any> {
    let params = new HttpParams()
      .set('markOfMastery', rankOfMastery)
    return this.http.get<any>(`${this.baseUrl}api/TanksStat/TanksByMastery/${accountId}`, { params })
    .pipe(catchError(this.handleError));
  }

// api/AccountInfo/ShortAccountInfo/46512100
  getShortAccountInfo(accountId: number) : Observable<AccountInfo> {
    return this.http.get<AccountInfo>(`${this.baseUrl}api/AccountInfo/ShortAccountInfo/${accountId}`)
    .pipe(catchError(this.handleError));
  }

  // api/AccountInfo/Achievements/46512100
  getAccountAchievements(accountId: number) : Observable<any> {
    return this.http.get<any>(`${this.baseUrl}api/AccountInfo/Achievements/${accountId}`)
                    .pipe(catchError(this.handleError));
  }


  // api/WgRequests/Authentication
  getAuthenticationRequest(redirectUrl: string) : Observable<string> {
    let params = new HttpParams()
      .set('redirectUrl', redirectUrl);
    return this.http.get<string>(`${this.baseUrl}api/WgRequests/Authentication`, { params: params })
                    .pipe(catchError(this.handleError));
  }

  putNewAccountInfo(accountInfo: AccountInfo) : Observable<{}>{
    return this.http.put<AccountInfo>(`${this.baseUrl}api/AccountInfo/${accountInfo.accountId}`, 
                                accountInfo, httpOptions)
                                .pipe(catchError(this.handleError));
  }

  downloadDictionariesAndImages() : Observable<{}> {
    return this.http.get(`${this.baseUrl}api/Dictionaries/LoadDictionariesAndPicturesIfNeeded`)
                    .pipe(catchError(this.handleError));
  }

  saveAllAccountInfo(accountId: number): Observable<{}> {
    return this.http.get(`${this.baseUrl}api/StatisticsCollector/${accountId}`)
                    .pipe(catchError(this.handleError));
  }

   private handleError(error: HttpErrorResponse) {
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
}
