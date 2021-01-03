using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Restaurant.Backend.Entities.Context;
using Restaurant.Backend.Repositories.Repositories;

namespace Restaurant.Backend.RepositoriesTest
{
    public class BaseRepositoryTest
    {
        protected AppDbContext Context;
        protected CustomerRepository CustomerRepository;
        protected IdentificationTypeRepository IdentificationTypeRepository;

        protected Mock<ILogger<CustomerRepository>> CustomerRepositoryLogger;
        protected Mock<ILogger<IdentificationTypeRepository>> IdentificationTypeRepositoryLogger;

        [SetUp]
        public void Setup()
        {
            CustomerRepositoryLogger = new Mock<ILogger<CustomerRepository>>();
            IdentificationTypeRepositoryLogger = new Mock<ILogger<IdentificationTypeRepository>>();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Test")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            Context = new AppDbContext(options);

            CustomerRepository = new CustomerRepository(Context, CustomerRepositoryLogger.Object);
            IdentificationTypeRepository = new IdentificationTypeRepository(Context, IdentificationTypeRepositoryLogger.Object);
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var customer in CustomerRepository.GetAll().Result)
            {
                _ = CustomerRepository.Delete(customer);
            }

            foreach (var identificationType in IdentificationTypeRepository.GetAll().Result)
            {
                _ = IdentificationTypeRepository.Delete(identificationType);
            }
        }

    }
}