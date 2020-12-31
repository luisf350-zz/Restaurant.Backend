using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Restaurant.Backend.Entities.Context;
using Restaurant.Backend.Entities.Entities;
using Restaurant.Backend.Repositories.Repositories;
using System;
using System.Linq;

namespace Restaurant.Backend.RepositoriesTest
{
    public class IdentificationTypeRepositoryTest
    {
        private AppDbContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("Test")
                .Options;

            _context = new AppDbContext(options);
        }

        [Test]
        public void GetAllTest()
        {
            // Setup
            GenerateDbRecords(10);
            var repository = new IdentificationTypeRepository(_context);

            // Act
            var result = repository.GetAll().Result;

            // Assert
            Assert.AreEqual(result.Count(), 10);
        }

        private void GenerateDbRecords(int numberRecords)
        {
            for (int i = 0; i < numberRecords; i++)
            {
                var id = Guid.NewGuid();
                _context.IdentificationTypes.Add(new IdentificationType
                {
                    Id = id,
                    Name = $"Name for {id}",
                    Description = $"Description for {id}",
                    CreationDate = DateTimeOffset.UtcNow.AddDays(i).AddHours(i).AddMinutes(i)
                });
            }

            _context.SaveChanges();
        }
    }
}
