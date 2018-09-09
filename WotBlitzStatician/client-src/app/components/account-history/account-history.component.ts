import { Component, OnInit, Input } from '@angular/core';

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

  constructor(private accountsInfoService: AccountInfoService) {
  }

  ngOnInit() {
      let now = new Date();
      now.setMonth(now.getMonth() - 1);
      this.dateFrom = new Date(now.getFullYear(), now.getMonth(), now.getDate());
  }

  loadHistory() {
    this.accountsInfoService.getAccountStatHistory(this.accountId, this.dateFrom).subscribe(data => {
      this.accountHistory = data as any[];
    }, error => console.error(error));
  }
}
