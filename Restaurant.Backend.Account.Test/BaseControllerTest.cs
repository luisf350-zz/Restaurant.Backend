using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Restaurant.Backend.CommonApi.Base;
using Restaurant.Backend.CommonApi.Profiles;
using Restaurant.Backend.Domain.Contract;
using Restaurant.Backend.Domain.Implementation;
using Restaurant.Backend.Entities.Context;
using Restaurant.Backend.Repositories.Repositories;

namespace Restaurant.Backend.Account.Test
{
    public class BaseControllerTest<TController>
        where TController : BaseController
    {
        protected Mock<ILogger<TController>> LoggerController;
        protected ICustomerRepository CustomerRepository;
        protected IIdentificationTypeRepository IdentificationTypeRepository;
        protected Mock<IConfiguration> Config;
        protected ICustomerDomain CustomerDomain;
        protected IIdentificationTypeDomain IdentificationTypeDomain;
        protected IMapper Mapper;
        protected AppDbContext Context;

        protected Mock<ILogger<CustomerRepository>> CustomerRepositoryLogger;
        protected Mock<ILogger<IdentificationTypeRepository>> IdentificationTypeRepositoryLogger;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Test")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            Context = new AppDbContext(options);

            LoggerController = new Mock<ILogger<TController>>();
            Config = new Mock<IConfiguration>(); 
            Config.Setup(x => x.GetSection("AppSettings:Token").Value).Returns("SuperSecretKey2020");

            // Logger for Repositories
            CustomerRepositoryLogger = new Mock<ILogger<CustomerRepository>>();
            IdentificationTypeRepositoryLogger = new Mock<ILogger<IdentificationTypeRepository>>();

            // Repositories
            CustomerRepository = new CustomerRepository(Context, CustomerRepositoryLogger.Object);
            IdentificationTypeRepository = new IdentificationTypeRepository(Context, IdentificationTypeRepositoryLogger.Object);

            // Domains
            CustomerDomain = new CustomerDomain(CustomerRepository);
            IdentificationTypeDomain = new IdentificationTypeDomain(IdentificationTypeRepository);

            // AutoMapper
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()).CreateMapper();
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var customer in CustomerRepository.GetAll().Result)
            {
                CustomerRepository.Delete(customer);
            }

            foreach (var customer in IdentificationTypeRepository.GetAll().Result)
            {
                IdentificationTypeRepository.Delete(customer);
            }
        }
    }
}
