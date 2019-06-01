import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { State } from '../../state/app.state';
import { WargamingLogin, RefreshAccountInfo, AppActionTypes, WargamingLogout, AccountInfoRefreshed, ReturnFromGuestAccount } from '../../state/app.actions';
import { getAccountId, getCurremtAccountNick, getLoggedinAccountNick } from '../../state/app.selectors';
import { takeWhile, map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Actions, ofType } from '@ngrx/effects';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit, OnDestroy {
  componentActive = true;
  currentAccountId: number;

  public currentAccountNick$: Observable<string>;
  public loggedinAccountNick$: Observable<string>;

  public showMenu: boolean;
  public showHistoryMenu: boolean;
  public showLoginButton: boolean;
  public showLogoutButton: boolean;
  public showReturnToLoggedinButton: boolean;

  public refreshEnabled = true;

  constructor(
    private store: Store<State>,
    private actions$: Actions) { }

  ngOnInit() {
    this.store.pipe(
      select(getAccountId),
      takeWhile(() => this.componentActive)
    )
      .subscribe(accountId => {
        this.currentAccountId = accountId.accountId;
        this.showMenu = accountId.accountId > 0;
        this.showHistoryMenu = accountId.accountLoggedIn;
        this.showLoginButton = accountId.accountId === 0;
        this.showLogoutButton = accountId.accountLoggedIn && accountId.accountId > 0;
        this.showReturnToLoggedinButton = !accountId.accountLoggedIn && accountId.accountId > 0;
      });

    this.actions$.pipe(
      ofType(AppActionTypes.AccountInfoRefreshed),
      map((action: AccountInfoRefreshed) => action.payload),
      takeWhile(() => this.componentActive)
    )
      .subscribe(accountId => this.refreshEnabled = true);

    this.currentAccountNick$ = this.store.pipe(select(getCurremtAccountNick));
    this.loggedinAccountNick$ = this.store.pipe(select(getLoggedinAccountNick));
  }

  ngOnDestroy(): void {
    this.componentActive = false;
  }


  public refreshStat() {
    this.refreshEnabled = false;
    this.store.dispatch<RefreshAccountInfo>(new RefreshAccountInfo({ accountId: this.currentAccountId, accountLoggedIn: true }));
  }

  public logIn() {
    this.store.dispatch<WargamingLogin>(new WargamingLogin());
  }

  public logOut() {
    this.store.dispatch<WargamingLogout>(new WargamingLogout());
  }

  public returnToLoggedinAccount() {
    this.store.dispatch<ReturnFromGuestAccount>(new ReturnFromGuestAccount());
  }
}
