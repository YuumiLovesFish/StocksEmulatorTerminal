# Stocks Emulator - Terminal
## About the Application
Stocks Emulator is a console application written in .NET6 using C#.
The application tries to emulate a terminal where an administrator can manage different trading accounts for users.
As this is not a real emulation, the application is designed to be a "game" where the user can do trading operations.
The current stocks prices are retrieved from external API that randomly changes the prices when we retrive the stocks.

The source code of the stocks API is located here: https://github.com/YuumiLovesFish/StocksEmulatorApi

To begin, you need to input your name. This will be the account username. It must be only in English letters from "a" to "z" (not case sensitive). And the length
must be between 1 and 20 characters.
If there is a saved account for this user, the account will be loaded from the saved .json file (the name of the file is the same as the username). Otherwise the applicaiton will create a new account in memory, and the user can save it to a .json file by selecting Save and Exit.


## Prerequisites
To build and run the console application you will need to:
1. Install the latest .NET 6
2. Install Visual Studio 2022
3. Connection to Internet to access the Stocks API (https://ylfstocksemulatorapi.azurewebsites.net/stocks)
4. Permissions for the application to save .json files in the application directory - this is used for the Save and Exit feature

## Build, Debug and Run
1. Clone this repository (https://github.com/YuumiLovesFish/StocksEmulatorTerminal)
2. Open to solution (YLF.StocksEmulator.sln) in Visual Studio 2022
3. Debug or Run the application

## How to Use
As this applicaiton is more like a "game" rather than to try to simulate real stocks trading,
the main options are - openning an account, buying stocks, selling stocks, saving account.
The application connects to internet to an API and retrieves the current stocks price. Every time when we ask the API for stocks and prices
it changes the prices slightly. That's why the application has an option to refresh the current stock prices.
Options:
1. Refresh Stocks information - this calls the API to make it change the prices and return the new prices
2. Buy - this option allows the user to buy stocks that the API returns
3. Sell - his option allows the user to sell stocks that the API returns
4. TopUp - if the account runs out of money, this option allows the user to add more money into the account
5. Save and Exit - this option will create a JSON file in the same directory where the appliction is located.
6. Exit without saving - this option will create a JSON file in the same directory where the appliction is located.

## Areas of Improvements
Most of the classes have unit tests, but more tests could be added.
The UI can be better designed. The UI can be extracted into more classes.
The exception handling can be improved.

## Future Plans
Adding of a real-time chat service where the user can chat with support.
