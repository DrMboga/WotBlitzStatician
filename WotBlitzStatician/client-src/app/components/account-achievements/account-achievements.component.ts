import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'account-achievements',
  templateUrl: './account-achievements.component.html',
  styleUrls: ['./account-achievements.component.css']
})
export class AccountAchievementsComponent implements OnInit {

  @Input("achievements")
  public achievements: any[];

  public battleAchievements: any[];
  public epicAchievements: any[];
  public platoonAchievements: any[];
  public titleAchievements: any[];
  public commemorativeAchievements: any[];
  public stepAchievements: any[];


  constructor() { }

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
