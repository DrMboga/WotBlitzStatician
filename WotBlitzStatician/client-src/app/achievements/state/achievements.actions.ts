import { Action } from '@ngrx/store';
import { CurrentAccountId } from '../../home/state/home.state';
import { AccountAchievementDto } from '../../model/account-achievement-dto';
import { TankByAchievementDto } from '../../model/tank-by-achievement-dto';

export enum AchievementsActionTypes {
  LoadAccountAchievements = '[Achievements] Load Account Achievements',
  AccountAchievementsLoaded = '[Achievements] AccountAchievements loases',
  AccountAchievementsLoadFail = '[Achievements] AccountAchievements load fail',
  LoadTanksByAchievement = '[Achievements] Load tanks by achievement',
  ClearTanksByAchievement = '[Achievements] Clear tanks by achievement',
  TanksByAchievementLoaded = '[Achievements] Tanks by achievement loaded',
  TanksByAchievementLoadFailed = '[Achievements] Tanks by achievement load failed'
}

export class LoadAccountAchievements implements Action {
  readonly type = AchievementsActionTypes.LoadAccountAchievements;
  constructor(public payload: CurrentAccountId) {}
}
export class AccountAchievementsLoaded implements Action {
  readonly type = AchievementsActionTypes.AccountAchievementsLoaded;
  constructor(public payload: AccountAchievementDto[]) {}
}
export class AccountAchievementsLoadFail implements Action {
  readonly type = AchievementsActionTypes.AccountAchievementsLoadFail;
  constructor(public payload: string) {}
}
export class LoadTanksByAchievement implements Action {
  readonly type = AchievementsActionTypes.LoadTanksByAchievement;
  constructor(public payload: {accountId: number, achievementId: string}) {}
}
export class ClearTanksByAchievement implements Action {
  readonly type = AchievementsActionTypes.ClearTanksByAchievement;
}
export class TanksByAchievementLoaded implements Action {
  readonly type = AchievementsActionTypes.TanksByAchievementLoaded;
  constructor(public payload: TankByAchievementDto[]) {}
}
export class TanksByAchievementLoadFailed implements Action {
  readonly type = AchievementsActionTypes.TanksByAchievementLoadFailed;
  constructor(public payload: string) {}
}

export type AchievementActions =
  LoadAccountAchievements |
  AccountAchievementsLoaded |
  AccountAchievementsLoadFail |
  LoadTanksByAchievement |
  TanksByAchievementLoaded |
  TanksByAchievementLoadFailed |
  ClearTanksByAchievement;
