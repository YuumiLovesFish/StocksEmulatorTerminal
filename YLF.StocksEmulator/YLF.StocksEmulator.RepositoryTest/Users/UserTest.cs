using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YLF.StocksEmulator.Repository.Stocks;
using YLF.StocksEmulator.Repository.Users;
using FluentAssertions;

namespace YLF.StocksEmulator.RepositoryTest.Users
{
    public class UserTest
    {
        private User _user;

        [SetUp]
        public void SetUpUser()
        {
            
            List<OwnedStock> listOfOwnedStock = new List<OwnedStock>();
            decimal accountBalance = 100m;
            Portfolio portfolio = new Portfolio(accountBalance, listOfOwnedStock);
            string name = "poppy";
            _user = new User(name, portfolio);
        }

        [Test]
        public void AddMoney_0Amount_AccountBalanceWillNotChange()
        {
            //arrange
            var accountBalance = _user.MyPortfolio.AccountBalance;
            decimal amountToBeAdd = 0m;
            //act
            _user.AddMoney(amountToBeAdd);
            //assert
            _user.MyPortfolio.AccountBalance.Should().Be(accountBalance);
            
        }
        [Test]
        public void AddMoney_Negative100Amount_AccountBalanceWillNotChange()
        {
            //arrange
            var accountBalance = _user.MyPortfolio.AccountBalance;
            decimal amountToBeAdd = -100m;
            //act
            _user.AddMoney(amountToBeAdd);
            //assert
            _user.MyPortfolio.AccountBalance.Should().Be(accountBalance);

        }
        [Test]
        public void AddMoney_100Amount_AccountBalancePlus100()
        {
            //arrange
            var accountBalance = _user.MyPortfolio.AccountBalance;
            decimal amountToBeAdd = 100m;
            //act
            _user.AddMoney(amountToBeAdd);
            //assert
            _user.MyPortfolio.AccountBalance.Should().Be(accountBalance + amountToBeAdd);

        }
        [Test]
        public void AddStock_StockNameIsNull_OwnedStockListWontChange()
        {
            //arrange
            string? stockName = null;
            int quantity = 1;
            //act
            _user.AddStock(stockName, quantity);
            //assert
            _user.MyPortfolio.MyStocks.Should().BeEmpty();
        
        }
        [Test]
        public void AddStock_StockNameIsEmpty_OwnedStockListWontChange()
        {
            //arrange
            string? stockName = String.Empty;
            int quantity = 1;
            //act
            _user.AddStock(stockName, quantity);
            //assert
            _user.MyPortfolio.MyStocks.Should().BeEmpty();

        }
        [Test]
        public void AddStock_NewStock_NewStockIsAdded()
        {
            //arrange
            string stockName = "aaa";
            int quantity = 1;
            OwnedStock ownedStock = new OwnedStock(stockName, quantity);
            _user.MyPortfolio.MyStocks.Add(ownedStock);

            //act
            _user.AddStock(stockName, quantity);
            //assert
            _user.MyPortfolio.MyStocks.Find(s => s.Name == stockName).Should().NotBeNull();

            _user.MyPortfolio.MyStocks.Find(s => s.Name == stockName)?.QuantityHold.Should().Be(2);

        }
        [Test]
        public void AddStock_AddQuantity1_NewStockIsAdded()
        {
            //arrange
            string stockName = "aaa";
            int quantity = 1;

            //act
            _user.AddStock(stockName, quantity);
            //assert
            _user.MyPortfolio.MyStocks.Find(s => s.Name == stockName).Should().NotBeNull();

            _user.MyPortfolio.MyStocks.Find(s => s.Name == stockName)?.QuantityHold.Should().Be(1);
        }

