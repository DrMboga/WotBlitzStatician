import { Component, OnInit } from '@angular/core';
import { AccountInfo } from '../../model/account-info';
import { AccountInfoService } from '../../services/account-info.service';

@Component({
  selector: 'app-account-search',
  templateUrl: './account-search.component.html',
  styleUrls: ['./account-search.component.css']
})
export class AccountSearchComponent implements OnInit {
  public searchString: string;
  public foundAccounts: AccountInfo[];

  constructor(private accountsInfoService: AccountInfoService) { }

  ngOnInit() {
  }

  public searchAccounts() {
    if (this.searchString.length < 4) {
      return;
    }

    this.accountsInfoService.findAccounts(this.searchString).subscribe(
      data => {
        this.foundAccounts = data;
      },
      error => console.error(error)
    );
    // this.searchString = '';
  }

  public selectAccount(accountId: number) {
    console.log('Selected', accountId);
  }

}
