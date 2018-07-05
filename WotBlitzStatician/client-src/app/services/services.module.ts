import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountInfoService } from './account-info.service';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [],
  providers: [
    AccountInfoService,
    {
      provide: 'BASE_URL', useFactory: getBaseUrl
    }
  ]
})
export class ServicesModule { }

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}

