import { Component, OnInit, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {
  title = 'app';
  public accounts: any;

  constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
    http.get(baseUrl + 'api/AccountInfo').subscribe(result => {
      this.accounts = result.json();
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
