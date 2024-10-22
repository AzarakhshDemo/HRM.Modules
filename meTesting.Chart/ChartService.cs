using meTesting.Aether.SDK;
using meTesting.HRM.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace meTesting.Chart;

public class ChartService(NotifSender notifSender) : IChartService
{
    static Dictionary<int, Position> _store = new();
    static Dictionary<string, ReassignmentRequest> _reqStore = new();
    public async void AssignToPosition(int personnelId, int posisionId)
    {
        var c = GetChart(posisionId);
        c.AssigneeId = personnelId;
        await notifSender.Send(personnelId.ToString(), $"your new position is {c.Name}");
    }

    public Position Create(Position inp)
    {
        var id = _store.Count + 1;
        inp.Id = id;
        _store[id] = inp;

        Console.WriteLine($"{id}:{inp} was created");

        return _store[id];
    }

    public void EditChart(Position chart)
    {

        if (_store.ContainsKey(chart.Id))
            _store[chart.Id] = chart;
    }

    public Position GetChart(int id)
    {
        return _store[id];
    }

    public ReassignmentRequest CreateReassignmentRequest(int creatorUserId, int userId, int posId, string trId)
    {
        if (GetReassignmentRequest(trId) is not null)
            throw new InvalidOperationException();
        var id = _reqStore.Count + 1;

        var inp = new ReassignmentRequest()
        {
            Id = id,
            CreatorId = creatorUserId,
            PositionId = posId,
            State = ReassignmentRequest.ReassignmentRequestStateEnum.Pending,
            TrId = trId,
            UserId = userId
        };
        _reqStore[trId] = inp;
        Console.WriteLine($"{id}:{inp} was created");
        return inp;

    }
    public IEnumerable<ReassignmentRequest> GetAllReassignmentRequest()
    {
        return _reqStore.Select(a => a.Value);
    }
    public ReassignmentRequest? GetReassignmentRequest(string trId)
    {
        return _reqStore.GetValueOrDefault(trId);
    }

}
public static class DiHelper
{
    public static IServiceCollection AddChartService(this IServiceCollection services)
    {
        services.RemoveAll<IChartService>();
        services.AddScoped<IChartService, ChartService>();
        return services;
    }
}