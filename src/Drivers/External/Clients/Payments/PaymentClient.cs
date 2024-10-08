﻿using System.Text;
using Adapters.Gateways.Payments;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace External.Clients.Payments;

public class PaymentClient(
    IHttpClientFactory httpClientFactory,
    IOptions<PaymentsClientSettings> paymentsClientSettings) : IPaymentClient
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    private readonly Uri _paymentsClientBaseUri = new(paymentsClientSettings.Value.BaseUri, UriKind.Absolute);

    public async Task<Payment> CreatePayment(Guid orderId, decimal amount)
    {
        var uri = new Uri(_paymentsClientBaseUri, "/api/payments");
        var payload = JsonConvert.SerializeObject(new CreatePaymentRequest(orderId, amount));
        var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new StringContent(payload, Encoding.UTF8, "application/json")
        };

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode is false)
            throw new Exception("Failed to create the payment.");

        var content = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<Payment>(content)!;
    }
}

public record CreatePaymentRequest(Guid OrderId, decimal Amount);