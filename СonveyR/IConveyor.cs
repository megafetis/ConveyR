using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace СonveyoR
{
    public interface IConveyor
    {
        Task Process<TContext>(TContext context, object entity, object payload = null, string workflowCase=null,
            CancellationToken cancellationToken = default)
            where TContext : class;
    }
}
