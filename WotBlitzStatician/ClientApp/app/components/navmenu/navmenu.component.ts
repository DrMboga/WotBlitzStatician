import { Component, Inject } from '@angular/core';
import { Observer } from "rxjs/Observer";

import { AccountInfo } from '../../Model/account-info';
import { AccountsInfoService } from '../../accounts-info-service';
import { AccountState, ACCOUNT_STATE } from '../../Model/account-state';


@Component({
	selector: 'nav-menu',
	templateUrl: './navmenu.component.html',
	styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
	public accounts: AccountInfo[] = new Array<AccountInfo>();
	public _currentAccountId: number = 0;
	public accountName: string = "None"

	constructor(private accountsInfoService: AccountsInfoService,
		@Inject(ACCOUNT_STATE) private accountState: Observer<AccountState>) {
		this.accountsInfoService.getAccounts().subscribe(data => {
			this.accounts = data;
			if (this.accounts.length > 0) {
				this.currentAccountId = this.accounts[0].accountId;
			}
		}, error => console.error(error));
	}

	get currentAccountId(): number {
		return this._currentAccountId;
	}

	set currentAccountId(newAccountId: number) {
		this._currentAccountId = newAccountId;
		var account = this.accounts.find(a => a.accountId == newAccountId)!;
		this.accountName = account.nickName;
		// raise event
		this.accountState.next(new AccountState(newAccountId));
	}
}
