using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConveyR
{
    public class Conveyor:IConveyor
    {

        private readonly ServiceFactory _serviceFactory;

        public Conveyor(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task Process<TContext>(TContext context, object entity, object payload = null, string group=null, CancellationToken cancellationToken = default) 
            where TContext : class
        {
            foreach (var service in _serviceFactory
                .GetInstances<TContext>( entity, payload, group))
            {
                try
                {
                    if(!cancellationToken.IsCancellationRequested)
                        await service.Process(context, entity, payload, cancellationToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public IEnumerable<IProcessHandler<TContext>> GetProcessHandlers<TContext>(object entity, object payload = null, string group = null) where TContext : class
        {
            return _serviceFactory.GetInstances<TContext>(entity, payload, group);
        }
    }
}
