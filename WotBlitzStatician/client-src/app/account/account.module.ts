import { NgModule } from '@angular/core';
import { AccountsService } from './account.service';
import { AccountInfoComponent } from './account-info/account-info.component';
import { AccountAggregatedInfoComponent } from './account-aggregated-info/account-aggreagated-info.component';
import { AccountAggregatedInfoService } from './account-aggregated-info/account-aggreagated-info.service';
import { SharedComponentModule } from '../shared/shared-component.module';
import { ChartRowComponent } from './account-aggregated-info/chartRow/chart-row.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,

    SharedComponentModule
  ],
  exports: [
  ],
  declarations: [
    AccountInfoComponent,
    AccountAggregatedInfoComponent,
    ChartRowComponent
  ],
  providers: [
    AccountsService,
    AccountAggregatedInfoService,
  ],
})
export class AccountModule { }
