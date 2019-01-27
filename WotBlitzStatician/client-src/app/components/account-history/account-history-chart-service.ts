import { Injectable } from '@angular/core';
import { ChartItem } from '../../model/chart-item';
import { AccountStatHistoryDto } from '../../model/account-stat-history-dto';
import { DatePipe } from '@angular/common';
import { Chart } from 'chart.js';

@Injectable()
export class AccountHistoryChartService {
  constructor(private datePipe: DatePipe) {}

  public createWinRatesChartData(
    accountStatHystory: AccountStatHistoryDto[]
  ): ChartItem[] {
    return accountStatHystory
      .map(h => ({
        x: this.datePipe.transform(h.updatedAt, 'shortDate'),
        y: h.winRate * 100
      }))
      .reverse();
  }

  public createWn7ChartData(
    accountStatHystory: AccountStatHistoryDto[]
  ): ChartItem[] {
    return accountStatHystory
      .map(h => ({
        x: this.datePipe.transform(h.updatedAt, 'shortDate'),
        y: h.wn7
      }))
      .reverse();
  }

  public createAvgDamageChartData(
    accountStatHystory: AccountStatHistoryDto[]
  ): ChartItem[] {
    return accountStatHystory
      .map(h => ({
        x: this.datePipe.transform(h.updatedAt, 'shortDate'),
        y: h.avgDamage
      }))
      .reverse();
  }

  public createAvgXpChartData(
    accountStatHystory: AccountStatHistoryDto[]
  ): ChartItem[] {
    return accountStatHystory
      .map(h => ({
        x: this.datePipe.transform(h.updatedAt, 'shortDate'),
        y: h.avgXp
      }))
      .reverse();
  }

  public createLineChart(
    data: ChartItem[],
    canvasName: string,
    label: string,
    color: string
  ): Chart {
    const commonOptions = {
      legend: {
        display: true
      },
      scales: {
        xAxes: [
          {
            display: true
          }
        ],
        yAxes: [
          {
            display: true
          }
        ]
      }
    };

    const xs: string[] = data.map(d => d.x);
    const ys: number[] = data.map(d => d.y);

    return new Chart(canvasName, {
      type: 'line',
      data: {
        labels: xs,
        datasets: [
          {
            label: label,
            data: ys,
            borderColor: color,
            fill: false
          }
        ]
      },
      options: commonOptions
    });
  }
}
