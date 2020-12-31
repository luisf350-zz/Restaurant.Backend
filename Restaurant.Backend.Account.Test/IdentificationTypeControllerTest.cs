using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Restaurant.Backend.Account.Controllers;
using Restaurant.Backend.Domain.Contract;
using Restaurant.Backend.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Restaurant.Backend.Account.Test
{
    public class IdentificationTypeControllerTest
    {
        private Mock<ILogger<IdentificationTypeController>> _logger;
        private Mock<IIdentificationTypeDomain> _identificationTypeDomain;

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ILogger<IdentificationTypeController>>();
            _identificationTypeDomain = new Mock<IIdentificationTypeDomain>();
        }

        [Test]
        public void LoginTest()
        {
            // Setup
            var mockedResultList = new List<IdentificationType>
            {
                new IdentificationType
                {
                    Id = Guid.NewGuid(),
                    Name = "Passport",
                    CreationDate = DateTimeOffset.Now.AddDays(-1).AddHours(-2).AddMinutes(-3)
                }
            };

            _identificationTypeDomain.Setup(x =>
                x.GetAll(It.IsAny<Expression<Func<IdentificationType, bool>>>(), It.IsAny<Func<IQueryable<IdentificationType>, IOrderedQueryable<IdentificationType>>>(), It.IsAny<Expression<Func<IdentificationType, object>>[]>()))
                .ReturnsAsync(mockedResultList);
            var controller = new IdentificationTypeController(_logger.Object, _identificationTypeDomain.Object);

            // Act
            var result = controller.GetAll().Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(typeof(OkObjectResult), result.GetType());
            Assert.IsNotEmpty($"{(result as OkObjectResult)?.Value}");

        }
    }
}
