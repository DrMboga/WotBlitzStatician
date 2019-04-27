import { Injectable } from '@angular/core';

/* NgRx */
import { Action } from '@ngrx/store';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { AccountsService } from '../account.service';
import { Observable, of } from 'rxjs';
import { AccountActionTypes, LoadAccountInfo, AccountInfoSuccessfullyLoaded, AccountInfoLoadFailed } from './account.actions';
import { map, mergeMap, catchError } from 'rxjs/operators';

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
    mergeMap((accountId: number) =>
      this.accountService.getAccount(accountId).pipe(
        map(accountInfo => (new AccountInfoSuccessfullyLoaded(accountInfo))),
        catchError(err => of(new AccountInfoLoadFailed(err)))
      )
    )
  );

}
