import { Injectable } from '@angular/core';

/* NgRx */
import { Action } from '@ngrx/store';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { AccountsService } from '../account.service';
import { Observable, of } from 'rxjs';
import {
  AccountActionTypes, LoadAccountInfo, AccountInfoSuccessfullyLoaded, AccountInfoLoadFailed,
  LoadAccountAggregatedInfo, AccounAggregatedtInfoSuccessfullyLoaded, AccountAggregatedInfoLoadFailed,
  AccountPrivateInfoLoad, AccountPrivateInfoLoaded, AccountPrivateInfoLoadFailed
} from './account.actions';
import { map, mergeMap, catchError } from 'rxjs/operators';
import { CurrentAccountId } from '../../home/state/home.state';

@Injectable()
export class AccountEffects {
  constructor(
    private accountService: AccountsService,
    private actions$: Actions
  ) { }

  @Effect()
  getAccountInfo$: Observable<Action> = this.actions$.pipe(
    ofType(AccountActionTypes.LoadAccountInfo),
    map((action: LoadAccountInfo) => action.payload),
    mergeMap((accountId: CurrentAccountId) =>
      this.accountService.getAccount(accountId.accountId, accountId.accountLoggedIn).pipe(
        map(accountInfo => (new AccountInfoSuccessfullyLoaded(accountInfo))),
        catchError(err => of(new AccountInfoLoadFailed(err)))
      )
    )
  );

  @Effect()
  getAggregatedAccountInfo$: Observable<Action> = this.actions$.pipe(
    ofType(AccountActionTypes.LoadAccountAggregatedInfo),
    map((action: LoadAccountAggregatedInfo) => action.payload),
    mergeMap((accountId: CurrentAccountId) =>
      this.accountService.getAggregatedAccountTanksInfo(accountId.accountId, accountId.accountLoggedIn).pipe(
        map(accountAggregatedInfo => (new AccounAggregatedtInfoSuccessfullyLoaded(accountAggregatedInfo))),
        catchError(err => of(new AccountAggregatedInfoLoadFailed(err)))
      )
    )
  );

  @Effect()
  getAccountPrivateInfo$: Observable<Action> = this.actions$.pipe(
    ofType(AccountActionTypes.AccountPrivateInfoLoad),
    map((action: AccountPrivateInfoLoad) => action.payload),
    mergeMap((accountId: CurrentAccountId) => {
      return this.accountService.getPlayerPrivateInfo(accountId.accountId).pipe(
        map(privateInfo => (new AccountPrivateInfoLoaded(privateInfo))),
        catchError(err => of(new AccountPrivateInfoLoadFailed(err)))
      );
    }
    )
  );

}
