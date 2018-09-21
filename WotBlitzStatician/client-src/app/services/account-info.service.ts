import { Injectable, Inject } from '@angular/core';
import { DatePipe } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";

import { AccountInfo } from '../model/account-info';

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

}
