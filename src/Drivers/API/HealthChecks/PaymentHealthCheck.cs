using External.Clients.Payments;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace API.HealthChecks;

public class PaymentsClientHealthCheck : IHealthCheck, IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly Uri _paymentsClientHeartbeatUri;

    public PaymentsClientHealthCheck(
        IHttpClientFactory httpClientFactory,
        IOptions<PaymentsClientSettings> paymentsClientSettings)
    {
        _httpClient = httpClientFactory.CreateClient();

        var paymentsClientBaseUri = new Uri(paymentsClientSettings.Value.BaseUri, UriKind.Absolute);
        _paymentsClientHeartbeatUri = new Uri(paymentsClientBaseUri, "heartbeat");
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = new())
    {
        try
        {
            var response = await _httpClient.GetAsync(_paymentsClientHeartbeatUri, cancellationToken);

            if (response.IsSuccessStatusCode)
                return HealthCheckResult.Healthy();

            return HealthCheckResult.Degraded(
                "Erro de comunicação com Payments API.\n" +
                $"Status Code: {response.StatusCode}\n" +
                $"Content: {await response.Content.ReadAsStringAsync(cancellationToken)}");
        }
        catch (Exception e)
        {
            return HealthCheckResult.Unhealthy(description: "Erro de comunicação com Payments API.", exception: e);
        }
    }

    public void Dispose() => _httpClient.Dispose();
}