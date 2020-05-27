using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConveyR;
using NUnitTest.Entities;

namespace NUnitTest.Handlers
{
    [ProcessConfig(Group = "rollback")]
    class RollbackChangeHandler:AbstractProcessHandler<TestEntitiesStore, IHasFaledCount>
    {
        protected override Task Process(TestEntitiesStore context, IHasFaledCount entity, CancellationToken cancellationToken = default)
        {
            entity.FailCount++;
            return Task.CompletedTask;
        }
    }
}
