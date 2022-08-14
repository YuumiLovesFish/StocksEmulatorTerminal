using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using YLF.StocksEmulator.Repository.Exceptions;
using YLF.StocksEmulator.Services;
using YLF.StocksEmulator.Services.Exceptions;

namespace YLF.StocksEmulator.Ui
{
    public class MenuUi
    {
        private readonly ITradingService _tradingService;
        private readonly IUserService _userService;
        private readonly StatusUi _statusUi;

        public MenuUi(ITradingService tradingService, IUserService userService, StatusUi statusUi)
        {
            _tradingService = tradingService;
            _userService = userService;
            _statusUi = statusUi;
        }

        public async Task OptionBuyAsync()
        {
            string continueBuying = "no";
            do
            {
                Console.WriteLine("Please enter the stock name you would like to purchase");
                var stcokName = Console.ReadLine();
                Console.WriteLine("Please enter the quantity you would like to purchase?");
                var quantityInString = Console.ReadLine();
                int quantity;

                while (!int.TryParse(quantityInString, out quantity) || quantity <= 0)
                {
                    Console.WriteLine("You have enter an invalid quantity.");
                    Console.WriteLine($"Do you wish to continue purchasing {stcokName.Trim().ToUpper()}?");

                    var response = EnterYesToContinue();
                    if (response.Trim().ToLower() != "yes")
                    {
                        return;
                    }
                    Console.WriteLine("Please enter the quantity you would like to purchase?");
                    quantityInString = Console.ReadLine();
                }
                try
                {
                    var stockBought = await _tradingService.BuyStockAsync(_userService.User, stcokName, quantity);

                    await _statusUi.RenderUserAccountAndStocksInfoAsync();
                    Console.Write(Environment.NewLine);
                    Console.WriteLine($"You have successfully bought {quantity} share(s) of stock {stockBought.Name} at a share price of {Math.Round(stockBought.Price, 2):0.00}");
                    Console.WriteLine("Do you want to buy another stock?");
                   
                    continueBuying = EnterYesToContinue();
                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
                
              
               
               
            }
            while (continueBuying == "yes");

      
        }

        public async Task OptionSellAsync()
        {
           
            string continueSelling="no";

            do
            {
                Console.WriteLine("Please enter the stock name you would like to sell");
                var stcokName = Console.ReadLine();
                Console.WriteLine("Please enter the quantity you would like to sell?");
                var quantityInString = Console.ReadLine();
                int quantity;
                



                while (!int.TryParse(quantityInString, out quantity) || quantity <= 0)
                {
                    Console.WriteLine("You have enter an invalid quantity.");
                    Console.WriteLine($"Do you wish to continue selling {stcokName.Trim().ToUpper()}?");

                    var respond = EnterYesToContinue();
                    if (respond.Trim().ToLower() != "yes")
                    {
                        return;
                    }
                    Console.WriteLine("Please enter the quantity you would like to sell?");
                    quantityInString = Console.ReadLine();
                }
                try
                {
                    var stockSold = await _tradingService.SellStcokAsync(_userService.User, stcokName, quantity);
                    await _statusUi.RenderUserAccountAndStocksInfoAsync();
                    Console.Write(Environment.NewLine);
                    Console.WriteLine($"You have successfully sold {quantity} share(s) of stock {stockSold.Name} at a share price of {Math.Round(stockSold.Price, 2):0.00}");
                    Console.WriteLine("Do you want to buy other stocks?");

                    continueSelling = EnterYesToContinue();

                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }


              
            }
            while (continueSelling == "yes");

         
        }
        public void OptionTopupAsync()
        {
            string topupAmountInString;
            decimal topupAmount;
            string wantToTopUp;
          
            Console.WriteLine("Please enter the amount you would like to topup");
            topupAmountInString = Console.ReadLine();

            while (!decimal.TryParse(topupAmountInString, out topupAmount) || topupAmount > 1000 || topupAmount <= 0)
            {
                Console.WriteLine("You have enter an invalid amount.");
                Console.WriteLine("Do you wish to continue with the TopUp service?");

                wantToTopUp = EnterYesToContinue();
                if (wantToTopUp != "yes")
                {
                    return;
                }
                Console.Write(Environment.NewLine);

                Console.WriteLine("Please enter the amount you would like to topup");
                topupAmountInString = Console.ReadLine();
            }
           

            _userService.TopUpAccount(topupAmount);
            Console.WriteLine("You have topped up successfully!");
          
        }
        private void HandleException(Exception ex)
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine("Oooooops..... Something went wrong! Please try again.");
            Console.WriteLine(ex.Message);
            PressEnterToContinue();
        }

        public void SaveAndLogout()
        {
            try
            {
                _userService.ExitAndSaveProgess();
                Console.WriteLine("Your account has been saved successfully.");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
           
        }
        private void PressEnterToContinue()
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine("Press Enter to continue");
            Console.ReadKey();

            
        }
        private string EnterYesToContinue()
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine($"Type 'yes' to continue to the service, or press 'Enter' to go back to the main menu");
            return Console.ReadLine().Trim().ToLower();
        }

        public async Task<bool> RenderOptionMenuAsync()
        {
            Console.WriteLine("Please type down the number in front of the menu option.");
            Console.Write(Environment.NewLine);
            Console.Write(Environment.NewLine);
            Console.WriteLine("1. \tRefresh Stocks information");
            Console.WriteLine("2. \tBuy");
            Console.WriteLine("3. \tSell");
            Console.WriteLine("4. \tTopUp");
            Console.WriteLine("5. \tSave & Exit");
            Console.WriteLine("6. \tExit without Saving");
            Console.WriteLine("7. \t**Coming soon** Real-time support chat");

            string option = Console.ReadLine();
            int optionIndex;
            while (!int.TryParse(option, out optionIndex) || optionIndex < 1 || optionIndex > 7)
            {
                Console.WriteLine("Oooooops..... The option you have selected is invalid.");
                Console.WriteLine("Please type down the number in front of the menu option.");
                Console.WriteLine("For example: For 'Buy' option, please type number '2', and press Enter.");
                option = Console.ReadLine();
            }

            switch (optionIndex)
            {
                case 1:
                    break;
                case 2:
                    await OptionBuyAsync();
                    break;
                case 3:
                    await OptionSellAsync();
                    break;
                case 4:
                    OptionTopupAsync();
                    break;
                case 5:
                    SaveAndLogout();
                    return true;
                case 6:
                    return true;
                case 7:
                    Console.WriteLine("This feautre is currently in developlemnt. Stay tuned for furhter updates.");
                    PressEnterToContinue();
                    break;
                default:
                    break;
            }

            return false;

        }

    }
}
