import { Component, OnInit, Input, OnDestroy, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { TankByAchievementDto } from '../../model/tank-by-achievement-dto';
import { AccountAchievementDto } from '../../model/account-achievement-dto';
import { Subscription, of, Observable } from 'rxjs';
import { Actions, ofType } from '@ngrx/effects';
import { Store, select } from '@ngrx/store';
import { State } from '../../state/app.state';
import { getAccountId } from '../../state/app.selectors';
import { takeWhile, map, mergeMap, catchError, tap, filter } from 'rxjs/operators';
import { AppActionTypes, AccountInfoRefreshed } from '../../state/app.actions';
import { AccountAchievementsService } from '../account-achievements.service';
import { CurrentAccountId } from '../../home/state/home.state';

@Component({
  selector: 'app-account-achievements',
  templateUrl: './account-achievements.component.html',
  styleUrls: ['./account-achievements.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AccountAchievementsComponent implements OnInit, OnDestroy {
  private componentActive = true;
  private isAccountLoggedin: boolean;

  public battleAchievements$: Observable<AccountAchievementDto[]>;
  public epicAchievements$: Observable<AccountAchievementDto[]>;
  public platoonAchievements$: Observable<AccountAchievementDto[]>;
  public titleAchievements$: Observable<AccountAchievementDto[]>;
  public commemorativeAchievements$: Observable<AccountAchievementDto[]>;
  public stepAchievements$: Observable<AccountAchievementDto[]>;

  public tanksByAchievement: TankByAchievementDto[];

  public loadAchievemntsError$: Observable<string>;

  constructor(
    private store: Store<State>,
    private actions$: Actions,
    private accountAchievementsService: AccountAchievementsService,
    private changeDetector: ChangeDetectorRef) {
  }

  ngOnInit() {
    this.store.pipe(
      select(getAccountId),
      takeWhile(() => this.componentActive),
      mergeMap((accountId: CurrentAccountId) =>
        this.accountAchievementsService.getAccountAchievements(accountId.accountId, accountId.accountLoggedIn)
          .pipe(
            tap(() => this.isAccountLoggedin = accountId.accountLoggedIn),
          ))
    )
      .subscribe(
        (accountAchievements: AccountAchievementDto[]) => this.refreshAchievements(accountAchievements),
        err => this.handleError(err)
      );

    this.actions$.pipe(
      ofType(AppActionTypes.AccountInfoRefreshed),
      map((action: AccountInfoRefreshed) => action.payload),
      takeWhile(() => this.componentActive),
      mergeMap((accountId: CurrentAccountId) =>
        this.accountAchievementsService.getAccountAchievements(accountId.accountId, accountId.accountLoggedIn)
          .pipe(
            tap(() => this.isAccountLoggedin = accountId.accountLoggedIn),
          ))
    )
      .subscribe(
        (accountAchievements: AccountAchievementDto[]) => this.refreshAchievements(accountAchievements),
        err => this.handleError(err)
      );
  }

  private refreshAchievements(accountAchievements: AccountAchievementDto[]) {
    this.battleAchievements$ = of(accountAchievements.filter(
      achievement => achievement.section === 'battle'
    ));
    this.epicAchievements$ = of(accountAchievements.filter(
      achievement => achievement.section === 'epic'
    ));
    this.platoonAchievements$ = of(accountAchievements.filter(
      achievement => achievement.section === 'platoon'
    ));
    this.titleAchievements$ = of(accountAchievements.filter(
      achievement => achievement.section === 'title'
    ));
    this.commemorativeAchievements$ = of(accountAchievements.filter(
      achievement => achievement.section === 'commemorative'
    ));
    this.stepAchievements$ = of(accountAchievements.filter(
      achievement => achievement.section === 'step'
    ));
    this.changeDetector.markForCheck();
  }

  private handleError(error: string) {
    this.loadAchievemntsError$ = of(error);
    this.battleAchievements$ = null;
    this.epicAchievements$ = null;
    this.platoonAchievements$ = null;
    this.titleAchievements$ = null;
    this.commemorativeAchievements$ = null;
    this.stepAchievements$ = null;

    this.changeDetector.markForCheck();
  }

  getTanksByAchievement(achievement: AccountAchievementDto) {
    console.log('isAccountLoggedin', this.isAccountLoggedin);
    if (
      !this.isAccountLoggedin ||
      achievement.isAchievementOption ||
      (achievement.section === 'battle' &&
        achievement.achievementId.includes('Mastery'))
    ) {
      this.tanksByAchievement = null;
      return;
    }
    console.log('achievementId', achievement.achievementId);
    // this.accountsInfoService
    // .getTanksByAchievement(
    //   this.accountGlobalInfo.accountId,
    //   achievement.achievementId
    // )
    // .subscribe(
    //   data => {
    //     this.tanksByAchievement = data;
    //     this.tanksByAchievement.sort(
    //       (left, right): number => {
    //         if (left.achievementsCount < right.achievementsCount) {
    //           return 11;
    //         }
    //         if (left.achievementsCount > right.achievementsCount) {
    //           return -1;
    //         }
    //         return 0;
    //       }
    //     );
    //   },
    //   error => console.error(error)
    // );
  }

  ngOnDestroy(): void {
    this.componentActive = false;
  }
}
