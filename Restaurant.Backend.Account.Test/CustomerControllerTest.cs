﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Restaurant.Backend.Account.Controllers;
using Restaurant.Backend.Dto.Account;

namespace Restaurant.Backend.Account.Test
{
    public class CustomerControllerTest
    {
        private Mock<ILogger<CustomerController>> _logger;
        private Mock<IConfiguration> _config;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<CustomerController>>();
            _config = new Mock<IConfiguration>();
            _config.Setup(x => x.GetSection("AppSettings:Token").Value).Returns("SuperSecretKey2020");
        }

        [Test]
        public void LoginTest()
        {
            // Setup
            var controller = new CustomerController(_logger.Object, _config.Object);

            // Act
            var result = controller.Login(new CustomerLoginDto()).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(typeof(OkObjectResult), result.GetType());
            Assert.IsNotEmpty($"{(result as OkObjectResult)?.Value}");

        }
    }
}
