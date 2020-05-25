using System.Threading.Tasks;

namespace СonveyoR
{
    /// <summary>
    /// Abstract handler class for comfortable creating of your own handlers
    /// </summary>
    /// <typeparam name="TProcessContext">Any context object</typeparam>
    /// <typeparam name="TEntity">Entity to handle</typeparam>
    public abstract class AbstractProcessHandler<TProcessContext, TEntity> : IProcessHandler<TProcessContext> where TProcessContext : class where TEntity : class
    {
        public Task Process(TProcessContext context, params object[] args)
        {
            return Process(context, (TEntity)args[0]);
        }

        protected abstract Task Process(TProcessContext context, TEntity entity);

    }
    /// <summary>
    /// Abstract handler class for comfortable creating of your own handlers
    /// </summary>
    /// <typeparam name="TProcessContext">Any context object</typeparam>
    /// <typeparam name="TEntity">Entity to handle</typeparam>
    /// <typeparam name="TPayload">Payload object</typeparam>
    public abstract class AbstractProcessHandler<TProcessContext, TEntity, TPayload> : IProcessHandler<TProcessContext> where TProcessContext : class where TEntity : class
    {
        public Task Process(TProcessContext context, params object[] args)
        {
            return Process(context, (TEntity)args[0], (TPayload)args[1]);
        }

        protected abstract Task Process(TProcessContext context, TEntity entity, TPayload payload);

    }
}
