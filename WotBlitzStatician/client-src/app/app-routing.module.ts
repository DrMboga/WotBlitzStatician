import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountInfoComponent } from './account/account-info/account-info.component';
import { AuthGuard } from './auth.guard';
import { AccountTanksComponent } from './tanks/account-tanks/account-tanks.component';
import { AccountAchievementsComponent } from './achievements/account-achievements/account-achievements.component';
import { AccountHistoryComponent } from './history/account-history/account-history.component';
import { SplashScreenComponent } from './home/splash-screen/splash-screen.component';

const routes: Routes = [
  { path: '', redirectTo: 'account', pathMatch: 'full' },
  { path: 'account', component: AccountInfoComponent, canActivate: [AuthGuard] },
  { path: 'tanks', component: AccountTanksComponent, canActivate: [AuthGuard] },
  { path: 'account-achievements', component: AccountAchievementsComponent, canActivate: [AuthGuard] },
  { path: 'account-history', component: AccountHistoryComponent, canActivate: [AuthGuard] },
  { path: 'splash-screen', component: SplashScreenComponent },
  { path: '**', redirectTo: 'account' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
