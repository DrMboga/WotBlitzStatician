import { TankStatisticDto } from './tank-statistic-dto';

export interface TankByAchievementDto {
  achievementId: string;
  achievementsCount: number;
  tankInfo: TankStatisticDto;
}
