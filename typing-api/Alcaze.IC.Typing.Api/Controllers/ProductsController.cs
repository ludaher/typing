using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Alcaze.IC.Typing.DTO;
using Alcaze.API;
using Alcaze.API.Factory;
using Alcaze.Helper.Log;
using Alcaze.IC.Typing.DTO.PersistenceEntities;
using Alcaze.Helper.Exceptions;

namespace Alcaze.IC.Typing.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : DefaultCrudController<Product>
    {
        [HttpGet]
        [Route("{id}")]
        public virtual async Task<Product> Get(int id)
        {
            try
            {
                var conditions = new Conditions();
                conditions.AddCondition("ProductId", Helper.Lambda.ComparisonOperator.Equal, id);
                using (var manager = CrudManagerFactory.GetCrudManager<Product>())
                {
                    var result = await manager.FindAsync(conditions, 0, 0, "ProductId");
                    return result.ResultList.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                await Logger.ErrorAsync($"Error obteniendo elementos de {typeof(Product).Name}", ex);
                throw;
            }
        }

        public override Task<Product> Put([FromBody] Product value)
        {
            return base.Put(value);
        }
    }
}