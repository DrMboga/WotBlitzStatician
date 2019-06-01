import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Store, select } from '@ngrx/store';
import { State } from './state/app.state';
import { getAccountId } from './state/app.selectors';
import { take, map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router: Router,
    private store: Store<State>) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean>|boolean {
    return this.store.pipe(
      select(getAccountId),
      take(1),
      map((accountId) => {
        if (accountId.accountId > 0) {
          return true;
        } else {
          // ToDo: Redirect to splash screen
          this.router.navigate(['/splash-screen']);
        }
        return false;
      })
    );
  }
}
