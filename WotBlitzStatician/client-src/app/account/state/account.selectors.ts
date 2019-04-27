import { AccountState } from './account.state';
import { createFeatureSelector, createSelector } from '@ngrx/store';

const getAccountFeatureState = createFeatureSelector<AccountState>('account');

export const getAccountInfo = createSelector(
  getAccountFeatureState,
  state => state.currentAccount
);

export const getMasters = createSelector(
  getAccountFeatureState,
  state => state.masters
);

export const getPrivateInfo = createSelector(
  getAccountFeatureState,
  state => state.playerPrivateInfo
);

export const getAccountInoError = createSelector(
  getAccountFeatureState,
  state => state.error
);
