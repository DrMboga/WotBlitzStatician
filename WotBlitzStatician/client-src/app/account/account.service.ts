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

  getAccount(accountId: number): Observable<AccountInfoDto> {
    return new Observable();
    // const url = this.accountGlobalInfo.isGuestAccount
    //   ? `${this.baseUrl}api/GuestAccount/${accountId}/accountinfo`
    //   : `${this.baseUrl}api/AccountInfo/${accountId}`;
    // return this.http
    //   .get<AccountInfoDto>(url)
    //   .pipe(catchError(this.handleError));
  }

  getPlayerPrivateInfo(accountId: number): Observable<PlayerPrivateInfo> {
    return new Observable();

    // if (this.accountGlobalInfo.isGuestAccount) {
    //   return new Observable<PlayerPrivateInfo>();
    // }
    // return this.http
    //   .get<PlayerPrivateInfo>(
    //     `${this.baseUrl}api/WgRequests/AccountPrivateInfo/${accountId}`
    //   )
    //   .pipe(catchError(this.handleError));
  }
}
