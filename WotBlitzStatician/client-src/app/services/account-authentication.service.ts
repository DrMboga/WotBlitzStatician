import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import { AccountGlobalInfo } from '../components/account-global-info';
import { AccountInfoService } from './account-info.service';
import { AccountInfo } from '../model/account-info';

@Injectable()
export class AccountAuthenticationService {
  private readonly accountIdCookieName: string = 'AccountId';

  public showButtons: boolean = false;
  public wgAuthResponse: any;
  public status: string;

  constructor(
    private router: Router,
    private cookieService: CookieService,
    private accountGlobalInfo: AccountGlobalInfo,
    private accountsInfoService: AccountInfoService
  ) { }

  public parseWargamingAuthResponse(params: any) {
    if (params.hasOwnProperty('status') && params.status === 'ok') {
      this.wgAuthResponse = params;
      this.saveAccountInfoAndEnter();
      this.showButtons = false;
    }
    else {
      this.checkCookieAndWgLogin();
    }
  }

  public checkCookieAndWgLogin() {
    let accountIdFromCookie = this.getAccountIdFromCookie();
    if (accountIdFromCookie > 0) {
      this.accountsInfoService.getShortAccountInfo(accountIdFromCookie)
        .subscribe(a => {
          if (a != null) {
            let now = new Date();
            let tokenExpiration = new Date(a.accessTokenExpiration);
            if (now.getTime() <= tokenExpiration.getTime()) {
              this.accountGlobalInfo.accountId = a.accountId;
              this.accountGlobalInfo.accountNick = a.nickName;
              this.router.navigate(['/']);
            }
          }
          this.showButtons = true;
        },
          error => {
            console.error(error);
            this.showButtons = true;
          });
    }
    else {
      this.showButtons = true;
    }
  }

  public saveAccountInfoAndEnter() {
    this.accountGlobalInfo.accountId = this.wgAuthResponse.account_id;
    this.accountGlobalInfo.accountNick = this.wgAuthResponse.nickname;
    let accountInfo: AccountInfo = {
      accountId: this.accountGlobalInfo.accountId,
      nickName: this.accountGlobalInfo.accountNick,
      lastBattleTime: null,
      accountCreatedAt: null,
      accessToken: this.wgAuthResponse.access_token,
      accessTokenExpiration: new Date(+this.wgAuthResponse.expires_at * 1000)
    };

    this.status = 'Saving new account';
    this.accountsInfoService.putNewAccountInfo(accountInfo)
      .subscribe(() => { 
        this.cookieService.set(this.accountIdCookieName, this.accountGlobalInfo.accountId.toString());
        // Saving new accountId to cookie
        this.SaveAccountInfo(accountInfo);
      }, error => console.error(error));

  }

  private SaveAccountInfo(accountInfo: AccountInfo) {
    this.status = 'Getting nessesary dictionaries and other data';
    // 1. Check if dictionaries are empty, and then load dictionaries
    this.accountsInfoService.downloadDictionariesAndImages().subscribe(() => {
      this.status = `Getting ${accountInfo.nickName} statistics`;
      // 2. Load info from WG by account
      this.accountsInfoService.saveAllAccountInfo(accountInfo.accountId).subscribe(() => {
        this.router.navigate(['/']);
      }, error => console.error('Account saving error', error));
    }, error => console.error('Dictionaries error', error));
  }

  public getAccountIdFromCookie(): number {
    if (this.cookieService.check(this.accountIdCookieName)) {
      return +this.cookieService.get(this.accountIdCookieName);
    }
    return 0;
  }

}
