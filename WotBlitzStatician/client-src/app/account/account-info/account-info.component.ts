import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs';
import { Store, select } from '@ngrx/store';

import { AccountInfoDto } from '../../model/account-info-dto';
import { AccountMasteryInfo } from '../../model/account-mastery-info';
import { PlayerPrivateInfo } from '../../model/player-private-info';
import { State } from '../state/account.state';
import { getAccountInfo } from '../state/account.selectors';
import { LoadAccountInfo } from '../state/account.actions';
import { getAccountId } from '../../state/app.selectors';
import { ChangeCurrentAccount } from '../../state/app.actions';
import { takeWhile } from 'rxjs/operators';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit, OnDestroy {
  public account$: Observable<AccountInfoDto>;

  componentActive = true;


  public account: AccountInfoDto;
  public mastery: AccountMasteryInfo;
  public rank1: AccountMasteryInfo;
  public rank2: AccountMasteryInfo;
  public rank3: AccountMasteryInfo;
  public playerPrivateInfo: PlayerPrivateInfo;

  constructor(private store: Store<State>) { }

  ngOnInit() {
    this.account$ = this.store.pipe(select(getAccountInfo));

    this.store.pipe(
      select(getAccountId),
      takeWhile(() => this.componentActive)
    )
      .subscribe(accountId => this.store.dispatch<LoadAccountInfo>(new LoadAccountInfo(accountId)));

    // ToDo: masters$, privateInfo$, error$
  }


  private refreshAccountInfo() {
    // const id = this.accountGlobalInfo.accountId;
    // if (id != null) {
    //   this.accountService.getAccount(id).subscribe(
    //     data => {
    //       this.account = data;
    //       this.mastery = this.GetMasteryInfo(this.account.accountMasteryInfo, 4);
    //       this.rank3 = this.GetMasteryInfo(this.account.accountMasteryInfo, 3);
    //       this.rank2 = this.GetMasteryInfo(this.account.accountMasteryInfo, 2);
    //       this.rank1 = this.GetMasteryInfo(this.account.accountMasteryInfo, 1);
    //     },
    //     error => console.error(error)
    //   );
    //   this.accountService
    //     .getPlayerPrivateInfo(id)
    //     .subscribe(
    //       data => (this.playerPrivateInfo = data),
    //       error => console.error(error)
    //     );
    // }
  }

  ngOnDestroy(): void {
    this.componentActive = false;
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
      masteryInfo = { markOfMastery: markOfMastery, tanksCount: 0, allTanksCount: tanksCount, masteryTanksRatio: 0 };
    }
    return masteryInfo;
  }
}
