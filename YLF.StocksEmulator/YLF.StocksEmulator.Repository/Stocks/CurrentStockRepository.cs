using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using YLF.StocksEmulator;


namespace YLF.StocksEmulator.Repository.Stocks
{
    public class CurrentStockRepository : ICurrentStockRepository
    {
        public const string HttpClientName = "StockClient";
        private IHttpClientFactory _httpClientFactory;

        public CurrentStockRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
       

        public async Task<CurrentStock> GetStockAsync(string stockName)
        {
            using var client = _httpClientFactory.CreateClient(HttpClientName);
            return await client.GetFromJsonAsync<CurrentStock>($"stocks/{stockName}");
        }

        public async Task<List<CurrentStock>> GetAllStocksAsync()
        {
            using var client = _httpClientFactory.CreateClient(HttpClientName);
            return await client.GetFromJsonAsync<List<CurrentStock>>("stocks");
        }
    }
}
