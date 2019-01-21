import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable()
export class AccountGlobalInfo {
    public accountId: number;
    public accountNick: string;
    public accountInfoChanged: Subject<{}>;

    constructor(
            accountId: number,
            accountNick: string) {
        this.accountId = accountId;
        this.accountNick = accountNick;
        this.accountInfoChanged = new Subject<{}>();
    }

    public EmitAccountInfoChanged()
    {
        this.accountInfoChanged.next(null);
    }
}
