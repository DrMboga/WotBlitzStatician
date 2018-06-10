import { Component, Inject } from '@angular/core';

import { AccountsInfoService } from '../../accounts-info-service';
import { AccountState, ACCOUNT_STATE } from '../../Model/account-state';

import { Observable } from "rxjs/Observable";

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
	public accountId: number = 0;

	constructor(accountsInfoService: AccountsInfoService,
		@Inject(ACCOUNT_STATE) accountState: Observable<AccountState>) {
		accountState.subscribe(newAccount => {
			this.accountId = newAccount.accountId;
			// ToDo: fetch new accont data from accountsInfoService
		});
	}
}
