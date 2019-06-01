import { Component, OnInit, OnDestroy, ChangeDetectionStrategy } from '@angular/core';
import { DatePipe } from '@angular/common';

import { AccountTanksFilter } from './account-tanks-filter';
import { TankStatisticDto } from '../../model/tank-statistic-dto';
import { BehaviorSubject, Observable, merge, combineLatest, of } from 'rxjs';
import { AccountTanksService } from './account-tanks.service';
import { Store, select } from '@ngrx/store';
import { State } from '../../state/app.state';
import { getAccountId } from '../../state/app.selectors';
import { Actions, ofType } from '@ngrx/effects';
import { AppActionTypes, AccountInfoRefreshed } from '../../state/app.actions';
import { map, mergeMap, catchError, tap } from 'rxjs/operators';

@Component({
  selector: 'app-account-tanks',
  templateUrl: './account-tanks.component.html',
  styleUrls: ['./account-tanks.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AccountTanksComponent implements OnInit, OnDestroy {
  public filter: AccountTanksFilter;
  public filter$: BehaviorSubject<AccountTanksFilter>;
  public showGarageinfo$ = new BehaviorSubject<boolean>(false);
  public error$ = new BehaviorSubject<string>(null);

  public tanks$: Observable<TankStatisticDto[]>;

  constructor(
    private accountTanksService: AccountTanksService,
    private store: Store<State>,
    private actions$: Actions,
    datePipe: DatePipe
  ) {
    this.filter = new AccountTanksFilter(datePipe);
    this.filter$ = new BehaviorSubject<AccountTanksFilter>(this.filter);

    const accountIdChanged$ = merge(
      this.store.pipe(
        select(getAccountId),
      ),
      this.actions$.pipe(
        ofType(AppActionTypes.AccountInfoRefreshed),
        map((action: AccountInfoRefreshed) => action.payload)
      )
    );

    this.tanks$ = combineLatest(
      accountIdChanged$.pipe(
        tap(accountId => {
          this.showGarageinfo$.next(accountId.accountLoggedIn);
        })
      ),
      this.filter$
    ).pipe(
      mergeMap(([accountId, filter]) =>
        this.accountTanksService.getTanksDataByQuery(filter.getFilterQuery(accountId))
          .pipe(
            catchError(err => {
              this.error$.next(err);
              return of([]);
            })
          )
      )
    );
  }

  applyFilter() {
    this.filter$.next(this.filter);
  }
  ngOnInit() {}
  ngOnDestroy(): void {
  }
}
