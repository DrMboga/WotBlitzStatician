import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AccountTanksComponent } from './account-tanks/account-tanks.component';

export const routes = [
  {path: '', component: AccountTanksComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  declarations: [],
  providers: [],
})
export class TankRoutesModule { }
