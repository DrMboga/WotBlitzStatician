export interface TankStatisticDto {
    TankAccountTankStatisticId: number;
    TankAccountId: number;
    TankTankId: number;
    TankBattleLifeTimeInSeconds: number;
    TankLastBattleTime: Date;
    TankMarkOfMastery: string;
    TankInGarage: boolean;
    TankInGarageUpdated: Date;
    TankBattles: number;
    TankCapturePoints: number;
    TankDamageDealt: number;
    TankDamageReceived: number;
    TankDroppedCapturePoints: number;
    TankFrags: number;
    TankFrags8P: number;
    TankHits: number;
    TankLosses: number;
    TankMaxFrags: number;
    TankMaxXp: number;
    TankShots: number;
    TankSpotted: number;
    TankSurvivedBattles: number;
    TankWinAndSurvived: number;
    TankWins: number;
    TankXp: number;
    TankWn7: number;
    TankWn8: number;
    WinRate: number;
    AvgDamage: number;
    AvgXp: number;
    SurvivalRate: number;
    VehicleName: string;
    VehicleTier: number;
    TankTierRoman: string;
    VehicleNation: string;
    VehicleType: string;
    VehicleIsPremium: boolean;
    VehiclePreviewImageUrl: string;
    PreviewLocalImage: string;
    VehicleNormalImageUrl: string;
    NormalLocalImage: string;
}