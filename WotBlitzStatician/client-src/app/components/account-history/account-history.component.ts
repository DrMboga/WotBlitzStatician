import { Component, OnInit, Input } from '@angular/core';
import { Chart } from 'chart.js';
import { DatePipe } from '@angular/common';

import { AccountInfoService } from '../../services/account-info.service';

@Component({
    selector: 'account-history',
    templateUrl: './account-history.component.html',
    styleUrls: ['./account-history.component.css']
})
export class AccountHistoryComponent implements OnInit {

    @Input("accountId")
    public accountId: number;
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
        private datePipe: DatePipe) {
    }

    ngOnInit() {
        let now = new Date();
        now.setMonth(now.getMonth() - 1);
        this.dateFrom = new Date(now.getFullYear(), now.getMonth(), now.getDate());
    }

    loadHistory() {
        this.accountsInfoService.getAccountStatHistory(this.accountId, this.dateFrom).subscribe(data => {
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
}
