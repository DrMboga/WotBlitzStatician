import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AccountGlobalInfo } from './account-global-info';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
export class AuthGuard implements CanActivate {
  private readonly accountIdCookieName: string = 'AccountId';

  constructor(private router: Router,
    private accountGlobalInfo: AccountGlobalInfo,
    private cookieService: CookieService) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
      if(this.accountGlobalInfo.accountId > 0) {
        return true;
      }
      
      let accountIdFromCookie = this.getAccountIdFromCookie();
      if(accountIdFromCookie > 0){
        // ToDo: check if accountId valid (method get account info by Id, then check if token is not expired)
        this.accountGlobalInfo.accountId = accountIdFromCookie;
        return true;
      }

      // ToDo: if account id is not in the cookie or token expired - redirect to wg auth
      // then save the auth data to db - method post by AccountInfo entity
      // then save new account ud in the cookie: // this.cookieService.set(this.accountIdCookieName, '46512100');
      return true;
  }

  private getAccountIdFromCookie() : number {
    if(this.cookieService.check(this.accountIdCookieName)) {
      return +this.cookieService.get(this.accountIdCookieName);
    }
    return 0;
  }
}
