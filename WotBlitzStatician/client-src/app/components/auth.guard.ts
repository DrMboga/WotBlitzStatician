import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AccountGlobalInfo } from './account-global-info';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router: Router,
    private accountGlobalInfo: AccountGlobalInfo) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
      return true;

      if(this.accountGlobalInfo.accountId > 0) {
        return true;
      }
      // ToDo: Redirect to splash screen
      this.router.navigate(['/splash-screen']);

      return false;
  }
}
