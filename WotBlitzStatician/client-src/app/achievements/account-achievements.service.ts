import { Injectable, Inject } from '@angular/core';
import { WebapiRequestsService } from '../shared/services/webapi-requests.service';
import { HttpClient } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { Observable } from 'rxjs';
import { AccountAchievementDto } from '../model/account-achievement-dto';
import { catchError } from 'rxjs/operators';

@Injectable()
export class AccountAchievementsService extends WebapiRequestsService {

  constructor(
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private datePipe: DatePipe) { super(http, baseUrl); }

  // api/AccountInfo/Achievements/46512100
  getAccountAchievements(
    accountId: number,
    isAccountLoggedin: boolean
  ): Observable<AccountAchievementDto[]> {
    return this.http
      .get<AccountAchievementDto[]>(
        `${this.baseUrl}api/AccountInfo/Achievements/${accountId}`
      )
      .pipe(catchError(this.handleError));
  }

}
