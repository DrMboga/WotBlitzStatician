import { AppActions, AppActionTypes } from './app.actions';
import { HomeState, initialState } from '../home/state/home.state';

export function appReducer(state: HomeState = initialState, action: AppActions): HomeState {
  switch (action.type) {
    case AppActionTypes.ChangeCurrentAccount:
      return {
        ...state,
        currentAccountId: action.payload.currentAccountId,
        currentAccountNick: action.payload.currentAccountNick
      };
    case AppActionTypes.WargamingLoginUrlLoaded:
      return {
        ...state,
        wargamingAuthUrl: action.payload
      };
    case AppActionTypes.WargamingLoginUrlLoadFailed:
      return {
        ...state,
        wargamingAuthUrlLoadError: action.payload
      };
    case AppActionTypes.ClearWargamingLoginUrl:
      return {
        ...state,
        wargamingAuthUrl: null,
        wargamingAuthUrlLoadError: null
      };
    default:
      return state;
  }
}
