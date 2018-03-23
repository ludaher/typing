using Alcaze.APIr;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.API
{
    public abstract class UnimplementCrud<Entity> : ICrudManager<Entity>
    {
        public bool Any(Conditions searchConditions)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Conditions searchConditions)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(Entity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task DeleteAsync(Entity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Conditions searchConditions)
        {
            throw new NotImplementedException();
        }

        public abstract void Dispose();

        public virtual FindResult<Entity> Find(Conditions searchConditions, int page, int pageSize, string orderBy, bool ascending = true, List<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public virtual Task<FindResult<Entity>> FindAsync(Conditions searchConditions, int page, int pageSize, string orderBy, bool ascending = true, List<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public virtual FindResult<object> FindSelect(Conditions searchConditions, string selectFields, int page, int pageSize, string orderBy, bool ascending = true)
        {
            throw new NotImplementedException();
        }

        public virtual Task<FindResult<object>> FindSelectAsync(Conditions searchConditions, string selectFields, int page, int pageSize, string orderBy, bool ascending = true)
        {
            throw new NotImplementedException();
        }

        public virtual List<Entity> Insert(List<Entity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual Entity Insert(Entity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<Entity>> InsertAsync(List<Entity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual Task<Entity> InsertAsync(Entity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Entity Update(Entity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<Entity> UpdateAsync(Entity entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<List<Entity>> UpdateAsync(List<Entity> entity)
        {
            throw new NotImplementedException();
        }
    }
}
