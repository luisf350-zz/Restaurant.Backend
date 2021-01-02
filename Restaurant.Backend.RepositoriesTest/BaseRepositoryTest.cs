using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Restaurant.Backend.Entities.Context;

namespace Restaurant.Backend.RepositoriesTest
{
    public class BaseRepositoryTest<T>
    {
        protected AppDbContext Context;
        protected Mock<ILogger<T>> Logger;

        [SetUp]
        public void Setup()
        {
            Logger = new Mock<ILogger<T>>();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Test")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            Context = new AppDbContext(options);
        }
    }
}