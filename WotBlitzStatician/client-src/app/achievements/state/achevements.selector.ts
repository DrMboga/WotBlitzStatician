import { State } from '../../state/app.state';
import { AchievementsState } from './achievements.state';
import { createFeatureSelector, createSelector } from '@ngrx/store';

export interface State extends State {
  achievementsState: AchievementsState;
}

const getAchievementsFeatureState = createFeatureSelector<AchievementsState>('achievementsState');

export const getEpicAchievements = createSelector(
  getAchievementsFeatureState,
  state => state == null ? null : state.epicAchievements
);
export const getBattleAchievements = createSelector(
  getAchievementsFeatureState,
  state => state == null ? null : state.battleAchievements
);
export const getPlatoonAchievements = createSelector(
  getAchievementsFeatureState,
  state => state == null ? null : state.platoonAchievements
);
export const getTitleAchievements = createSelector(
  getAchievementsFeatureState,
  state => state == null ? null : state.titleAchievements
);
export const getCommemorativeAchievements = createSelector(
  getAchievementsFeatureState,
  state => state == null ? null : state.commemorativeAchievements
);
export const getStepAchievements = createSelector(
  getAchievementsFeatureState,
  state => state == null ? null : state.stepAchievements
);
export const getAchevementsLoadError = createSelector(
  getAchievementsFeatureState,
  state => state == null ? null : state.achevementsLoadError
);
export const getTanksByAchievement = createSelector(
  getAchievementsFeatureState,
  state => state == null ? null : state.tanksByAchievement
);
export const getTanksByAchievemntsLoadError = createSelector(
  getAchievementsFeatureState,
  state => state == null ? null : state.tanksByAchievemntsLoadError
);
