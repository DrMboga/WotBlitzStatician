import { Component, OnInit, Inject } from '@angular/core';
import { Router } from "@angular/router";

import { AccountGlobalInfo, GLOBAL_ACCOUNT_STATE } from '../account-global-info';
import { Observable } from 'rxjs/Observable';

@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  public accountId: number;
  public accountNick: string;

  constructor(@Inject(GLOBAL_ACCOUNT_STATE) private accountStateChange : Observable<AccountGlobalInfo>,
              public accountGlobalInfo: AccountGlobalInfo) {
  }

  ngOnInit() {
    // ToDo: Refresh nav menu!!!
    
    // this.accountStateChange.subscribe(newAccountState => {
    //   this.accountId = newAccountState.accountId;
    //   this.accountNick = newAccountState.accountNick;
    //   console.log('newAccountState', newAccountState.accountId);
    // });

  }
}
