﻿using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DcrdClient
{
    /// <summary>
    /// Http client to communicate with dcrd.
    /// </summary>
    public class DcrdHttpClient : IDcrdClient
    {
        private readonly string _apiUrl;
        private readonly HttpClientHandler _httpClientHandler;

        public DcrdHttpClient(string apiUrl, HttpClientHandler httpClientHandler)
        {
            _apiUrl = apiUrl;
            _httpClientHandler = httpClientHandler;
        }

        private async Task<DcrdRpcResponse<T>> Perform<T>(string method, params object[] parameters)
        {
            const string jsonrpc = "1.0";
            const string id = "1";
            
            using (var httpClient = new HttpClient(_httpClientHandler, false))
            {
                var request = new
                {
                    jsonrpc = jsonrpc,
                    id = id,
                    method = method,
                    @params = parameters
                };
                
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(_apiUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();
                
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException(responseString);
                
                return JsonConvert.DeserializeObject<DcrdRpcResponse<T>>(responseString);
            }
        }

        public async Task<DcrdRpcResponse> PingAsync()
        {
            return await Perform<string>("ping");
        }

        public async Task<DcrdRpcResponse> SendRawTransactionAsync(string hexTransaction)
        {
            return await Perform<string>("sendrawtransaction", hexTransaction);
        }

        public async Task<GetBestBlockResult> GetBestBlockAsync()
        {
            var result = await Perform<GetBestBlockResult>("getbestblock");
            return result.Result;
        }

        public async Task<decimal> EstimateFeeAsync(int numBlocks)
        {
            var result = await Perform<decimal>("estimatefee", numBlocks);
            return result.Result;
        }
    }
}
