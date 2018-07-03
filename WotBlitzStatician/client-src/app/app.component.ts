import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  public accounts: any;

  constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
    http.get(baseUrl + 'api/AccountInfo').subscribe(result => {
      this.accounts = result.json();
    }, error => console.error(error));
  }
}
