using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace СonveyoR
{
    public interface IProcessStepHandler<in TProcessContext> where TProcessContext : class
    {
        Task Process(TProcessContext context, params object[] args);
    }
}
