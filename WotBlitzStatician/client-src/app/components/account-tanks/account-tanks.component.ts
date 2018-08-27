import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";

import { AccountInfoService } from '../../services/account-info.service';
import { AccountTanksFilter } from "./account-tanks-filter";

@Component({
  selector: 'app-account-tanks',
  templateUrl: './account-tanks.component.html',
  styleUrls: ['./account-tanks.component.css']
})
export class AccountTanksComponent implements OnInit {
  public accountId: number = 0;
  public filter: AccountTanksFilter;
  public tanks: any[];
  public sortColumn: string;
  public sortAscending: boolean;

  constructor(private accountsInfoService: AccountInfoService,
    activeRoute: ActivatedRoute) {
    this.filter = new AccountTanksFilter();
    this.sortColumn = 'TankLastBattleTime';
    this.sortAscending = false;

    let id = activeRoute.snapshot.params["accountId"];
    if (id != null) {
      this.accountId = id;

      this.filter.accountId = this.accountId;
      this.filter.inGarage = true;

      this.accountsInfoService.getDataByQuery(this.filter.getFilterQuery()).subscribe(data => {
        this.tanks = data.value;
        this.sortTanks();
      }, error => console.error(error));

    }
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

  ngOnInit() {
  }

}
