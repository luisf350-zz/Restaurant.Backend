using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Restaurant.Backend.Account.Controllers;
using Restaurant.Backend.Dto.Entities;
using Restaurant.Backend.Entities.Entities;
using System;


namespace Restaurant.Backend.Account.Test
{
    public class IdentificationTypeControllerTest : BaseControllerTest<IdentificationTypeController>
    {
        [Test]
        public void GetAllTest()
        {
            // Setup
            var controller = new IdentificationTypeController(Logger.Object, IdentificationTypeDomain, Mapper);

            // Act
            var result = controller.GetAll().Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(typeof(OkObjectResult), result.GetType());
            Assert.IsNotEmpty($"{(result as OkObjectResult)?.Value}");
        }

        [Test]
        public void GetTest()
        {
            // Setup
            var mockedResult = new IdentificationType
            {
                Id = Guid.NewGuid(),
                Name = "Passport",
                CreationDate = DateTimeOffset.Now.AddDays(-1).AddHours(-2).AddMinutes(-3)
            };

            IdentificationTypeRepository.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(mockedResult);
            var controller = new IdentificationTypeController(Logger.Object, IdentificationTypeDomain, Mapper);

            // Act
            var result = controller.Get(Guid.NewGuid()).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(typeof(OkObjectResult), result.GetType());
            Assert.IsNotEmpty($"{(result as OkObjectResult)?.Value}");
        }

        [Test]
        public void CreateTest()
        {
            // Setup
            var modelDto = new IdentificationTypeDto
            {
                Id = Guid.NewGuid(),
                Name = "Passport",
                Description = string.Empty
            };

            IdentificationTypeRepository.Setup(x => x.Create(It.IsAny<IdentificationType>())).ReturnsAsync(1);
            var controller = new IdentificationTypeController(Logger.Object, IdentificationTypeDomain, Mapper);

            // Act
            var result = controller.Create(modelDto).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(typeof(CreatedResult), result.GetType());
            Assert.IsNotEmpty($"{(result as CreatedResult)?.Value}");
        }

        [Test]
        public void UpdateNotFoundTest()
        {
            // Setup
            var modelDto = new IdentificationTypeDto
            {
                Id = Guid.NewGuid(),
                Name = "Passport",
                Description = string.Empty
            };

            var mockedModel = new IdentificationType
            {
                Id = Guid.NewGuid(),
                Name = "Passport",
                Description = string.Empty
            };

            IdentificationTypeRepository.Setup(x => x.Update(mockedModel)).ReturnsAsync(false);
            var controller = new IdentificationTypeController(Logger.Object, IdentificationTypeDomain, Mapper);

            // Act
            var result = controller.Update(modelDto).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(typeof(NotFoundObjectResult), result.GetType());
            Assert.IsNotEmpty($"{(result as NotFoundObjectResult)?.Value}");
        }

        [Test]
        public void UpdateTest()
        {
            // Setup
            var modelDto = new IdentificationTypeDto
            {
                Id = Guid.NewGuid(),
                Name = "Passport",
                Description = string.Empty
            };

            var mockedModel = new IdentificationType
            {
                Id = Guid.NewGuid(),
                Name = "Passport",
                Description = string.Empty
            };

            IdentificationTypeRepository.Setup(x => x.Update(mockedModel)).ReturnsAsync(true);
            var controller = new IdentificationTypeController(Logger.Object, IdentificationTypeDomain, Mapper);

            // Act
            var result = controller.Update(modelDto).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(typeof(NotFoundObjectResult), result.GetType());
            Assert.IsNotEmpty($"{(result as NotFoundObjectResult)?.Value}");
        }

        [Test]
        public void DeleteNotFoundTest()
        {
            // Setup
            var modelDto = new IdentificationTypeDto
            {
                Id = Guid.NewGuid(),
                Name = "Passport",
                Description = string.Empty
            };

            IdentificationTypeRepository.Setup(x => x.Delete(It.IsAny<IdentificationType>())).ReturnsAsync(0);
            var controller = new IdentificationTypeController(Logger.Object, IdentificationTypeDomain, Mapper);

            // Act
            var result = controller.Delete(modelDto).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(typeof(NotFoundObjectResult), result.GetType());
            Assert.IsNotEmpty($"{(result as NotFoundObjectResult)?.Value}");
        }

        [Test]
        public void DeletedTest()
        {
            // Setup
            var modelDto = new IdentificationTypeDto
            {
                Id = Guid.NewGuid(),
                Name = "Passport",
                Description = string.Empty
            };

            var mockedModel = new IdentificationType
            {
                Id = Guid.NewGuid(),
                Name = "Passport",
                Description = string.Empty
            };

            IdentificationTypeRepository.Setup(x => x.GetById(modelDto.Id)).ReturnsAsync(mockedModel);
            IdentificationTypeRepository.Setup(x => x.Delete(It.IsAny<IdentificationType>())).ReturnsAsync(1);
            var controller = new IdentificationTypeController(Logger.Object, IdentificationTypeDomain, Mapper);

            // Act
            var result = controller.Delete(modelDto).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreSame(typeof(OkObjectResult), result.GetType());
            Assert.IsNotEmpty($"{(result as OkObjectResult)?.Value}");
        }
    }
}
