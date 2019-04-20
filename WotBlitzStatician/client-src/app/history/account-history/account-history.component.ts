import { Component, OnInit } from '@angular/core';

import { AccountInfoService } from '../../shared/services/account-info.service';
import { AccountGlobalInfo } from '../../shared/account-global-info';
import { AccountStatHistoryDto } from '../../model/account-stat-history-dto';
import { AccountHistoryChartService } from './account-history-chart.service';

@Component({
  selector: 'app-account-history',
  templateUrl: './account-history.component.html',
  styleUrls: ['./account-history.component.css']
})
export class AccountHistoryComponent implements OnInit {
  public dateFrom: Date;
  public accountHistory: AccountStatHistoryDto[];
  public rareAccountHistory: AccountStatHistoryDto[];

  public winRateChart = [];
  public wn7Chart = [];
  public avgDamageChart = [];
  public avgXpChart = [];

  constructor(
    private accountsInfoService: AccountInfoService,
    public accountGlobalInfo: AccountGlobalInfo,
    private chartService: AccountHistoryChartService
  ) {}

  ngOnInit() {
    const now = new Date();
    now.setMonth(now.getMonth() - 1);
    this.dateFrom = new Date(now.getFullYear(), now.getMonth(), now.getDate());
  }

  loadHistory() {
    // const context = canvas.getContext('winRateCanvas');

    // context.clearRect(0, 0, canvas.width, canvas.height);
    this.accountsInfoService
      .getAccountStatHistory(this.accountGlobalInfo.accountId, this.dateFrom)
      .subscribe(
        data => {
          this.accountHistory = data;
          this.rareAccountHistory = this.chartService.rarefyArray(this.accountHistory);
          this.createCharts();
        },
        error => console.error(error)
      );
  }

  createCharts() {
    this.winRateChart = this.chartService.createLineChart(
      this.chartService.createWinRatesChartData(this.rareAccountHistory),
      'winRateCanvas',
      'WinRate',
      '#3cba9f'
    );

    this.wn7Chart = this.chartService.createLineChart(
      this.chartService.createWn7ChartData(this.rareAccountHistory),
      'wn7Canvas',
      'Wn7',
      '#ffcc00'
    );

    this.avgDamageChart = this.chartService.createLineChart(
      this.chartService.createAvgDamageChartData(this.rareAccountHistory),
      'avgDmgCanvas',
      'Avg Damage',
      '#0090ff'
    );

    this.avgDamageChart = this.chartService.createLineChart(
      this.chartService.createAvgXpChartData(this.rareAccountHistory),
      'avgXpCanvas',
      'Avg Xp',
      '#ff0043'
    );
  }
}
