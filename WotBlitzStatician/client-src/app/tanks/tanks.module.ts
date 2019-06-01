import { NgModule } from '@angular/core';
import { AccountTanksComponent } from './account-tanks/account-tanks.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedComponentModule } from '../shared/shared-component.module';
import { TankRoutesModule } from './tanks.routes';
import { TanksTableComponent } from './account-tanks/tanks-table/tanks-table.component';
import { AccountTanksService } from './account-tanks/account-tanks.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    TankRoutesModule,
    SharedComponentModule
  ],
  exports: [],
  declarations: [
    AccountTanksComponent,
    TanksTableComponent
  ],
  providers: [
    AccountTanksService
  ],
})
export class TanksModule { }
