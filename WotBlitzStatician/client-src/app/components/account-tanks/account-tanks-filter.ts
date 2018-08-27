export class AccountTanksFilter {
  accountId: number;
  inGarage: boolean;

  getFilterQuery(): string {
    return 'api/TanksInfo(' + this.accountId + ')?$filter=TankInGarage eq ' + this.inGarage;
  }
}
