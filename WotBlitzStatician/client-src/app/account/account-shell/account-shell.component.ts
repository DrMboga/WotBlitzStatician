import { Component, OnInit, ChangeDetectionStrategy, OnDestroy } from '@angular/core';
import { Observable } from 'rxjs';
import { takeWhile } from 'rxjs/operators';
import { Store, select } from '@ngrx/store';

import { AccountInfoDto } from '../../model/account-info-dto';
import { LoadAccountInfo } from '../state/account.actions';
import { PlayerPrivateInfo } from '../../model/player-private-info';
import { State } from '../state/account.selectors';
import { getAccountId } from '../../state/app.selectors';
import { getAccountInfo, getPrivateInfo, getAccountInoError } from '../state/account.selectors';

@Component({
  selector: 'app-account-shell',
  templateUrl: 'account-shell.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class AccountInfoShellComponent implements OnInit, OnDestroy {
  public account$: Observable<AccountInfoDto>;
  public privateInfo$: Observable<PlayerPrivateInfo>;
  public errorMessage$: Observable<string>;

  componentActive = true;

  constructor(private store: Store<State>) { }

  ngOnInit() {
    this.store.pipe(
      select(getAccountId),
      takeWhile(() => this.componentActive)
      )
      .subscribe(accountId => this.store.dispatch<LoadAccountInfo>(new LoadAccountInfo(accountId)));

      this.account$ = this.store.pipe(select(getAccountInfo));
      this.privateInfo$ = this.store.pipe(select(getPrivateInfo));
      this.errorMessage$ = this.store.pipe(select(getAccountInoError));
      // Aggregated info
  }

  ngOnDestroy(): void {
    this.componentActive = false;
  }

}
