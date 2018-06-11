import { Component, Inject } from '@angular/core';

import { AccountInfo } from '../../Model/account-info';
import { AccountsInfoService } from '../../accounts-info-service';
import { AccountState, ACCOUNT_STATE } from '../../Model/account-state';

import { Observable } from "rxjs/Observable";

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
	public accountId: number = 0;
	public account: any; //AccountInfo | undefined;

	constructor(accountsInfoService: AccountsInfoService,
		@Inject(ACCOUNT_STATE) accountState: Observable<AccountState>) {
		accountState.subscribe(newAccount => {
			this.accountId = newAccount.accountId;

			// Fetch new accont data from accountsInfoService
			accountsInfoService.getAccount(this.accountId).subscribe(data => {
				this.account = data;
			}, error => console.error(error));
		});
	}
}
