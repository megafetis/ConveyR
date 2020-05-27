using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConveyR;
using NUnitTest.Entities;
using NUnitTest.Payloads;

namespace NUnitTest.Handlers
{
    class ChangeDescriptionHandler: AbstractProcessHandler<TestEntitiesStore, IHasDescription,IHasDescriptionPayload>
    {
        protected override Task Process(TestEntitiesStore context, IHasDescription entity, IHasDescriptionPayload payload, CancellationToken cancellationToken = default)
        {
            entity.Description = payload.Description;
            return Task.CompletedTask;
        }
    }
}
