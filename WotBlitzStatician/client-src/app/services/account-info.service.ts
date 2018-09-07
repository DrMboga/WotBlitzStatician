import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";

import { AccountInfo } from '../model/account-info';

@Injectable()
export class AccountInfoService {

  constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
  }

  getAccounts(): Observable<AccountInfo[]> {
    return this.http.get(`${this.baseUrl}api/AccountInfo`).map(response => response.json());
  }

  getAccount(accountId: number): Observable<AccountInfo> {
    return this.http.get(`${this.baseUrl}api/AccountInfo/${accountId}`)
      .map(response => response.json());
  }

  getDataByQuery(dataQuery: string): Observable<any> {
    return this.http.get(`${this.baseUrl}${dataQuery}`)
      .map(response => response.json());
  }

  getAccountStatHistory(accountId: number, dateFrom: Date): Observable<any> {
    return this.http.get(`${this.baseUrl}/api/AccountInfo/AccountStatHistory/${accountId}?dateFrom=${dateFrom}`)
      .map(response => response.json());
  }

}
