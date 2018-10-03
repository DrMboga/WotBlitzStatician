import { Component, OnInit, Input } from '@angular/core';
import { AccountInfoService } from '../../services/account-info.service';

@Component({
  selector: 'account-achievements',
  templateUrl: './account-achievements.component.html',
  styleUrls: ['./account-achievements.component.css']
})
export class AccountAchievementsComponent implements OnInit {

  @Input("achievements")
  public achievements: any[];
  @Input("accountId")
  public accountId: number;

  public battleAchievements: any[];
  public epicAchievements: any[];
  public platoonAchievements: any[];
  public titleAchievements: any[];
  public commemorativeAchievements: any[];
  public stepAchievements: any[];

  constructor(private accountsInfoService: AccountInfoService) { }

  ngOnInit() {
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
  }
}
