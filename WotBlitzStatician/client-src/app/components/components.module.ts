import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';


import { AppComponent } from './app/app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { AccountInfoComponent } from './account-info/account-info.component';
import { AccountTanksComponent } from './account-tanks/account-tanks.component';
import { AccountAchievementsComponent } from './account-achievements/account-achievements.component';
import { BlitzColorScaleDirective } from './blitz-color-scale.directive';
import { AccountHistoryComponent } from './account-history/account-history.component';
import { TankCardComponent } from './tank-card/tank-card.component';


@NgModule({
  imports: [
    FormsModule,
    BrowserModule,
    CommonModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'account', pathMatch: 'full' },
      { path: 'account', component: AccountInfoComponent },
      { path: 'account/:accountId', component: AccountInfoComponent },
      { path: 'tanks/:accountId', component: AccountTanksComponent },
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
    TankCardComponent
  ],
  exports: [
    AppComponent,
    NavMenuComponent
  ]
})
export class ComponentsModule { }
