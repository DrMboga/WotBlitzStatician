import { Component, OnInit } from '@angular/core';

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
  public mastery: any;
  public rank1: any;
  public rank2: any;
  public rank3: any;

  constructor(private accountsInfoService: AccountInfoService,
    public accountGlobalInfo: AccountGlobalInfo) {
    let id = accountGlobalInfo.accountId;
    if (id != null) {
      this.accountsInfoService.getAccount(id).subscribe(data => {
        this.account = data;
        this.achievements = this.account.achievements;
        this.mastery = this.account.accountMasteryInfo.find(m => m.markOfMastery === 4);
        this.rank3 = this.account.accountMasteryInfo.find(m => m.markOfMastery === 3);
        this.rank2 = this.account.accountMasteryInfo.find(m => m.markOfMastery === 2);
        this.rank1 = this.account.accountMasteryInfo.find(m => m.markOfMastery === 1);
      }, error => console.error(error));
    }
  }

  ngOnInit() {
  }
}
