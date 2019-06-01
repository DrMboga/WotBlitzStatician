import { AppActions, AppActionTypes } from './app.actions';
import { HomeState, initialState } from '../home/state/home.state';

export function appReducer(state: HomeState = initialState, action: AppActions): HomeState {
  switch (action.type) {
    case AppActionTypes.ChangeCurrentAccount:
      return {
        ...state,
        currentAccountId: action.payload.currentAccountId,
        currentAccountNick: action.payload.currentAccountNick,
        loggedInAccountNick: action.payload.loggedInAccountNick
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
    case AppActionTypes.WargamingLogout:
    case AppActionTypes.ReturnFromGuestAccount:
    return {
      ...state,
      currentAccountId: { accountId: 0, accountLoggedIn: false},
      currentAccountNick: 'WotblitzStatician',
      loggedInAccountNick: null
    };
    case AppActionTypes.GuestAccountSelected:
      return {
        ...state,
        currentAccountId: { accountId: action.payload.accountId, accountLoggedIn: false },
        currentAccountNick: action.payload.accountNick
      };
    default:
      return state;
  }
}
