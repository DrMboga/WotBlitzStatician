import { NgModule } from '@angular/core';
import { AccountHistoryComponent } from './account-history/account-history.component';
import { AccountHistoryChartService } from './account-history/account-history-chart.service';
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
    AccountHistoryComponent
  ],
  providers: [
    AccountHistoryChartService
  ],
})
export class HistoryModule { }
