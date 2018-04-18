import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
//import { Observable } from "rxjs/Observable";
//import "rxjs/add/observable/from";
import { AccountInfo } from "./Model/account-info";

@Injectable()
export class AccountsInfoService {
	private accountInfo: AccountInfo[]; 

	constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
		http.get(baseUrl + 'api/AccountInfo').subscribe(result => {
			this.accountInfo = result.json() as AccountInfo[];
		}, error => console.error(error));
	}

	getAccounts(): AccountInfo[] {
		return this.accountInfo;
	}


	/*
	getAccounts(): Observable<AccountInfo[]> {
		//http.get(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
		//	this.forecasts = result.json() as WeatherForecast[];
		//}, error => console.error(error));
		return Observable.from([this.dummyInfo]);
	}
	*/

}
