import { NgModule } from '@angular/core';
import { AccountAchievementsComponent } from './account-achievements/account-achievements.component';
import { RouterModule } from '@angular/router';

export const routes = [
  {path: '', component: AccountAchievementsComponent}
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  declarations: [],
  providers: [],
})
export class AchievementsRoutesModule { }
