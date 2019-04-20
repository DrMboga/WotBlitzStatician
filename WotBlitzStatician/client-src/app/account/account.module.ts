import { NgModule } from '@angular/core';
import { AccountsService } from './account.service';
import { AccountInfoComponent } from './account-info/account-info.component';
import { AccountAggregatedInfoComponent } from './account-aggregated-info/account-aggreagated-info.component';
import { AccountAggregatedInfoService } from './account-aggregated-info/account-aggreagated-info.service';
import { SharedComponentModule } from '../shared/shared-component.module';
import { ChartRowComponent } from './account-aggregated-info/chartRow/chart-row.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AccountRoutesModule } from './account.routes';
import { AccountGlobalInfo } from '../shared/account-global-info';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    AccountRoutesModule,
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
    {provide: AccountGlobalInfo, useValue: new AccountGlobalInfo(90277267, 'DummyAccount') }, // ToDo: Change dummy data later

    AccountsService,
    AccountAggregatedInfoService,
  ],
})
export class AccountModule { }
