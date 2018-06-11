import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import { AccountInfo } from "./Model/account-info";

@Injectable()
export class AccountsInfoService {

	constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
	}

	getAccounts(): Observable<AccountInfo[]> {
		return this.http.get(this.baseUrl + 'api/AccountInfo').map(response => response.json());
	}

	getAccount(accountId: number): Observable<AccountInfo> {
		return this.http.get(this.baseUrl + 'api/AccountInfo/' + accountId)
			.map(response => response.json());
	}

}
