using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace СonveyoR
{
    public delegate IEnumerable<object> ServiceFactory(Type contextType, ProcessCase processCase, Type entityType,
        Type payloadType = null);
     
    public static class ServiceFactoryExtensions
    {
        private static readonly ConcurrentDictionary<string, Type[]> HandlersPerContext = new ConcurrentDictionary<string, Type[]>();

        public static void SetHandlerTypes(IEnumerable<Type> handlerTypes)
        {
            _allHandlerTypes = handlerTypes.ToArray();
        }

        private static Type[] _allHandlerTypes;
        public static IEnumerable<IProcessStepHandler<TContext>> GetInstances<TContext>(this ServiceFactory factory,
            ProcessCase processCase, object entity, object payload = null)
            where TContext : class
        {

            var results = factory(typeof(TContext), processCase, entity.GetType(), payload?.GetType());
            return results.Cast<IProcessStepHandler<TContext>>();
        }

        public static Type[] GetProcessServiceTypes(Type contextType,ProcessCase processCase, Type entityType, Type payloadType = null)
        {
            var key = $"{contextType.Name}_{processCase:G}_{entityType.Name}_{(payloadType != null ? payloadType.Name : string.Empty) }";

            return HandlersPerContext.GetOrAdd(key, (strKey) =>
                _allHandlerTypes.Where(p =>
                    ((p.BaseType.GetGenericArguments().Length == 2 &&
                      p.BaseType.GetGenericArguments()[0] == contextType &&
                      entityType.InheritsOrImplements(p.BaseType.GetGenericArguments()[1])) || (payloadType != null && p.BaseType.GetGenericArguments().Length == 3 &&
                                                                                                p.BaseType.GetGenericArguments()[0] == contextType &&
                                                                                                entityType.InheritsOrImplements(p.BaseType.GetGenericArguments()[1]) &&
                                                                                                payloadType.InheritsOrImplements(p.BaseType.GetGenericArguments()[2])))
                    && DefaultOrder(p).ProcessCase == processCase
                    )
                    .OrderBy(p => DefaultOrder(p).Order)
                    .ToArray()
            );
        }

        private static ProcessOrderAttribute DefaultOrder(Type handlerType)
        {
            return handlerType.GetCustomAttribute<ProcessOrderAttribute>() ?? new ProcessOrderAttribute(ProcessCase.PreProcess, 0);
        }

        #region InheritsOrImplements

        public static bool InheritsOrImplements(this Type child, Type parent)
        {
            parent = ResolveGenericTypeDefinition(parent);

            var currentChild = child.IsGenericType
                ? child.GetGenericTypeDefinition()
                : child;

            while (currentChild != typeof(object))
            {
                if (parent == currentChild || HasAnyInterfaces(parent, currentChild))
                    return true;

                currentChild = currentChild.BaseType != null
                               && currentChild.BaseType.IsGenericType
                    ? currentChild.BaseType.GetGenericTypeDefinition()
                    : currentChild.BaseType;

                if (currentChild == null)
                    return false;
            }
            return false;
        }

        private static Type ResolveGenericTypeDefinition(Type parent)
        {
            var shouldUseGenericType = true;
            if (parent.IsGenericType && parent.GetGenericTypeDefinition() != parent)
                shouldUseGenericType = false;

            if (parent.IsGenericType && shouldUseGenericType)
                parent = parent.GetGenericTypeDefinition();
            return parent;
        }

        private static bool HasAnyInterfaces(Type parent, Type child)
        {
            return child.GetInterfaces()
                .Any(childInterface =>
                {
                    var currentInterface = childInterface.IsGenericType
                        ? childInterface.GetGenericTypeDefinition()
                        : childInterface;

                    return currentInterface == parent;
                });
        }

        #endregion
    }
}
