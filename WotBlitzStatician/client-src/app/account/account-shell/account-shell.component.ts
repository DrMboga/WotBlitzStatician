import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs';
import { takeWhile, map } from 'rxjs/operators';
import { Store, select } from '@ngrx/store';

import { AccountInfoDto } from '../../model/account-info-dto';
import { LoadAccountInfo, LoadAccountAggregatedInfo } from '../state/account.actions';
import { PlayerPrivateInfo } from '../../model/player-private-info';
import { State, getAccountAggregatedInfo, getAccountAggregatetInfoError } from '../state/account.selectors';
import { getAccountId } from '../../state/app.selectors';
import { getAccountInfo, getPrivateInfo, getAccountInoError } from '../state/account.selectors';
import { AccountTanksInfoAggregatedDto } from '../../model/account-tanks-info-aggregated-dto';
import { Actions, ofType } from '@ngrx/effects';
import { AppActionTypes, AccountInfoRefreshed } from '../../state/app.actions';

@Component({
  selector: 'app-account-shell',
  templateUrl: 'account-shell.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class AccountInfoShellComponent implements OnInit, OnDestroy {
  public account$: Observable<AccountInfoDto>;
  public privateInfo$: Observable<PlayerPrivateInfo>;
  public accountAggregatedInfo$: Observable<AccountTanksInfoAggregatedDto[]>;
  public errorMessage$: Observable<string>;
  public aggregatedInfoErrorMessage$: Observable<string>;

  componentActive = true;

  constructor(private store: Store<State>,
    private actions$: Actions) { }

  ngOnInit() {
    this.store.pipe(
      select(getAccountId),
      takeWhile(() => this.componentActive)
    )
      .subscribe(accountId => {
        if (accountId.accountId === 0) {
          return;
        }
        this.store.dispatch<LoadAccountInfo>(new LoadAccountInfo(accountId));
        this.store.dispatch<LoadAccountAggregatedInfo>(new LoadAccountAggregatedInfo(accountId));
      });

      this.actions$.pipe(
        ofType(AppActionTypes.AccountInfoRefreshed),
        map((action: AccountInfoRefreshed) => action.payload),
        takeWhile(() => this.componentActive)
      )
        .subscribe(accountId => {
          if (accountId.accountId === 0) {
            return;
          }
          this.store.dispatch<LoadAccountInfo>(new LoadAccountInfo(accountId));
          this.store.dispatch<LoadAccountAggregatedInfo>(new LoadAccountAggregatedInfo(accountId));
        });


    this.account$ = this.store.pipe(select(getAccountInfo));
    this.privateInfo$ = this.store.pipe(select(getPrivateInfo));
    this.errorMessage$ = this.store.pipe(select(getAccountInoError));
    this.accountAggregatedInfo$ = this.store.pipe(select(getAccountAggregatedInfo));
    this.aggregatedInfoErrorMessage$ = this.store.pipe(select(getAccountAggregatetInfoError));
  }

  ngOnDestroy(): void {
    this.componentActive = false;
  }

}
