using NUnit.Framework;
using Restaurant.Backend.Repositories.Repositories;
using System;
using System.Linq;
using System.Text;
using Restaurant.Backend.Entities.Entities;

namespace Restaurant.Backend.RepositoriesTest
{
    public class CustomerRepositoryTest : BaseRepositoryTest<CustomerRepository>
    {
        [Test]
        public void GetAllTest()
        {
            // Setup
            GenerateDbRecords(10);
            var repository = new CustomerRepository(Context, Logger.Object);

            // Act
            var result = repository.GetAll().Result;

            // Assert
            Assert.AreEqual(result.Count(), 10);
        }

        [Test]
        public void GetTest()
        {
            // Setup
            var id = Guid.NewGuid();
            GenerateDbRecord(id, "email@mail.com");
            var repository = new CustomerRepository(Context, Logger.Object);

            // Act
            var result = repository.GetById(id).Result;

            // Assert
            Assert.AreEqual(result.Id, id);
        }

        [Test]
        public void GetNotFoundTest()
        {
            // Setup
            var id = Guid.NewGuid();
            var repository = new CustomerRepository(Context, Logger.Object);

            // Act
            var result = repository.GetById(id).Result;

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void CreateTest()
        {
            // Setup
            var id = Guid.NewGuid();
            var model = new Customer
            {
                Id = id,
                FirstName = $"First Name for {id}",
                LastName = $"Last Name for {id}",
                Birthday = new DateTime(2020, 01, 01),
                CreationDate = DateTimeOffset.UtcNow.AddDays(1).AddHours(2).AddMinutes(3),
                ModificationDate = DateTimeOffset.MinValue,
                Email = "email@mail.com",
                Gender = (short)new Random().Next(1, 2),
                IdentificationNumber = new Random().Next(8000000, 1100000000),
                PasswordHash = Encoding.ASCII.GetBytes("PasswordHash"),
                PasswordSalt = Encoding.ASCII.GetBytes("PasswordSalt"),
                PhoneNumber = new Random().Next(300000000, 326000000).ToString(),
                VerifiedEmail = false,
                VerifiedPhoneNumber = false,
                Active = true,
                IdentificationType = new IdentificationType
                {
                    Id = id,
                    Name = $"Name for {id}",
                    Description = $"Description for {id}",
                    CreationDate = DateTimeOffset.UtcNow.AddDays(1).AddHours(2).AddMinutes(3),
                    ModificationDate = DateTimeOffset.MinValue
                }
            };
            var repository = new CustomerRepository(Context, Logger.Object);

            // Act
            var result = repository.Create(model).Result;
            var dbRecord = repository.GetById(model.Id).Result;

            // Assert
            Assert.AreEqual(2, result);
            Assert.AreEqual(dbRecord.FirstName, model.FirstName);
            Assert.AreEqual(dbRecord.LastName, model.LastName);
            Assert.AreEqual(dbRecord.Email, model.Email);
            Assert.AreEqual(dbRecord.CreationDate, model.CreationDate);
            Assert.AreEqual(dbRecord.ModificationDate, model.ModificationDate);

            _ = repository.Delete(model).Result;
        }

        [Test]
        public void CreateDuplicateEmailTest()
        {
            // Setup
            var id = Guid.NewGuid();
            var model1 = new Customer
            {
                Id = id,
                FirstName = $"First Name for {id}",
                LastName = $"Last Name for {id}",
                Birthday = new DateTime(2020, 01, 01),
                CreationDate = DateTimeOffset.UtcNow.AddDays(1).AddHours(2).AddMinutes(3),
                ModificationDate = DateTimeOffset.MinValue,
                Email = "email@mail.com",
                Gender = (short)new Random().Next(1, 2),
                IdentificationNumber = new Random().Next(8000000, 1100000000),
                PasswordHash = Encoding.ASCII.GetBytes("PasswordHash"),
                PasswordSalt = Encoding.ASCII.GetBytes("PasswordSalt"),
                PhoneNumber = new Random().Next(300000000, 326000000).ToString(),
                VerifiedEmail = false,
                VerifiedPhoneNumber = false,
                Active = true,
                IdentificationType = new IdentificationType
                {
                    Id = id,
                    Name = $"Name for {id}",
                    Description = $"Description for {id}",
                    CreationDate = DateTimeOffset.UtcNow.AddDays(1).AddHours(2).AddMinutes(3),
                    ModificationDate = DateTimeOffset.MinValue
                }
            };
            var model2 = new Customer
            {
                Id = id,
                FirstName = $"First Name for {id}",
                LastName = $"Last Name for {id}",
                Birthday = new DateTime(2020, 01, 01),
                CreationDate = DateTimeOffset.UtcNow.AddDays(1).AddHours(2).AddMinutes(3),
                ModificationDate = DateTimeOffset.MinValue,
                Email = "email@mail.com",
                Gender = (short)new Random().Next(1, 2),
                IdentificationNumber = new Random().Next(8000000, 1100000000),
                PasswordHash = Encoding.ASCII.GetBytes("PasswordHash"),
                PasswordSalt = Encoding.ASCII.GetBytes("PasswordSalt"),
                PhoneNumber = new Random().Next(300000000, 326000000).ToString(),
                VerifiedEmail = false,
                VerifiedPhoneNumber = false,
                Active = true,
                IdentificationType = new IdentificationType
                {
                    Id = id,
                    Name = $"Name for {id}",
                    Description = $"Description for {id}",
                    CreationDate = DateTimeOffset.UtcNow.AddDays(1).AddHours(2).AddMinutes(3),
                    ModificationDate = DateTimeOffset.MinValue
                }
            };
            var repository = new CustomerRepository(Context, Logger.Object);

            // Act
            var result1 = repository.Create(model1).Result;
            var result2 = repository.Create(model1).Result;
            
            // Assert
            Assert.AreEqual(0, result2);

            _ = repository.Delete(model1).Result;
        }

        [Test]
        public void UpdateTest()
        {
            // Setup
            var id = Guid.NewGuid();
            const string newName = "Updated Name";
            GenerateDbRecord(id, "email@mail.com");
            var repository = new CustomerRepository(Context, Logger.Object);
            var model = repository.GetById(id).Result;
            model.FirstName = newName;

            // Act
            var result = repository.Update(model).Result;
            var dbRecord = repository.GetById(model.Id).Result;

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(newName, dbRecord.FirstName);
        }

        [Test]
        public void DeleteTest()
        {
            // Setup
            var id = Guid.NewGuid();
            GenerateDbRecord(id, "email@mail.com");
            var repository = new CustomerRepository(Context, Logger.Object);
            var model = repository.GetById(id).Result;

            // Act
            var result = repository.Delete(model).Result;
            var dbRecord = repository.GetById(model.Id).Result;

            // Assert
            Assert.AreEqual(result, 1);
            Assert.IsNull(dbRecord);
        }

        #region Private methods

        private void GenerateDbRecords(int numberRecords)
        {
            for (int i = 0; i < numberRecords; i++)
            {
                var id = Guid.NewGuid();
                Context.Customers.Add(new Customer
                {
                    Id = id,
                    FirstName = $"First Name for {id}",
                    LastName = $"Last Name for {id}",
                    Birthday = new DateTime(2020, 01, 01),
                    CreationDate = DateTimeOffset.UtcNow.AddDays(1).AddHours(2).AddMinutes(3),
                    ModificationDate = DateTimeOffset.MinValue,
                    Email = $"email{id}@mail.com",
                    Gender = (short)new Random().Next(1, 2),
                    IdentificationNumber = new Random().Next(8000000, 1100000000),
                    PasswordHash = Encoding.ASCII.GetBytes("PasswordHash"),
                    PasswordSalt = Encoding.ASCII.GetBytes("PasswordSalt"),
                    PhoneNumber = new Random().Next(300000000, 326000000).ToString(),
                    VerifiedEmail = false,
                    VerifiedPhoneNumber = false,
                    Active = true,
                    IdentificationType = new IdentificationType
                    {
                        Id = id,
                        Name = $"Name for {id}",
                        Description = $"Description for {id}",
                        CreationDate = DateTimeOffset.UtcNow.AddDays(1).AddHours(2).AddMinutes(3),
                        ModificationDate = DateTimeOffset.MinValue
                    }
                });
            }

            Context.SaveChanges();
        }

        private void GenerateDbRecord(Guid id, string email)
        {
            Context.Customers.Add(new Customer
            {
                Id = id,
                FirstName = $"First Name for {id}",
                LastName = $"Last Name for {id}",
                Birthday = new DateTime(2020, 01, 01),
                CreationDate = DateTimeOffset.UtcNow.AddDays(1).AddHours(2).AddMinutes(3),
                ModificationDate = DateTimeOffset.MinValue,
                Email = email,
                Gender = (short)new Random().Next(1, 2),
                IdentificationNumber = new Random().Next(8000000, 1100000000),
                PasswordHash = Encoding.ASCII.GetBytes("PasswordHash"),
                PasswordSalt = Encoding.ASCII.GetBytes("PasswordSalt"),
                PhoneNumber = new Random().Next(300000000, 326000000).ToString(),
                VerifiedEmail = false,
                VerifiedPhoneNumber = false,
                Active = true,
                IdentificationType = new IdentificationType
                {
                    Id = id,
                    Name = $"Name for {id}",
                    Description = $"Description for {id}",
                    CreationDate = DateTimeOffset.UtcNow.AddDays(1).AddHours(2).AddMinutes(3),
                    ModificationDate = DateTimeOffset.MinValue
                }
            });

            Context.SaveChanges();
        }

        #endregion

    }
}
