import { Component, OnInit, OnDestroy } from '@angular/core';
import { AccountInfoService } from '../../shared/services/account-info.service';
import { AccountAggregatedInfoService } from './account-aggreagated-info.service';
import { Subscription } from 'rxjs';
import { ChartRowData } from './chart-row-data';

@Component({
  selector: 'app-account-aggregated-info',
  templateUrl: 'account-aggreagated-info.component.html'
})
export class AccountAggregatedInfoComponent implements OnInit, OnDestroy {
  private subscription: Subscription;

  public charts: ChartRowData[];

  constructor(
    private accountsInfoService: AccountInfoService,
    private accountAggregatedInfoService: AccountAggregatedInfoService
  ) {
    this.charts = [];
    this.readData();
    // this.subscription = accountGlobalInfo.accountInfoChanged
    //   .asObservable()
    //   .subscribe(() => this.readData());
  }

  readData() {
    this.charts = [];
    // this.accountsInfoService
    //   .getAggregatedAccountTanksInfo(this.accountGlobalInfo.accountId)
    //   .subscribe(data => {
    //     this.accountAggregatedInfoService.aggregateAccountTnksInfo(data);
    //     this.constructCharts();
    //   });
  }

  constructCharts() {
    this.charts = [
      {
        id: 'battles',
        caption: 'Количество боёв',
        dataByType: this.accountAggregatedInfoService.battlesByType,
        dataByTier: this.accountAggregatedInfoService.battlesByTier,
        dataByNation: this.accountAggregatedInfoService.battlesByNation,
        dataByPremium: this.accountAggregatedInfoService.battlesByPremium
      },
      {
        id: 'win',
        caption: 'Процент побед',
        dataByType: this.accountAggregatedInfoService.winRateByType,
        dataByTier: this.accountAggregatedInfoService.winRateByTier,
        dataByNation: this.accountAggregatedInfoService.winRateByNation,
        dataByPremium: this.accountAggregatedInfoService.winRateByPremium
      },
      {
        id: 'wn7',
        caption: 'Wn7',
        dataByType: this.accountAggregatedInfoService.wn7RateByType,
        dataByTier: this.accountAggregatedInfoService.wn7RateByTier,
        dataByNation: this.accountAggregatedInfoService.wn7RateByNation,
        dataByPremium: this.accountAggregatedInfoService.wn7RateByPremium
      },
      {
        id: 'Dmg',
        caption: 'Средний урон',
        dataByType: this.accountAggregatedInfoService.dmgByType,
        dataByTier: this.accountAggregatedInfoService.dmgByTier,
        dataByNation: this.accountAggregatedInfoService.dmgByNation,
        dataByPremium: this.accountAggregatedInfoService.dmgByPremium
      },
      {
        id: 'Mastery',
        caption: 'Знак мастер',
        dataByType: this.accountAggregatedInfoService.masteryByType,
        dataByTier: this.accountAggregatedInfoService.masteryByTier,
        dataByNation: this.accountAggregatedInfoService.masteryByNation,
        dataByPremium: this.accountAggregatedInfoService.masteryByPremium
      }
    ];
  }

  ngOnInit() {}
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
