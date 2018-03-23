using Alcaze.API;
using Alcaze.IC.Typing.DTO.PersistenceEntities;
using System;
using System.Collections.Generic;
using System.Text;
using Alcaze.Helper.Lambda;
using System.Threading.Tasks;
using Alcaze.IC.Typing.DAL;
using Alcaze.Helper.Exceptions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Alcaze.IC.Typing.Validator.PersistenceEntities
{
    public class Customers_Validator : ICrudValidator<Customers>
    {
        public void Delete(Customers entity)
        {
        }

        public async Task DeleteAsync(Customers entity)
        {
        }

        public void Dispose()
        {
        }

        public void Find(List<Tuple<string, ComparisonOperator, object>> searchConditions, int page, int pageSize, string orderBy, bool ascending = true)
        {
        }

        public async Task FindAsync(List<Tuple<string, ComparisonOperator, object>> searchConditions, int page, int pageSize, string orderBy, bool ascending = true)
        {
        }

        public void Insert(Customers entity)
        {
            using (var ctx = new ImaginCrudContext())
            {
                if (ctx.Customers.Any(x => x.CustomerId.Equals(entity.CustomerId)))
                    throw new DuplicatedEntityException($"Ya existe un cliente con número de documento {entity.CustomerId}.");
            }
        }

        public void Insert(List<Customers> entity)
        {
            using(var ctx = new ImaginCrudContext())
            {
                foreach(var item in entity)
                {
                    if (ctx.Customers.Any(x=>x.CustomerId.Equals(item.CustomerId)))
                        throw new DuplicatedEntityException($"Ya existe un cliente con número de documento {item.CustomerId}.");

                }
            }
        }

        public async Task InsertAsync(Customers entity)
        {
            using (var ctx = new ImaginCrudContext())
            {
                    if (await ctx.Customers.AnyAsync(x => x.CustomerId.Equals(entity.CustomerId)))
                        throw new DuplicatedEntityException($"Ya existe un cliente con número de documento {entity.CustomerId}.");
            }
        }

        public async Task InsertAsync(List<Customers> entity)
        {
            using (var ctx = new ImaginCrudContext())
            {
                foreach (var item in entity)
                {
                    if (await ctx.Customers.AnyAsync(x => x.CustomerId.Equals(item.CustomerId)))
                        throw new DuplicatedEntityException($"Ya existe un cliente con número de documento {item.CustomerId}.");

                }
            }
        }

        public void Update(Customers entity)
        {
        }

        public async Task UpdateAsync(Customers entity)
        {
        }

        public async Task UpdateAsync(List<Customers> entity)
        {
        }
    }
}
