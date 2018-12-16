import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';

import { AccountInfoService } from '../../services/account-info.service';
import { AccountTanksFilter } from "./account-tanks-filter";
import { AccountTanksFooter } from "./account-tanks-footer";
import { AccountGlobalInfo } from '../account-global-info';
import { TankStatisticDto } from '../../model/tank-statistic-dto';


@Component({
  selector: 'app-account-tanks',
  templateUrl: './account-tanks.component.html',
  styleUrls: ['./account-tanks.component.css']
})
export class AccountTanksComponent implements OnInit {
  public accountId: number = 0;
  public filter: AccountTanksFilter;
  public tanks: TankStatisticDto[];
  public sortColumn: string;
  public sortAscending: boolean;
  public tableFooter: AccountTanksFooter;

  constructor(private accountsInfoService: AccountInfoService,
    public accountGlobalInfo: AccountGlobalInfo,
    datePipe: DatePipe) {
    this.filter = new AccountTanksFilter(datePipe);
    this.tableFooter = new AccountTanksFooter();

    this.sortColumn = 'TankLastBattleTime';
    this.sortAscending = false;

    let id = accountGlobalInfo.accountId;
    if (id != null) {
      this.accountId = id;

      this.filter.accountId = this.accountId;
      this.filter.inGarage = true;

      this.queryData();
    }
  }

  queryData() {
    this.accountsInfoService.getTanksDataByQuery(this.filter.getFilterQuery()).subscribe(data => {
      this.tanks = data;
      this.sortTanks();
      this.calculateFooter();
    }, error => console.error(error));
  }

  sortTanks() {
    this.tanks.sort((left, right): number => {
      if (left[this.sortColumn] < right[this.sortColumn]) return -1;
      if (left[this.sortColumn] > right[this.sortColumn]) return 1;
      return 0;
    });

    if (!this.sortAscending) {
      this.tanks.reverse();
    }
  }

  calculateFooter() {
    let battlesSum: number = 0;
    let winsSum: number = 0;
    let wn7Sum: number = 0;
    let damageSum: number = 0;
    let xpSum: number = 0;
    let avgTierSum: number = 0;
    for (let tank of this.tanks) {
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

  trackByTankId(index: number, item: TankStatisticDto){
    return item.TankTankId;
  }

  ngOnInit() {
  }
}
