import { Component, OnInit, OnDestroy, ChangeDetectionStrategy } from '@angular/core';

import { AccountStatHistoryDto } from '../../model/account-stat-history-dto';
import { AccountHistoryChartService } from './account-history-chart.service';
import { HistoryService } from '../history.service';
import { Store, select } from '@ngrx/store';
import { State } from '../../state/app.state';
import { getAccountId } from '../../state/app.selectors';
import { takeWhile, map, catchError, mergeMap, shareReplay } from 'rxjs/operators';
import { Observable, combineLatest, of, BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-account-history',
  templateUrl: './account-history.component.html',
  styleUrls: ['./account-history.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AccountHistoryComponent implements OnInit, OnDestroy {
  componentActive = true;

  public error$ = new BehaviorSubject<string>(null);
  public dateFrom$: BehaviorSubject<Date>;
  public accountHistory$: Observable<AccountStatHistoryDto[]>;
  private rareAccountHistory$: Observable<AccountStatHistoryDto[]>;

  public winRateChart = [];
  public wn7Chart = [];
  public avgDamageChart = [];
  public avgXpChart = [];

  constructor(
    private historyService: HistoryService,
    private chartService: AccountHistoryChartService,
    private store: Store<State>,
  ) {
    const now = new Date();
    now.setMonth(now.getMonth() - 1);
    this.dateFrom$ = new BehaviorSubject<Date>(new Date(now.getFullYear(), now.getMonth(), now.getDate()));

    this.accountHistory$ = combineLatest(
      this.dateFrom$,
      this.store.pipe(
        select(getAccountId),
      )
    ).pipe(
      mergeMap(([dateFrom, accountId]) =>
        this.historyService.getAccountStatHistory(accountId.accountId, dateFrom)
          .pipe(
            catchError(err => {
              this.error$.next(err);
              return of([]);
            })),
          ),
      shareReplay()
    );

    this.rareAccountHistory$ = this.accountHistory$.pipe(
      map(accountHistory => this.chartService.rarefyArray(accountHistory)),
      shareReplay()
    );
  }

  ngOnInit() {
    this.createCharts();
  }

  public dateFromChanged(newDateFrom: Date) {
    this.dateFrom$.next(newDateFrom);
  }

  // ToDo: Get rid of subscription when my own chart component will be created
  createCharts() {
    this.rareAccountHistory$.pipe(
      map(rareHistory => this.chartService.createWinRatesChartData(rareHistory)),
      takeWhile(() => this.componentActive)
    ).subscribe(chartData =>
      this.winRateChart = this.chartService.createLineChart(
        chartData,
        'winrateCanvas',
        'WinRate',
        '#3cba9f'
      ));

    this.rareAccountHistory$.pipe(
      map(rareHistory => this.chartService.createWn7ChartData(rareHistory)),
      takeWhile(() => this.componentActive)
    ).subscribe(chartData =>
      this.wn7Chart = this.chartService.createLineChart(
        chartData,
        'wn7Canvas',
        'Wn7',
        '#ffcc00'
      ));

    this.rareAccountHistory$.pipe(
      map(rareHistory => this.chartService.createAvgDamageChartData(rareHistory)),
      takeWhile(() => this.componentActive)
    ).subscribe(chartData => this.avgDamageChart = this.chartService.createLineChart(
      chartData,
      'avgDmgCanvas',
      'Avg Damage',
      '#0090ff'
    ));

    this.rareAccountHistory$.pipe(
      map(rareHistory => this.chartService.createAvgXpChartData(rareHistory)),
      takeWhile(() => this.componentActive)
    ).subscribe(chartData => this.avgXpChart = this.chartService.createLineChart(
      chartData,
      'avgXpCanvas',
      'Avg Xp',
      '#ff0043'
    ));
  }

  ngOnDestroy(): void {
    this.componentActive = false;
  }
}
