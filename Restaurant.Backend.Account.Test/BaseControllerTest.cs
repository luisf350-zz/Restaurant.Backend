using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Restaurant.Backend.CommonApi.Profiles;
using Restaurant.Backend.Domain.Contract;
using Restaurant.Backend.Domain.Implementation;
using Restaurant.Backend.Repositories.Repositories;

namespace Restaurant.Backend.Account.Test
{
    public class BaseControllerTest<T>
    {
        protected Mock<ILogger<T>> Logger;
        protected Mock<IIdentificationTypeRepository> IdentificationTypeRepository;
        protected Mock<IConfiguration> Config;
        protected IIdentificationTypeDomain IdentificationTypeDomain;
        protected IMapper Mapper;

        [SetUp]
        public void Setup()
        {
            Logger = new Mock<ILogger<T>>();
            IdentificationTypeRepository = new Mock<IIdentificationTypeRepository>();
            Config = new Mock<IConfiguration>();

            Config.Setup(x => x.GetSection("AppSettings:Token").Value).Returns("SuperSecretKey2020");
            IdentificationTypeDomain = new IdentificationTypeDomain(IdentificationTypeRepository.Object);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            Mapper = config.CreateMapper();
        }
    }
}
