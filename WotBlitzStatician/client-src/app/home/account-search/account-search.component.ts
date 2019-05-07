import { Component, OnInit } from '@angular/core';
import { AccountInfo } from '../../model/account-info';
import { BlitzStaticianService } from '../../shared/services/blitz-statician.service';
import { take } from 'rxjs/operators';
import { Store } from '@ngrx/store';
import { State } from '../../state/app.state';
import { GuestAccountSelected } from '../../state/app.actions';

@Component({
  selector: 'app-account-search',
  templateUrl: './account-search.component.html',
  styleUrls: ['./account-search.component.css']
})
export class AccountSearchComponent implements OnInit {
  public searchString: string;
  public foundAccounts: AccountInfo[];

  constructor(private store: Store<State>,
    private blitzStatician: BlitzStaticianService) { }

  ngOnInit() {
  }

  public searchAccounts() {
    if (this.searchString.length < 4) {
      return;
    }

    this.blitzStatician.findAccounts(this.searchString)
      .pipe(take(1))
      .subscribe(
      data => {
        this.foundAccounts = data;
      },
      error => console.error(error)
    );

  }

  public selectAccount(account: AccountInfo) {
    this.searchString = '';
    this.store.dispatch<GuestAccountSelected>(new GuestAccountSelected({ accountId: account.accountId, accountNick: account.nickName }));
  }

}
