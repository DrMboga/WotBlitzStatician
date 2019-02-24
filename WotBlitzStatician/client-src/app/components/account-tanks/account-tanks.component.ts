import { Component, OnInit, OnDestroy } from '@angular/core';
import { DatePipe } from '@angular/common';

import { AccountInfoService } from '../../services/account-info.service';
import { AccountTanksFilter } from './account-tanks-filter';
import { AccountTanksFooter } from './account-tanks-footer';
import { AccountGlobalInfo } from '../account-global-info';
import { TankStatisticDto } from '../../model/tank-statistic-dto';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-account-tanks',
  templateUrl: './account-tanks.component.html',
  styleUrls: ['./account-tanks.component.css']
})
export class AccountTanksComponent implements OnInit, OnDestroy {
  public accountId = 0;
  public filter: AccountTanksFilter;
  public tanks: TankStatisticDto[];
  public sortColumn: string;
  public sortAscending: boolean;
  public tableFooter: AccountTanksFooter;
  private subscription: Subscription;

  constructor(
    private accountsInfoService: AccountInfoService,
    public accountGlobalInfo: AccountGlobalInfo,
    datePipe: DatePipe
  ) {
    this.filter = new AccountTanksFilter(datePipe);
    this.tableFooter = new AccountTanksFooter();

    this.sortColumn = 'TankLastBattleTime';
    this.sortAscending = false;

    const id = accountGlobalInfo.accountId;
    if (id != null) {
      this.accountId = id;

      this.filter.accountId = this.accountId;
      this.filter.inGarage = false;
      const now = new Date();
      now.setMonth(now.getMonth() - 1);
      this.filter.dataFrom = new Date(now.getFullYear(), now.getMonth(), now.getDate());

      this.queryData();
    }
    this.subscription = this.accountGlobalInfo.accountInfoChanged
      .asObservable()
      .subscribe(() => this.queryData());
  }

  queryData() {
    this.accountsInfoService
      .getTanksDataByQuery(this.filter.getFilterQuery())
      .subscribe(
        data => {
          this.tanks = data;
          this.sortTanks();
          this.calculateFooter();
        },
        error => console.error(error)
      );
  }

  sortTanks() {
    this.tanks.sort(
      (left, right): number => {
        if (left[this.sortColumn] < right[this.sortColumn]) {
          return -1;
        }
        if (left[this.sortColumn] > right[this.sortColumn]) {
          return 1;
        }
        return 0;
      }
    );

    if (!this.sortAscending) {
      this.tanks.reverse();
    }
  }

  calculateFooter() {
    let battlesSum = 0;
    let winsSum = 0;
    let wn7Sum = 0;
    let damageSum = 0;
    let xpSum = 0;
    let avgTierSum = 0;
    for (const tank of this.tanks) {
      battlesSum += tank.TankBattles;
      winsSum += tank.WinRate;
      wn7Sum += tank.TankWn7;
      damageSum += tank.AvgDamage;
      xpSum += tank.AvgXp;
      avgTierSum += tank.VehicleTier;
    }
    this.tableFooter.battlesSum = battlesSum;
    this.tableFooter.avgWinrate = winsSum / this.tanks.length;
    this.tableFooter.avgWn7 = wn7Sum / this.tanks.length;
    this.tableFooter.avgDamage = damageSum / this.tanks.length;
    this.tableFooter.avgXp = xpSum / this.tanks.length;
    this.tableFooter.avgTier = avgTierSum / this.tanks.length;
  }

  trackByTankId(index: number, item: TankStatisticDto) {
    return item.TankTankId;
  }

  ngOnInit() {}
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
