import { NgModule } from '@angular/core';
import { AccountInfoComponent } from './account-info/account-info.component';
import { RouterModule } from '@angular/router';

export const routes = [
  {path: '', component: AccountInfoComponent}
  // {path: '', component: AccountInfoComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  declarations: [],
  providers: [],
})
export class AccountRoutesModule { }
