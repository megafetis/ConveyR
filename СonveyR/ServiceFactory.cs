using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UTypeExtensions;


namespace ConveyR
{
    public delegate IEnumerable<object> ServiceFactory(Type contextType, Type entityType,
        Type payloadType = null, string processCase=null);
     
    public static class ServiceFactoryExtensions
    {
        private static readonly ConcurrentDictionary<string, Type[]> HandlersPerContext = new ConcurrentDictionary<string, Type[]>();

        public static void SetHandlerTypes(IEnumerable<Type> handlerTypes)
        {
            _allHandlerTypes = handlerTypes.ToArray();
        }

        private static Type[] _allHandlerTypes;
        public static IEnumerable<IProcessHandler<TContext>> GetInstances<TContext>(this ServiceFactory factory,
            object entity, object payload = null, string processCase=null)
            where TContext : class
        {
            var results = factory(typeof(TContext), entity.GetType(), payload?.GetType(), processCase);
            return results.Cast<IProcessHandler<TContext>>();
        }
        public static IEnumerable<IProcessHandler<TContext>> GetInstances<TContext>(this ServiceFactory factory,
            Type entityType, Type payloadType = null, string processCase=null)
            where TContext : class
        {
            var results = factory(typeof(TContext), entityType, payloadType, processCase);
            return results.Cast<IProcessHandler<TContext>>();
        }

        public static Type[] GetProcessServiceTypes(Type contextType, Type entityType, Type payloadType = null, string group = null)
        {
            var key = $"{contextType.Name}_{group}_{entityType.Name}_{(payloadType != null ? payloadType.Name : string.Empty) }";

            return HandlersPerContext.GetOrAdd(key, (strKey) =>
                _allHandlerTypes.Where(p =>
                        contextType.InheritsOrImplements(p.BaseType.GetGenericArguments()[0]) && DefaultOrder(p).Group == group &&
                        (
                        // Two generic arguments
                        (p.BaseType.GetGenericArguments().Length == 2 && entityType.InheritsOrImplements(p.BaseType.GetGenericArguments()[1]))
                        ||
                        // Three generic arguments
                        (payloadType != null && p.BaseType.GetGenericArguments().Length == 3 && entityType.InheritsOrImplements(p.BaseType.GetGenericArguments()[1]) && payloadType.InheritsOrImplements(p.BaseType.GetGenericArguments()[2]))
                        )
                    ).OrderBy(p => DefaultOrder(p).Order).ToArray()
            );
        }

        private static ProcessConfigAttribute _defaultAttr = new ProcessConfigAttribute();
        private static ProcessConfigAttribute DefaultOrder(Type handlerType)
        {
            return handlerType.GetCustomAttribute<ProcessConfigAttribute>() ?? _defaultAttr;
        }

    }
}
