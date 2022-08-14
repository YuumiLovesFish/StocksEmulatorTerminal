using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YLF.StocksEmulator.Repository.Users;
using YLF.StocksEmulator.Services;
using FluentAssertions;
using YLF.StocksEmulator.Repository.Stocks;

namespace YLF.StocksEmulator.ServicesTest
{
    public class UserServiceTest
    {
        [Test]
        public void Login_NameIsNull_UserIsNull()
        {
            //arrange
            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            UserService userService = new UserService(userRepoMock.Object);
            string userString = null;
            //act
            userService.Login(userString);
            //assert
            userService.User.Should().BeNull();
        
        }
        [Test]
        public void Login_NameIsEmpty_UserIsNull()
        {
            //arrange
            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            UserService userService = new UserService(userRepoMock.Object);
            string userString = string.Empty;
            //act
            userService.Login(userString);
            //assert
            userService.User.Should().BeNull();

        }

        [Test]
        public void Login_UserDoesntExist_CreateNewUser()
        {
            //arrange
            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(f => f.IfUserExist(It.IsAny<string>())).Returns(false);
            UserService userService = new UserService(userRepoMock.Object);
            string userName = Guid.NewGuid().ToString();
            //act
            userService.Login(userName);
            //assert
            userService.User.Name.Should().Be(userName);
            userService.User.MyPortfolio.AccountBalance.Should().Be(10000m);
            userService.User.MyPortfolio.MyStocks.Should().BeEmpty();

        }
        [Test]
        public void Login_UserExist_GetUser()
        {
            //arrange
            string stockName = "aaa";
            int originalStockOwnCount = 1;
            OwnedStock ownedStock = new OwnedStock(stockName, originalStockOwnCount);
            List<OwnedStock> listOfOwnedStock = new List<OwnedStock>();
            listOfOwnedStock.Add(ownedStock);

            decimal accountBalance = 100m;
            Portfolio portfolio = new Portfolio(accountBalance, listOfOwnedStock);
            string name = "poppy";
            User user = new User(name, portfolio);
           



            Mock<IUserRepository> userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(f => f.IfUserExist(It.IsAny<string>())).Returns(true);
            userRepoMock.Setup(f => f.GetUser(It.IsAny<string>())).Returns(user);
            UserService userService = new UserService(userRepoMock.Object);
            string userName = user.Name;
            //act
            userService.Login(userName);
            //assert
            userService.User.Name.Should().Be(user.Name);
            userService.User.MyPortfolio.AccountBalance.Should().Be(user.MyPortfolio.AccountBalance);
            userService.User.MyPortfolio.MyStocks.Find(s => s.Name == stockName)?.QuantityHold.Should().Be(originalStockOwnCount);

        }

    }
}
