import { Injectable } from '@angular/core';

import { AccountTanksInfoAggregatedDto } from '../../model/account-tanks-info-aggregated-dto';
import { Chart } from 'chart.js';
import { RomanNumberPipe } from '../pipes/roman-number.pipe';

@Injectable()
export class AccountAggregatedInfoService {
  public battlesByType: Map<string, number>;
  public battlesByTier: Map<string, number>;
  public battlesByNation: Map<string, number>;
  public battlesByPremium: Map<string, number>;

  public winRateByTier: Map<string, number>;
  public winRateByType: Map<string, number>;
  public winRateByNation: Map<string, number>;
  public winRateByPremium: Map<string, number>;

  constructor(private romanNumPipe: RomanNumberPipe) {}

  aggregateAccountTnksInfo(dataToAggregate: AccountTanksInfoAggregatedDto[]) {
    this.battlesByType = new Map<string, number>();
    this.battlesByTier = new Map<string, number>();
    this.battlesByNation = new Map<string, number>();
    this.battlesByPremium = new Map<string, number>();
    this.winRateByType = new Map<string, number>();
    this.winRateByTier = new Map<string, number>();
    this.winRateByNation = new Map<string, number>();
    this.winRateByPremium = new Map<string, number>();

    const winsByType = new Map<string, number>();
    const winsByTier = new Map<string, number>();
    const winsByNation = new Map<string, number>();
    const winsByPremium = new Map<string, number>();

    dataToAggregate.forEach(dataElement => {
      const tankType = this.transformtypeName(dataElement.type);
      this.setOrAddValue(this.battlesByType, tankType, dataElement.battles);
      this.setOrAddValue(winsByType, tankType, dataElement.wins);

      const tier = this.romanNumPipe.transform(dataElement.tier);
      this.setOrAddValue(this.battlesByTier, tier, dataElement.battles);
      this.setOrAddValue(winsByTier, tier, dataElement.wins);

      const nation = this.transformNation(dataElement.nation);
      this.setOrAddValue(this.battlesByNation, nation, dataElement.battles);
      this.setOrAddValue(winsByNation, nation, dataElement.wins);

      const prem: string = dataElement.isPremium ? 'Премиум' : 'Исследуемая';
      this.setOrAddValue(this.battlesByPremium, prem, dataElement.battles);
      this.setOrAddValue(winsByPremium, prem, dataElement.wins);
    });

    const persentageAvg = (value, battles) => Math.round((10000 * value) / battles) / 100;
    this.createAvgsCollection(
      winsByType,
      this.battlesByType,
      this.winRateByType,
      persentageAvg
    );
    this.createAvgsCollection(
      winsByTier,
      this.battlesByTier,
      this.winRateByTier,
      persentageAvg
    );
    this.createAvgsCollection(
      winsByNation,
      this.battlesByNation,
      this.winRateByNation,
      persentageAvg
    );
    this.createAvgsCollection(
      winsByPremium,
      this.battlesByPremium,
      this.winRateByPremium,
      persentageAvg
    );
  }

  public createDoughnutChart(
    data: Map<string, number>,
    canvasName: string
  ): Chart {
    const colorsPool = ['#36a2eb', '#cc65fe', '#ffce56', '#ff6384'];

    const commonOptions = Chart.defaults.doughnut;
    const labels: string[] = new Array();
    const ys: number[] = new Array();
    const colors: string[] = new Array();

    let colorsIterator = 0;

    data.forEach(function(value, key) {
      labels.push(key);
      ys.push(value);
      colors.push(colorsPool[colorsIterator]);
      colorsIterator++;
      if (colorsIterator === colorsPool.length) {
        colorsIterator = 0;
      }
    });

    return new Chart(canvasName, {
      type: 'doughnut',
      data: {
        labels: labels,
        datasets: [
          {
            data: ys,
            backgroundColor: colors
          }
        ]
      },
      options: commonOptions
    });
  }

  public createBarChart(
    data: Map<string, number>,
    canvasName: string,
    color: string,
    label: string
  ): Chart {
    const commonOptions = Chart.defaults.bar;
    const labels: string[] = new Array();
    const ys: number[] = new Array();
    const colors: string[] = new Array();

    data.forEach(function(value, key) {
      labels.push(key);
      ys.push(value);
      colors.push(color);
    });

    return new Chart(canvasName, {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [
          {
            data: ys,
            backgroundColor: colors,
            label: label
          }
        ]
      },
      options: commonOptions
    });
  }

  private setOrAddValue(
    collection: Map<string, number>,
    key: string,
    value: number
  ) {
    if (collection.has(key)) {
      collection.set(key, collection.get(key) + value);
    } else {
      collection.set(key, value);
    }
  }

  private transformtypeName(type: string): string {
    switch (type) {
      case 'lightTank':
        return 'ЛТ';
      case 'AT-SPG':
        return 'ПТ';
      case 'mediumTank':
        return 'СТ';
      case 'heavyTank':
        return 'ТТ';
      default:
        return '';
    }
  }

  private transformNation(nation: string): string {
    switch (nation) {
      case 'china':
        return 'Ch';
      case 'france':
        return 'Fr';
      case 'germany':
        return 'Ger';
      case 'japan':
        return 'Jap';
      case 'other':
        return 'WG';
      case 'uk':
        return 'UK';
      case 'usa':
        return 'US';
      case 'ussr':
        return 'USSR';
      default:
        return '';
    }
  }

  private createAvgsCollection(
    nonAvgCollection: Map<string, number>,
    collectionWithBattlesCount: Map<string, number>,
    collectionToMakeAvgs: Map<string, number>,
    avgCalc: (val: number, battlesCount: number) => number
  ) {
    nonAvgCollection.forEach(function(value, key) {
      const battles = collectionWithBattlesCount.get(key);
      if (battles > 0) {
        const avgVal = avgCalc(value, battles);
        collectionToMakeAvgs.set(key, avgVal);
      }
    });
  }
}
