import { Component, OnInit } from '@angular/core';
import { AccountInfoService } from '../../services/account-info.service';
import { AccountGlobalInfo } from '../account-global-info';
import { AccountAggregatedInfoService } from './account-aggreagated-info.service';

@Component({
  selector: 'app-account-aggregated-info',
  templateUrl: 'account-aggreagated-info.component.html'
})
export class AccountAggregatedInfoComponent implements OnInit {
  constructor(
    private accountsInfoService: AccountInfoService,
    private accountAggregatedInfoService: AccountAggregatedInfoService,
    public accountGlobalInfo: AccountGlobalInfo
  ) {
    this.accountsInfoService
      .getAggregatedAccountTanksInfo(this.accountGlobalInfo.accountId)
      .subscribe(data => {
        this.accountAggregatedInfoService.aggregateAccountTnksInfo(data);
        this.constructCharts();
      });
  }

  constructCharts() {
    // ToDo: construct chart for each array in this.accountAggregatedInfoService
    this.accountAggregatedInfoService.battlesByType.forEach(function(value, key) {
      console.log(key + ' = ' + value);
    });
  }

  ngOnInit() {}
}
