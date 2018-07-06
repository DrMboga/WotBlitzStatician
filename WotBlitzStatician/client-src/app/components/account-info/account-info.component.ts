import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {
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
