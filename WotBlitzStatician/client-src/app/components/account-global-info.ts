import { Injectable, InjectionToken } from "@angular/core";

@Injectable()
export class AccountGlobalInfo {
    constructor(
        public accountId: number,
        public accountNick: string){}
}

export const GLOBAL_ACCOUNT_STATE = new InjectionToken<string>('global_account_state');
