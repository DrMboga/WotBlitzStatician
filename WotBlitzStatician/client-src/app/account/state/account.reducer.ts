import { AccountState } from './account.state';
import { AccountActions } from './account.actions';

export function accountReducer(state: AccountState, action: AccountActions): AccountState {
  console.log('account reducer', action.type, action.payload);
  return state;
}
