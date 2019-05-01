import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountAuthenticationService } from '../../shared/services/account-authentication.service';
import { WgAuthResponse } from '../../model/wg-auth-response';
import { map, take } from 'rxjs/operators';

@Component({
  selector: 'app-splash-screen',
  templateUrl: './splash-screen.component.html',
  styleUrls: ['./splash-screen.component.css']
})
export class SplashScreenComponent implements OnInit {
  constructor(
    activeRoute: ActivatedRoute,
    public accountAuthService: AccountAuthenticationService) {

      // Parse query parameters if any - redirect from wg auth
      activeRoute.queryParams
            .pipe(
              map(params => params as WgAuthResponse),
              take(1)
            )
            .subscribe(params =>
              accountAuthService.parseRote(params)
            , error => console.error('queryParams eror', error));
    }

  ngOnInit() {
  }
}
