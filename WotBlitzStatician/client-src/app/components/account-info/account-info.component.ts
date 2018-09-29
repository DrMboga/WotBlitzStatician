import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";

import { AccountInfoService } from '../../services/account-info.service';
import { AccountGlobalInfo } from '../account-global-info';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {
  public account: any;
  public achievements: any[];

  constructor(private accountsInfoService: AccountInfoService,
    public accountGlobalInfo: AccountGlobalInfo) {
    let id = accountGlobalInfo.accountId;
    if (id != null) {
      this.accountsInfoService.getAccount(id).subscribe(data => {
        this.account = data;
        this.achievements = this.account.achievements;
      }, error => console.error(error));
    }
  }

  ngOnInit() {
  }

}
