import { Injectable } from "@angular/core";

@Injectable()
export class AccountGlobalInfo {
    public accountId: number;
    public accountNick: string;

    constructor(
        accountId: number,
        accountNick: string){
            this.accountId = accountId;
            this.accountNick = accountNick
        }
}
