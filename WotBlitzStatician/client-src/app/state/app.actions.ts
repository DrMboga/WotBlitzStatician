import { Action } from '@ngrx/store';
import { HomeState } from '../home/state/home.state';

export enum AppActionTypes {
  ChangeCurrentAccount = '[App] Change current account',
  WargamingLogin = '[App] WG Login',
  WargamingLoginUrlLoaded = '[App] WG Login Url loaded',
  WargamingLoginUrlLoadFailed = '[App] WG Login Url load failed',
  ClearWargamingLoginUrl = '[App] Clear WG Login Url'
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

export type AppActions = ChangeCurrentAccount
  | WargamingLogin
  | WargamingLoginUrlLoaded
  | WargamingLoginUrlLoadFailed
  | ClearWargamingLoginUrl;
