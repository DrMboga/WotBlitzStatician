import { createFeatureSelector, createSelector } from '@ngrx/store';
import { State } from './app.state';

const getAppFeatureState = createFeatureSelector<State>('app');

export const getAccountId = createSelector(
  getAppFeatureState,
  state => state.currentAccountId
);
