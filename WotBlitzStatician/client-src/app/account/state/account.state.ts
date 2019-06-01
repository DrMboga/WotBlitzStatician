import { AccountInfoDto } from '../../model/account-info-dto';
import { AccountMasteryInfo } from '../../model/account-mastery-info';
import { PlayerPrivateInfo } from '../../model/player-private-info';
import { AccountTanksInfoAggregatedDto } from '../../model/account-tanks-info-aggregated-dto';

export interface AccountState {
  currentAccount: AccountInfoDto;
  masters: AccountMasters;
  playerPrivateInfo: PlayerPrivateInfo;
  aggregatedInfo: AccountTanksInfoAggregatedDto[];
  error: string;
  aggregatedInfoError: string;
  privateInfoLoadError: string;
}

export interface AccountMasters {
  mastery: AccountMasteryInfo;
  rank1: AccountMasteryInfo;
  rank2: AccountMasteryInfo;
  rank3: AccountMasteryInfo;
}
