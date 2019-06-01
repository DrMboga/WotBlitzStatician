import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountAuthenticationService } from '../../shared/services/account-authentication.service';
import { WgAuthResponse } from '../../model/wg-auth-response';
import { map, take, takeWhile } from 'rxjs/operators';
import { Store, select } from '@ngrx/store';
import { State } from '../../state/app.state';
import { getAccountId, getWgAuthUrl, getWgAuthUrlError } from '../../state/app.selectors';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-splash-screen',
  templateUrl: './splash-screen.component.html',
  styleUrls: ['./splash-screen.component.css']
})
export class SplashScreenComponent implements OnInit, OnDestroy {
  componentActive = true;

  public wgAuthUrlGettingError$: Observable<string>;

  constructor(
    private activeRoute: ActivatedRoute,
    private router: Router,
    private accountAuthService: AccountAuthenticationService,
    private store: Store<State>) { }

  ngOnInit() {
    // Parse query parameters if any - redirect from wg auth
    this.activeRoute.queryParams
      .pipe(
        map(params => params as WgAuthResponse),
        take(1)
      )
      .subscribe(params =>
        this.accountAuthService.parseRote(params)
        , error => console.error('queryParams eror', error));

    // Listen to ChangeAccount to redirect to the root
    this.store.pipe(
      select(getAccountId),
      takeWhile(() => this.componentActive)
    )
      .subscribe(accountId => {
        if (accountId.accountId > 0) {
          this.router.navigate(['/']);
        }
      });

    // Listen to wg auth url selector
    this.store.pipe(
      select(getWgAuthUrl),
      takeWhile(() => this.componentActive)
    )
      .subscribe(wgAuthUrl => {
        if (wgAuthUrl != null) {
          window.location.href = wgAuthUrl;
        }
      });

    this.wgAuthUrlGettingError$ = this.store.pipe(select(getWgAuthUrlError));
  }

  ngOnDestroy(): void {
    this.componentActive = false;
  }
}
