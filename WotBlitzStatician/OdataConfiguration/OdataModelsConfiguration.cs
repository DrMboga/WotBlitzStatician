using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using WotBlitzStatician.Model.Dto;

namespace WotBlitzStatician.OdataConfiguration
{
  public static class OdataModelsConfiguration
  {
    public static IEdmModel GetEdmModel()
    {
      var builder = new ODataConventionModelBuilder()
      {
        ContainerName = "Wotblitzstatician",
        Namespace = "Wotblitzstatician.OData"
      };

      ConfigureTanksOdata(builder);
      ConfigureGuestTanksOdata(builder);

      return builder.GetEdmModel();
    }

    public static void ConfigureTanksOdata(ODataConventionModelBuilder builder)
    {
      builder.EntityType<AccountTankInfoDto>()
        .HasKey(t => t.TankAccountTankStatisticId)
        .Filter()
        .Select()
        .Expand()
        .OrderBy();
      builder.EntitySet<AccountTankInfoDto>("TanksInfo");
    }

    public static void ConfigureGuestTanksOdata(ODataConventionModelBuilder builder)
    {
      builder.EntityType<AccountTankInfoDto>()
        .HasKey(t => t.TankAccountTankStatisticId)
        .Filter()
        .Select()
        .Expand()
        .OrderBy();
      builder.EntitySet<AccountTankInfoDto>("GuestTanksInfo");
    }
  }
}
