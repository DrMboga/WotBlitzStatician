export interface HomeState {
  currentAccountId: CurrentAccountId;
  currentAccountNick: string | null;
}

export interface CurrentAccountId {
  accountId: number | null;
  accountLoggedIn: boolean;
}
