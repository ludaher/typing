using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.API
{
    public interface IProcess<Entity, ReturnEntity> : IDisposable
    {
        Task<ReturnEntity> Execute(Entity entity);
    }
}
