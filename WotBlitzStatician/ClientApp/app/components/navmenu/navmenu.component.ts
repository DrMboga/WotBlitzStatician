import { Component, Inject } from '@angular/core';

import { AccountInfo } from '../../Model/account-info';
import { AccountsInfoService } from '../../accounts-info-service';

import { Http } from '@angular/http';

@Component({
	selector: 'nav-menu',
	templateUrl: './navmenu.component.html',
	styleUrls: ['./navmenu.component.css']
})
export class NavMenuComponent {
	public accounts: AccountInfo[];
	public currentAccountId: number;

	// ToDo: Recreate via AccountsInfoService module
	constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
		// ToDo: why 2 times?
		http.get(baseUrl + 'api/AccountInfo').subscribe(result => {
			this.accounts = result.json() as AccountInfo[];
			if (this.accounts.length > 0) {
				this.currentAccountId = this.accounts[0].accountId;
			}
		}, error => console.error(error));
	}

	public getAccountName(): string {
		// ToDo: get from accounts array by 
		return "yeah";
	}
}
