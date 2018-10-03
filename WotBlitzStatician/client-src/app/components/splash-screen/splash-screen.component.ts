import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountGlobalInfo } from '../account-global-info';
import { CookieService } from 'ngx-cookie-service';
import { AccountInfoService } from '../../services/account-info.service';

@Component({
  selector: 'splash-screen',
  templateUrl: './splash-screen.component.html',
  styleUrls: ['./splash-screen.component.css']
})
export class SplashScreenComponent implements OnInit {
  private readonly accountIdCookieName: string = 'AccountId';

  constructor(private router: Router,
    private accountGlobalInfo: AccountGlobalInfo,
    private cookieService: CookieService,
    private accountsInfoService: AccountInfoService) { 

      let accountIdFromCookie = this.getAccountIdFromCookie();
      if(accountIdFromCookie > 0){
        
        this.accountsInfoService.getShortAccountInfo(accountIdFromCookie)
          .subscribe(a => {
            // ToDo: check if accountId valid (method get account info by Id, then check if token is not expired)
            this.accountGlobalInfo.accountId = a.accountId;
            this.accountGlobalInfo.accountNick = a.nickName;  
            this.router.navigate(['/']);
          });
      }

      // ToDo: if account id is not in the cookie or token expired - redirect to wg auth
      // then save the auth data to db - method post by AccountInfo entity
      // then save new account ud in the cookie: // this.cookieService.set(this.accountIdCookieName, '46512100');

    }

  ngOnInit() {
  }

  private getAccountIdFromCookie() : number {
    if(this.cookieService.check(this.accountIdCookieName)) {
      return +this.cookieService.get(this.accountIdCookieName);
    }
    return 0;
  }

}
