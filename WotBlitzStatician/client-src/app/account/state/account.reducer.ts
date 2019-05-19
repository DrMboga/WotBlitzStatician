import { AccountState } from './account.state';
import { AccountActions, AccountActionTypes } from './account.actions';

export function accountReducer(state: AccountState, action: AccountActions): AccountState {
  switch (action.type) {
    case AccountActionTypes.AccountInfoLoadSuccess:
      return {
        ...state,
        currentAccount: action.payload
      };
    case AccountActionTypes.AccountInfoLoadFailed:
      return {
        ...state,
        error: action.payload
      };
    case AccountActionTypes.AccountAggregatedInfoLoaded:
      return {
        ...state,
        aggregatedInfo: action.payload
      };
    case AccountActionTypes.AccountAggregatedInfoLoadFailed:
      return {
        ...state,
        aggregatedInfoError: action.payload
      };
    case AccountActionTypes.AccountPrivateInfoLoaded:
      return {
        ...state,
        playerPrivateInfo: action.payload
      };
    case AccountActionTypes.AccountPrivateInfoLoadFailed:
      return {
        ...state,
        privateInfoLoadError: action.payload
      };
    default:
      return state;
  }
}
