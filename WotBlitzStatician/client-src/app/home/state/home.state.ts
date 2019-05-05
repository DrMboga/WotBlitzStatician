export interface HomeState {
  currentAccountId: CurrentAccountId;
  currentAccountNick: string | null;
  loggedInAccountNick: string | null;
  wargamingAuthUrl: string | null;
  wargamingAuthUrlLoadError: string | null;
}

export interface CurrentAccountId {
  accountId: number | null;
  accountLoggedIn: boolean;
}


export const initialState: HomeState = {
  currentAccountId: {accountId: 0, accountLoggedIn: false},
  currentAccountNick: 'WotBlitzStatician',
  loggedInAccountNick: null,
  wargamingAuthUrl: null,
  wargamingAuthUrlLoadError: null
};
