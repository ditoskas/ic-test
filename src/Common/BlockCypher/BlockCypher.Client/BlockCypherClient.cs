using BlockCypher.Data;
using BlockCypher.Data.Exceptions;
using BlockCypher.Data.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BlockCypher.Client
{
    public class BlockCypherClient : IBlockCypherClient
    {
        private readonly HttpClient _client;
        public BlockCypherClient(HttpClient httpClient)
        {
            _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };
        }

        #region Public Methods
        public async Task<BlockCypherChain?> GetBlockCypherChain(string coin, string chain = "main", CancellationToken cancellationToken = default)
        {
            string path = string.Format(BlockCypherEndPoints.ChainInfo, coin, chain);
            return await Execute<BlockCypherChain>(path, HttpMethod.Get, null, cancellationToken);
        }

        public async Task<BlockCypherBlockHash?> GetBlockHash(string hash, string coin, string chain = "main", CancellationToken cancellationToken = default)
        {
            string path = string.Format(BlockCypherEndPoints.BlockByHash, coin, chain, hash);
            return await Execute<BlockCypherBlockHash>(path, HttpMethod.Get, null, cancellationToken);
        }
        #endregion

        #region Internal Methods
        private async Task<T?> Execute<T>(string endpoint, HttpMethod method, object? payload = null, CancellationToken cancellationToken = default) where T : class
        {
            string? json = await SendRequest(endpoint, method, payload, cancellationToken);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            return JsonSerializer.Deserialize<T>(json);
        }
        private async Task<string?> SendRequest(string endpoint, HttpMethod method, object? payload = null, CancellationToken cancellationToken = default)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, endpoint);
            //Add body if needed
            if (payload != null)
            {
                string json = JsonSerializer.Serialize(payload);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            HttpResponseMessage response = await _client.SendAsync(request, cancellationToken);
            string content = await response.Content.ReadAsStringAsync(cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return content;
            }
            else
            {
                throw new BlockCypherException(content);
            }
        }
        #endregion
    }
}
