import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AccountInfoShellComponent } from './account-shell/account-shell.component';

export const routes = [
  {path: '', component: AccountInfoShellComponent}
  // {path: '', component: AccountInfoComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  declarations: [],
  providers: [],
})
export class AccountRoutesModule { }
