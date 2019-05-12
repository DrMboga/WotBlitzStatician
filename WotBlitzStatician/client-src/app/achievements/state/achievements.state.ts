import { AccountAchievementDto } from '../../model/account-achievement-dto';
import { TankByAchievementDto } from '../../model/tank-by-achievement-dto';

export interface AchievementsState {
  battleAchievements: AccountAchievementDto[];
  epicAchievements: AccountAchievementDto[];
  platoonAchievements: AccountAchievementDto[];
  titleAchievements: AccountAchievementDto[];
  commemorativeAchievements: AccountAchievementDto[];
  stepAchievements: AccountAchievementDto[];

  achevementsLoadError: string;

  tanksByAchievement: TankByAchievementDto[];

  tanksByAchievemntsLoadError: string;
}
