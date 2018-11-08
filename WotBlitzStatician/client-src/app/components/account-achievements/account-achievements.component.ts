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
      // this.accountsInfoService.getAccountAchievements(this.accountGlobalInfo.accountId).subscribe(
      //   data => {
      //     this.achievements = data as any[];
      //     this.battleAchievements = this.achievements.filter(
      //       achievement => achievement.section === 'battle');
      //     this.epicAchievements = this.achievements.filter(
      //       achievement => achievement.section === 'epic');
      //     this.platoonAchievements = this.achievements.filter(
      //       achievement => achievement.section === 'platoon');
      //     this.titleAchievements = this.achievements.filter(
      //       achievement => achievement.section === 'title');
      //     this.commemorativeAchievements = this.achievements.filter(
      //       achievement => achievement.section === 'commemorative');
      //     this.stepAchievements = this.achievements.filter(
      //       achievement => achievement.section === 'step');
      
      //   }, error => console.error(error));
    }

  ngOnInit() {
    this.achievements = JSON.parse(this.achievementsString) as any[];
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

  private achievementsString = `[{
    "achievementId": "warrior",
    "section": "battle",
    "sectionName": "«Герой битвы»",
    "order": 3,
    "name": "«Воин» (warrior)",
    "description": "Уничтожить наибольшее количество машин противника, но не менее 4.\\r• При равном счёте награждается игрок, получивший наибольшее количество очков опыта за бой.\\n• Выдаётся только одна награда за бой.",
    "count": 135,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/warrior.png",
    "localImage": "img/achievements/warrior.png",
    "isAchievementOption": false
}, {
    "achievementId": "mainGun",
    "section": "battle",
    "sectionName": "«Герой битвы»",
    "order": 4,
    "name": "«Основной калибр» (mainGun)",
    "description": "Нанести наибольшее количество урона за бой.\\r• Нанесённый урон должен составлять не менее 35% от суммарной прочности техники противника и не менее 1000 единиц.\\n• При равном количестве нанесённого урона награждается игрок, получивший наибольшее количество очков опыта за бой.\\n• Выдаётся только одна награда за бой.",
    "count": 162,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/mainGun.png",
    "localImage": "img/achievements/mainGun.png",
    "isAchievementOption": false
}, {
    "achievementId": "supporter",
    "section": "battle",
    "sectionName": "«Герой битвы»",
    "order": 5,
    "name": "«Поддержка» (supporter)",
    "description": "Нанести урон или сбить гусеницу наибольшему количеству машин противника, но не менее чем 4.\\r• Засчитываются цели, которые впоследствии были уничтожены другими игроками или самоуничтожились.\\n• Рикошеты и непробития не учитываются.\\n• При равном количестве повреждённой техники награждается игрок, получивший наибольшее количество очков опыта за бой.\\n• Выдаётся только одна награда за бой.",
    "count": 398,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/supporter.png",
    "localImage": "img/achievements/supporter.png",
    "isAchievementOption": false
}, {
    "achievementId": "camper",
    "section": "battle",
    "sectionName": "«Герой битвы»",
    "order": 6,
    "name": "«Танкист-снайпер» (camper)",
    "description": "Нанести наибольшее количество урона за бой с дистанции не менее 250 м.\\r• Произвести не менее 8 выстрелов.\\n• Не менее 80% попаданий должны быть с нанесением урона.\\n• Учитывается урон технике противника и повреждения модулей.\\n• Общая точность стрельбы за бой должна быть не менее 85%.\\n• Нанесённый урон должен превышать количество очков прочности машины игрока и быть не менее 1000 единиц.\\n• При равном количестве нанесённого урона награждается игрок, получивший наибольшее количество очков опыта за бой.\\n• Выдаётся только одна награда за бой.",
    "count": 1,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/camper.png",
    "localImage": "img/achievements/camper.png",
    "isAchievementOption": false
}, {
    "achievementId": "evileye",
    "section": "battle",
    "sectionName": "«Герой битвы»",
    "order": 200,
    "name": "«Дозорный» (evileye)",
    "description": "Предоставить разведданные, по которым союзники повредят не менее 3 машин противника.\\r• Засчитываются цели, разведданные по которым в момент их повреждения передавал только один игрок.\\n• При равном количестве повреждённой техники награждается игрок, получивший наибольшее количество очков опыта за бой.\\n• Выдаётся только одна награда за бой.",
    "count": 25,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/evileye.png",
    "localImage": "img/achievements/evileye.png",
    "isAchievementOption": false
}, {
    "achievementId": "steelwall",
    "section": "battle",
    "sectionName": "«Герой битвы»",
    "order": 201,
    "name": "«Стальная cтена» (steelwall)",
    "description": "Получить не менее 1 000 единиц потенциального урона и не менее 11 попаданий.\\r• Выжить в бою.\\n• При равном количестве полученного потенциального урона награждается игрок, получивший больше попаданий.\\n• При равном количестве полученного потенциального урона и попаданий награждается игрок, получивший наибольшее количество очков опыта за бой.\\n• Выдаётся только одна награда за бой.",
    "count": 70,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/steelwall.png",
    "localImage": "img/achievements/steelwall.png",
    "isAchievementOption": false
}, {
    "achievementId": "defender",
    "section": "battle",
    "sectionName": "«Герой битвы»",
    "order": 202,
    "name": "«Защитник» (defender)",
    "description": "Защитить базу, сбив наибольшее количество очков захвата, но не менее 70.\\r• При равном количестве сбитых очков награждается игрок, получивший наибольшее количество очков опыта за бой.\\n• Выдаётся только одна награда за бой.\\n• Не засчитываются очки захвата точек, полученные в режиме «Превосходство».",
    "count": 17,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/defender.png",
    "localImage": "img/achievements/defender.png",
    "isAchievementOption": false
}, {
    "achievementId": "invader",
    "section": "battle",
    "sectionName": "«Герой битвы»",
    "order": 203,
    "name": "«Захватчик» (invader)",
    "description": "Получить наибольшее количество очков захвата базы, но не менее 80.\\r• Выиграть бой захватом базы.\\n• Засчитываются только очки, обеспечившие захват.\\n• Не засчитываются очки захвата точек, полученные в режиме «Превосходство».",
    "count": 2,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/invader.png",
    "localImage": "img/achievements/invader.png",
    "isAchievementOption": false
}, {
    "achievementId": "scout",
    "section": "battle",
    "sectionName": "«Герой битвы»",
    "order": 204,
    "name": "«Разведчик» (scout)",
    "description": "Обнаружить наибольшее количество машин противника, но не менее 5.\\r• Победить в бою.\\n• Выдаётся только одна награда за бой.",
    "count": 51,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/scout.png",
    "localImage": "img/achievements/scout.png",
    "isAchievementOption": false
}, {
    "achievementId": "kamikaze",
    "section": "title",
    "sectionName": "Почётные звания",
    "order": 703,
    "name": "«Камикадзе» (kamikaze)",
    "description": "Уничтожить противника тараном.\\r• Машина противника должна быть выше уровнем.\\n• Не более одной награды на игрока за бой.",
    "count": 8,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/kamikaze.png",
    "localImage": "img/achievements/kamikaze.png",
    "isAchievementOption": false
}, {
    "achievementId": "handOfDeath",
    "section": "title",
    "sectionName": "Почётные звания",
    "order": 1002,
    "name": "«Коса Смерти» (handOfDeath)",
    "description": "Уничтожить подряд не менее 3 машин противника, затратив на вторую и последующие по одному снаряду.\\r• Серия ведётся для каждой единицы техники отдельно и может продолжиться в следующем бою на этой же машине.\\n• В общий зачёт идёт максимальная серия.\\n• Выдаётся в момент окончания очередной рекордной серии.",
    "count": 1,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/handOfDeath.png",
    "localImage": "img/achievements/handOfDeath.png",
    "isAchievementOption": false
}, {
    "achievementId": "beasthunter",
    "section": "title",
    "sectionName": "Почётные звания",
    "order": 1012,
    "name": "«Зверобой» (beasthunter)",
    "description": "Уничтожить 100 машин «семейства кошачьих».\\nЗасчитываются:\\r\\t• Tiger II\\n\\t• Panther\\n\\t• Panther II\\n\\t• Panther/M10\\n\\t• Panther 8,8\\n\\t• Jagdpanther\\n\\t• Jagdpanther II\\n\\t• Tiger (P)\\n\\t• Tiger I\\n\\t• Jagdtiger\\n\\t• Jagdtiger 8,8\\n\\t• Новогодний Jagdtiger 8,8\\n\\t• Leopard Prototyp A\\n\\t• Leopard 1\\n\\t• Löwe\\n\\t• Kuro Mori Mine\\n\\t• VK 16.02 Leopard\\n\\n• Засчитывается сумма результатов по всем машинам.\\n• В общем зачёте повторно полученные награды суммируются.",
    "count": 3,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/beasthunter.png",
    "localImage": "img/achievements/beasthunter.png",
    "isAchievementOption": false
}, {
    "achievementId": "sinai",
    "section": "title",
    "sectionName": "Почётные звания",
    "order": 1013,
    "name": "«Лев Синая» (sinai)",
    "description": "Уничтожить 100 танков ИС или машин на их базе.\\nЗасчитываются:\\r\\t• ИС\\n\\t• ИС-3\\n\\t• ИС-3 Защитник\\n\\t• ИС-4\\n\\t• ИС-6\\n\\t• ИС-6 Бесстрашный\\n\\t• ИС-7\\n\\t• ИС-8\\n\\t• ИСУ-152\\n\\t• Объект 263\\n\\t• Объект 268\\n\\t• Объект 704\\n\\t• ИСУ-122С\\n\\t• ИС-2 (1945)\\n\\t• ИС-2Ш\\n\\t• ИС-5\\n\\t• ИСУ-130\\n\\n• Засчитывается сумма результатов по всем машинам.\\n• В общем зачёте повторно полученные награды суммируются.",
    "count": 4,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/sinai.png",
    "localImage": "img/achievements/sinai.png",
    "isAchievementOption": false
}, {
    "achievementId": "armorPiercer",
    "section": "title",
    "sectionName": "Почётные звания",
    "order": 1014,
    "name": "«Бронебойщик» (armorPiercer)",
    "description": "Пробить броню противника не менее 10 раз подряд.\\r• Серия прерывается непробитием, рикошетом или промахом.\\n• Серия ведётся для каждой машины отдельно и может продолжиться в следующем бою на этой же машине.\\n• В общий зачёт идёт максимальная серия.",
    "count": 1,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/armorPiercer.png",
    "localImage": "img/achievements/armorPiercer.png",
    "isAchievementOption": false
}, {
    "achievementId": "titleSniper",
    "section": "title",
    "sectionName": "Почётные звания",
    "order": 1015,
    "name": "«Стрелок» (titleSniper)",
    "description": "Попасть в противника не менее 10 раз подряд.\\r• Серия прерывается промахом.\\n• Серия ведётся для каждой машины отдельно и может продолжиться в следующем бою на этой же машине.\\n• В общий зачёт идёт максимальная серия.",
    "count": 1,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/titleSniper.png",
    "localImage": "img/achievements/titleSniper.png",
    "isAchievementOption": false
}, {
    "achievementId": "tankExpert3",
    "section": "title",
    "sectionName": "Почётные звания",
    "order": 1303,
    "name": "«Эксперт: Китай» (tankExpert3)",
    "description": "Уничтожить всю технику из Дерева танков Китая, исключая премиум и наградные машины.",
    "count": 1,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/tankExpert3.png",
    "localImage": "img/achievements/tankExpert3.png",
    "isAchievementOption": false
}, {
    "achievementId": "tankExpert6",
    "section": "title",
    "sectionName": "Почётные звания",
    "order": 1306,
    "name": "«Эксперт: Япония» (tankExpert6)",
    "description": "Уничтожить всю технику из Дерева танков Японии, исключая премиум и наградные машины.",
    "count": 1,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/tankExpert6.png",
    "localImage": "img/achievements/tankExpert6.png",
    "isAchievementOption": false
}, {
    "achievementId": "medalLafayettePool",
    "section": "epic",
    "sectionName": "Эпические медали",
    "order": 101,
    "name": "Медаль Пула (medalLafayettePool)",
    "description": "Уничтожить 6 машин противника в одном бою.\\r• Управлять техникой V уровня и выше.",
    "count": 5,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalLafayettePool.png",
    "localImage": "img/achievements/medalLafayettePool.png",
    "isAchievementOption": false
}, {
    "achievementId": "medalRadleyWalters",
    "section": "epic",
    "sectionName": "Эпические медали",
    "order": 102,
    "name": "Медаль Рэдли-Уолтерса (medalRadleyWalters)",
    "description": "Уничтожить 5 машин противника в одном бою.\\r• Управлять техникой V уровня и выше.",
    "count": 15,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalRadleyWalters.png",
    "localImage": "img/achievements/medalRadleyWalters.png",
    "isAchievementOption": false
}, {
    "achievementId": "medalKolobanov",
    "section": "epic",
    "sectionName": "Эпические медали",
    "order": 600,
    "name": "Медаль Колобанова (medalKolobanov)",
    "description": "Победить в бою, оставшись в одиночку против 3 машин противника.\\r• Выжить в бою.",
    "count": 3,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalKolobanov.png",
    "localImage": "img/achievements/medalKolobanov.png",
    "isAchievementOption": false
}, {
    "achievementId": "medalOskin",
    "section": "epic",
    "sectionName": "Эпические медали",
    "order": 603,
    "name": "Медаль Оськина (medalOskin)",
    "description": "Уничтожить 3 машины противника в одном бою, управляя средним танком.\\r• Уничтоженная техника должна превосходить боевую машину игрока как минимум на 1 уровень.",
    "count": 1,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalOskin.png",
    "localImage": "img/achievements/medalOskin.png",
    "isAchievementOption": false
}, {
    "achievementId": "medalOrlik",
    "section": "epic",
    "sectionName": "Эпические медали",
    "order": 605,
    "name": "Медаль Орлика (medalOrlik)",
    "description": "Уничтожить не менее 3 машин противника в одном бою, управляя лёгким танком.\\r• Уничтоженная техника должна превосходить боевую машину игрока как минимум на 1 уровень.",
    "count": 2,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalOrlik.png",
    "localImage": "img/achievements/medalOrlik.png",
    "isAchievementOption": false
}, {
    "achievementId": "medalHalonen",
    "section": "epic",
    "sectionName": "Эпические медали",
    "order": 606,
    "name": "Медаль Халонена (medalHalonen)",
    "description": "Уничтожить не менее 3 машин противника в одном бою, управляя ПТ-САУ.\\r• Уничтоженная техника должна превосходить ПТ-САУ игрока как минимум на 1 уровень.",
    "count": 2,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalHalonen.png",
    "localImage": "img/achievements/medalHalonen.png",
    "isAchievementOption": false
}, {
    "achievementId": "huntsman",
    "section": "commemorative",
    "sectionName": "Памятные знаки",
    "order": 300,
    "name": "«Егерь» (huntsman)",
    "description": "Уничтожить все лёгкие танки противника в одном бою, но не менее 3.",
    "count": 3,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/huntsman.png",
    "localImage": "img/achievements/huntsman.png",
    "isAchievementOption": false
}, {
    "achievementId": "ironMan",
    "section": "commemorative",
    "sectionName": "Памятные знаки",
    "order": 1007,
    "name": "«Невозмутимый» (ironMan)",
    "description": "Получить подряд не менее 10 рикошетов и непробитий от противника.",
    "count": 24,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/ironMan.png",
    "localImage": "img/achievements/ironMan.png",
    "isAchievementOption": false
}, {
    "achievementId": "cadet",
    "section": "commemorative",
    "sectionName": "Памятные знаки",
    "order": 1051,
    "name": "«Кадет» (cadet)",
    "description": "Пройти боевое обучение.",
    "count": 1,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/cadet.png",
    "localImage": "img/achievements/cadet.png",
    "isAchievementOption": false
}, {
    "achievementId": "firstBlood",
    "section": "commemorative",
    "sectionName": "Памятные знаки",
    "order": 1052,
    "name": "«Первая кровь» (firstBlood)",
    "description": "Уничтожить 1 единицу техники противника.",
    "count": 1,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/firstBlood.png",
    "localImage": "img/achievements/firstBlood.png",
    "isAchievementOption": false
}, {
    "achievementId": "firstVictory",
    "section": "commemorative",
    "sectionName": "Памятные знаки",
    "order": 1053,
    "name": "«Первая победа» (firstVictory)",
    "description": "Победить в первом обычном бою с другими игроками.",
    "count": 1,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/firstVictory.png",
    "localImage": "img/achievements/firstVictory.png",
    "isAchievementOption": false
}, {
    "achievementId": "medalKay",
    "section": "step",
    "sectionName": "Этапные награды",
    "order": 1400,
    "name": "Медаль Кея",
    "description": "За получение статуса «Герой битвы».\\r• IV степени – 1\\n• III степени – 10\\n• II степени – 100\\n• I степени – 1 000",
    "count": 2,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalKay2.png",
    "localImage": "img/achievements/medalKay2.png",
    "isAchievementOption": true
}, {
    "achievementId": "medalCarius",
    "section": "step",
    "sectionName": "Этапные награды",
    "order": 1401,
    "name": "Медаль Кариуса",
    "description": "За количество уничтоженной техники противника.\\r• IV степени – 10\\n• III степени – 100\\n• II степени – 1 000\\n• I степени – 10 000",
    "count": 2,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalCarius2.png",
    "localImage": "img/achievements/medalCarius2.png",
    "isAchievementOption": true
}, {
    "achievementId": "medalEkins",
    "section": "step",
    "sectionName": "Этапные награды",
    "order": 1402,
    "name": "Медаль Экинса",
    "description": "За количество уничтоженной техники противника VIII–X уровней.\\r• IV степени – 3\\n• III степени – 30\\n• II степени – 300\\n• I степени – 3 000",
    "count": 2,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalEkins2.png",
    "localImage": "img/achievements/medalEkins2.png",
    "isAchievementOption": true
}, {
    "achievementId": "medalKnispel",
    "section": "step",
    "sectionName": "Этапные награды",
    "order": 1403,
    "name": "Медаль Книспеля",
    "description": "За суммарное количество нанесённого и полученного урона.\\r• IV степени – 10 000\\n• III степени – 100 000\\n• II степени – 1 000 000\\n• I степени – 10 000 000",
    "count": 1,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalKnispel1.png",
    "localImage": "img/achievements/medalKnispel1.png",
    "isAchievementOption": true
}, {
    "achievementId": "medalPoppel",
    "section": "step",
    "sectionName": "Этапные награды",
    "order": 1404,
    "name": "Медаль Попеля",
    "description": "За количество обнаруженной техники противника.\\r• IV степени – 20\\n• III степени – 200\\n• II степени – 2 000\\n• I степени – 20 000",
    "count": 2,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalPoppel2.png",
    "localImage": "img/achievements/medalPoppel2.png",
    "isAchievementOption": true
}, {
    "achievementId": "medalAbrams",
    "section": "step",
    "sectionName": "Этапные награды",
    "order": 1405,
    "name": "Медаль Абрамса",
    "description": "За количество победных боёв, в которых игрок остался жив.\\r• IV степени – 10\\n• III степени – 100\\n• II степени – 1 000\\n• I степени – 10 000",
    "count": 2,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalAbrams2.png",
    "localImage": "img/achievements/medalAbrams2.png",
    "isAchievementOption": true
}, {
    "achievementId": "medalLeClerc",
    "section": "step",
    "sectionName": "Этапные награды",
    "order": 1406,
    "name": "Медаль Леклерка",
    "description": "За суммарное количество очков захвата базы.\\r• Победить в бою.\\n• Засчитываются все очки захвата, набранные за один бой, за исключением очков захвата, полученных в режиме «Превосходство».\\n\\n• IV степени – 30\\n• III степени – 300\\n• II степени – 3 000\\n• I степени – 30 000",
    "count": 3,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalLeClerc3.png",
    "localImage": "img/achievements/medalLeClerc3.png",
    "isAchievementOption": true
}, {
    "achievementId": "medalLavrinenko",
    "section": "step",
    "sectionName": "Этапные награды",
    "order": 1407,
    "name": "Медаль Лавриненко",
    "description": "За суммарное количество сбитых очков захвата базы.\\r• Засчитывается не более 100 очков за бой.\\n• Не засчитываются сбитые очки захвата точек, заработанные в режиме «Превосходство»\\n\\n• IV степени – 30\\n• III степени – 300\\n• II степени – 3 000\\n• I степени – 30 000",
    "count": 2,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalLavrinenko2.png",
    "localImage": "img/achievements/medalLavrinenko2.png",
    "isAchievementOption": true
}, {
    "achievementId": "medalSupremacy",
    "section": "step",
    "sectionName": "Этапные награды",
    "order": 1408,
    "name": "Медаль режима «Превосходство»",
    "description": "За количество набранных очков победы в режиме «Превосходство».\\r• Накопительная медаль.\\n• Засчитываются все очки победы, набранные в боях в режиме «Превосходство».\\n\\n• IV степени – 10 000\\n• III степени – 100 000\\n• II степени – 1 000 000\\n• I степени – 10 000 000",
    "count": 3,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalSupremacy3.png",
    "localImage": "img/achievements/medalSupremacy3.png",
    "isAchievementOption": true
}, {
    "achievementId": "medalCrucialContribution",
    "section": "platoon",
    "sectionName": "Взводные награды",
    "order": 400,
    "name": "«Решающий вклад» (medalCrucialContribution)",
    "description": "Взвод должен уничтожить не менее 6 машин противника.\\r• Выдаётся обоим игрокам взвода.",
    "count": 1,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalCrucialContribution.png",
    "localImage": "img/achievements/medalCrucialContribution.png",
    "isAchievementOption": false
}, {
    "achievementId": "medalBrothersInArms",
    "section": "platoon",
    "sectionName": "Взводные награды",
    "order": 401,
    "name": "«Братья по оружию» (medalBrothersInArms)",
    "description": "Каждый игрок взвода должен уничтожить не менее 2 машин противника.\\r• Взвод должен выжить в бою.\\n• Выдаётся обоим игрокам взвода.",
    "count": 1,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/medalBrothersInArms.png",
    "localImage": "img/achievements/medalBrothersInArms.png",
    "isAchievementOption": false
}, {
    "achievementId": "jointVictory",
    "section": "platoon",
    "sectionName": "Взводные награды",
    "order": 1055,
    "name": "«Совместная победа»",
    "description": "За количество победных боёв в составе взвода.\\r• IV степени – 1\\n• III степени – 10\\n• II степени – 100\\n• I степени – 1 000",
    "count": 4,
    "image": "http://glossary-ru-static.gcdn.co/icons/wotb/current/achievements/jointVictory4.png",
    "localImage": "img/achievements/jointVictory4.png",
    "isAchievementOption": true
}]`;
}
