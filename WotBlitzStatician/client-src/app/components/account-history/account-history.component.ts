import { Component, OnInit, Input } from '@angular/core';
import { Chart } from 'chart.js';
import { DatePipe } from '@angular/common';

import { AccountInfoService } from '../../services/account-info.service';
import { AccountGlobalInfo } from '../account-global-info';

@Component({
    selector: 'account-history',
    templateUrl: './account-history.component.html',
    styleUrls: ['./account-history.component.css']
})
export class AccountHistoryComponent implements OnInit {

    public dateFrom: Date;
    public accountHistory: any[];

    public dates: string[];
    public winRates: number[];
    public wn7Data: number[];
    public avgDamageData: number[];
    public avgXpData: number[];

    public winRateChart = [];
    public wn7Chart = [];
    public avgDamageChart = [];
    public avgXpChart = [];

    constructor(private accountsInfoService: AccountInfoService,
        public accountGlobalInfo: AccountGlobalInfo,
        private datePipe: DatePipe) {
    }

    ngOnInit() {
        let now = new Date();
        now.setMonth(now.getMonth() - 1);
        this.dateFrom = new Date(now.getFullYear(), now.getMonth(), now.getDate());

        /////
        this.accountHistory = JSON.parse(this.dataString);
        this.dates = this.accountHistory.map(h => this.datePipe.transform(h.updatedAt, 'shortDate')).reverse();
        this.winRates = this.accountHistory.map(h => h.winRate * 100).reverse();
        this.wn7Data = this.accountHistory.map(h => h.wn7).reverse();
        this.avgDamageData = this.accountHistory.map(h => h.avgDamage).reverse();
        this.avgXpData = this.accountHistory.map(h => h.avgXp).reverse();
        this.createCharts();
        /////
    }

    loadHistory() {
        this.accountsInfoService.getAccountStatHistory(this.accountGlobalInfo.accountId, this.dateFrom).subscribe(data => {
            this.accountHistory = data as any[];
            this.dates = this.accountHistory.map(h => this.datePipe.transform(h.updatedAt, 'shortDate')).reverse();
            this.winRates = this.accountHistory.map(h => h.winRate * 100).reverse();
            this.wn7Data = this.accountHistory.map(h => h.wn7).reverse();
            this.avgDamageData = this.accountHistory.map(h => h.avgDamage).reverse();
            this.avgXpData = this.accountHistory.map(h => h.avgXp).reverse();
            this.createCharts();
        }, error => console.error(error));
    }

    createCharts() {
        let commonOptions = {
            legend: {
                display: true
            },
            scales: {
                xAxes: [{
                    display: true
                }],
                yAxes: [{
                    display: true
                }],
            }
        };
        this.winRateChart = new Chart('winRateCanvas', {
            type: 'line',
            data: {
                labels: this.dates,
                datasets: [
                    {
                        label: "WinRate",
                        data: this.winRates,
                        borderColor: "#3cba9f", 
                        fill: false
                    },
                ]
            },
            options: commonOptions
        });
        this.wn7Chart = new Chart('wn7Canvas', {
            type: 'line',
            data: {
                labels: this.dates,
                datasets: [
                    {
                        label: "Wn7",
                        data: this.wn7Data,
                        borderColor: "#ffcc00", 
                        fill: false
                    },
                ]
            },
            options: commonOptions
        });
        this.avgDamageChart = new Chart('avgDmgCanvas', {
            type: 'line',
            data: {
                labels: this.dates,
                datasets: [
                    {
                        label: "Avg Damage",
                        data: this.avgDamageData,
                        borderColor: "#0090ff", 
                        fill: false
                    },
                ]
            },
            options: commonOptions
        });
        this.avgXpChart = new Chart('avgXpCanvas', {
            type: 'line',
            data: {
                labels: this.dates,
                datasets: [
                    {
                        label: "Avg Xp",
                        data: this.avgXpData,
                        borderColor: "#ff0043", 
                        fill: false
                    },
                ]
            },
            options: commonOptions
        });
    }

