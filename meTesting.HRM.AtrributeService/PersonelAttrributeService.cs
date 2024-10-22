using meTesting.HRM.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace meTesting.HRM.AtrributeService;

public class PersonelAttrributeService : IPersonelAttrributeService
{
    static Dictionary<int, Dictionary<string, object>> _store = new();

    public T GetAttributeValue<T>(int userId, PersonelAttributeAtom<T> attrKey)
    {
        if (_store[userId][attrKey.Name] is T val)
            return val;
        return default(T);

    }

    public void SetAttribute<T>(int userId, PersonelAttributeAtom<T> attrKey, T value)
    {
        if (!_store.ContainsKey(userId))
            _store[userId] = new();
        _store[userId][attrKey.Name] = value;
    }
}

public static class DiHelper
{
    public static IServiceCollection AddPersonelAttrributeService(this IServiceCollection services)
    {
        services.RemoveAll<IPersonelAttrributeService>();
        services.AddScoped<IPersonelAttrributeService, PersonelAttrributeService>();
        return services;
    }
}