import { Action } from '@ngrx/store';
import { AccountInfoDto } from '../../model/account-info-dto';
import { CurrentAccountId } from '../../home/state/home.state';

export enum AccountActionTypes {
  LoadAccountInfo = '[AccountInfo] loadAccount',
  AccountInfoLoadSuccess = '[AccountInfo] Load success',
  AccountInfoLoadFailed = '[AccountInfo] Load failed'
}

export class LoadAccountInfo implements Action {
  readonly type = AccountActionTypes.LoadAccountInfo;
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

export type AccountActions =
  LoadAccountInfo
  | AccountInfoSuccessfullyLoaded
  | AccountInfoLoadFailed;
