using System.Threading;
using System.Threading.Tasks;

namespace ConveyR
{
    /// <summary>
    /// Common process handler interface
    /// </summary>
    /// <typeparam name="TProcessContext"></typeparam>
    public interface IProcessHandler<in TProcessContext> where TProcessContext : class
    {
        Task Process(TProcessContext context, object entitiy, object payload = null, CancellationToken cancellationToken = default);
    }
}
