import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";


@Component({
  selector: 'app-account-tanks',
  templateUrl: './account-tanks.component.html',
  styleUrls: ['./account-tanks.component.css']
})
export class AccountTanksComponent implements OnInit {
  public accountId: number = 0;

  constructor(activeRoute: ActivatedRoute) {
    let id = activeRoute.snapshot.params["accountId"];
    if (id != null) {
      this.accountId = id;
    }
  }

  ngOnInit() {
  }

}
