using NUnit.Framework;
using Restaurant.Backend.Entities.Entities;
using Restaurant.Backend.Repositories.Repositories;
using System;
using System.Linq;

namespace Restaurant.Backend.RepositoriesTest
{
    public class IdentificationTypeRepositoryTest : BaseRepositoryTest<IdentificationTypeRepository>
    {
        [Test]
        public void GetAllTest()
        {
            // Setup
            GenerateDbRecords(10);
            var repository = new IdentificationTypeRepository(Context, Logger.Object);

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
            GenerateDbRecord(id, $"Name for {id}", $"Description for {id}");
            var repository = new IdentificationTypeRepository(Context, Logger.Object);

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
            var repository = new IdentificationTypeRepository(Context, Logger.Object);

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
            var model = new IdentificationType
            {
                Id = id,
                Name = $"Name for {id}",
                Description = $"Description for {id}",
                CreationDate = DateTimeOffset.UtcNow.AddDays(1).AddHours(2).AddMinutes(3)
            };
            var repository = new IdentificationTypeRepository(Context, Logger.Object);

            // Act
            var result = repository.Create(model).Result;
            var dbRecord = repository.GetById(model.Id).Result;

            // Assert
            Assert.AreEqual(result, 1);
            Assert.AreEqual(dbRecord.Name, model.Name);
            Assert.AreEqual(dbRecord.Description, model.Description);
            Assert.AreEqual(dbRecord.CreationDate, model.CreationDate);
            Assert.AreEqual(dbRecord.ModificationDate, model.ModificationDate);

            _ = repository.Delete(model).Result;
        }

        [Test]
        public void UpdateTest()
        {
            // Setup
            var id = Guid.NewGuid();
            const string newName = "Updated Name";
            GenerateDbRecord(id, $"Name for {id}", $"Description for {id}");
            var repository = new IdentificationTypeRepository(Context, Logger.Object);
            var model = repository.GetById(id).Result;
            model.Name = newName;
            model.Description = string.Empty;

            var oldModificationDate = model.ModificationDate;

            // Act
            var result = repository.Update(model).Result;
            var dbRecord = repository.GetById(model.Id).Result;

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(dbRecord.Name, newName);
            Assert.AreEqual(dbRecord.Description, string.Empty);
            Assert.AreEqual(dbRecord.CreationDate, model.CreationDate);
            Assert.AreNotEqual(dbRecord.ModificationDate, oldModificationDate);
        }

        [Test]
        public void DeleteTest()
        {
            // Setup
            var id = Guid.NewGuid();
            GenerateDbRecord(id, $"Name for {id}", $"Description for {id}");
            var repository = new IdentificationTypeRepository(Context, Logger.Object);
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
                Context.IdentificationTypes.Add(new IdentificationType
                {
                    Id = id,
                    Name = $"Name for {id}",
                    Description = $"Description for {id}",
                    CreationDate = DateTimeOffset.UtcNow.AddDays(i).AddHours(i).AddMinutes(i),
                    ModificationDate = DateTimeOffset.MinValue
                });
            }

            Context.SaveChanges();
        }

        private void GenerateDbRecord(Guid id, string name, string description)
        {
            Context.IdentificationTypes.Add(new IdentificationType
            {
                Id = id,
                Name = name,
                Description = description,
                CreationDate = DateTimeOffset.UtcNow.AddDays(1).AddHours(2).AddMinutes(3),
                ModificationDate = DateTimeOffset.MinValue
            });

            Context.SaveChanges();
        }

        #endregion

    }
}
