using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConveyR;
using NUnitTest.Entities;

namespace NUnitTest.Handlers
{
    [ProcessConfig(Group = "after")]
    class TimestampEntityPostHandler:AbstractProcessHandler<TestEntitiesStore,ITimestampedEntity>
    {
        protected override Task Process(TestEntitiesStore context, ITimestampedEntity entity, CancellationToken cancellationToken = default)
        {
            entity.Timestamp = DateTime.UtcNow;
            return Task.CompletedTask;
        }
    }
}