        [Test]
        public void AddStock_QuantityToBeAddedIsNegative1_OwnedStockListWontChange()
        {
            //arrange
            string stockName = "aaa";
            int quantity = 1;
            OwnedStock ownedStock = new OwnedStock(stockName, quantity);
            _user.MyPortfolio.MyStocks.Add(ownedStock);
            int quantityToBeAdded = -1;
            //act
            _user.AddStock(stockName, quantityToBeAdded);
            //assert
            _user.MyPortfolio.MyStocks.Find(s => s.Name == stockName).Should().NotBeNull();
            _user.MyPortfolio.MyStocks.Find(s => s.Name == stockName)?.QuantityHold.Should().Be(1);
        }
        [Test]
        public void AddStock_QuantityToBeAddedIs0_OwnedStockListWontChange()
        {
            //arrange
            string stockName = "aaa";
            int quantity = 1;
            OwnedStock ownedStock = new OwnedStock(stockName, quantity);
            _user.MyPortfolio.MyStocks.Add(ownedStock);
            int quantityToBeAdded =0;
            //act
            _user.AddStock(stockName, quantityToBeAdded);
            //assert
            _user.MyPortfolio.MyStocks.Find(s => s.Name == stockName).Should().NotBeNull();
            _user.MyPortfolio.MyStocks.Find(s => s.Name == stockName)?.QuantityHold.Should().Be(1);
        }

        [Test]
        public void RemoveStock_StockNameIsNull_OwnedStockListWontChange()
        {
            //arrange
            string? stockName = null;
            int quantity = 1;
            //act
            _user.RemoveStock(stockName, quantity);
            //assert
            _user.MyPortfolio.MyStocks.Should().BeEmpty();

        }
        [Test]
        public void RemoveStock_StockNameIsEmpty_OwnedStockListWontChange()
        {
            //arrange
            string? stockName = String.Empty;
            int quantity = 1;
            //act
            _user.RemoveStock(stockName, quantity);
            //assert
            _user.MyPortfolio.MyStocks.Should().BeEmpty();

        }
        [Test]
        public void RemoveStock_CorrectInputStock_StockIsRemoved()
        {
            //arrange
            string stockName = "aaa";
            int quantity = 1;
            OwnedStock ownedStock = new OwnedStock(stockName, quantity);
            _user.MyPortfolio.MyStocks.Add(ownedStock);

            //act
            _user.RemoveStock(stockName, quantity);
            //assert
            _user.MyPortfolio.MyStocks.Find(s => s.Name == stockName).Should().NotBeNull();

            _user.MyPortfolio.MyStocks.Find(s => s.Name == stockName)?.QuantityHold.Should().Be(0);

        }
        [Test]
        public void RemoveStock_QuantityToBeRemovedIsNegative1_OwnedStockListWontChange()
        {
            //arrange
            string stockName = "aaa";
            int quantity = 1;
            OwnedStock ownedStock = new OwnedStock(stockName, quantity);
            _user.MyPortfolio.MyStocks.Add(ownedStock);
            int quantityToBeAdded = -1;
            //act
            _user.RemoveStock(stockName, quantityToBeAdded);
            //assert
            _user.MyPortfolio.MyStocks.Find(s => s.Name == stockName).Should().NotBeNull();
            _user.MyPortfolio.MyStocks.Find(s => s.Name == stockName)?.QuantityHold.Should().Be(1);
        }
        [Test]
        public void RemoveStock_QuantityToBeRemovedIs0_OwnedStockListWontChange()
        {
            //arrange
            string stockName = "aaa";
            int quantity = 1;
            OwnedStock ownedStock = new OwnedStock(stockName, quantity);
            _user.MyPortfolio.MyStocks.Add(ownedStock);
            int quantityToBeAdded = -1;
            //act
            _user.RemoveStock(stockName, quantityToBeAdded);
            //assert
            _user.MyPortfolio.MyStocks.Find(s => s.Name == stockName).Should().NotBeNull();
            _user.MyPortfolio.MyStocks.Find(s => s.Name == stockName)?.QuantityHold.Should().Be(1);
        }
    }
}
