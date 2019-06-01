import { Component, Input, Output, EventEmitter } from '@angular/core';
import { AccountAchievementDto } from '../../model/account-achievement-dto';

@Component({
  selector: 'app-account-achievements-section',
  templateUrl: 'account-achievements-section.component.html'
})

export class AccountAcievementSectionComponent {
  @Input() public achievements: AccountAchievementDto[];

  constructor() { }

  @Output() selected = new EventEmitter<AccountAchievementDto>();

  achievementSelected(achievement: AccountAchievementDto): void {
    this.selected.emit(achievement);
  }

}
