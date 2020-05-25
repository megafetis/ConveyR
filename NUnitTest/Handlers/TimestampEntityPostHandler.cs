using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnitTest.Entities;
using СonveyoR;

namespace NUnitTest.Handlers
{
    [ProcessOrder(ProcessCase.PostProcess)]
    class TimestampEntityPostHandler:ProcessStepHandler<ChangeEntityContext,ITimestampedEntity>
    {
        protected override Task Process(ChangeEntityContext context, ITimestampedEntity entity)
        {
            entity.Timestamp = DateTime.UtcNow;
            return Task.CompletedTask;
        }
    }
}
