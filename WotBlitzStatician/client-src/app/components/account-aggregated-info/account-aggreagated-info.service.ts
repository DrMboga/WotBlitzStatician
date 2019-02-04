import { Injectable } from '@angular/core';

import { AccountTanksInfoAggregatedDto } from '../../model/account-tanks-info-aggregated-dto';

@Injectable()
export class AccountAggregatedInfoService {
  public battlesByType: Map<string, number>;

  constructor() {
    this.battlesByType = new Map<string, number>();

  }

  aggregateAccountTnksInfo(dataToAggregate: AccountTanksInfoAggregatedDto[]) {
    this.battlesByType.clear();
    dataToAggregate.forEach(dataElement => {
      if (this.battlesByType.has(dataElement.typeName)) {
        this.battlesByType.set(
          dataElement.typeName,
          this.battlesByType.get(dataElement.typeName) + dataElement.battles
        );
      } else {
        this.battlesByType.set(dataElement.typeName, dataElement.battles);
      }
    });
  }
}
