using Flights.Application.Dtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Flights.Application.WebhookService
{
    public class WebhookService : IWebhookService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;
        private readonly ILogger<WebhookService> _logger;

        // Im actually not sure about the way of handling the httpclient connection in this service, the main reason is
        // if we would want to initialize the httpclient in notify async, we would do a instance per call, which is not really a good idea
        // my way of thinking about this problem is that we would create this service as transient,
        public WebhookService(IHttpClientFactory httpClientFactory, ILogger<WebhookService> logger)
        {
            _logger = logger;

            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.Timeout = new TimeSpan(0, 0, 30);

        }
        public async Task NotifyAsync(string url, WebhookSendDataDto dto)
        {
            try
            {
                var serializedObject = JsonSerializer.Serialize(dto, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

                var request = new HttpRequestMessage(HttpMethod.Post, url);

                request.Content =  new StringContent(serializedObject);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await _httpClient.SendAsync(request);

                return;
            }
            catch (Exception)
            {

                _logger.LogWarning($"Something went wrong in WebhookService for subscribtion with url {url}");
                return;
            }



        }
    }
}
