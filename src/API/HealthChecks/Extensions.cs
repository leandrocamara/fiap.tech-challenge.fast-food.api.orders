using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace API.HealthChecks;

public static class Extensions
{
    public static void UseCustomHealthChecks(this IApplicationBuilder builder)
    {
        builder.UseHealthChecks("/heartbeat", new HealthCheckOptions
        {
            Predicate = _ => false,
            ResponseWriter = (context, report) =>
            {
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync(JsonConvert.SerializeObject(report, Formatting.Indented));
            }
        });
    }
}