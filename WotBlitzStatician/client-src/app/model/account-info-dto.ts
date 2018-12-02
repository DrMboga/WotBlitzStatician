import { PlayerClanInfo } from "./player-clan-info";
import { PlayerStatistics } from "./player-statistics";
import { AccountMasteryInfo } from "./account-mastery-info";

export interface AccountInfoDto {
    accountId: number;
    nickName: string;
    lastBattleTime: Date;
    accountCreatedAt: Date;
    playerClanInfo: PlayerClanInfo;
    playerStatistics: PlayerStatistics;
    accountMasteryInfo: AccountMasteryInfo[];
  }
  