import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';

import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { WebapiRequestsService } from '../shared/services/webapi-requests.service';

import { AccountInfoDto } from '../model/account-info-dto';
import { PlayerPrivateInfo } from '../model/player-private-info';

@Injectable()
export class AccountsService extends WebapiRequestsService {

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private datePipe: DatePipe) { super(http, baseUrl); }

  getAccount(accountId: number, isLoggedIn: boolean): Observable<AccountInfoDto> {
    const url = isLoggedIn
      ? `${this.baseUrl}api/AccountInfo/${accountId}`
      : `${this.baseUrl}api/GuestAccount/${accountId}/accountinfo`;
    return this.http
      .get<AccountInfoDto>(url)
      .pipe(catchError(this.handleError));
  }

  getPlayerPrivateInfo(accountId: number, isLoggedIn: boolean): Observable<PlayerPrivateInfo> {
    if (isLoggedIn) {
      return this.http
        .get<PlayerPrivateInfo>(
          `${this.baseUrl}api/WgRequests/AccountPrivateInfo/${accountId}`
        )
        .pipe(catchError(this.handleError));
    }
    return new Observable<PlayerPrivateInfo>();
  }
}
