export interface State {
  currentAccountId: CurrentAccountId;
  currentAccountNick: string | null;
}

export interface CurrentAccountId {
  accountId: number | null;
  accountLoggedIn: boolean;
}
