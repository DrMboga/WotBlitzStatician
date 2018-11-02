import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";

import { AccountInfoService } from '../../services/account-info.service';
import { AccountGlobalInfo } from '../account-global-info';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.css']
})
export class AccountInfoComponent implements OnInit {
  public account: any;
  public achievements: any[];

  constructor(private accountsInfoService: AccountInfoService,
    public accountGlobalInfo: AccountGlobalInfo) {
    let id = accountGlobalInfo.accountId;
    // if (id != null) {
    //   this.accountsInfoService.getAccount(id).subscribe(data => {
    //     this.account = data;
    //     this.achievements = this.account.achievements;
    //   }, error => console.error(error));
    // }
  }

  ngOnInit() {
    this.account = JSON.parse(this.accountString);
    this.achievements = this.account.achievements;
  }

  //-------//
  private accountString = '{"accountId":90277267,"nickName":"Mboga","accountCreatedAt":"2018-01-20T11:17:35","lastBattleTime":"2018-10-19T19:25:21","playerClanInfo":{"clanId":40493,"playerJoinedAt":"2018-02-20T19:52:55","playerRole":"Заместитель командующего","clanTag":"XXX_L","clanName":"Crowd of friends","clanMotto":"Всем улыбок и побед","clanDescription":"Клан для общения и игры без обязательств"},"playerStatistics":{"battles":4972,"updatedAt":"2018-10-21T10:10:08","wins":2774,"avgTier":6.5844730490748189,"wn7":1376.1238458706337,"frags":4242,"frags8P":1051,"maxFrags":6,"maxFragsTankInfo":"ИС-5 (Объект 730)","maxXp":2013,"maxXpTankInfo":"Т-43","winRate":0.5579243765084473049074818986,"avgDamage":1139.9115044247787610619469027,"avgXp":596.64199517296862429605792438,"survivalRate":0.3433226065969428801287208367,"capturePoints":1952,"damageDealt":5667640,"damageReceived":4789348,"droppedCapturePoints":4200,"hits":34979,"losses":2178,"maxFragsTankId":58881,"maxXpTankId":6657,"shots":41343,"spotted":6781,"survivedBattles":1707,"winAndSurvived":1689,"xp":2966504,"battleLifeTimeInSeconds":773133,"battleLifeTyme":"8.22:45:33","credits":1637205,"freeXp":55145,"gold":1333,"isPremium":false,"premiumExpiresAt":"2018-10-20T19:26:04"},"accountMasteryInfo":[{"markOfMastery":1,"tanksCount":1,"allTanksCount":81,"masteryTanksRatio":0.012345679012345679012345679},{"markOfMastery":2,"tanksCount":19,"allTanksCount":81,"masteryTanksRatio":0.2345679012345679012345679012},{"markOfMastery":3,"tanksCount":30,"allTanksCount":81,"masteryTanksRatio":0.3703703703703703703703703704},{"markOfMastery":4,"tanksCount":31,"allTanksCount":81,"masteryTanksRatio":0.3827160493827160493827160494}],"achievements":[]}';
  //-------//

}
