using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.Helper.Processes
{
    public class ProcessesQueue<Entity, ReturnEntity>
    {
        public bool processing;
        public ProcessesQueue()
        {
            _methods = new Dictionary<Func<Entity, ReturnEntity>, Entity>();
        }
        private Dictionary<Func<Entity, ReturnEntity>, Entity> _methods;

        public void ExecuteMethod(Func<Entity, ReturnEntity> method, Entity entity)
        {
            _methods.Add(method, entity);
            Next();
        }

        public void Next()
        {
            if (processing == false)
                processing = true;
            if (!_methods.Any())
            {
                processing = false;
                return;
            }
            var item = _methods.FirstOrDefault();
            var method = item.Key;
            method(item.Value);
            _methods.Remove(method);
            Next();
        }
    }
}
