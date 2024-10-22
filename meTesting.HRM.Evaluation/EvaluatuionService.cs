using meTesting.HRM.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace meTesting.HRM.Evaluation;

public class EvaluatuionService : IEvaluationService
{
    static Dictionary<int, EvaluationResult> _store = new();

    public Task<EvaluationResult> GetUserEvaluation(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<EvaluationResult> SetEvaluationResult(int userId, int score)
    {
        throw new NotImplementedException();
    }
}
public static class DiHelper
{
    public static IServiceCollection AddEvaluationService(this IServiceCollection services)
    {
        services.RemoveAll<IEvaluationService>();
        services.AddScoped<IEvaluationService, EvaluatuionService>();
        return services;
    }
}
