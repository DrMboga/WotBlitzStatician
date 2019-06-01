import { Component, OnInit, OnDestroy, ChangeDetectionStrategy } from '@angular/core';
import { TankByAchievementDto } from '../../model/tank-by-achievement-dto';
import { AccountAchievementDto } from '../../model/account-achievement-dto';
import { of, Observable } from 'rxjs';
import { Actions, ofType } from '@ngrx/effects';
import { Store, select } from '@ngrx/store';
import { getAccountId } from '../../state/app.selectors';
import { takeWhile, map } from 'rxjs/operators';
import { AppActionTypes, AccountInfoRefreshed } from '../../state/app.actions';
import { CurrentAccountId } from '../../home/state/home.state';
import {
  State, getEpicAchievements, getBattleAchievements, getPlatoonAchievements, getTitleAchievements,
  getCommemorativeAchievements, getStepAchievements, getAchevementsLoadError, getTanksByAchievement,
  getTanksByAchievemntsLoadError
} from '../state/achevements.selector';
import { LoadAccountAchievements, LoadTanksByAchievement, ClearTanksByAchievement } from '../state/achievements.actions';

@Component({
  selector: 'app-account-achievements',
  templateUrl: './account-achievements.component.html',
  styleUrls: ['./account-achievements.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AccountAchievementsComponent implements OnInit, OnDestroy {
  private componentActive = true;
  private currentAccount: CurrentAccountId;

  public battleAchievements$: Observable<AccountAchievementDto[]>;
  public epicAchievements$: Observable<AccountAchievementDto[]>;
  public platoonAchievements$: Observable<AccountAchievementDto[]>;
  public titleAchievements$: Observable<AccountAchievementDto[]>;
  public commemorativeAchievements$: Observable<AccountAchievementDto[]>;
  public stepAchievements$: Observable<AccountAchievementDto[]>;

  public tanksByAchievement$: Observable<TankByAchievementDto[]>;
  public clickedAchievementImage$: Observable<string>;
  public clickedAchievementDescription$: Observable<string>;

  public loadAchievemntsError$: Observable<string>;
  public loadTanksByAchievemntsError$: Observable<string>;

  constructor(
    private store: Store<State>,
    private actions$: Actions
  ) {
  }

  ngOnInit() {
    this.store.pipe(
      select(getAccountId),
      takeWhile(() => this.componentActive),
    )
      .subscribe(accountId => {
        this.currentAccount = accountId;
        if (accountId.accountId === 0) {
          return;
        }
        this.store.dispatch<LoadAccountAchievements>(new LoadAccountAchievements(accountId));
      }
      );

    this.actions$.pipe(
      ofType(AppActionTypes.AccountInfoRefreshed),
      map((action: AccountInfoRefreshed) => action.payload),
      takeWhile(() => this.componentActive),
    )
      .subscribe(accountId => {
        this.currentAccount = accountId;
        if (accountId.accountId === 0) {
          return;
        }
        this.store.dispatch<LoadAccountAchievements>(new LoadAccountAchievements(accountId));
      }
      );

    this.epicAchievements$ = this.store.pipe(select(getEpicAchievements));
    this.battleAchievements$ = this.store.pipe(select(getBattleAchievements));
    this.platoonAchievements$ = this.store.pipe(select(getPlatoonAchievements));
    this.titleAchievements$ = this.store.pipe(select(getTitleAchievements));
    this.commemorativeAchievements$ = this.store.pipe(select(getCommemorativeAchievements));
    this.stepAchievements$ = this.store.pipe(select(getStepAchievements));
    this.loadAchievemntsError$ = this.store.pipe(select(getAchevementsLoadError));
    this.tanksByAchievement$ = this.store.pipe(select(getTanksByAchievement));
    this.loadTanksByAchievemntsError$ = this.store.pipe(select(getTanksByAchievemntsLoadError));
  }

  getTanksByAchievement(achievement: AccountAchievementDto) {
    this.clickedAchievementImage$ = of(achievement.image);
    this.clickedAchievementDescription$ = of(`${achievement.name}`);
    if (
      !this.currentAccount.accountLoggedIn ||
      achievement.isAchievementOption ||
      (achievement.section === 'battle' &&
        achievement.achievementId.includes('Mastery'))
    ) {
      this.store.dispatch<ClearTanksByAchievement>(
        new ClearTanksByAchievement());
      return;
    }
    this.store.dispatch<LoadTanksByAchievement>(
      new LoadTanksByAchievement({ accountId: this.currentAccount.accountId, achievementId: achievement.achievementId }));
  }

  ngOnDestroy(): void {
    this.componentActive = false;
  }
}
