import { Injectable, Inject } from '@angular/core';

/* NgRx */
import { Action } from '@ngrx/store';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { Observable, of } from 'rxjs';
import {
  AppActionTypes, WargamingLoginUrlLoaded, WargamingLoginUrlLoadFailed,
  RefreshAccountInfo, ChangeCurrentAccount, AccountInfoRefreshed, AccountInfoRefreshFailed
} from '../../state/app.actions';
import { mergeMap, map, catchError } from 'rxjs/operators';
import { BlitzStaticianService } from '../../shared/services/blitz-statician.service';

@Injectable()
export class HomeEffects {
  constructor(
    private blitzStaticianService: BlitzStaticianService,
    @Inject('BASE_URL') private baseUrl: string,
    private actions$: Actions

  ) { }

  @Effect()
  wargamingLogin$: Observable<Action> = this.actions$.pipe(
    ofType(AppActionTypes.WargamingLogin),
    mergeMap(() =>
      this.blitzStaticianService.getAuthenticationRequest(`${this.baseUrl}splash-screen`).pipe(
        map(authRequest => (new WargamingLoginUrlLoaded(authRequest))),
        catchError(err => of(new WargamingLoginUrlLoadFailed(err)))
      )
    )
  );

  @Effect()
  RefreshLoggedinAccountInfo: Observable<Action> = this.actions$.pipe(
    ofType(AppActionTypes.RefreshAccountInfo),
    map((action: RefreshAccountInfo) => action.payload),
    mergeMap((accountId) =>
      this.blitzStaticianService.saveAllAccountInfo(accountId.accountId).pipe(
        map(() => (new AccountInfoRefreshed(accountId))),
        catchError(err => of(new AccountInfoRefreshFailed(err)))
      )
    )
  );
}
