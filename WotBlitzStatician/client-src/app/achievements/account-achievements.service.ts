import { Injectable, Inject } from '@angular/core';
import { WebapiRequestsService } from '../shared/services/webapi-requests.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { Observable } from 'rxjs';
import { AccountAchievementDto } from '../model/account-achievement-dto';
import { catchError } from 'rxjs/operators';
import { TankByAchievementDto } from '../model/tank-by-achievement-dto';

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

  getTanksByAchievement(
    accountId: number,
    achievementId: string
  ): Observable<TankByAchievementDto[]> {
    const params = new HttpParams().set('achievementId', achievementId);
    return this.http
      .get<TankByAchievementDto[]>(
        `${this.baseUrl}api/TanksStat/TanksByAchievement/${accountId}`,
        { params }
      )
      .pipe(catchError(this.handleError));
  }
}
