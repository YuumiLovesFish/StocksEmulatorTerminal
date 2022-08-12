using System;


namespace YLF.StocksEmulator.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = StartUp.CreateHostBuilder(args).Build();
            
        }
    }
    
}
