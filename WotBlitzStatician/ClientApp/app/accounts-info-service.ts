import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
//import { Observable } from "rxjs/Observable";
//import "rxjs/add/observable/from";
import { AccountInfo } from "./Model/account-info";

@Injectable()
export class AccountsInfoService {
	private dummyInfo: AccountInfo[] = [
		{ AccountId: 20, NickName: "Petya" },
		{ AccountId: 25, NickName: "Vasya" }
	];

	//constructor(http: Http) { }

	getAccounts(): AccountInfo[] {
		//http.get(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
		//	this.forecasts = result.json() as WeatherForecast[];
		//}, error => console.error(error));
		return this.dummyInfo;
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