import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { AccountInfoService } from './account-info.service';
import { AccountInfo } from '../../model/account-info';
import { WgAuthResponse } from '../../model/wg-auth-response';
import { BlitzStaticianService } from './blitz-statician.service';

@Injectable()
export class AccountAuthenticationService {
  private readonly accountIdCookieName: string = 'AccountId';

  public showButtons = false;
  public status: string;

  constructor(
    private router: Router,
    private cookieService: CookieService,
    private accountsInfoService: AccountInfoService,
    private blitzStaticianService: BlitzStaticianService
  ) {}

  public parseRote(response: WgAuthResponse) {
    if (response.status === 'ok') {
      this.saveAccountInfoAndEnter(response);
      this.showButtons = false;
    } else {
      this.checkCookieAndWgLogin();
    }
  }

  public checkCookieAndWgLogin() {
    // const accountIdFromCookie = this.getAccountIdFromCookie();
    // if (accountIdFromCookie > 0) {
    //   this.accountsInfoService
    //     .getShortAccountInfo(accountIdFromCookie)
    //     .subscribe(
    //       a => {
    //         if (a != null) {
    //           const now = new Date();
    //           const tokenExpiration = new Date(a.accessTokenExpiration);
    //           if (now.getTime() <= tokenExpiration.getTime()) {
    //             this.accountGlobalInfo.accountId = a.accountId;
    //             this.accountGlobalInfo.accountNick = a.nickName;
    //             this.router.navigate(['/']);
    //           }
    //         }
    //         this.showButtons = true;
    //       },
    //       error => {
    //         console.error(error);
    //         this.showButtons = true;
    //       }
    //     );
    // } else {
    //   this.showButtons = true;
    // }
  }

  public saveAccountInfoAndEnter(wgAuthResponse: WgAuthResponse) {
    // this.accountGlobalInfo.accountId = wgAuthResponse.account_id;
    // this.accountGlobalInfo.accountNick = wgAuthResponse.nickname;
    // this.accountGlobalInfo.isGuestAccount = false;
    // const accountInfo: AccountInfo = {
    //   accountId: this.accountGlobalInfo.accountId,
    //   nickName: this.accountGlobalInfo.accountNick,
    //   lastBattleTime: null,
    //   accountCreatedAt: null,
    //   accessToken: wgAuthResponse.access_token,
    //   accessTokenExpiration: new Date(+wgAuthResponse.expires_at * 1000)
    // };

    // this.status = 'Saving new account';
    // this.blitzStaticianService.putNewAccountInfo(accountInfo).subscribe(
    //   () => {
    //     this.cookieService.set(
    //       this.accountIdCookieName,
    //       this.accountGlobalInfo.accountId.toString()
    //     );
    //     // Saving new accountId to cookie
    //     this.SaveAccountInfo(accountInfo);
    //   },
    //   error => console.error(error)
    // );
  }

  private SaveAccountInfo(accountInfo: AccountInfo) {
    this.status = 'Getting nessesary dictionaries and other data';
    // 2. Load info from WG by account
    this.blitzStaticianService
      .saveAllAccountInfo(accountInfo.accountId)
      .subscribe(
        () => {
          this.status = '';
          this.router.navigate(['/']);
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
