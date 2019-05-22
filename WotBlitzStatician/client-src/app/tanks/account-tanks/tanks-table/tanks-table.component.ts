import { Component, OnInit, Input } from '@angular/core';
import { TankStatisticDto } from '../../../model/tank-statistic-dto';
import { AccountTanksFooter } from '../account-tanks-footer';

@Component({
  selector: 'app-tanks-table',
  templateUrl: 'tanks-table.component.html',
  styleUrls: ['tanks-table.component.css']
})

export class TanksTableComponent implements OnInit {
  private _tanks: TankStatisticDto[];
  @Input() public set tanks(t: TankStatisticDto[]) {
    this._tanks = t;
    if (t == null) {
      return;
    }
    this.sortTanks();
    this.calculateFooter();
  }
  public get tanks(): TankStatisticDto[] {
    return this._tanks;
  }

  @Input() public showGarageInfo: boolean;

  public sortColumn = 'TankLastBattleTime';
  public sortAscending = false;
  public tableFooter: AccountTanksFooter = new AccountTanksFooter();


  constructor() { }

  ngOnInit() { }

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


}
