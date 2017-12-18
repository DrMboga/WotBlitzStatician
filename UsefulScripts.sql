----BestTanks----
SELECT DISTINCT v.Tier, v.Nation, v.Name, t.Battles, t.Wins *100 / t.Battles AS WinRate, t.Wn7, t.Effectivity, t.DamageDealt / t.Battles AS AvgDamage, t.LastBattleTime
FROM AccountTankStatistics t
  INNER JOIN (
    SELECT ts.AccountId, ts.TankId, max(ts.LastBattleTime) AS LastBattle
    FROM AccountTankStatistics AS ts
    GROUP BY ts.AccountId, ts.TankId
    ) latest ON t.TankId = latest.TankId AND t.AccountId = latest.AccountId AND t.LastBattleTime = latest.LastBattle
  INNER JOIN VehicleEncyclopedia v ON v.TankId=t.TankId
WHERE t.Battles > 50
      AND t.Wn7 > 1100 -- Green and more
      AND t.AccountId = 46512100
ORDER BY t.Wn7 DESC , t.Effectivity DESC, t.DamageDealt / t.Battles DESC, t.Wins *100 / t.Battles DESC, v.Tier DESC ;

----Most relevant achievements----
SELECT *
FROM AccountInfoAchievment aa
  INNER JOIN Achievement a ON aa.AchievementId = a.AchievementId
WHERE a.Section IN ('battle', 'platoon', 'epic')
  AND aa.TankId IS NULL
  AND aa.AccountId = 46512100
ORDER BY a.Section DESC, a."Order";

----Achievements by tanks----
SELECT v.Tier, v.Nation, v.Name, aa.Count, a.Name, a.Description, a.Section, a.Image
FROM AccountInfoAchievment aa
  INNER JOIN VehicleEncyclopedia v on aa.TankId = v.TankId
  INNER JOIN Achievement a ON aa.AchievementId = a.AchievementId
WHERE aa.TankId IS NOT NULL
  AND a.Section IN ('battle', 'platoon', 'epic')--aa.AchievementId = 'medalLafayettePool'
  AND aa.Count > 0
ORDER BY aa.TankId, a.Section DESC, a."Order", aa.Count DESC;

----Most achievemented tanks (Not working properly)----
SELECT v.Tier, v.Nation, v.Name, aa.Count, a.Name, a.Description, a.Section, a.Image
FROM AccountInfoAchievment aa
  INNER JOIN VehicleEncyclopedia v on aa.TankId = v.TankId
  INNER JOIN Achievement a ON aa.AchievementId = a.AchievementId
  INNER JOIN (
    SELECT aag.TankId, SUM(aag.Count) AchievementsCount
    FROM AccountInfoAchievment aag
      INNER JOIN Achievement ach ON aag.AchievementId = ach.AchievementId
    WHERE aag.TankId IS NOT NULL
      AND ach.Section IN ('battle', 'platoon', 'epic')
    GROUP BY aag.TankId) ag ON aa.TankId = ag.TankId
WHERE aa.Count > 0
  AND a.Section IN ('battle', 'platoon', 'epic')
ORDER BY AchievementsCount DESC, a.Section DESC, a."Order", aa.Count DESC;
;


--------------------------------------------------------------
SELECT *
FROM AccountInfo;

SELECT *
FROM AccountInfoStatistics;

SELECT *
FROM AccountClanInfo;

-- All tanks actual statistic
SELECT v.Name, ts.TankId, max(ts.LastBattleTime)
FROM AccountTankStatistics AS ts
INNER JOIN VehicleEncyclopedia v ON v.TankId=ts.TankId
GROUP BY v.Name, v.TankId
ORDER BY max(ts.LastBattleTime) DESC;

;

--DELETE FROM AccountInfoStatistics
-- Group tanks
SELECT v.Name, t.*
FROM AccountTankStatistics t INNER JOIN
  (SELECT TankId
FROM AccountTankStatistics
GROUP BY TankId
HAVING count(*) > 1) g ON g.TankId =t.TankId
  INNER JOIN VehicleEncyclopedia v ON v.TankId = t.TankId
ORDER BY t.TankId, t.LastBattleTime;

-- Dublicates
SELECT ts1.*
FROM AccountTankStatistics ts1
  INNER JOIN
  (SELECT TankId, Battles, BattleLifeTime
   FROM AccountTankStatistics
    GROUP BY TankId, Battles, BattleLifeTime
    HAVING count(*) > 1
  )ts2 ON ts1.TankId = ts2.TankId AND ts1.Battles = ts2.Battles and ts1.BattleLifeTime = ts2.BattleLifeTime
  INNER JOIN VehicleEncyclopedia v ON v.TankId = ts1.TankId
ORDER BY ts1.TankId;


-- Achievements stuff
SELECT *
FROM AccountInfoAchievment
WHERE TankId IS NULL ;

SELECT *
FROM AccountInfoAchievment
WHERE TankId IS NOT NULL ;

-- Tanks Last session
SELECT v.Name, ts.*
FROM AccountTankStatistics ts
  INNER JOIN VehicleEncyclopedia v ON v.TankId = ts.TankId
WHERE ts.AccountId = 46512100 AND
  ts.LastBattleTime >=
  (SELECT acs.UpdatedAt
  FROM AccountInfoStatistics acs
  WHERE acs.AccountId = 46512100 AND acs.AccountInfoStatisticsId < (SELECT max(AccountInfoStatisticsId) FROM AccountInfoStatistics WHERE AccountId = 46512100)
  ORDER BY acs.UpdatedAt DESC LIMIT 1)
ORDER BY ts.LastBattleTime DESC ;

-- Account 46512100 last session and delta
-- Previous date
SELECT min(ls.UpdatedAt)
FROM
  (SELECT s.*
    FROM AccountInfoStatistics s
    WHERE s.AccountId = 46512100
    ORDER BY s.UpdatedAt DESC
    LIMIT 2) ls;

SELECT DISTINCT ts.TankId
FROM AccountTankStatistics ts
WHERE ts.LastBattleTime >
      (SELECT min(ls.UpdatedAt)
       FROM
         (SELECT s.*
          FROM AccountInfoStatistics s
          WHERE s.AccountId = 46512100
          ORDER BY s.UpdatedAt
            DESC
          LIMIT 2) ls);

SELECT *
FROM AccountTankStatistics ts
WHERE ts.TankId = 15937
ORDER BY ts.LastBattleTime;

SELECT *
FROM AchievementOption ao
WHERE ao.AchievementId = 'markOfMastery';

-- UPDATE DictionaryLanguage
-- SET LastUpdated = '2017-09-01 02:30:05.8940834'
SELECT *
FROM DictionaryLanguage;

VACUUM