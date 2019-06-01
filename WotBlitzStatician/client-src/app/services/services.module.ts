import { NgModule } from '@angular/core';
// import { CommonModule } from '@angular/common';

// import { AccountInfoService } from '../shared/services/account-info.service';
// import { AccountAuthenticationService } from '../shared/services/account-authentication.service';

import { environment } from '../../environments/environment';
// import { AccountsService } from '../account-info/accounts.service';
// import { BlitzStaticianService } from '../shared/services/blitz-statician.service';

// ToDo: Obsolete
@NgModule({
  imports: [
  ],
  declarations: [],
  providers: [
    // AccountInfoService,
    // {
    //   provide: 'BASE_URL', useFactory: getBaseUrl
    // },
    // AccountAuthenticationService,
    // AccountsService,
    // BlitzStaticianService
  ]
})
export class ServicesModule { }

export function getBaseUrl() {
  // ng serve --configuration=debug
  if(environment.debug){
    return environment.debugApiUrl;
  }

  return document.getElementsByTagName('base')[0].href;
}

