using Alcaze.Helper.Exceptions;
using Alcaze.Helper.Lambda;
using Alcaze.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alcaze.APIr
{
    internal class CrudManager<Entity> : ICrudManager<Entity>
    {
        private ICrudManager<Entity> _crudManager;
        private ICrudValidator<Entity> _crudValidator;
        public ICrudValidator<Entity> CrudValidator { set { _crudValidator = value; } }

        public CrudManager(ICrudManager<Entity> crudManager)
        {
            if (crudManager == null)
                throw new IncompleteException("El gestor de CRUD no fué configurado");
            _crudManager = crudManager;
        }

        public void Delete(Entity entity)
        {
            if (_crudValidator != null)
                _crudValidator.Delete(entity);
            _crudManager.Delete(entity);
        }

        public FindResult<Entity> Find(Conditions searchConditions, int page, int pageSize, string orderBy, bool ascending = true, List<string> includes = null)
        {
            if (_crudValidator != null)
                _crudValidator.Find(searchConditions, page, pageSize, orderBy, ascending);
            return _crudManager.Find(searchConditions, page, pageSize, orderBy, ascending, includes);
        }

        public FindResult<object> FindSelect(Conditions searchConditions, string selectFields, int page, int pageSize, string orderBy, bool ascending = true)
        {
            if (_crudValidator != null)
                _crudValidator.Find(searchConditions, page, pageSize, orderBy, ascending);
            return _crudManager.FindSelect(searchConditions, selectFields, page, pageSize, orderBy, ascending);
        }

        Entity ICrudManager<Entity>.Insert(Entity entity)
        {
            if (_crudValidator != null)
                _crudValidator.Insert(entity);
            return _crudManager.Insert(entity);
        }

        List<Entity> ICrudManager<Entity>.Insert(List<Entity> entities)
        {
            if (_crudValidator != null)
                _crudValidator.Insert(entities);
            return _crudManager.Insert(entities);
        }

        Entity ICrudManager<Entity>.Update(Entity entity)
        {
            if (_crudValidator != null)
                _crudValidator.Update(entity);
            return _crudManager.Update(entity);
        }

        public async Task<FindResult<Entity>> FindAsync(Conditions searchConditions, int page, int pageSize, string orderBy, bool ascending = true, List<string> includes = null)
        {
            if (_crudValidator != null)
                await _crudValidator.FindAsync(searchConditions, page, pageSize, orderBy, ascending);
            return await _crudManager.FindAsync(searchConditions, page, pageSize, orderBy, ascending, includes);
        }

        public async Task<FindResult<object>> FindSelectAsync(Conditions searchConditions, string selectFields, int page, int pageSize, string orderBy, bool ascending = true)
        {
            if (_crudValidator != null)
                await _crudValidator.FindAsync(searchConditions, page, pageSize, orderBy, ascending);
            return await _crudManager.FindSelectAsync(searchConditions, selectFields, page, pageSize, orderBy, ascending);
        }

        public async Task<Entity> InsertAsync(Entity entity)
        {
            if (_crudValidator != null)
                await _crudValidator.InsertAsync(entity);
            return await _crudManager.InsertAsync(entity);
        }

        public async Task<List<Entity>> InsertAsync(List<Entity> entities)
        {
            if (_crudValidator != null)
                await _crudValidator.InsertAsync(entities);
            return await _crudManager.InsertAsync(entities);
        }

        public async Task<Entity> UpdateAsync(Entity entity)
        {
            if (_crudValidator != null)
                await _crudValidator.UpdateAsync(entity);
            return await _crudManager.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Entity entity)
        {
            if (_crudValidator != null)
                await _crudValidator.DeleteAsync(entity);
            await _crudManager.DeleteAsync(entity);
        }


        public async Task DeleteAsync(Conditions searchConditions)
        {
            await _crudManager.DeleteAsync(searchConditions);
        }

        public void Dispose()
        {
            if (_crudValidator != null)
                _crudValidator.Dispose();
            _crudManager.Dispose();
        }

        public bool Any(Conditions searchConditions)
        {
            return _crudManager.Any(searchConditions);

        }

        public async Task<bool> AnyAsync(Conditions searchConditions)
        {
            return await _crudManager.AnyAsync(searchConditions);
        }

        public async Task<List<Entity>> UpdateAsync(List<Entity> entity)
        {
            if (_crudValidator != null)
                await _crudValidator.UpdateAsync(entity);
            return await _crudManager.UpdateAsync(entity);
        }

    }
}
