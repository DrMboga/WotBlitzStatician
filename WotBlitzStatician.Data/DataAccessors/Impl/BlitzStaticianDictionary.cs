namespace WotBlitzStatician.Data.DataAccessors.Impl
{
  using System;
  using System.Diagnostics;
  using Microsoft.Extensions.Logging;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using Microsoft.EntityFrameworkCore;
  using WotBlitzStatician.Model;
  using WotBlitzStatician.Data.DataAccessors.Impl.EntityHelpers;

  public class BlitzStaticianDictionary : IBlitzStaticianDictionary
  {
    private readonly BlitzStaticianDbContext _dbContext;
    private readonly ILogger<BlitzStaticianDictionary> _logger;

    public BlitzStaticianDictionary(BlitzStaticianDbContext dbContext, ILogger<BlitzStaticianDictionary> logger)
    {
      _dbContext = dbContext;
      _logger = logger;
    }

    public void CreateDatabase()
    {
      _dbContext.Database.Migrate();
    }

    public async Task SaveAchievements(List<Achievement> achievements)
    {
      await _dbContext.MergeAchievements(achievements);

      _dbContext.SaveChanges();
    }

    public async Task SaveDictionaries(
        List<DictionaryLanguage> languages,
        List<DictionaryNations> natons,
        List<DictionaryVehicleType> vehicleTypes,
        List<AchievementSection> achievementSections,
        List<DictionaryClanRole> clanRoles)
    {
      await _dbContext.MergeLanguages(languages);
      await _dbContext.MergeNations(natons);
      await _dbContext.MergeVehicleType(vehicleTypes);
      await _dbContext.MergeAchievementSection(achievementSections);
      await _dbContext.MergeClanRoles(clanRoles);

      await _dbContext.SaveChangesAsync();
    }

    public async Task SaveVehicles(List<VehicleEncyclopedia> vehicles)
    {
      await _dbContext.MergeVehicles(vehicles);
      await _dbContext.SaveChangesAsync();
    }

    public async Task<Dictionary<long, double>> GetVehiclesTires()
    {
      return await _dbContext.VehicleEncyclopedia
          .Select(v => new { v.TankId, v.Tier })
          .ToDictionaryAsync(s => s.TankId, s => Convert.ToDouble(s.Tier));
    }

    public async Task<List<string>> GetAllImages()
    {
      return await _dbContext.Achievement.AsNoTracking()
          .Where(a => a.Image != null)
          .Select(a => a.Image)
          .Union(_dbContext.AchievementOption.AsNoTracking()
              .Where(o => o.Image != null)
              .Select(o => o.Image))
          .Union(_dbContext.VehicleEncyclopedia.AsNoTracking()
              .Where(v => v.PreviewImageUrl != null)
              .Select(v => v.PreviewImageUrl))
          .ToListAsync();
    }

    public async Task<string> GetClanRole(string clanRoleId)
    {
      return await _dbContext.DictionaryClanRole.AsNoTracking()
                    .Where(d => d.ClanRoleId == clanRoleId)
                    .Select(d => d.RoleName)
                    .FirstOrDefaultAsync();
    }

    public async Task<List<VehicleEncyclopedia>> GetVehicles(List<long> tankId)
    {
      return await _dbContext.VehicleEncyclopedia.AsNoTracking()
                    .Where(v => tankId.Contains(v.TankId))
                    .ToListAsync();
    }

    public async Task<List<Achievement>> GetAchievements(List<string> acievementIds)
    {
      return await _dbContext.Achievement.AsNoTracking()
                    .Where(a => acievementIds.Contains(a.AchievementId))
                    .ToListAsync();
    }

    public async Task<List<AchievementSection>> GetAchievementSections()
    {
      return await _dbContext.AchievementSection.AsNoTracking()
                    .ToListAsync();
    }
  }
}
