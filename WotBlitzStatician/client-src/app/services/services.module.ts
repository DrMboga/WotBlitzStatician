import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountInfoService } from './account-info.service';
import { AccountAuthenticationService } from './account-authentication.service';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [],
  providers: [
    AccountInfoService,
    {
      provide: 'BASE_URL', useFactory: getBaseUrl
    },
    AccountAuthenticationService
  ]
})
export class ServicesModule { }

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

