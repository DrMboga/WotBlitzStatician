import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { AccountGlobalInfo } from '../account-global-info';
import { AccountInfoService } from '../../services/account-info.service';
import { AccountAuthenticationService } from '../../services/account-authentication.service';

@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  constructor(public accountGlobalInfo: AccountGlobalInfo,
    private accountsInfoService: AccountInfoService,
    @Inject('BASE_URL') private baseUrl: string,
    private accountAuthService: AccountAuthenticationService,
    private router: Router) {
  }

  ngOnInit() {
  }

  private logInLogOff(){
    if(this.accountGlobalInfo.accountId === 0){
      this.accountsInfoService.getAuthenticationRequest(`${this.baseUrl}splash-screen`).subscribe(
        authRequest => {
          // Redirect to authRequest
          window.location.href = authRequest;
      });
    }
    else {
      this.accountAuthService.dropCookie();
      this.accountGlobalInfo.accountId = 0;
      this.accountGlobalInfo.accountNick = 'WotBlitzStatician';
      this.router.navigate(['/splash-screen']);
    }
  }
}
