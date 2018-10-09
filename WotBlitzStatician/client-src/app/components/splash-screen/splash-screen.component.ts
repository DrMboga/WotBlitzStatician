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

  public showButtons: boolean = false;
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
            this.showButtons=false;
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
        this.showButtons = true;
      },
      error => {
        // ToDo: check 404 error and make post instead of put
        console.error(error);
        this.showButtons = true;
      });
    }
    else{
      this.showButtons = true;
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
    let accountInfo: AccountInfo = {
      accountId: this.accountGlobalInfo.accountId,
      nickName: this.accountGlobalInfo.accountNick,
      lastBattleTime: null,
      accountCreatedAt: null,
      accessToken: this.wgAuthResponse.access_token,
      accessTokenExpiration: new Date(+this.wgAuthResponse.expires_at * 1000)
    };

    // ToDo: Analyze whether this is new account or existing. And make post or put
    this.accountsInfoService.putNewAccountInfo(accountInfo)
          .subscribe(()=>{}, error => console.error(error));

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
