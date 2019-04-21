import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { AccountAuthenticationService } from '../../shared/services/account-authentication.service';
import { BlitzStaticianService } from '../../shared/services/blitz-statician.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  public refreshEnabled = true;

  constructor(
    private blitzStaticianService: BlitzStaticianService,
    @Inject('BASE_URL') private baseUrl: string,
    private accountAuthService: AccountAuthenticationService,
    private router: Router) {
  }

  ngOnInit() {
  }

  public refreshStat() {
    // if (this.accountGlobalInfo.accountId === 0) {
    //   return;
    // }
    // this.refreshEnabled = false;
    // this.blitzStaticianService.saveAllAccountInfo(this.accountGlobalInfo.accountId).subscribe(
    //   () => {
    //     this.refreshEnabled = true;
    //     this.accountGlobalInfo.EmitAccountInfoChanged();
    //   }
    // );
  }

  public logInLogOff() {
    // if (this.accountGlobalInfo.accountId === 0) {
    //   this.blitzStaticianService.getAuthenticationRequest(`${this.baseUrl}splash-screen`).subscribe(
    //     authRequest => {
    //       // Redirect to authRequest
    //       window.location.href = authRequest;
    //   });
    // } else {
    //   this.accountAuthService.dropCookie();
    //   this.accountGlobalInfo.accountId = 0;
    //   this.accountGlobalInfo.accountNick = 'WotBlitzStatician';
    //   this.router.navigate(['/splash-screen']);
    // }
  }
}
