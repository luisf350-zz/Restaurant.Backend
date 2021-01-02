using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Restaurant.Backend.Account.Controllers;

namespace Restaurant.Backend.Account.Test
{
    public class CustomerControllerTest : BaseControllerTest<CustomerController>
    {
        [Test]
        public void GetAllTest()
        {
            // Setup
            var controller = new CustomerController(Logger.Object, Config.Object, Mapper, CustomerDomain);

            // Act
            var result = controller.GetAll().Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(typeof(OkObjectResult), result.GetType());
            Assert.IsNotEmpty($"{(result as OkObjectResult)?.Value}");
        }
    }
}
