import { Component, OnInit } from '@angular/core';

import { AccountGlobalInfo } from '../account-global-info';

@Component({
  selector: 'nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  public accountId: number;
  public accountNick: string;

  constructor(public accountGlobalInfo: AccountGlobalInfo) {
  }

  ngOnInit() {
  }
}
