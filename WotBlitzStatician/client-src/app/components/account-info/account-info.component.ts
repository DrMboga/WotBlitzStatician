import { Component, OnInit, OnDestroy } from '@angular/core';

import { AccountInfoService } from '../../services/account-info.service';
import { AccountGlobalInfo } from '../account-global-info';
import { AccountInfoDto } from '../../model/account-info-dto';
import { AccountMasteryInfo } from '../../model/account-mastery-info';
import { PlayerPrivateInfo } from '../../model/player-private-info';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit, OnDestroy {
  public account: AccountInfoDto;
  public mastery: AccountMasteryInfo;
  public rank1: AccountMasteryInfo;
  public rank2: AccountMasteryInfo;
  public rank3: AccountMasteryInfo;
  public playerPrivateInfo: PlayerPrivateInfo;
  subscription: Subscription;

  constructor(private accountsInfoService: AccountInfoService,
    public accountGlobalInfo: AccountGlobalInfo) {
    this.refreshAccountInfo();
    this.subscription = accountGlobalInfo.accountInfoChanged.asObservable().subscribe(() =>
      this.refreshAccountInfo())
  }

  private refreshAccountInfo() {
    let id = this.accountGlobalInfo.accountId;
    if (id != null) {
      this.accountsInfoService.getAccount(id).subscribe(data => {
        this.account = data;
        this.mastery = this.account.accountMasteryInfo.find(m => m.markOfMastery === 4);
        this.rank3 = this.account.accountMasteryInfo.find(m => m.markOfMastery === 3);
        this.rank2 = this.account.accountMasteryInfo.find(m => m.markOfMastery === 2);
        this.rank1 = this.account.accountMasteryInfo.find(m => m.markOfMastery === 1);
      }, error => console.error(error));
      this.accountsInfoService.getPlayerPrivateInfo(id).subscribe(data => this.playerPrivateInfo = data, error => console.error(error));
    }
  }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
