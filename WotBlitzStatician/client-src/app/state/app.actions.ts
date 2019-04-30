import { Action } from '@ngrx/store';
import { HomeState } from '../home/state/home.state';

export enum AppActionTypes {
  ChangeCurrentAccount = '[App] Change current account'
}

export class ChangeCurrentAccount implements Action {
  readonly type = AppActionTypes.ChangeCurrentAccount;
  constructor(public payload: HomeState) {}
}

export type AppActions = ChangeCurrentAccount;
