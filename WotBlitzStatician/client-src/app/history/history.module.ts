import { NgModule } from '@angular/core';
import { AccountHistoryComponent } from './account-history/account-history.component';
import { AccountHistoryChartService } from './account-history/account-history-chart.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedComponentModule } from '../shared/shared-component.module';
import { HistoryRoutesModule } from './history.routes';
import { HistoryService } from './history.service';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HistoryRoutesModule,
    SharedComponentModule
  ],
  exports: [],
  declarations: [
    AccountHistoryComponent
  ],
  providers: [
    HistoryService,
    AccountHistoryChartService
  ],
})
export class HistoryModule { }
