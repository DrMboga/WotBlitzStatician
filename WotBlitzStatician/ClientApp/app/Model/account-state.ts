import { OpaqueToken } from "@angular/core";

export class AccountState {
	constructor(public accountId: number) {}
}

export const ACCOUNT_STATE = new OpaqueToken("account_state");
