using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Restaurant.Backend.Account.Controllers;
using Restaurant.Backend.Dto.Account;

namespace Restaurant.Backend.Account.Test
{
    public class CustomerControllerTest : BaseControllerTest<CustomerController>
    {
        [Test]
        public void LoginTest()
        {
            // Setup
            var controller = new CustomerController(Logger.Object, Config.Object, Mapper);

            // Act
            var result = controller.Login(new CustomerLoginDto()).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(typeof(OkObjectResult), result.GetType());
            Assert.IsNotEmpty($"{(result as OkObjectResult)?.Value}");

        }
    }
}
