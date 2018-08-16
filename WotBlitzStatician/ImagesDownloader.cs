using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using WotBlitzStatician.Model.Common;
using WotBlitzStatician.WotApiClient;

namespace WotBlitzStatician
{
  internal static class ImagesDownloader
  {
    public static void DowloadImagesFromWg(this IWargamingApiClient wargamingApiClient, List<string> wgImageUrls)
    {
      foreach (var wgImageUrl in wgImageUrls)
      {
        string imgLocalPath = $"wwwroot/{wgImageUrl.MakeImagePathLocal()}".Replace('/', Path.DirectorySeparatorChar);
        EnsureThatDirectoryExists(imgLocalPath);

        string filePath = Path.Combine(Directory.GetCurrentDirectory(), imgLocalPath);
        if (!File.Exists(filePath))
        {
          wargamingApiClient.DownloadFile(wgImageUrl, filePath);
        }
      }
    }

    private static void EnsureThatDirectoryExists(string imgLocalPath)
    {
      var tree = imgLocalPath.Split(Path.DirectorySeparatorChar);
      string currentDir = Directory.GetCurrentDirectory();
      for (int i = 0; i < tree.Length - 1; i++)
      {
        currentDir = Path.Combine(currentDir, tree[i]);
        if (!Directory.Exists(currentDir))
        {
          Directory.CreateDirectory(currentDir);
        }
      }
    }
  }
}
