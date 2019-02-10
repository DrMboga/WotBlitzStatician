import { Component, OnInit, OnDestroy } from '@angular/core';
import { AccountInfoService } from '../../services/account-info.service';
import { AccountGlobalInfo } from '../account-global-info';
import { AccountAggregatedInfoService } from './account-aggreagated-info.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-account-aggregated-info',
  templateUrl: 'account-aggreagated-info.component.html'
})
export class AccountAggregatedInfoComponent implements OnInit, OnDestroy {
  public battlesByTypeChart = [];
  public battlesByTierChart = [];
  public battlesByNationChart = [];
  public battlesByPremiumChart = [];
  public winrateByTypeChart = [];
  public winrateByTierChart = [];
  public winrateByNationChart = [];
  public winrateByPremiumChart = [];
  subscription: Subscription;

  constructor(
    private accountsInfoService: AccountInfoService,
    private accountAggregatedInfoService: AccountAggregatedInfoService,
    public accountGlobalInfo: AccountGlobalInfo
  ) {
    this.readData();
    this.subscription = accountGlobalInfo.accountInfoChanged
      .asObservable()
      .subscribe(() => this.readData());
  }

  readData() {
    this.accountsInfoService
      .getAggregatedAccountTanksInfo(this.accountGlobalInfo.accountId)
      .subscribe(data => {
        this.accountAggregatedInfoService.aggregateAccountTnksInfo(data);
        this.constructCharts();
      });
  }

  constructCharts() {
    console.log('ЛТ', this.accountAggregatedInfoService.winRateByType.get('ЛТ'));
    this.accountAggregatedInfoService.createBarChart(
      this.accountAggregatedInfoService.battlesByType,
      'battlesByTypeCanvas',
      '#36a2eb',
      'По типам танков'
    );
    this.accountAggregatedInfoService.createBarChart(
      this.accountAggregatedInfoService.battlesByTier,
      'battlesByTierCanvas',
      '#ffcc00',
      'По уровням'
    );
    this.accountAggregatedInfoService.createBarChart(
      this.accountAggregatedInfoService.battlesByNation,
      'battlesByNationCanvas',
      '#ff6384',
      'По нациям'
    );
    this.accountAggregatedInfoService.createDoughnutChart(
      this.accountAggregatedInfoService.battlesByPremium,
      'battlesBypremiumCanvas'
    );

    this.accountAggregatedInfoService.createBarChart(
      this.accountAggregatedInfoService.winRateByType,
      'winrateByTypeCanvas',
      '#36a2eb',
      'По типам танков'
    );
    this.accountAggregatedInfoService.createBarChart(
      this.accountAggregatedInfoService.winRateByTier,
      'winrateByTierCanvas',
      '#ffcc00',
      'По уровням'
    );
    this.accountAggregatedInfoService.createBarChart(
      this.accountAggregatedInfoService.winRateByNation,
      'winrateByNationCanvas',
      '#ff6384',
      'По нациям'
    );
    this.accountAggregatedInfoService.createDoughnutChart(
      this.accountAggregatedInfoService.winRateByPremium,
      'winrateBypremiumCanvas'
    );
  }

  ngOnInit() {}
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
