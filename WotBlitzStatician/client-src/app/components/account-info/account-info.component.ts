import { Component, OnInit } from '@angular/core';

import { AccountInfoService } from '../../services/account-info.service';
import { AccountGlobalInfo } from '../account-global-info';
import { AccountInfoDto } from '../../model/account-info-dto';
import { AccountMasteryInfo } from '../../model/account-mastery-info';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {
  public account: AccountInfoDto;
  public mastery: AccountMasteryInfo;
  public rank1: AccountMasteryInfo;
  public rank2: AccountMasteryInfo;
  public rank3: AccountMasteryInfo;

  constructor(private accountsInfoService: AccountInfoService,
    public accountGlobalInfo: AccountGlobalInfo) {
    let id = accountGlobalInfo.accountId;
    if (id != null) {
      this.accountsInfoService.getAccount(id).subscribe(data => {
        this.account = data;
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
