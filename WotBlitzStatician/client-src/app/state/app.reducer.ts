import { State } from './app.state';
import { AppActions, AppActionTypes } from './app.actions';

export function appReducer(state: State, action: AppActions): State {
  console.log('appReducer', action.type, action.payload);
  switch (action.type) {
    case AppActionTypes.ChangeCurrentAccount:
      return {
        ...state,
        currentAccountId: action.payload.currentAccountId,
        currentAccountNick: action.payload.currentAccountNick,
        currentAccountLoggedIn: action.payload.currentAccountLoggedIn
      };
    default:
      return state;
  }
}
