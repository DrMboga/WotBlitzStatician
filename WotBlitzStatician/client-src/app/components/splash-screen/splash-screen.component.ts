import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountAuthenticationService } from '../../services/account-authentication.service';

@Component({
  selector: 'splash-screen',
  templateUrl: './splash-screen.component.html',
  styleUrls: ['./splash-screen.component.css']
})
export class SplashScreenComponent implements OnInit {
  constructor(
    activeRoute: ActivatedRoute,
    private accountAuthService: AccountAuthenticationService) { 

      // Parse query parameters if any - redirect from wg auth
      activeRoute.queryParams.subscribe(
        (params) => accountAuthService.parseWargamingAuthResponse(params)
        , error => console.error('queryParams eror', error));
    }

  ngOnInit() {
  }
}
