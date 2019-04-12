import { Component, OnInit, OnDestroy } from '@angular/core';

import { AccountGlobalInfo } from '../account-global-info';
import { AccountInfoDto } from '../../model/account-info-dto';
import { AccountMasteryInfo } from '../../model/account-mastery-info';
import { PlayerPrivateInfo } from '../../model/player-private-info';
import { Subscription } from 'rxjs';
import { AccountsService } from '../../services/accounts.service';

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

  constructor(
    private accountService: AccountsService,
    public accountGlobalInfo: AccountGlobalInfo
  ) {
    this.refreshAccountInfo();
    this.subscription = accountGlobalInfo.accountInfoChanged
      .asObservable()
      .subscribe(() => this.refreshAccountInfo());
  }

  private refreshAccountInfo() {
    const id = this.accountGlobalInfo.accountId;
    if (id != null) {
      this.accountService.getAccount(id).subscribe(
        data => {
          this.account = data;
          this.mastery = this.GetMasteryInfo(this.account.accountMasteryInfo, 4);
          this.rank3 = this.GetMasteryInfo(this.account.accountMasteryInfo, 3);
          this.rank2 = this.GetMasteryInfo(this.account.accountMasteryInfo, 2);
          this.rank1 = this.GetMasteryInfo(this.account.accountMasteryInfo, 1);
        },
        error => console.error(error)
      );
      this.accountService
        .getPlayerPrivateInfo(id)
        .subscribe(
          data => (this.playerPrivateInfo = data),
          error => console.error(error)
        );
    }
  }

  ngOnInit() {}

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  private GetMasteryInfo(
    masters: AccountMasteryInfo[],
    markOfMastery: number
  ): AccountMasteryInfo {
    let masteryInfo: AccountMasteryInfo = masters.find(
      m => m.markOfMastery === markOfMastery
    );
    if (masteryInfo == null) {
      const tanksCount = masters.length === 0 ? 0 : masters[0].allTanksCount;
      masteryInfo = { markOfMastery: markOfMastery, tanksCount: 0, allTanksCount: tanksCount, masteryTanksRatio: 0};
    }
    return masteryInfo;
  }
}
