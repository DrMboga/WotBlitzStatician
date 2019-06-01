import { Component, Input } from '@angular/core';
import { AccountAggregatedInfoService } from './account-aggreagated-info.service';
import { ChartRowData } from './chart-row-data';
import { AccountTanksInfoAggregatedDto } from '../../model/account-tanks-info-aggregated-dto';

@Component({
  selector: 'app-account-aggregated-info',
  templateUrl: 'account-aggreagated-info.component.html'
})
export class AccountAggregatedInfoComponent {
  public charts: ChartRowData[];

  private _aggregatedInfo: AccountTanksInfoAggregatedDto[];
  @Input() public set aggregatedInfo(inputInfo: AccountTanksInfoAggregatedDto[]) {
    this.charts = [];
    this._aggregatedInfo = inputInfo;
    if (inputInfo == null) {
      return;
    }
    this.accountAggregatedInfoService.aggregateAccountTnksInfo(inputInfo);
    this.constructCharts();
  }

  public get aggregatedInfo(): AccountTanksInfoAggregatedDto[] {
    return this._aggregatedInfo;
  }

  constructor(
    private accountAggregatedInfoService: AccountAggregatedInfoService
  ) { }

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
}
