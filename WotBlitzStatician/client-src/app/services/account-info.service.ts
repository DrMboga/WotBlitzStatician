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
    return this.http.get<AccountInfo>(`${this.baseUrl}api/AccountInfo/ShortAccountInfo/${accountId}`)
            .map(d => d as AccountInfo);
  }
}
