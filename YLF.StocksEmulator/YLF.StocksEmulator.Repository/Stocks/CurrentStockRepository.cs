using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using YLF.StocksEmulator;
using YLF.StocksEmulator.Terminal;

namespace YLF.StocksEmulator.Repository.Stocks
{
    public class CurrentStockRepository : ICurrentStockRepository
    {
        private IHttpClientFactory _httpClientFactory;

        public CurrentStockRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CurrentStock> GetStock(string stockName)
        {
            using var client = _httpClientFactory.CreateClient(Constants.HttpClientName);
            return await client.GetFromJsonAsync<CurrentStock>($"stocks/{stockName}");
        }

        public async Task<List<CurrentStock>> GetAllStocks()
        {
            using var client = _httpClientFactory.CreateClient(Constants.HttpClientName);
            return await client.GetFromJsonAsync<List<CurrentStock>>("stocks");
        }
    }
}
