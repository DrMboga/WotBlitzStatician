import { Action } from '@ngrx/store';
import { AccountInfoDto } from '../../model/account-info-dto';
import { CurrentAccountId } from '../../home/state/home.state';
import { AccountTanksInfoAggregatedDto } from '../../model/account-tanks-info-aggregated-dto';
import { PlayerPrivateInfo } from '../../model/player-private-info';

export enum AccountActionTypes {
  LoadAccountInfo = '[AccountInfo] loadAccount',
  LoadAccountAggregatedInfo = '[AccountInfo] loadAccountAgregatedInfo',
  AccountInfoLoadSuccess = '[AccountInfo] Load success',
  AccountInfoLoadFailed = '[AccountInfo] Load failed',
  AccountAggregatedInfoLoaded = '[AccountInfo] AggregatedInfo Load success',
  AccountAggregatedInfoLoadFailed = '[AccountInfo] AggregatedInfo Load failed',
  AccountPrivateInfoLoad = '[AccountInfo] LoadPrivate Info',
  AccountPrivateInfoLoaded = '[AccountInfo] Acount Private Info Loaded',
  AccountPrivateInfoLoadFailed = '[AccountInfo] Account private info load failed'
}

export class LoadAccountInfo implements Action {
  readonly type = AccountActionTypes.LoadAccountInfo;
  constructor(public payload: CurrentAccountId) { }
}

export class LoadAccountAggregatedInfo implements Action {
  readonly type = AccountActionTypes.LoadAccountAggregatedInfo;
  constructor(public payload: CurrentAccountId) { }
}

export class AccountInfoSuccessfullyLoaded implements Action {
  readonly type = AccountActionTypes.AccountInfoLoadSuccess;
  constructor(public payload: AccountInfoDto) {}
}

export class AccountInfoLoadFailed implements Action {
  readonly type = AccountActionTypes.AccountInfoLoadFailed;
  constructor(public payload: string) {}
}

export class AccounAggregatedtInfoSuccessfullyLoaded implements Action {
  readonly type = AccountActionTypes.AccountAggregatedInfoLoaded;
  constructor(public payload: AccountTanksInfoAggregatedDto[]) {}
}

export class AccountAggregatedInfoLoadFailed implements Action {
  readonly type = AccountActionTypes.AccountAggregatedInfoLoadFailed;
  constructor(public payload: string) {}
}

export class AccountPrivateInfoLoad implements Action {
  readonly type = AccountActionTypes.AccountPrivateInfoLoad;
  constructor(public payload: CurrentAccountId) { }
}

export class AccountPrivateInfoLoaded implements Action {
  readonly type = AccountActionTypes.AccountPrivateInfoLoaded;
  constructor(public payload: PlayerPrivateInfo) {}
}

export class AccountPrivateInfoLoadFailed implements Action {
  readonly type = AccountActionTypes.AccountPrivateInfoLoadFailed;
  constructor(public payload: string) {}
}

export type AccountActions =
  LoadAccountInfo
  | AccountInfoSuccessfullyLoaded
  | AccountInfoLoadFailed
  | LoadAccountAggregatedInfo
  | AccounAggregatedtInfoSuccessfullyLoaded
  | AccountAggregatedInfoLoadFailed
  | AccountPrivateInfoLoad
  | AccountPrivateInfoLoadFailed
  | AccountPrivateInfoLoaded;
