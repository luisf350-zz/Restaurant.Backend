using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Restaurant.Backend.CommonApi.Profiles;
using Restaurant.Backend.Domain.Contract;

namespace Restaurant.Backend.Account.Test
{
    public class BaseControllerTest<T>
    {
        protected Mock<ILogger<T>> Logger;
        protected Mock<IIdentificationTypeDomain> IdentificationTypeDomain;
        protected Mock<IConfiguration> Config;
        protected IMapper Mapper;

        [SetUp]
        public void Setup()
        {
            Logger = new Mock<ILogger<T>>();
            IdentificationTypeDomain = new Mock<IIdentificationTypeDomain>();
            Config = new Mock<IConfiguration>();
            Config.Setup(x => x.GetSection("AppSettings:Token").Value).Returns("SuperSecretKey2020");

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            Mapper = config.CreateMapper();
        }
    }
}
