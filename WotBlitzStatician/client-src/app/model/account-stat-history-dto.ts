export interface AccountStatHistoryDto{
    updatedAt: Date;
    battles: number;
    battlesDiff: number;
    wins: number;
    avgTier: number;
    avgTierDiff: number;
    wn7: number;
    wn7Diff: number;
    winRate: number;
    winRateDiff: number;
    avgDamage: number;
    avgDamageDiff: number;
    avgXp: number;
    avgXpDiff: number;
    survivalRate: number;
    survivalRateDiff: number;
    credits: number;
    creditsDiff: number;
    freeXp: number;
    freeXpDiff: number;
    gold: number;
    goldDiff: number;
}