export class AccountTanksFilter {
  accountId: number;
  inGarage: boolean;
  tires: FilterItem[];
  vehicleTypes: FilterItem[];
  nations: FilterItem[];
  dataFrom: Date;
  minBattles: number;

  constructor() {
    this.fillNations();
    this.fillTires();
    this.fillVehicleTypes();
  }

  getFilterQuery(): string {
    return 'api/TanksInfo(' + this.accountId + ')?$filter=TankInGarage eq ' + this.inGarage;
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
