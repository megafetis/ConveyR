using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnitTest.Entities;
using СonveyoR;

namespace NUnitTest.Handlers
{
    [ProcessConfig("rollback")]
    class RollbackChangeHandler:AbstractProcessHandler<TestEntitiesStore, IHasFaledCount>
    {
        protected override Task Process(TestEntitiesStore context, IHasFaledCount entity)
        {
            entity.FailCount++;
            return Task.CompletedTask;
        }
    }
}
