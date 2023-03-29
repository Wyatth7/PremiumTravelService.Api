namespace PremiumTravelService.Api.DataStorage.DataHandling;

public interface IFileReadWrite<TModel>
{
    Task Write(TModel payload);
    Task<TModel> Read();
}