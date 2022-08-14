using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.Extensions.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using YLF.StocksEmulator.Services;
using YLF.StocksEmulator.Repository.Stocks;
using YLF.StocksEmulator.Repository.Users;

namespace YLF.StocksEmulator.Terminal
{
    public static class StartUp
    {

       
        public static  IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddHttpClient(CurrentStockRepository.HttpClientName, congfigClient=>congfigClient.BaseAddress=new Uri(context.Configuration.GetValue<string>("StockApiUrl")));
                    services.AddSingleton<IUserService, UserService>();
                    services.AddScoped<ICurrentStockRepository, CurrentStockRepository>();
                    services.AddScoped<IUserRepository, UserRepository>();
                    services.AddScoped<ITradingService, TradingService>();
                });

            return hostBuilder;
        }

        
    }
}
