import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";

import { AccountInfo } from '../../model/account-info';
import { AccountInfoService } from '../../services/account-info.service';

@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  public accounts: AccountInfo[] = new Array<AccountInfo>();
  public _currentAccountId: number = 0;
  public accountName: string = "None"

  constructor(private accountsInfoService: AccountInfoService,
              private router: Router) {
    this.accountsInfoService.getAccounts().subscribe(data => {
      this.accounts = data;
      if (this.accounts.length > 0) {
        this.currentAccountId = this.accounts[0].accountId;
      }
    }, error => console.error(error));
  }

  ngOnInit() {
  }

  get currentAccountId(): number {
    return this._currentAccountId;
  }

  set currentAccountId(newAccountId: number) {
    this._currentAccountId = newAccountId;
    var account = this.accounts.find(a => a.accountId == newAccountId)!;
    this.accountName = account.nickName;
    // raise event
    this.router.navigateByUrl("/account/" + newAccountId);
  }
}
