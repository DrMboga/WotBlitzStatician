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

      // ToDo: parse query parameters if any - redirect from wg auth

      let accountIdFromCookie = this.getAccountIdFromCookie();
      if(accountIdFromCookie > 0){
        this.accountsInfoService.getShortAccountInfo(accountIdFromCookie)
          .subscribe(a => {
            if(a != null){
              let now = new Date();
              let tokenExpiration = new Date(a.accessTokenExpiration);
              if(now.getTime() <= tokenExpiration.getTime()){
                this.accountGlobalInfo.accountId = a.accountId;
                this.accountGlobalInfo.accountNick = a.nickName;  
                this.router.navigate(['/']);
              }
          }
            this.redirectToWargamingLogin();
          });
      }
      this.redirectToWargamingLogin();
    }

  ngOnInit() {
  }

  private getAccountIdFromCookie() : number {
    if(this.cookieService.check(this.accountIdCookieName)) {
      return +this.cookieService.get(this.accountIdCookieName);
    }
    return 0;
  }

  private redirectToWargamingLogin(){
    // ToDo: get wg auth url from service, plus redirect url
  }

}
