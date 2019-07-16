import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { SplashScreenComponent } from './home/splash-screen/splash-screen.component';

const routes: Routes = [
  { path: '', redirectTo: 'account', pathMatch: 'full' },
  {
    path: 'account',
    loadChildren: () => import('./account/account.module').then(m => m.AccountModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'tanks',
    loadChildren: () => import('./tanks/tanks.module').then(m => m.TanksModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'account-achievements',
    loadChildren: () => import('./achievements/achievements.module').then(m => m.AchievementsModule),
    canActivate: [AuthGuard]
  },
  {
    path: 'account-history',
    loadChildren: () => import('./history/history.module').then(m => m.HistoryModule),
    canActivate: [AuthGuard]
  },
  { path: 'splash-screen', component: SplashScreenComponent },
  { path: '**', redirectTo: 'account' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
