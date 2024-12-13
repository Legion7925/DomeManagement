using GymManagement.Infrastructure.Common.Middleware;
using Microsoft.AspNetCore.Builder;

namespace GymManagement.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrustrucureMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<EventualConsistencyMiddleware>();

        return app;
    }
}
