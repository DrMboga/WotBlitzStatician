import { Component, OnInit, Input } from '@angular/core';
import { AccountInfoService } from '../../services/account-info.service';
import { AccountGlobalInfo } from '../account-global-info';

@Component({
  selector: 'account-achievements',
  templateUrl: './account-achievements.component.html',
  styleUrls: ['./account-achievements.component.css']
})
export class AccountAchievementsComponent implements OnInit {

  public achievements: any[];

  public battleAchievements: any[];
  public epicAchievements: any[];
  public platoonAchievements: any[];
  public titleAchievements: any[];
  public commemorativeAchievements: any[];
  public stepAchievements: any[];

  constructor(private accountsInfoService: AccountInfoService,
    public accountGlobalInfo: AccountGlobalInfo) { 
      this.accountsInfoService.getAccountAchievements(this.accountGlobalInfo.accountId).subscribe(
        data => {
          this.achievements = data as any[];
          this.battleAchievements = this.achievements.filter(
            achievement => achievement.section === 'battle');
          this.epicAchievements = this.achievements.filter(
            achievement => achievement.section === 'epic');
          this.platoonAchievements = this.achievements.filter(
            achievement => achievement.section === 'platoon');
          this.titleAchievements = this.achievements.filter(
            achievement => achievement.section === 'title');
          this.commemorativeAchievements = this.achievements.filter(
            achievement => achievement.section === 'commemorative');
          this.stepAchievements = this.achievements.filter(
            achievement => achievement.section === 'step');
      
        }, error => console.error(error));
    }

  ngOnInit() {
  }
}
