using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Peyk.Matrix.Client
{
    public class MatrixClient : IMatrixClient
    {
        private readonly ILogger _logger;

        private readonly HttpClient _httpClient;

        public MatrixClient(
            string server
        )
        {
            if (!string.IsNullOrWhiteSpace(server))
            {
                _httpClient = new HttpClient
                {
                    BaseAddress = new Uri($"https://{server}", UriKind.Absolute)
                };
            }
            else
                throw new ArgumentException();
        }

        public MatrixClient(
            HttpClient httpClient
        )
        {
            _httpClient = httpClient ?? throw new ArgumentException();
        }

        public MatrixClient(
            string server,
            ILogger<MatrixClient> logger
        )
            : this(server)
        {
            _logger = logger;
        }

        public MatrixClient(
            HttpClient httpClient,
            ILogger<MatrixClient> logger
        )
            : this(httpClient)
        {
            _logger = logger ?? throw new ArgumentException();
        }

        public async Task<TResponse> MakeRequestAsync<TResponse>(
            IRequest<TResponse> request,
            CancellationToken cancellationToken = default
        )
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var httpRequest = new HttpRequestMessage(request.HttpMethod, request.Url)
            {
                Content = request.ToHttpContent(),
            };

            _logger?.LogDebug(
                "Making request {requestMethod} {requestType}.",
                request.HttpMethod,
                request.Url
            );

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await _httpClient.SendAsync(httpRequest, cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (HttpRequestException e)
            {
                _logger?.LogError(e, "");
                throw;
            }
            finally
            {
                httpRequest.Dispose();
            }

            string responseContent = await httpResponse.Content.ReadAsStringAsync()
                .ConfigureAwait(false);
            if (httpResponse.IsSuccessStatusCode)
            {
                TResponse response;
                try
                {
                    response = JsonConvert.DeserializeObject<TResponse>(responseContent);
                }
                catch (JsonSerializationException e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    httpResponse.Dispose();
                }

                return response;
            }
            else
            {
                JObject jObj;
                try
                {
                    jObj = JsonConvert.DeserializeObject<JObject>(responseContent);
                }
                catch (JsonSerializationException e)
                {
                    throw new MatrixClientException("", "Response is not a valid JSON.", e);
                }
                finally
                {
                    httpResponse.Dispose();
                }

                var error = new MatrixClientError(jObj, (int) httpResponse.StatusCode);
                throw new MatrixClientException(error);
            }
        }
    }
}