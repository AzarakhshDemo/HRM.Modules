using meTesting.HRM.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace meTesting.Personnel;

public class PersonelService : IPersonelService
{
    static Dictionary<int, Personel> _store = new();

    public Personel Create(Personel inp)
    {
        var id = _store.Count + 1;
        inp.Id = id;
        _store[id] = inp;
        Console.WriteLine($"{id}:{inp} was created");
        return inp;
    }

    public Personel Get(int id)
    {
        return _store[id];
    }

    IEnumerable<Personel> IPersonelService.GetAll()
    {
        return _store.Select(a => a.Value).ToList();
    }
}
public static class DiHelper
{
    public static IServiceCollection AddPersonelService(this IServiceCollection services)
    {
        services.RemoveAll<IPersonelService>();
        services.AddScoped<IPersonelService, PersonelService>();
        return services;
    }
}