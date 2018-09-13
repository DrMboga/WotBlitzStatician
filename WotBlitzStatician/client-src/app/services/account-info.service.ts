import { Injectable, Inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Http } from '@angular/http';
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";

import { AccountInfo } from '../model/account-info';

@Injectable()
export class AccountInfoService {

    constructor(
        private http: Http,
        @Inject('BASE_URL') private baseUrl: string,
        private datePipe: DatePipe) {
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
        let datefromString = this.datePipe.transform(dateFrom, 'yyyy-MM-dd');
        return this.http.get(`${this.baseUrl}/api/AccountInfo/AccountStatHistory/${accountId}?dateFrom=${datefromString}`)
      .map(response => response.json());
  }

}
