import { Injectable } from '@angular/core';

/* NgRx */
import { Action } from '@ngrx/store';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { AccountAchievementsService } from '../account-achievements.service';
import { Observable, of } from 'rxjs';
import {
  AchievementsActionTypes, LoadAccountAchievements, AccountAchievementsLoaded,
  AccountAchievementsLoadFail,
  LoadTanksByAchievement,
  TanksByAchievementLoaded,
  TanksByAchievementLoadFailed
} from './achievements.actions';
import { mergeMap, catchError, map } from 'rxjs/operators';
import { CurrentAccountId } from '../../home/state/home.state';

@Injectable()
export class AchievementEffects {
  constructor(
    private achievementsService: AccountAchievementsService,
    private actions$: Actions
  ) { }

  @Effect()
  getAccountAchievements: Observable<Action> = this.actions$.pipe(
    ofType(AchievementsActionTypes.LoadAccountAchievements),
    map((action: LoadAccountAchievements) => action.payload),
    mergeMap((accountId: CurrentAccountId) =>
      this.achievementsService.getAccountAchievements(accountId.accountId, accountId.accountLoggedIn).pipe(
        map(achievements => (new AccountAchievementsLoaded(achievements))),
        catchError(err => of(new AccountAchievementsLoadFail(err))
        ))
    ));

  @Effect()
  getTanksByAchievement: Observable<Action> = this.actions$.pipe(
    ofType(AchievementsActionTypes.LoadTanksByAchievement),
    map((action: LoadTanksByAchievement) => action.payload),
    mergeMap((payload: { accountId: number, achievementId: string }) =>
      this.achievementsService.getTanksByAchievement(payload.accountId, payload.achievementId).pipe(
        map(tanks => (new TanksByAchievementLoaded(tanks))),
        catchError(err => of(new TanksByAchievementLoadFailed(err)))
      )
    )
  );
}
