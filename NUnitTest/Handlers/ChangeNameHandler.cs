using System;
using System.Threading;
using System.Threading.Tasks;
using ConveyR;
using NUnitTest.Entities;
using NUnitTest.Payloads;

namespace NUnitTest.Handlers
{
    class ChangeNameHandler:AbstractProcessHandler<TestEntitiesStore, IHasName,IHasNamePayload>
    {
        protected override Task Process(TestEntitiesStore context, IHasName entity, IHasNamePayload payload,CancellationToken cancellationToken = default)
        {
            if(payload.Name==null)
                throw new ArgumentNullException("Name","Entity name must be named");
            entity.Name = payload.Name;
            return Task.CompletedTask;
        }
    }
}
