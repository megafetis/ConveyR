using System.Threading;
using System.Threading.Tasks;

namespace ConveyR
{
    /// <summary>
    /// Abstract handler class for comfortable creating of your own handlers
    /// </summary>
    /// <typeparam name="TProcessContext">Any context object</typeparam>
    /// <typeparam name="TEntity">Entity to handle</typeparam>
    public abstract class AbstractProcessHandler<TProcessContext, TEntity> : IProcessHandler<TProcessContext> where TProcessContext : class where TEntity : class
    {
        public Task Process(TProcessContext context, object entity,object payload=null, CancellationToken cancellationToken = default)
        {
            return this.Process(context, (TEntity)entity,cancellationToken);
        }

        protected abstract Task Process(TProcessContext context, TEntity entity, CancellationToken cancellationToken = default);

    }
    /// <summary>
    /// Abstract handler class for comfortable creating of your own handlers
    /// </summary>
    /// <typeparam name="TProcessContext">Any context object</typeparam>
    /// <typeparam name="TEntity">Entity to handle</typeparam>
    /// <typeparam name="TPayload">Payload object</typeparam>
    public abstract class AbstractProcessHandler<TProcessContext, TEntity, TPayload> : IProcessHandler<TProcessContext> where TProcessContext : class where TEntity : class
    {
        public Task Process(TProcessContext context, object entity, object payload = null, CancellationToken cancellationToken = default)
        {
            return Process(context, (TEntity)entity, (TPayload)payload, cancellationToken);
        }

        protected abstract Task Process(TProcessContext context, TEntity entity, TPayload payload, CancellationToken cancellationToken = default);

    }
}
