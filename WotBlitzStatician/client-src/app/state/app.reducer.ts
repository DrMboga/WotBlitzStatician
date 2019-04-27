import { State } from './app.state';
import { AppActions, AppActionTypes } from './app.actions';

export function appReducer(state: State, action: AppActions): State {
  switch (action.type) {
    case AppActionTypes.ChangeCurrentAccount:
      return {
        ...state,
        currentAccountId: action.payload.currentAccountId,
        currentAccountNick: action.payload.currentAccountNick
      };
    default:
      return state;
  }
}
