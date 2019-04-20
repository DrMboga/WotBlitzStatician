import { Component, OnInit } from '@angular/core';
import { AccountInfo } from '../../model/account-info';
import { AccountGlobalInfo } from '../../shared/account-global-info';
import { BlitzStaticianService } from '../../shared/services/blitz-statician.service';

@Component({
  selector: 'app-account-search',
  templateUrl: './account-search.component.html',
  styleUrls: ['./account-search.component.css']
})
export class AccountSearchComponent implements OnInit {
  public searchString: string;
  public foundAccounts: AccountInfo[];

  constructor(private blitzStatician: BlitzStaticianService,
              private accountGlobalInfo: AccountGlobalInfo) { }

  ngOnInit() {
  }

  public searchAccounts() {
    if (this.searchString.length < 4) {
      return;
    }

    this.blitzStatician.findAccounts(this.searchString).subscribe(
      data => {
        this.foundAccounts = data;
      },
      error => console.error(error)
    );

  }

  public selectAccount(account: AccountInfo) {
    this.searchString = '';
    this.blitzStatician.putGuestAccountToCache(account.accountId).subscribe(() => {
      this.accountGlobalInfo.isGuestAccount = true;
      this.accountGlobalInfo.accountId = account.accountId;
      this.accountGlobalInfo.accountNick = account.nickName;
      // ToDo: logoff
      // ToDo: hide private info
      // ToDo: getting rid of square brackets and clan info if there is no clan
      this.accountGlobalInfo.EmitAccountInfoChanged();
    });
  }

}
