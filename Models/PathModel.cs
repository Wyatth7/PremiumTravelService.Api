namespace PremiumTravelService.Api.Models;

public static class PathModel
{
    public static string Path => System.IO.Path.GetFullPath(@"DataStorage/Storage/JsonStorage.json");
}