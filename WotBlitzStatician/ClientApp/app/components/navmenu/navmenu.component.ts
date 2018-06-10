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
	public accountName: string = "None"

	constructor(private accountsInfoService: AccountsInfoService) {
		this.accountsInfoService.getAccounts().subscribe(data => {
			this.accounts = data;
			if (this.accounts.length > 0) {
				this.accountName = this.accounts[0].nickName;
			}
		});
	}
}
