import { Injectable, Inject } from '@angular/core';
import { WebapiRequestsService } from '../shared/services/webapi-requests.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { Observable } from 'rxjs';
import { AccountStatHistoryDto } from '../model/account-stat-history-dto';
import { catchError } from 'rxjs/operators';

@Injectable()
export class HistoryService extends WebapiRequestsService {

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private datePipe: DatePipe) { super(http, baseUrl); }

  getAccountStatHistory(
    accountId: number,
    dateFrom: Date
  ): Observable<AccountStatHistoryDto[]> {
    const datefromString = this.datePipe.transform(dateFrom, 'yyyy-MM-dd');
    const params = new HttpParams().set('dateFrom', datefromString);
    return this.http
      .get<AccountStatHistoryDto[]>(
        `${this.baseUrl}api/AccountInfo/AccountStatHistory/${accountId}`,
        { params }
      )
      .pipe(catchError(this.handleError));
  }

}
