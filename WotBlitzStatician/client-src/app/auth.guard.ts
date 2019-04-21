import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    // ToDo: Change authGuard later
    return true;
    // if (this.accountGlobalInfo.accountId > 0) {
    //   return true;
    // }
    // // ToDo: Redirect to splash screen
    // this.router.navigate(['/splash-screen']);

    // return false;
  }
}
