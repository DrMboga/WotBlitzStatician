import { Component, OnInit, Input, OnDestroy, ChangeDetectionStrategy } from '@angular/core';
import { TankByAchievementDto } from '../../model/tank-by-achievement-dto';
import { AccountAchievementDto } from '../../model/account-achievement-dto';
import { Subscription, of } from 'rxjs';
import { Actions, ofType } from '@ngrx/effects';
import { Store, select } from '@ngrx/store';
import { State } from '../../state/app.state';
import { getAccountId } from '../../state/app.selectors';
import { takeWhile, map, mergeMap, catchError } from 'rxjs/operators';
import { AppActionTypes, AccountInfoRefreshed } from '../../state/app.actions';
import { AccountAchievementsService } from '../account-achievements.service';
import { CurrentAccountId } from '../../home/state/home.state';

@Component({
  selector: 'app-account-achievements',
  templateUrl: './account-achievements.component.html',
  styleUrls: ['./account-achievements.component.css'],
  // changeDetection: ChangeDetectionStrategy.OnPush
})
export class AccountAchievementsComponent implements OnInit, OnDestroy {
  private componentActive = true;
  private isAccountLoggedin: boolean;

  public achievements: AccountAchievementDto[];

  public battleAchievements: AccountAchievementDto[];
  public epicAchievements: AccountAchievementDto[];
  public platoonAchievements: AccountAchievementDto[];
  public titleAchievements: AccountAchievementDto[];
  public commemorativeAchievements: AccountAchievementDto[];
  public stepAchievements: AccountAchievementDto[];

  public clickedAchievement: AccountAchievementDto;
  public tanksByAchievement: TankByAchievementDto[];

  public loadAchievemntsError: string;

  constructor(
    private store: Store<State>,
    private actions$: Actions,
    private accountAchievementsService: AccountAchievementsService) {
  }

  ngOnInit() {
    this.store.pipe(
      select(getAccountId),
      takeWhile(() => this.componentActive),
      mergeMap((accountId: CurrentAccountId) =>
        this.accountAchievementsService.getAccountAchievements(accountId.accountId, accountId.accountLoggedIn)
        .pipe(
          map((accountAchievements: AccountAchievementDto[]) => this.refreshAchievements(accountAchievements, accountId.accountLoggedIn)),
          catchError(err => of(this.loadAchievemntsError = err))
        ))
    )
      .subscribe();

    this.actions$.pipe(
      ofType(AppActionTypes.AccountInfoRefreshed),
      map((action: AccountInfoRefreshed) => action.payload),
      takeWhile(() => this.componentActive),
      mergeMap((accountId: CurrentAccountId) =>
        this.accountAchievementsService.getAccountAchievements(accountId.accountId, accountId.accountLoggedIn)
        .pipe(
          map((accountAchievements: AccountAchievementDto[]) => this.refreshAchievements(accountAchievements, accountId.accountLoggedIn)),
          catchError(err => of(this.loadAchievemntsError = err))
        ))
    )
      .subscribe();
  }

  private refreshAchievements(accountAchievements: AccountAchievementDto[], isAccountLoggedin: boolean) {
    this.achievements = accountAchievements;
    this.isAccountLoggedin = isAccountLoggedin;
    this.battleAchievements = this.achievements.filter(
      achievement => achievement.section === 'battle'
    );
    this.epicAchievements = this.achievements.filter(
      achievement => achievement.section === 'epic'
    );
    this.platoonAchievements = this.achievements.filter(
      achievement => achievement.section === 'platoon'
    );
    this.titleAchievements = this.achievements.filter(
      achievement => achievement.section === 'title'
    );
    this.commemorativeAchievements = this.achievements.filter(
      achievement => achievement.section === 'commemorative'
    );
    this.stepAchievements = this.achievements.filter(
      achievement => achievement.section === 'step'
    );
  }


  getTanksByAchievement() {
    if (
      !this.isAccountLoggedin ||
      this.clickedAchievement.isAchievementOption ||
      (this.clickedAchievement.section === 'battle' &&
        this.clickedAchievement.achievementId.includes('Mastery'))
    ) {
      this.tanksByAchievement = null;
      return;
    }
    console.log('achievementId', this.clickedAchievement.achievementId);
    // this.accountsInfoService
    // .getTanksByAchievement(
    //   this.accountGlobalInfo.accountId,
    //   this.clickedAchievement.achievementId
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
