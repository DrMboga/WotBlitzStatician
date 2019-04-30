import { AppActions, AppActionTypes } from './app.actions';
import { HomeState } from '../home/state/home.state';

export function appReducer(state: HomeState, action: AppActions): HomeState {
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
