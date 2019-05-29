import { DatePipe } from '@angular/common';
import { CurrentAccountId } from '../../home/state/home.state';
export class AccountTanksFilter {
  inGarage: boolean;
  tires: FilterItem[];
  vehicleTypes: FilterItem[];
  nations: FilterItem[];
  dataFrom: Date;
  minBattles: number;

  constructor(private datePipe: DatePipe) {
    this.fillNations();
    this.fillTires();
    this.fillVehicleTypes();
  }

  /*
  api/TanksInfo(90277267)?$filter=TankInGarage eq true
  api/TanksInfo(90277267)?$filter=VehicleTier in (8, 9) and VehicleType in ('heavyTank')
  api/TanksInfo(90277267)?$filter=VehicleType in ('heavyTank', 'mediumTank')
  api/TanksInfo(90277267)?$filter=VehicleNation in ('ussr')
  api/TanksInfo(90277267)?$filter=TankBattles gt 100
  api/TanksInfo(90277267)?$filter=TankLastBattleTime gt 2018-09-04T00:00:00.00Z
  */
  getFilterQuery(accountId: CurrentAccountId): string {
    const methodName = accountId.accountLoggedIn
      ? 'TanksInfo' : 'GuestTanksInfo';
    let query = `api/${methodName}(${accountId.accountId})`;
    const filters: string[] = new Array();
    if (this.inGarage) {
      filters.push('TankInGarage eq true');
    }

    if (this.tires.some(t => t.isSelected)) {
      let tierQuery = 'VehicleTier in (';
      const tiresFilterRow: string[] = new Array();
      for (const tier of this.tires.filter(t => t.isSelected)) {
        tiresFilterRow.push(tier.itemValue);
      }
      tierQuery = tierQuery.concat(tiresFilterRow.join(', '));
      tierQuery = tierQuery.concat(')');
      filters.push(tierQuery);
    }

    if (this.vehicleTypes.some(t => t.isSelected)) {
      let vehicleQuery = 'VehicleType in (';
      const vehiclesFilterRow: string[] = new Array();
      for (const vehicle of this.vehicleTypes.filter(t => t.isSelected)) {
        vehiclesFilterRow.push('\'' + vehicle.itemValue + '\'');
      }
      vehicleQuery = vehicleQuery.concat(vehiclesFilterRow.join(', '));
      vehicleQuery = vehicleQuery.concat(')');
      filters.push(vehicleQuery);
    }

    if (this.nations.some(t => t.isSelected)) {
      let nationQuery = 'VehicleNation in (';
      const nationsFilterRow: string[] = new Array();
      for (const nation of this.nations.filter(t => t.isSelected)) {
        nationsFilterRow.push('\'' + nation.itemValue + '\'');
      }
      nationQuery = nationQuery.concat(nationsFilterRow.join(', '));
      nationQuery = nationQuery.concat(')');
      filters.push(nationQuery);
    }

    if (this.minBattles > 0) {
      filters.push('TankBattles gt ' + this.minBattles);
    }

    if (this.dataFrom) {
      filters.push('TankLastBattleTime gt ' + this.datePipe.transform(this.dataFrom, 'yyyy-MM-ddTHH:mm:ss.ss') + 'Z');
    }

    if (filters.length > 0) {
      query = query.concat('?$filter=');
      query = query.concat(filters.join(' and '));
    }
    return query;
  }
  // ToDo: Use dictionary wotb.DictionaryNation
  private fillNations() {
    this.nations = [
      { itemValue: 'china', itemName: 'china', isSelected: false },
      { itemValue: 'france', itemName: 'france', isSelected: false },
      { itemValue: 'germany', itemName: 'germany', isSelected: false },
      { itemValue: 'japan', itemName: 'japan', isSelected: false },
      { itemValue: 'other', itemName: 'other', isSelected: false },
      { itemValue: 'uk', itemName: 'uk', isSelected: false },
      { itemValue: 'usa', itemName: 'usa', isSelected: false },
      { itemValue: 'ussr', itemName: 'ussr', isSelected: false },
    ];
  }

  // ToDo: Use dictionary wotb.DictionaryVehicleType
  private fillTires() {
    this.tires = [
      { itemValue: '1', itemName: 'I', isSelected: false },
      { itemValue: '2', itemName: 'II', isSelected: false },
      { itemValue: '3', itemName: 'III', isSelected: false },
      { itemValue: '4', itemName: 'IV', isSelected: false },
      { itemValue: '5', itemName: 'V', isSelected: false },
      { itemValue: '6', itemName: 'VI', isSelected: false },
      { itemValue: '7', itemName: 'VII', isSelected: false },
      { itemValue: '8', itemName: 'VIII', isSelected: false },
      { itemValue: '9', itemName: 'IX', isSelected: false },
      { itemValue: '10', itemName: 'X', isSelected: false },
    ];
  }

  // ToDo: Use dictionary wotb.DictionaryNation
  private fillVehicleTypes() {
    this.vehicleTypes = [
      { itemValue: 'AT-SPG', itemName: 'AT-SPG', isSelected: false },
      { itemValue: 'heavyTank', itemName: 'heavyTank', isSelected: false },
      { itemValue: 'lightTank', itemName: 'lightTank', isSelected: false },
      { itemValue: 'mediumTank', itemName: 'mediumTank', isSelected: false }
    ];
  }
}

export class FilterItem {
  itemValue: string;
  itemName: string;
  isSelected: boolean;
}
