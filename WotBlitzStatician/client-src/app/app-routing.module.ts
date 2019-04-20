import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { SplashScreenComponent } from './home/splash-screen/splash-screen.component';

const routes: Routes = [
  { path: '', redirectTo: 'account', pathMatch: 'full' },
  { path: 'account', loadChildren: './account/account.module#AccountModule', canActivate: [AuthGuard] },
  { path: 'tanks', loadChildren: './tanks/tanks.module#TanksModule', canActivate: [AuthGuard] },
  { path: 'account-achievements', loadChildren: './achievements/achievements.module#AchievementsModule', canActivate: [AuthGuard] },
  { path: 'account-history', loadChildren: './history/history.module#HistoryModule', canActivate: [AuthGuard] },
  { path: 'splash-screen', component: SplashScreenComponent },
  { path: '**', redirectTo: 'account' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
