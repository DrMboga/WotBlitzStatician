import { Component, OnInit, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AccountGlobalInfo } from '../account-global-info';
import { CookieService } from 'ngx-cookie-service';
import { AccountInfoService } from '../../services/account-info.service';
import { AccountInfo } from '../../model/account-info';

@Component({
  selector: 'splash-screen',
  templateUrl: './splash-screen.component.html',
  styleUrls: ['./splash-screen.component.css']
})
export class SplashScreenComponent implements OnInit {
  private readonly accountIdCookieName: string = 'AccountId';

  public wgAuthResponse: any;

  constructor(private router: Router,
    private activeRoute: ActivatedRoute,
    private accountGlobalInfo: AccountGlobalInfo,
    private cookieService: CookieService,
    private accountsInfoService: AccountInfoService,
    @Inject('BASE_URL') private baseUrl: string) { 

      // Parse query parameters if any - redirect from wg auth
      activeRoute.queryParams.subscribe(
        params => {
          if (params.hasOwnProperty('status') && params.status === 'ok') {
            this.wgAuthResponse = params;
            this.saveAccountInfoAndEnter();
          }
          else{
            this.checkCookieAndWgLogin();
          }
        }, error => console.error('queryParams eror', error));
    }

  ngOnInit() {
  }

  private checkCookieAndWgLogin(){
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
        });
    }
  }

  private getAccountIdFromCookie() : number {
    if(this.cookieService.check(this.accountIdCookieName)) {
      return +this.cookieService.get(this.accountIdCookieName);
    }
    return 0;
  }

  private saveAccountInfoAndEnter() {
    this.accountGlobalInfo.accountId = this.wgAuthResponse.account_id;
    this.accountGlobalInfo.accountNick = this.wgAuthResponse.nickname;
    let accessToken: string = this.wgAuthResponse.access_token;
    let accessTokenExpiration: Date = new Date(+this.wgAuthResponse.expires_at * 1000);
    console.log('accessToken', accessToken);
    console.log('accessTokenExpiration', accessTokenExpiration);
    // ToDo: Make method to save AccountInfo

    // Saving new accountId to cookie
    this.cookieService.set(this.accountIdCookieName, this.accountGlobalInfo.accountId.toString())

    this.router.navigate(['/']);
  }

  private redirectToWargamingLogin(){
    this.accountsInfoService.getAuthenticationRequest(`${this.baseUrl}splash-screen`).subscribe(
      authRequest => {
        // Redirect to authRequest
        window.location.href = authRequest;
      });
    
  }

}
