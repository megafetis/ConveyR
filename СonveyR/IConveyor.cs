using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConveyR
{
    /// <summary>
    /// Defines a conveyor to encapsulate handling
    /// </summary>
    public interface IConveyor
    {
        /// <summary>
        /// Start handling the entity object
        /// </summary>
        /// <typeparam name="TContext">Class of context object</typeparam>
        /// <param name="context">Context is an any object that plays the role of a data processing context. For most cases pass "this" reference of your custom repo class</param>
        /// <param name="entity">An Object to process</param>
        /// <param name="payload">optional objet, that contains required data for entity object</param>
        /// <param name="group">Specifying a group name to tell, what group of handlers to use. Most cases: before commit, after commit, rollback handlers</param>
        /// <param name="cancellationToken">token</param>
        /// <returns>Task</returns>
        Task Process<TContext>(TContext context, object entity, object payload = null, string group=null,
            CancellationToken cancellationToken = default)
            where TContext : class;

        /// <summary>
        /// Get process handlers for call each of them manually.
        /// </summary>
        /// <typeparam name="TContext">Type of handler context object</typeparam>
        /// <param name="entity">An Object to process</param>
        /// <param name="payload">optional objet, that contains required data for entity object</param>
        /// <param name="group">Specifying a group name to tell, what group of handlers to use. Most cases: before commit, after commit, rollback handlers</param>
        /// <returns>Collection of handlers IProcessHandler<TContext></returns>
        IEnumerable<IProcessHandler<TContext>> GetProcessHandlers<TContext>(object entity,
            object payload = null, string group = null) where TContext : class;
    }
}
