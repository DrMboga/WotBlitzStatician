import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { CookieService } from 'ngx-cookie-service';

import { AppComponent } from './app/app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AccountInfoComponent } from './account-info/account-info.component';
import { AccountTanksComponent } from './account-tanks/account-tanks.component';
import { AccountAchievementsComponent } from './account-achievements/account-achievements.component';
import { BlitzColorScaleDirective } from './pipes/blitz-color-scale.directive';
import { AccountHistoryComponent } from './account-history/account-history.component';
import { TankCardComponent } from './tank-card/tank-card.component';
import { AccountGlobalInfo } from './account-global-info';
import { AuthGuard } from './auth.guard';
import { SplashScreenComponent } from './splash-screen/splash-screen.component';
import { IconsModule } from './icons.module';
import { RomanNumberPipe } from './pipes/roman-number.pipe';
import { AccountHistoryChartService } from './account-history/account-history-chart.service';
import { AccountAggregatedInfoComponent } from './account-aggregated-info/account-aggreagated-info.component';
import { AccountAggregatedInfoService } from './account-aggregated-info/account-aggreagated-info.service';

@NgModule({
  imports: [
    FormsModule,
    BrowserModule,
    CommonModule,
    IconsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'account', pathMatch: 'full' },
      { path: 'account', component: AccountInfoComponent, canActivate: [AuthGuard] },
      { path: 'tanks', component: AccountTanksComponent, canActivate: [AuthGuard] },
      { path: 'account-achievements', component: AccountAchievementsComponent, canActivate: [AuthGuard] },
      { path: 'account-history', component: AccountHistoryComponent, canActivate: [AuthGuard] },
      { path: 'splash-screen', component: SplashScreenComponent },
      { path: '**', redirectTo: 'account' }
    ])
  ],
  declarations: [
    AppComponent,
    NavMenuComponent,
    AccountInfoComponent,
    AccountTanksComponent,
    AccountAchievementsComponent,
    BlitzColorScaleDirective,
    AccountHistoryComponent,
    TankCardComponent,
    SplashScreenComponent,
    RomanNumberPipe,
    AccountAggregatedInfoComponent
  ],
  exports: [
    AppComponent,
    NavMenuComponent
  ],
  providers: [
    {provide: AccountGlobalInfo, useValue: new AccountGlobalInfo(0, 'WotBlitzStatician') },
    AuthGuard,
    CookieService,
    AccountHistoryChartService,
    AccountAggregatedInfoService
  ]
})
export class ComponentsModule { }
