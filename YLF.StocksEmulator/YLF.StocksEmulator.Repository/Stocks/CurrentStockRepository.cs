using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using YLF.StocksEmulator;
using YLF.StocksEmulator.Repository.Exceptions;

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
            if (string.IsNullOrEmpty(stockName))
            {
                throw new StockNotFoundException($"Stock namecan not be empty!");
            }
            using var client = _httpClientFactory.CreateClient(HttpClientName);
            CurrentStock currentStock = null;
            try
            {
                currentStock = await client.GetFromJsonAsync<CurrentStock>($"stocks/{stockName}");
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new StockNotFoundException($"Stock: {stockName} deos not exist!");
                }
                
            }
            return currentStock;


        }

        public async Task<List<CurrentStock>> GetAllStocksAsync()
        {
            using var client = _httpClientFactory.CreateClient(HttpClientName);
            return await client.GetFromJsonAsync<List<CurrentStock>>("stocks");
        }
    }
}
