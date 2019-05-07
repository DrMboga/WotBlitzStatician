import { Injectable, Inject } from '@angular/core';

/* NgRx */
import { Action } from '@ngrx/store';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { Observable, of } from 'rxjs';
import {
  AppActionTypes, WargamingLoginUrlLoaded, WargamingLoginUrlLoadFailed,
  RefreshAccountInfo, ChangeCurrentAccount, AccountInfoRefreshed, AccountInfoRefreshFailed, GuestAccountSelected
} from '../../state/app.actions';
import { mergeMap, map, catchError, tap } from 'rxjs/operators';
import { BlitzStaticianService } from '../../shared/services/blitz-statician.service';
import { Router } from '@angular/router';
import { AccountAuthenticationService } from '../../shared/services/account-authentication.service';

@Injectable()
export class HomeEffects {
  constructor(
    private blitzStaticianService: BlitzStaticianService,
    private accauntAuthService: AccountAuthenticationService,
    @Inject('BASE_URL') private baseUrl: string,
    private actions$: Actions,
    private router: Router
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

  @Effect({ dispatch: false })
  Logout = this.actions$.pipe(
    ofType(AppActionTypes.WargamingLogout),
    tap(() => {
      this.accauntAuthService.dropCookie();
      this.router.navigate(['/splash-screen']);
    })
  );

  @Effect({ dispatch: false })
  ReturnFromGuestAccount = this.actions$.pipe(
    ofType(AppActionTypes.ReturnFromGuestAccount),
    tap(() => {
      this.router.navigate(['/splash-screen']);
    })
  );

  @Effect({ dispatch: false })
  GuestAccountSelected = this.actions$.pipe(
    ofType(AppActionTypes.GuestAccountSelected),
    map((action: GuestAccountSelected) => action.payload.accountId),
    mergeMap((accountId) => this.blitzStaticianService.putGuestAccountToCache(accountId))
  );
}
