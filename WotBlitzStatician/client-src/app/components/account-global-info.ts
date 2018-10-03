import { Injectable } from "@angular/core";

@Injectable()
export class AccountGlobalInfo {
    constructor(
        public accountId: number,
        public accountNick: string){}
}
