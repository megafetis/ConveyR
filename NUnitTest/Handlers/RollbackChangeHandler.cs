using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnitTest.Entities;
using СonveyoR;

namespace NUnitTest.Handlers
{
    [ProcessOrder(ProcessCase.RollbackProcess)]
    class RollbackChangeHandler:ProcessStepHandler<ChangeEntityContext, IHasFaledCount>
    {
        protected override Task Process(ChangeEntityContext context, IHasFaledCount entity)
        {
            entity.FailCount++;
            return Task.CompletedTask;
        }
    }
}
