using System;
using System.Collections.Generic;
using System.Linq;
using СonveyoR;

namespace NUnitTest
{
    public class SimpleServiceFactory
    {
        public SimpleServiceFactory(params Type[] handlerTypes)
        {
            ServiceFactoryExtensions.SetHandlerTypes(handlerTypes);
        }
        public IEnumerable<object> GetServices(Type contextType, Type entityType, Type payloadType = null, string processCase=null)
        {
            var types = ServiceFactoryExtensions.GetProcessServiceTypes(contextType, entityType, payloadType, processCase);

            if(!types.Any())
                yield break;

            foreach (var type in types)
            {
                var h = Activator.CreateInstance(type);
                yield return h;
            }
        }
    }
}
