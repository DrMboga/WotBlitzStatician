import { Component, Input } from '@angular/core';

import { AccountInfoDto } from '../../model/account-info-dto';
import { AccountMasteryInfo } from '../../model/account-mastery-info';
import { PlayerPrivateInfo } from '../../model/player-private-info';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent {

  @Input() public playerPrivateInfo: PlayerPrivateInfo;

  private _account: AccountInfoDto;
  @Input() public set account(account: AccountInfoDto) {
    this._account = account;
    if (this._account != null && this._account.accountMasteryInfo != null) {
      this.mastery = this.GetMasteryInfo(this._account.accountMasteryInfo, 4);
      this.rank3 = this.GetMasteryInfo(this._account.accountMasteryInfo, 3);
      this.rank2 = this.GetMasteryInfo(this._account.accountMasteryInfo, 2);
      this.rank1 = this.GetMasteryInfo(this._account.accountMasteryInfo, 1);
    }
  }
  public get account(): AccountInfoDto {
    return this._account;
  }

  public mastery: AccountMasteryInfo;
  public rank1: AccountMasteryInfo;
  public rank2: AccountMasteryInfo;
  public rank3: AccountMasteryInfo;

  constructor() { }

  private GetMasteryInfo(
    masters: AccountMasteryInfo[],
    markOfMastery: number
  ): AccountMasteryInfo {
    let masteryInfo: AccountMasteryInfo = masters.find(
      m => m.markOfMastery === markOfMastery
    );
    if (masteryInfo == null) {
      const tanksCount = masters.length === 0 ? 0 : masters[0].allTanksCount;
      masteryInfo = { markOfMastery: markOfMastery, tanksCount: 0, allTanksCount: tanksCount, masteryTanksRatio: 0 };
    }
    return masteryInfo;
  }
}
