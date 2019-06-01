import { State } from '../../state/app.state';
import { AccountState } from './account.state';
import { createFeatureSelector, createSelector } from '@ngrx/store';

export interface State extends State {
  accountState: AccountState;
}

const getAccountFeatureState = createFeatureSelector<AccountState>('accountState');

export const getAccountInfo = createSelector(
  getAccountFeatureState,
  state => state == null ? null : state.currentAccount
);

export const getMasters = createSelector(
  getAccountFeatureState,
  state => state == null ? null : state.masters
);

export const getPrivateInfo = createSelector(
  getAccountFeatureState,
  state => state == null ? null : state.playerPrivateInfo
);

export const getAccountInoError = createSelector(
  getAccountFeatureState,
  state => state == null ? null : state.error
);

export const getAccountAggregatedInfo = createSelector(
  getAccountFeatureState,
  state => state == null ? null : state.aggregatedInfo
);

export const getAccountAggregatetInfoError = createSelector(
  getAccountFeatureState,
  state => state == null ? null : state.aggregatedInfoError
);

export const getPrivateInfoError = createSelector(
  getAccountFeatureState,
  state => state == null ? null : state.privateInfoLoadError
);
