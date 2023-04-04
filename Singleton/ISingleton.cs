namespace PremiumTravelService.Api.Singleton;

public interface ISingleton<out TSingleton, TData>
{
    static TSingleton GetInstance() => throw new NotImplementedException();
    static Task<IReadOnlyCollection<TData>> GetData() => throw new NotImplementedException();
}