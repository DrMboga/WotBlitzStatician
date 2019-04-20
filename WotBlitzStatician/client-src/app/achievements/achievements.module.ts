import { NgModule } from '@angular/core';
import { AccountAchievementsComponent } from './account-achievements/account-achievements.component';
import { TankCardComponent } from './tank-card/tank-card.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedComponentModule } from '../shared/shared-component.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SharedComponentModule
],
  exports: [],
  declarations: [
    AccountAchievementsComponent,
    TankCardComponent,
  ],
  providers: [],
})
export class AchievementsModule { }
