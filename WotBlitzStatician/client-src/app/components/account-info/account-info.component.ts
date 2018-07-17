import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";

import { AccountInfoService } from '../../services/account-info.service';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {
  public account: any;
  public battleAchievements: any[];

  constructor(private accountsInfoService: AccountInfoService,
    activeRoute: ActivatedRoute) {
    let id = activeRoute.snapshot.params["accountId"];
    if (id != null) {
      this.accountsInfoService.getAccount(id).subscribe(data => {
        this.account = data;
        this.battleAchievements = this.account.achievements.filter(
          achievement => achievement.section === 'battle');
      }, error => console.error(error));
    }
  }

  ngOnInit() {
  }

}
