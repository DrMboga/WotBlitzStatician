import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { AccountInfoService } from '../../shared/services/account-info.service';
import { AccountGlobalInfo } from '../../shared/account-global-info';
import { TankByAchievementDto } from '../../model/tank-by-achievement-dto';
import { AccountAchievementDto } from '../../model/account-achievement-dto';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-account-achievements',
  templateUrl: './account-achievements.component.html',
  styleUrls: ['./account-achievements.component.css']
})
export class AccountAchievementsComponent implements OnInit, OnDestroy {
  public achievements: AccountAchievementDto[];

  public battleAchievements: AccountAchievementDto[];
  public epicAchievements: AccountAchievementDto[];
  public platoonAchievements: AccountAchievementDto[];
  public titleAchievements: AccountAchievementDto[];
  public commemorativeAchievements: AccountAchievementDto[];
  public stepAchievements: AccountAchievementDto[];

  public clickedAchievement: AccountAchievementDto;
  public tanksByAchievement: TankByAchievementDto[];

  subscription: Subscription;

  constructor(
    private accountsInfoService: AccountInfoService,
    public accountGlobalInfo: AccountGlobalInfo
  ) {
    this.RefreshAchievements();
    this.subscription = accountGlobalInfo.accountInfoChanged
      .asObservable()
      .subscribe(() => this.RefreshAchievements());
  }

  private RefreshAchievements() {
    this.accountsInfoService
      .getAccountAchievements(this.accountGlobalInfo.accountId)
      .subscribe(
        data => {
          this.achievements = data;
          this.battleAchievements = this.achievements.filter(
            achievement => achievement.section === 'battle'
          );
          this.epicAchievements = this.achievements.filter(
            achievement => achievement.section === 'epic'
          );
          this.platoonAchievements = this.achievements.filter(
            achievement => achievement.section === 'platoon'
          );
          this.titleAchievements = this.achievements.filter(
            achievement => achievement.section === 'title'
          );
          this.commemorativeAchievements = this.achievements.filter(
            achievement => achievement.section === 'commemorative'
          );
          this.stepAchievements = this.achievements.filter(
            achievement => achievement.section === 'step'
          );
        },
        error => console.error(error)
      );
  }

  ngOnInit() {}

  getTanksByAchievement() {
    if (
      this.clickedAchievement.isAchievementOption ||
      (this.clickedAchievement.section === 'battle' &&
        this.clickedAchievement.achievementId.includes('Mastery'))
    ) {
      this.tanksByAchievement = null;
      return;
    }
    this.accountsInfoService
      .getTanksByAchievement(
        this.accountGlobalInfo.accountId,
        this.clickedAchievement.achievementId
      )
      .subscribe(
        data => {
          this.tanksByAchievement = data;
          this.tanksByAchievement.sort(
            (left, right): number => {
              if (left.achievementsCount < right.achievementsCount) {
                return 11;
              }
              if (left.achievementsCount > right.achievementsCount) {
                return -1;
              }
              return 0;
            }
          );
        },
        error => console.error(error)
      );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
