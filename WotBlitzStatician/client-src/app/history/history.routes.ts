import { NgModule } from '@angular/core';
import { AccountHistoryComponent } from './account-history/account-history.component';
import { RouterModule } from '@angular/router';

export const routes = [
  {path: '', component: AccountHistoryComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  declarations: [],
  providers: [],
})
export class HistoryRoutesModule { }
