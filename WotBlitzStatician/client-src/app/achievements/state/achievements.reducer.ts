import { AchievementsState } from './achievements.state';
import { AchievementActions, AchievementsActionTypes } from './achievements.actions';

export function achievementReducer(state: AchievementsState, action: AchievementActions): AchievementsState {
  switch (action.type) {
    case AchievementsActionTypes.AccountAchievementsLoaded:
      return {
        ...state,
        battleAchievements: action.payload.filter(
          achievement => achievement.section === 'battle'
        ),
        epicAchievements: action.payload.filter(
          achievement => achievement.section === 'epic'
        ),
        platoonAchievements: action.payload.filter(
          achievement => achievement.section === 'platoon'
        ),
        titleAchievements: action.payload.filter(
          achievement => achievement.section === 'title'
        ),
        commemorativeAchievements: action.payload.filter(
          achievement => achievement.section === 'commemorative'
        ),
        stepAchievements: action.payload.filter(
          achievement => achievement.section === 'commemorative'
        ),
        tanksByAchievement: null
      };
    case AchievementsActionTypes.AccountAchievementsLoadFail:
      return {
        ...state,
        achevementsLoadError: action.payload,
        battleAchievements: null,
        epicAchievements: null,
        platoonAchievements: null,
        titleAchievements: null,
        commemorativeAchievements: null,
        stepAchievements: null,
        tanksByAchievement: null
      };
    case AchievementsActionTypes.TanksByAchievementLoaded:
      return {
        ...state,
        tanksByAchievement: action.payload.sort(
                (left, right): number => {
                  if (left.achievementsCount < right.achievementsCount) {
                    return 11;
                  }
                  if (left.achievementsCount > right.achievementsCount) {
                    return -1;
                  }
                  return 0;
                }
              )
      };
    case AchievementsActionTypes.ClearTanksByAchievement:
      return {
        ...state,
        tanksByAchievement: null
      };
    case AchievementsActionTypes.TanksByAchievementLoadFailed:
      return {
        ...state,
        tanksByAchievemntsLoadError: action.payload
      };
    default:
      return state;
  }
}
