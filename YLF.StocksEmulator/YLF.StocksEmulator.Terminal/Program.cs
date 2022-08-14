using System;
using YLF.StocksEmulator.Services;
using YLF.StocksEmulator.Ui;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace YLF.StocksEmulator.Terminal
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = StartUp.CreateHostBuilder(args).Build();
            var tradingService = host.Services.GetService<ITradingService>();
            var userService = host.Services.GetService<IUserService>();

            var statusUi = new StatusUi(tradingService, userService);
            var menuUi = new MenuUi(tradingService, userService, statusUi);
            bool finish;
            statusUi.Login();

            do
            {
                await statusUi.RenderUserAccountAndStocksInfoAsync();
                finish = await menuUi.RenderOptionMenuAsync();

            } while (!finish);

            Console.WriteLine("Press any key to close the application...");
            Console.ReadKey();
        }
    }

}
