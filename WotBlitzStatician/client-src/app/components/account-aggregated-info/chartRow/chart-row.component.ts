import { Component, OnInit, Input, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { ChartRowData } from '../chart-row-data';

import { Chart } from 'chart.js';

@Component({
  selector: 'app-chart-row',
  templateUrl: 'chart-row.component.html'
})
export class ChartRowComponent implements OnInit, AfterViewInit {
  @Input()
  public chartRow: ChartRowData;

  public dataByTypeChart = [];
  public dataByTierChart = [];
  public dataByNationChart = [];
  public dataByPremiumChart = [];

  constructor(private changeDetector: ChangeDetectorRef) {}

  ngAfterViewInit(): void {
    // const el = this.document.getElementById(`dataByTierCanvas-${this.chartRow.id}`);
    // console.log('el afterView', el);

    const blue = '#36a2eb';
    const green = '#55d6ba';
    const orange = '#ddb454';
    const red = '#db5e6a';
    this.dataByTypeChart = this.createBarChart(
      this.chartRow.dataByType,
      `dataByTypeCanvas-${this.chartRow.id}`,
      blue,
      'По типам танков'
    );
    this.dataByTierChart = this.createBarChart(
      this.chartRow.dataByTier,
      `dataByTierCanvas-${this.chartRow.id}`,
      red,
      'По уровням'
    );
    this.dataByNationChart = this.createBarChart(
      this.chartRow.dataByNation,
      `dataByNationCanvas-${this.chartRow.id}`,
      orange,
      'По нациям'
    );
    this.dataByPremiumChart = this.createDoughnutChart(
      this.chartRow.dataByPremium,
      `dataBypremiumCanvas-${this.chartRow.id}`
    );

    this.changeDetector.detectChanges();
  }

  ngOnInit() {
  }

  private createDoughnutChart(
    data: Map<string, number>,
    canvasName: string
  ): Chart {
    const colorsPool = ['#55d6ba', '#36a2eb', '#ffce56', '#ff6384', '#cc65fe'];

    const commonOptions = Chart.defaults.doughnut;
    const labels: string[] = new Array();
    const ys: number[] = new Array();
    const colors: string[] = new Array();

    let colorsIterator = 0;

    data.forEach(function(value, key) {
      labels.push(key);
      ys.push(value);
      colors.push(colorsPool[colorsIterator]);
      colorsIterator++;
      if (colorsIterator === colorsPool.length) {
        colorsIterator = 0;
      }
    });

    return new Chart(canvasName, {
      type: 'doughnut',
      data: {
        labels: labels,
        datasets: [
          {
            data: ys,
            backgroundColor: colors
          }
        ]
      },
      options: commonOptions
    });
  }

  private createBarChart(
    data: Map<string, number>,
    canvasName: string,
    color: string,
    label: string
  ): Chart {
    const commonOptions = Chart.defaults.bar;
    const labels: string[] = new Array();
    const ys: number[] = new Array();
    const colors: string[] = new Array();

    data.forEach(function(value, key) {
      labels.push(key);
      ys.push(value);
      colors.push(color);
    });

    return new Chart(canvasName, {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [
          {
            data: ys,
            backgroundColor: colors,
            label: label
          }
        ]
      },
      options: commonOptions
    });
  }
}
