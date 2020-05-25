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
        public IEnumerable<object> GetServices(Type contextType, ProcessCase processCase, Type entityType, Type payloadType = null)
        {
            var types = ServiceFactoryExtensions.GetProcessServiceTypes(contextType, processCase, entityType, payloadType);

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
