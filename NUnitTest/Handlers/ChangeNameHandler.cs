using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnitTest.Entities;
using NUnitTest.Payloads;
using СonveyoR;

namespace NUnitTest.Handlers
{
    class ChangeNameHandler:ProcessStepHandler<ChangeEntityContext, IHasName,IHasNamePayload>
    {
        protected override Task Process(ChangeEntityContext context, IHasName entity, IHasNamePayload payload)
        {
            if(payload.Name==null)
                throw new ArgumentNullException("Name","Entity name must be named");
            entity.Name = payload.Name;
            return Task.CompletedTask;
        }
    }
}
