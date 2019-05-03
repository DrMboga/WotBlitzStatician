import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DatePipe } from '@angular/common';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { WebapiRequestsService } from './webapi-requests.service';
import { AccountInfo } from '../../model/account-info';

@Injectable()
export class BlitzStaticianService extends WebapiRequestsService {

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private datePipe: DatePipe) { super(http, baseUrl); }

  // api/WgRequests/Authentication
  getAuthenticationRequest(redirectUrl: string): Observable<string> {
    const params = new HttpParams().set('redirectUrl', redirectUrl);
    return this.http
      .get<string>(`${this.baseUrl}api/WgRequests/Authentication`, {
        params: params
      })
      .pipe(catchError(this.handleError));
  }

  putNewAccountInfo(accountInfo: AccountInfo): Observable<{}> {
    return this.http
      .put<AccountInfo>(
        `${this.baseUrl}api/AccountInfo/${accountInfo.accountId}`,
        accountInfo,
        this.httpOptions
      )
      .pipe(catchError(this.handleError));
  }

  saveAllAccountInfo(accountId: number): Observable<{}> {
    return this.http
      .get(`${this.baseUrl}api/StatisticsCollector/${accountId}`)
      .pipe(catchError(this.handleError));
  }

  findAccounts(
    accountNickTemplate: string
  ): Observable<AccountInfo[]> {
    return this.http
      .get<AccountInfo[]>(
        `${this.baseUrl}api/WgRequests/FindAccounts/${accountNickTemplate}`
      )
      .pipe(catchError(this.handleError));
  }

  // api/GuestAccount/accountId
  putGuestAccountToCache(accountId: number): Observable<{}> {
    return this.http
      .put(
        `${this.baseUrl}api/GuestAccount/${accountId}`,
        this.httpOptions
      )
      .pipe(catchError(this.handleError));
  }

  // api/AccountInfo/ShortAccountInfo/46512100
  getShortAccountInfo(accountId: number): Observable<AccountInfo> {
    return this.http
      .get<AccountInfo>(
        `${this.baseUrl}api/AccountInfo/ShortAccountInfo/${accountId}`
      )
      .pipe(catchError(this.handleError));
  }


}
