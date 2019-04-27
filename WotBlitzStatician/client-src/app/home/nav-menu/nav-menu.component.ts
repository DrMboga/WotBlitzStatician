import { Component, OnInit, Inject } from '@angular/core';
import { Router } from '@angular/router';

import { AccountAuthenticationService } from '../../shared/services/account-authentication.service';
import { BlitzStaticianService } from '../../shared/services/blitz-statician.service';
import { Store } from '@ngrx/store';
import { State } from '../../state/app.state';
import { ChangeCurrentAccount } from '../../state/app.actions';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  public refreshEnabled = true;

  constructor(
    private store: Store<State>,
    private blitzStaticianService: BlitzStaticianService,
    @Inject('BASE_URL') private baseUrl: string,
    private accountAuthService: AccountAuthenticationService,
    private router: Router) {
  }

  ngOnInit() {
    this.store.dispatch<ChangeCurrentAccount>(new ChangeCurrentAccount( {
      currentAccountId: { accountId: 90277267, accountLoggedIn: true},
      currentAccountNick: 'NickFromState'
     }));
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
