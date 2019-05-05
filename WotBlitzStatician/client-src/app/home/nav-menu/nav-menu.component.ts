import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { State } from '../../state/app.state';
import { WargamingLogin } from '../../state/app.actions';
import { getAccountId, getCurremtAccountNick, getLoggedinAccountNick } from '../../state/app.selectors';
import { takeWhile } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit, OnDestroy {
  componentActive = true;

  public currentAccountNick$: Observable<string>;
  public loggedinAccountNick$: Observable<string>;

  public showMenu: boolean;
  public showHistoryMenu: boolean;
  public showLoginButton: boolean;
  public showLogoutButton: boolean;
  public showReturnToLoggedinButton: boolean;

  public refreshEnabled = true;

  constructor(
    private store: Store<State>) { }

  ngOnInit() {
    this.store.pipe(
      select(getAccountId),
      takeWhile(() => this.componentActive)
    )
      .subscribe(accountId => {
        this.showMenu = accountId.accountId > 0;
        this.showHistoryMenu = accountId.accountLoggedIn;
        this.showLoginButton = accountId.accountId === 0;
        this.showLogoutButton = accountId.accountLoggedIn && accountId.accountId > 0;
        this.showReturnToLoggedinButton = !accountId.accountLoggedIn && accountId.accountId > 0;
      });

    this.currentAccountNick$ = this.store.pipe(select(getCurremtAccountNick));
    this.loggedinAccountNick$ = this.store.pipe(select(getLoggedinAccountNick));
  }

  ngOnDestroy(): void {
    this.componentActive = false;
  }


  public refreshStat() {
    // if (this.accountGlobalInfo.accountId === 0) {
    //   return;
    // }
    // this.refreshEnabled = false;
    // this.blitzStaticianService.saveAllAccountInfo(this.accountGlobalInfo.accountId).subscribe(
    //   () => {
    //     this.refreshEnabled = true;
    //     this.accountGlobalInfo.EmitAccountInfoChanged();
    //   }
    // );
  }

  public logIn() {
    this.store.dispatch<WargamingLogin>(new WargamingLogin());

    // if (this.accountGlobalInfo.accountId === 0) {
    //   this.blitzStaticianService.getAuthenticationRequest(`${this.baseUrl}splash-screen`).subscribe(
    //     authRequest => {
    //       // Redirect to authRequest
    //       window.location.href = authRequest;
    //   });
    // } else {
    //   this.accountAuthService.dropCookie();
    //   this.accountGlobalInfo.accountId = 0;
    //   this.accountGlobalInfo.accountNick = 'WotBlitzStatician';
    //   this.router.navigate(['/splash-screen']);
    // }
  }

  public logOut() {

  }

  public returnToLoggedinAccount() {}
}
