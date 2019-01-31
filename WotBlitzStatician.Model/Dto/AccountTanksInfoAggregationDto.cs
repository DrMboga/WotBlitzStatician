namespace WotBlitzStatician.Model.Dto
{
  public class AccountTanksInfoAggregationDto
  {
    public bool InGarage { get; set; }
    public long Battles { get; set; }
    public long Wins { get; set; }
    public long DamageDealt { get; set; }
    public MarkOfMastery MarkOfMastery { get; set; }
    public double Wn7 { get; set; }
    public long Tier { get; set; }
    public string Nation { get; set; }
    public string NationName { get; set; }
    public string Type { get; set; }
    public string TypeName { get; set; }
    public bool IsPremium { get; set; }
  }
}