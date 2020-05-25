using System.Threading.Tasks;

namespace СonveyoR
{

    public abstract class ProcessStepHandler<TProcessContext, TEntity> : IProcessStepHandler<TProcessContext> where TProcessContext : class where TEntity : class
    {
        public Task Process(TProcessContext context, params object[] args)
        {
            return Process(context, (TEntity)args[0]);
        }

        protected abstract Task Process(TProcessContext context, TEntity entity);

    }

    public abstract class ProcessStepHandler<TProcessContext, TEntity, TPayload> : IProcessStepHandler<TProcessContext> where TProcessContext : class where TEntity : class
    {
        public Task Process(TProcessContext context, params object[] args)
        {
            return Process(context, (TEntity)args[0], (TPayload)args[1]);
        }

        protected abstract Task Process(TProcessContext context, TEntity entity, TPayload payload);

    }
}
