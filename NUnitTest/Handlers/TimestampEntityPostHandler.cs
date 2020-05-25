using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnitTest.Entities;
using СonveyoR;

namespace NUnitTest.Handlers
{
    [ProcessConfig(Group = "after")]
    class TimestampEntityPostHandler:AbstractProcessHandler<TestEntitiesStore,ITimestampedEntity>
    {
        protected override Task Process(TestEntitiesStore context, ITimestampedEntity entity)
        {
            entity.Timestamp = DateTime.UtcNow;
            return Task.CompletedTask;
        }
    }
}
