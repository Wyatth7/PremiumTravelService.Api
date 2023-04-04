namespace PremiumTravelService.Api.Singleton;

/// <summary>
/// Singleton interface for use on all singleton objects
/// </summary>
/// <typeparam name="TSingleton">Singleton type</typeparam>
/// <typeparam name="TData">Data type to be returned</typeparam>
public interface ISingleton<out TSingleton, TData>
{
    /// <summary>
    /// Gets instance of singleton
    /// </summary>
    /// <returns>singleton instance</returns>
    /// <exception cref="NotImplementedException">Throws if not implemented</exception>
    static TSingleton GetInstance() => throw new NotImplementedException();
    
    /// <summary>
    /// Gets data based on data type generic
    /// </summary>
    /// <returns>Returns readonly collection of data</returns>
    /// <exception cref="NotImplementedException">Throws if not implemented</exception>
    Task<IReadOnlyCollection<TData>> GetData() => throw new NotImplementedException();
}