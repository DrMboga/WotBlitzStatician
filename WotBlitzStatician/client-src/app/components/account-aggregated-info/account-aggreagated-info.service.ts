import { Injectable } from '@angular/core';

import { AccountTanksInfoAggregatedDto } from '../../model/account-tanks-info-aggregated-dto';
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

  public wn7RateByTier: Map<string, number>;
  public wn7RateByType: Map<string, number>;
  public wn7RateByNation: Map<string, number>;
  public wn7RateByPremium: Map<string, number>;

  public dmgByTier: Map<string, number>;
  public dmgByType: Map<string, number>;
  public dmgByNation: Map<string, number>;
  public dmgByPremium: Map<string, number>;

  public masteryByTier: Map<string, number>;
  public masteryByType: Map<string, number>;
  public masteryByNation: Map<string, number>;
  public masteryByPremium: Map<string, number>;

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
    this.wn7RateByType = new Map<string, number>();
    this.wn7RateByTier = new Map<string, number>();
    this.wn7RateByNation = new Map<string, number>();
    this.wn7RateByPremium = new Map<string, number>();
    this.dmgByType = new Map<string, number>();
    this.dmgByTier = new Map<string, number>();
    this.dmgByNation = new Map<string, number>();
    this.dmgByPremium = new Map<string, number>();
    this.masteryByType = new Map<string, number>();
    this.masteryByTier = new Map<string, number>();
    this.masteryByNation = new Map<string, number>();
    this.masteryByPremium = new Map<string, number>();

    const tanksByType = new Map<string, number>();
    const tanksByTier = new Map<string, number>();
    const tanksByNation = new Map<string, number>();
    const tanksByPremium = new Map<string, number>();

    const winsByType = new Map<string, number>();
    const winsByTier = new Map<string, number>();
    const winsByNation = new Map<string, number>();
    const winsByPremium = new Map<string, number>();

    const wn7ByType = new Map<string, number>();
    const wn7ByTier = new Map<string, number>();
    const wn7ByNation = new Map<string, number>();
    const wn7ByPremium = new Map<string, number>();

    const dmgSumByType = new Map<string, number>();
    const dmgSumByTier = new Map<string, number>();
    const dmgSumByNation = new Map<string, number>();
    const dmgSumByPremium = new Map<string, number>();

    dataToAggregate.forEach(dataElement => {
      const tankType = this.transformtypeName(dataElement.type);
      this.setOrAddValue(this.battlesByType, tankType, dataElement.battles);
      this.setOrAddValue(winsByType, tankType, dataElement.wins);
      this.setOrAddValue(wn7ByType, tankType, dataElement.wn7);
      this.setOrAddValue(tanksByType, tankType, 1);
      this.setOrAddValue(dmgSumByType, tankType, dataElement.damageDealt);
      this.setOrAddValue(
        this.masteryByType,
        tankType,
        this.transformMastery(dataElement.markOfMastery)
      );

      const tier = this.romanNumPipe.transform(dataElement.tier);
      this.setOrAddValue(this.battlesByTier, tier, dataElement.battles);
      this.setOrAddValue(winsByTier, tier, dataElement.wins);
      this.setOrAddValue(wn7ByTier, tier, dataElement.wn7);
      this.setOrAddValue(tanksByTier, tier, 1);
      this.setOrAddValue(dmgSumByTier, tier, dataElement.damageDealt);
      this.setOrAddValue(
        this.masteryByTier,
        tier,
        this.transformMastery(dataElement.markOfMastery)
      );

      const nation = this.transformNation(dataElement.nation);
      this.setOrAddValue(this.battlesByNation, nation, dataElement.battles);
      this.setOrAddValue(winsByNation, nation, dataElement.wins);
      this.setOrAddValue(wn7ByNation, nation, dataElement.wn7);
      this.setOrAddValue(tanksByNation, nation, 1);
      this.setOrAddValue(dmgSumByNation, nation, dataElement.damageDealt);
      this.setOrAddValue(
        this.masteryByNation,
        nation,
        this.transformMastery(dataElement.markOfMastery)
      );

      const prem: string = dataElement.isPremium ? 'Премиум' : 'Исследуемая';
      this.setOrAddValue(this.battlesByPremium, prem, dataElement.battles);
      this.setOrAddValue(winsByPremium, prem, dataElement.wins);
      this.setOrAddValue(wn7ByPremium, prem, dataElement.wn7);
      this.setOrAddValue(tanksByPremium, prem, 1);
      this.setOrAddValue(dmgSumByPremium, prem, dataElement.damageDealt);
      this.setOrAddValue(
        this.masteryByPremium,
        prem,
        this.transformMastery(dataElement.markOfMastery)
      );
    });

    const persentageAvg = (value, battles) =>
      Math.round((10000 * value) / battles) / 100;

    const wn7Avg = (value, battles) =>
      Math.round((100 * value) / battles) / 100;

    this.createAvgsCollection(
      winsByType,
      this.battlesByType,
      this.winRateByType,
      persentageAvg
    );
    this.createAvgsCollection(
      wn7ByType,
      tanksByType,
      this.wn7RateByType,
      wn7Avg
    );
    this.createAvgsCollection(
      dmgSumByType,
      this.battlesByType,
      this.dmgByType,
      wn7Avg
    );
    this.createAvgsCollection(
      winsByTier,
      this.battlesByTier,
      this.winRateByTier,
      persentageAvg
    );
    this.createAvgsCollection(
      wn7ByTier,
      tanksByTier,
      this.wn7RateByTier,
      wn7Avg
    );
    this.createAvgsCollection(
      dmgSumByTier,
      this.battlesByTier,
      this.dmgByTier,
      wn7Avg
    );
    this.createAvgsCollection(
      winsByNation,
      this.battlesByNation,
      this.winRateByNation,
      persentageAvg
    );
    this.createAvgsCollection(
      wn7ByNation,
      tanksByNation,
      this.wn7RateByNation,
      wn7Avg
    );
    this.createAvgsCollection(
      dmgSumByNation,
      this.battlesByNation,
      this.dmgByNation,
      wn7Avg
    );
    this.createAvgsCollection(
      winsByPremium,
      this.battlesByPremium,
      this.winRateByPremium,
      persentageAvg
    );
    this.createAvgsCollection(
      wn7ByPremium,
      tanksByPremium,
      this.wn7RateByPremium,
      wn7Avg
    );
    this.createAvgsCollection(
      dmgSumByPremium,
      this.battlesByPremium,
      this.dmgByPremium,
      wn7Avg
    );
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

  private transformMastery(markOfMastery: number) {
    if (markOfMastery === 4) {
      return 1;
    }
    return 0;
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
