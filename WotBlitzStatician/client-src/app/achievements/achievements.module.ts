import { NgModule } from '@angular/core';
import { AccountAchievementsComponent } from './account-achievements/account-achievements.component';
import { TankCardComponent } from './tank-card/tank-card.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SharedComponentModule } from '../shared/shared-component.module';
import { AchievementsRoutesModule } from './achievements.routes';
import { AccountAchievementsService } from './account-achievements.service';
import { AccountAcievementSectionComponent } from './account-achievements-section/account-achievements-section.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    AchievementsRoutesModule,
    SharedComponentModule
],
  exports: [],
  declarations: [
    AccountAchievementsComponent,
    TankCardComponent,
    AccountAcievementSectionComponent
  ],
  providers: [
    AccountAchievementsService
  ],
})
export class AchievementsModule { }
