import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { AccountInfo } from '../../model/account-info';
import { WgAuthResponse } from '../../model/wg-auth-response';
import { BlitzStaticianService } from './blitz-statician.service';
import { Store } from '@ngrx/store';
import { State } from '../../state/app.state';
import { ChangeCurrentAccount } from '../../state/app.actions';

@Injectable()
export class AccountAuthenticationService {
  private readonly accountIdCookieName: string = 'AccountId';

  constructor(
    private router: Router,
    private cookieService: CookieService,
    private blitzStaticianService: BlitzStaticianService,
    private store: Store<State>
  ) {}

  public parseRote(response: WgAuthResponse) {
    if (response.status === 'ok') {
      this.saveAccountInfoAndEnter(response);
    } else {
      this.checkCookieAndWgTokenExpiration();
    }
  }

  public checkCookieAndWgTokenExpiration() {
    const accountIdFromCookie = this.getAccountIdFromCookie();
    if (accountIdFromCookie > 0) {
      this.blitzStaticianService
        .getShortAccountInfo(accountIdFromCookie)
        .subscribe(
          a => {
            if (a != null) {
              const now = new Date();
              const tokenExpiration = new Date(a.accessTokenExpiration);
              if (now.getTime() <= tokenExpiration.getTime()) {
                this.store.dispatch<ChangeCurrentAccount>(new ChangeCurrentAccount( {
                  currentAccountId: { accountId: a.accountId, accountLoggedIn: true},
                  currentAccountNick: a.nickName
                 }));
                // this.router.navigate(['/']);
              }
            }
          },
          error => {
            console.error(error);
          }
        );
    }
  }

  public saveAccountInfoAndEnter(wgAuthResponse: WgAuthResponse) {
    const accountInfo: AccountInfo = {
      accountId: wgAuthResponse.account_id,
      nickName: wgAuthResponse.nickname,
      lastBattleTime: null,
      accountCreatedAt: null,
      accessToken: wgAuthResponse.access_token,
      accessTokenExpiration: new Date(+wgAuthResponse.expires_at * 1000)
    };

    this.blitzStaticianService.putNewAccountInfo(accountInfo).subscribe(
      () => {
        this.cookieService.set(
          this.accountIdCookieName,
          wgAuthResponse.account_id.toString()
        );
        // Saving new accountId to cookie
        this.SaveAccountInfo(accountInfo);
      },
      error => console.error(error)
    );
    this.store.dispatch<ChangeCurrentAccount>(new ChangeCurrentAccount( {
      currentAccountId: { accountId: wgAuthResponse.account_id, accountLoggedIn: true},
      currentAccountNick: wgAuthResponse.nickname
     }));
  }

  private SaveAccountInfo(accountInfo: AccountInfo) {
    // 2. Load info from WG by account
    this.blitzStaticianService
      .saveAllAccountInfo(accountInfo.accountId)
      .subscribe(
        () => {
          // this.router.navigate(['/']);
        },
        error => console.error('Account saving error', error)
      );
  }

  public getAccountIdFromCookie(): number {
    if (this.cookieService.check(this.accountIdCookieName)) {
      return +this.cookieService.get(this.accountIdCookieName);
    }
    return 0;
  }

  public dropCookie() {
    if (this.cookieService.check(this.accountIdCookieName)) {
      this.cookieService.delete(this.accountIdCookieName);
    }
  }
}
