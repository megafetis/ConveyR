using System;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnitTest.Entities;
using NUnitTest.Handlers;
using NUnitTest.Payloads;
using ÑonveyoR;

namespace NUnitTest
{
    public class ChangeEntityTest
    {
        private IConveyor _conveyor;

        [SetUp]
        public void Setup()
        {
            var testFactory = new SimpleServiceFactory(typeof(ChangeNameHandler),typeof(ChangeDescriptionHandler),typeof(GenerateIdHandler),typeof(TimestampEntityPostHandler),typeof(RollbackChangeHandler));
            _conveyor = new Conveyor(testFactory.GetServices);
        }

        [Test]
        public async Task ChangeTestEntityWithDescriptionTest()
        {
            var entity = new TestEntity();
            var payload = new ChangeTestEntityPayload()
            {
                Name = "Name 1",
                Description = "Description 1"
            };

            Assert.Null(entity.Id);
            Assert.Null(entity.Name);
            Assert.Null(entity.Description);
            var changeContext = new ChangeEntityContext(_conveyor);
            
            await changeContext.ChangeEntity(entity, payload);
            
            Assert.AreEqual("Name 1",entity.Name);
            Assert.NotNull(entity.Id);
            Assert.AreEqual("Description 1", entity.Description);

        }

        [Test]
        public async Task ChangeTestEntityPostWithprocessTest()
        {
            var entity = new TestEntity();
            var payload = new ChangeTestEntityPayload()
            {
                Name = "Name 1",
                Description = "Description 1"
            };

            Assert.Null(entity.Id);
            Assert.Null(entity.Name);
            Assert.Null(entity.Description);
            var changeContext = new ChangeEntityContext(_conveyor);

            await changeContext.ChangeEntity(entity, payload);

            Assert.AreEqual("Name 1", entity.Name);
            Assert.NotNull(entity.Id);
            Assert.AreEqual("Description 1", entity.Description);

            Assert.Null(entity.Timestamp);

            await changeContext.AfterChangeEntity(entity, payload);

            Assert.NotNull(entity.Timestamp);
        }

        [Test]
        public async Task ChangeTestEntityWithRollbackTest()
        {
            var entity = new TestEntity();
            var payload = new ChangeTestEntityPayload()
            {
                //Name = "Name 1",
                Description = "Description 1"
            };

            Assert.Null(entity.Id);
            Assert.Null(entity.Name);
            Assert.Null(entity.Description);
            var changeContext = new ChangeEntityContext(_conveyor);

            try
            {
                await changeContext.ChangeEntity(entity, payload);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await changeContext.RollbackChangeEntitiy(entity, payload);
                Assert.Greater(entity.FailCount,0);
                return;
            }

            Assert.AreEqual("Name 1", entity.Name);
            Assert.NotNull(entity.Id);
            Assert.AreEqual("Description 1", entity.Description);

            Assert.Null(entity.Timestamp);

            await changeContext.AfterChangeEntity(entity, payload);

            Assert.NotNull(entity.Timestamp);
        }





        [Test]
        public async Task ChangeTestEntityWithoutDescriptionTest()
        {
            var entity = new TestEntity1();
            var payload = new ChangeTestEntityPayload()
            {
                Name = "Name 1",
                Description = "Description 1"
            };

            Assert.Null(entity.Id);
            Assert.Null(entity.Name);
            Assert.Null(entity.Description);
            var changeContext = new ChangeEntityContext(_conveyor);

            await changeContext.ChangeEntity(entity, payload);

            Assert.AreEqual("Name 1", entity.Name);
            Assert.NotNull(entity.Id);
            Assert.Null(entity.Description);

        }
    }
}