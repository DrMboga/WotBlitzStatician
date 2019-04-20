import { NgModule } from '@angular/core';
import { AccountTanksComponent } from './account-tanks/account-tanks.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedComponentModule } from '../shared/shared-component.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SharedComponentModule
  ],
  exports: [],
  declarations: [
    AccountTanksComponent
  ],
  providers: [],
})
export class TanksModule { }
