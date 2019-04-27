import { Action } from '@ngrx/store';
import { State } from './app.state';

export enum AppActionTypes {
  ChangeCurrentAccount = '[App] Change current account'
}

export class ChangeCurrentAccount implements Action {
  readonly type = AppActionTypes.ChangeCurrentAccount;
  constructor(public payload: State) {}
}

export type AppActions = ChangeCurrentAccount;
