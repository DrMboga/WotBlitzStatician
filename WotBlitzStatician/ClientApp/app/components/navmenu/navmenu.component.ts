import { Component } from '@angular/core';

import { AccountInfo } from '../../Model/account-info';
import { AccountsInfoService } from '../../accounts-info-service';

@Component({
	selector: 'nav-menu',
	templateUrl: './navmenu.component.html',
	styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
	public accounts: AccountInfo[] = new Array<AccountInfo>();
	public _currentAccountId: number = 0;
	public accountName: string = "None"

	constructor(private accountsInfoService: AccountsInfoService) {
		this.accountsInfoService.getAccounts().subscribe(data => {
			this.accounts = data;
			if (this.accounts.length > 0) {
				this.currentAccountId = this.accounts[0].accountId;
			}
		});
	}

	get currentAccountId(): number {
		return this._currentAccountId;
	}

	set currentAccountId(newAccountId: number) {
		this._currentAccountId = newAccountId;
		console.log(newAccountId);
		var account = this.accounts.find(a => a.accountId == newAccountId)!;
		this.accountName = account.nickName;
		// ToDo: raise event
	}
}
