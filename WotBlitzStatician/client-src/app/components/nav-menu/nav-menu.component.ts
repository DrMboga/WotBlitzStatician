import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";

import { AccountInfo } from '../../model/account-info';
import { AccountInfoService } from '../../services/account-info.service';
import { AccountGlobalInfo } from '../account-global-info';

@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  constructor(public accountGlobalInfo: AccountGlobalInfo,
              private router: Router) {
  }

  ngOnInit() {
  }

/*   set currentAccountId(newAccountId: number) {
    this._currentAccountId = newAccountId;
    var account = this.accounts.find(a => a.accountId == newAccountId)!;
    this.accountName = account.nickName;
    // raise event
    this.router.navigateByUrl("/account/" + newAccountId);
  }
 */
}
