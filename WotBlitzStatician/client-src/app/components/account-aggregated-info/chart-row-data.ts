export interface ChartRowData {
  id: string;
  caption: string;
  dataByType: Map<string, number>;
  dataByTier: Map<string, number>;
  dataByNation: Map<string, number>;
  dataByPremium: Map<string, number>;
}
