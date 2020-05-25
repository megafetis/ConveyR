using System.Threading.Tasks;

namespace СonveyoR
{

    public abstract class AbstractProcessHandler<TProcessContext, TEntity> : IProcessHandler<TProcessContext> where TProcessContext : class where TEntity : class
    {
        public Task Process(TProcessContext context, params object[] args)
        {
            return Process(context, (TEntity)args[0]);
        }

        protected abstract Task Process(TProcessContext context, TEntity entity);

    }

    public abstract class AbstractProcessHandler<TProcessContext, TEntity, TPayload> : IProcessHandler<TProcessContext> where TProcessContext : class where TEntity : class
    {
        public Task Process(TProcessContext context, params object[] args)
        {
            return Process(context, (TEntity)args[0], (TPayload)args[1]);
        }

        protected abstract Task Process(TProcessContext context, TEntity entity, TPayload payload);

    }
}
