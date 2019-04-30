import { AccountInfoDto } from '../../model/account-info-dto';
import { AccountMasteryInfo } from '../../model/account-mastery-info';
import { PlayerPrivateInfo } from '../../model/player-private-info';

export interface AccountState {
  currentAccount: AccountInfoDto;
  masters: AccountMasters;
  playerPrivateInfo: PlayerPrivateInfo;
  error: string;
}

export interface AccountMasters {
  mastery: AccountMasteryInfo;
  rank1: AccountMasteryInfo;
  rank2: AccountMasteryInfo;
  rank3: AccountMasteryInfo;
}
