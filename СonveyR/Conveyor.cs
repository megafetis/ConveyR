using System;
using System.Threading;
using System.Threading.Tasks;

namespace СonveyoR
{
    public class Conveyor:IConveyor
    {

        private readonly ServiceFactory _serviceFactory;

        public Conveyor(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task Process<TContext>(TContext context,ProcessCase workflowCase, object entity, object payload = null, CancellationToken cancellationToken = default) 
            where TContext : class
        {
            foreach (var service in _serviceFactory
                .GetInstances<TContext>(workflowCase, entity, payload))
            {
                try
                {
                    if(!cancellationToken.IsCancellationRequested)
                        await service.Process(context, entity, payload);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
