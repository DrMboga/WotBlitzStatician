import { createFeatureSelector, createSelector } from '@ngrx/store';
import { HomeState } from '../home/state/home.state';

const getAppFeatureState = createFeatureSelector<HomeState>('homeState');

export const getAccountId = createSelector(
  getAppFeatureState,
  state => state.currentAccountId
);

export const getWgAuthUrl = createSelector(
  getAppFeatureState,
  state => state.wargamingAuthUrl
);

export const getWgAuthUrlError = createSelector(
  getAppFeatureState,
  state => state.wargamingAuthUrlLoadError
);
