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
    default:
      return state;
  }
}
