import { Action } from '@ngrx/store';
import { HomeState, CurrentAccountId } from '../home/state/home.state';

export enum AppActionTypes {
  ChangeCurrentAccount = '[App] Change current account',
  WargamingLogin = '[App] WG Login',
  WargamingLoginUrlLoaded = '[App] WG Login Url loaded',
  WargamingLoginUrlLoadFailed = '[App] WG Login Url load failed',
  ClearWargamingLoginUrl = '[App] Clear WG Login Url',
  RefreshAccountInfo = '[App] Refresh accountInfo',
  AccountInfoRefreshed = '[App] AccountInfo refreshed',
  AccountInfoRefreshFailed = '[App] AccountInfo refreshFailed'
}

export class ChangeCurrentAccount implements Action {
  readonly type = AppActionTypes.ChangeCurrentAccount;
  constructor(public payload: HomeState) {}
}

export class WargamingLogin implements Action {
  readonly type = AppActionTypes.WargamingLogin;
}

export class WargamingLoginUrlLoaded implements Action {
  readonly type = AppActionTypes.WargamingLoginUrlLoaded;
  constructor(public payload: string) {}
}

export class WargamingLoginUrlLoadFailed implements Action {
  readonly type = AppActionTypes.WargamingLoginUrlLoadFailed;
  constructor(public payload: string) {}
}

export class ClearWargamingLoginUrl implements Action {
  readonly type = AppActionTypes.ClearWargamingLoginUrl;
}

export class RefreshAccountInfo implements Action {
  readonly type = AppActionTypes.RefreshAccountInfo;
  constructor(public payload: CurrentAccountId) {}
}

export class AccountInfoRefreshed implements Action {
  readonly type = AppActionTypes.AccountInfoRefreshed;
  constructor(public payload: CurrentAccountId) {}
}

export class AccountInfoRefreshFailed implements Action {
  readonly type = AppActionTypes.AccountInfoRefreshFailed;
  constructor(public payload: string) {}
}


export type AppActions = ChangeCurrentAccount
  | WargamingLogin
  | WargamingLoginUrlLoaded
  | WargamingLoginUrlLoadFailed
  | ClearWargamingLoginUrl
  | RefreshAccountInfo
  | AccountInfoRefreshed
  | AccountInfoRefreshFailed;