    private dataString = `[{
        "updatedAt": "2018-10-21T10:10:08",
        "battles": 4972,
        "battlesDiff": 379,
        "wins": 2774,
        "avgTier": 6.58,
        "avgTierDiff": 0.10,
        "wn7": 1376.12,
        "wn7Diff": -11.51,
        "winRate": 0.5579243765084473049,
        "winRateDiff": -0.0027113735459833504,
        "avgDamage": 1139,
        "avgDamageDiff": 26,
        "avgXp": 596,
        "avgXpDiff": 0,
        "survivalRate": 0.3433226065969428801,
        "survivalRateDiff": -0.0008968578054085242,
        "credits": 1637205,
        "creditsDiff": -3020581,
        "freeXp": 55145,
        "freeXpDiff": -36851,
        "gold": 1333,
        "goldDiff": 125
    }, {
        "updatedAt": "2018-09-19T00:05:15",
        "battles": 4593,
        "battlesDiff": 21,
        "wins": 2575,
        "avgTier": 6.48,
        "avgTierDiff": 0.00,
        "wn7": 1387.64,
        "wn7Diff": 0.31,
        "winRate": 0.5606357500544306553,
        "winRateDiff": -0.0003878719928134391,
        "avgDamage": 1113,
        "avgDamageDiff": 2,
        "avgXp": 596,
        "avgXpDiff": 0,
        "survivalRate": 0.3442194644023514043,
        "survivalRateDiff": 0.0001687207453085347,
        "credits": 4657786,
        "creditsDiff": 488309,
        "freeXp": 91996,
        "freeXpDiff": 1026,
        "gold": 1208,
        "goldDiff": 0
    }, {
        "updatedAt": "2018-09-17T21:45:15",
        "battles": 4572,
        "battlesDiff": 12,
        "wins": 2565,
        "avgTier": 6.48,
        "avgTierDiff": 0.00,
        "wn7": 1387.33,
        "wn7Diff": 0.64,
        "winRate": 0.5610236220472440944,
        "winRateDiff": 0.0002780080121563752,
        "avgDamage": 1111,
        "avgDamageDiff": 2,
        "avgXp": 596,
        "avgXpDiff": 0,
        "survivalRate": 0.3440507436570428696,
        "survivalRateDiff": -0.0000282037113781830,
        "credits": 4169477,
        "creditsDiff": 315145,
        "freeXp": 90970,
        "freeXpDiff": 661,
        "gold": 1208,
        "goldDiff": 0
    }, {
        "updatedAt": "2018-09-16T22:47:51",
        "battles": 4560,
        "battlesDiff": 24,
        "wins": 2557,
        "avgTier": 6.47,
        "avgTierDiff": 0.01,
        "wn7": 1386.68,
        "wn7Diff": 1.00,
        "winRate": 0.5607456140350877192,
        "winRateDiff": 0.0001195117423187598,
        "avgDamage": 1109,
        "avgDamageDiff": 2,
        "avgXp": 596,
        "avgXpDiff": 0,
        "survivalRate": 0.3440789473684210526,
        "survivalRateDiff": 0.0006045205606609116,
        "credits": 3854332,
        "creditsDiff": 936994,
        "freeXp": 90309,
        "freeXpDiff": 5190,
        "gold": 1208,
        "goldDiff": 0
    }, {
        "updatedAt": "2018-09-15T22:22:42",
        "battles": 4536,
        "battlesDiff": 32,
        "wins": 2543,
        "avgTier": 6.47,
        "avgTierDiff": 0.00,
        "wn7": 1385.68,
        "wn7Diff": 2.27,
        "winRate": 0.5606261022927689594,
        "winRateDiff": 0.0013454628611526184,
        "avgDamage": 1107,
        "avgDamageDiff": 3,
        "avgXp": 596,
        "avgXpDiff": 1,
        "survivalRate": 0.3434744268077601410,
        "survivalRateDiff": 0.0000019578912414910,
        "credits": 2917338,
        "creditsDiff": 1099139,
        "freeXp": 85119,
        "freeXpDiff": 4490,
        "gold": 1208,
        "goldDiff": 0
    }, {
        "updatedAt": "2018-09-14T23:43:51",
        "battles": 4504,
        "battlesDiff": 9,
        "wins": 2519,
        "avgTier": 6.46,
        "avgTierDiff": 0.00,
        "wn7": 1383.41,
        "wn7Diff": -0.27,
        "winRate": 0.5592806394316163410,
        "winRateDiff": 0.0002150109555318027,
        "avgDamage": 1104,
        "avgDamageDiff": 0,
        "avgXp": 595,
        "avgXpDiff": 0,
        "survivalRate": 0.3434724689165186500,
        "survivalRateDiff": -0.0000203008276415279,
        "credits": 1818199,
        "creditsDiff": 52518,
        "freeXp": 80629,
        "freeXpDiff": 495,
        "gold": 1208,
        "goldDiff": 0
    }, {
        "updatedAt": "2018-09-13T18:52:36",
        "battles": 4495,
        "battlesDiff": 13,
        "wins": 2513,
        "avgTier": 6.46,
        "avgTierDiff": 0.00,
        "wn7": 1383.68,
        "wn7Diff": 1.51,
        "winRate": 0.5590656284760845383,
        "winRateDiff": 0.0003864673872848953,
        "avgDamage": 1104,
        "avgDamageDiff": 1,
        "avgXp": 595,
        "avgXpDiff": 0,
        "survivalRate": 0.3434927697441601779,
        "survivalRateDiff": -0.0001038389126894428,
        "credits": 1765681,
        "creditsDiff": 150202,
        "freeXp": 80134,
        "freeXpDiff": 1566,
        "gold": 1208,
        "goldDiff": 0
    }, {
        "updatedAt": "2018-09-12T22:00:34",
        "battles": 4482,
        "battlesDiff": 29,
        "wins": 2504,
        "avgTier": 6.46,
        "avgTierDiff": 0.01,
        "wn7": 1382.17,
        "wn7Diff": -1.31,
        "winRate": 0.5586791610887996430,
        "winRateDiff": -0.0004944297488379047,
        "avgDamage": 1103,
        "avgDamageDiff": 2,
        "avgXp": 595,
        "avgXpDiff": 2,
        "survivalRate": 0.3435966086568496207,
        "survivalRateDiff": -0.0002165510107901726,
        "credits": 1615479,
        "creditsDiff": 699397,
        "freeXp": 78568,
        "freeXpDiff": 4788,
        "gold": 1208,
        "goldDiff": 0
    }, {
        "updatedAt": "2018-09-11T20:50:13",
        "battles": 4453,
        "battlesDiff": 16,
        "wins": 2490,
        "avgTier": 6.45,
        "avgTierDiff": 0.01,
        "wn7": 1383.48,
        "wn7Diff": 0.43,
        "winRate": 0.5591735908376375477,
        "winRateDiff": 0.0004627501795352264,
        "avgDamage": 1101,
        "avgDamageDiff": 2,
        "avgXp": 593,
        "avgXpDiff": 1,
        "survivalRate": 0.3438131596676397933,
        "survivalRateDiff": 0.0003378385046918556,
        "credits": 916082,
        "creditsDiff": 274160,
        "freeXp": 73780,
        "freeXpDiff": 4452,
        "gold": 1208,
        "goldDiff": 0
    }, {
        "updatedAt": "2018-09-10T23:41:19",
        "battles": 4437,
        "battlesDiff": 22,
        "wins": 2479,
        "avgTier": 6.44,
        "avgTierDiff": 0.01,
        "wn7": 1383.05,
        "wn7Diff": -2.90,
        "winRate": 0.5587108406581023213,
        "winRateDiff": -0.0009720585491456968,
        "avgDamage": 1099,
        "avgDamageDiff": 1,
        "avgXp": 592,
        "avgXpDiff": -1,
        "survivalRate": 0.3434753211629479377,
        "survivalRateDiff": -0.0005790389729524020,
        "credits": 641922,
        "creditsDiff": 44617,
        "freeXp": 69328,
        "freeXpDiff": 1422,
        "gold": 1208,
        "goldDiff": 0
    }, {
        "updatedAt": "2018-09-09T09:58:03",
        "battles": 4415,
        "battlesDiff": 6,
        "wins": 2471,
        "avgTier": 6.43,
        "avgTierDiff": 0.00,
        "wn7": 1385.95,
        "wn7Diff": -1.03,
        "winRate": 0.5596828992072480181,
        "winRateDiff": -0.0005348372409261710,
        "avgDamage": 1098,
        "avgDamageDiff": 0,
        "avgXp": 593,
        "avgXpDiff": 0,
        "survivalRate": 0.3440543601359003397,
        "survivalRateDiff": -0.0002413985395362672,
        "credits": 597305,
        "creditsDiff": 4295,
        "freeXp": 67906,
        "freeXpDiff": 229,
        "gold": 1208,
        "goldDiff": 0
    }, {
        "updatedAt": "2018-09-08T23:16:13",
        "battles": 4409,
        "battlesDiff": null,
        "wins": 2470,
        "avgTier": 6.43,
        "avgTierDiff": null,
        "wn7": 1386.98,
        "wn7Diff": null,
        "winRate": 0.5602177364481741891,
        "winRateDiff": null,
        "avgDamage": 1098,
        "avgDamageDiff": null,
        "avgXp": 593,
        "avgXpDiff": null,
        "survivalRate": 0.3442957586754366069,
        "survivalRateDiff": null,
        "credits": 593010,
        "creditsDiff": null,
        "freeXp": 67677,
        "freeXpDiff": null,
        "gold": 1208,
        "goldDiff": null
    }]`;
}
