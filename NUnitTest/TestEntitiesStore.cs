﻿using System.Threading.Tasks;
using ConveyR;
using NUnitTest.Entities;

namespace NUnitTest
{
    public class TestEntitiesStore
    {
        private readonly IConveyor _conveyor;

        public TestEntitiesStore(IConveyor conveyor)
        {
            _conveyor = conveyor;
        }
        public async Task ChangeEntity(IEntity entity, object payload)
        {
            await _conveyor.Process(this, entity, payload);
        }

        public async Task ChangeEntityManual(IEntity entity, object payload)
        {
            foreach (var processHandler in _conveyor.GetProcessHandlers<TestEntitiesStore>(entity,payload))
            {
                await processHandler.Process(this, entity, payload);
            }
        }

        public async Task AfterChangeEntity(IEntity entity, object payload = null)
        {
            await _conveyor.Process(this, entity, payload,"after");
        }

        public async Task RollbackChangeEntitiy(IEntity entity, object payload = null)
        {
            await _conveyor.Process(this, entity, payload,"rollback");
        }
    }
}
