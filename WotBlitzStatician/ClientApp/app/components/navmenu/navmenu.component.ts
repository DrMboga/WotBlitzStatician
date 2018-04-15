import { Component } from '@angular/core';

import { AccountInfo } from '../../Model/account-info';
import { AccountsInfoService } from '../../accounts-info-service';

@Component({
    selector: 'nav-menu',
    templateUrl: './navmenu.component.html',
    styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
	constructor(private accountInfoService: AccountsInfoService) {
		this.AccountId = 25;
	}

	public AccountId: number;

	get accounts(): AccountInfo[] {
		return this.accountInfoService.getAccounts();
	}

	changeAccountId(newAccountId: number) {
		this.AccountId = newAccountId;
	}
}
