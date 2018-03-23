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
    public class ProductsController : DefaultCrudController<Forms>
    {
        [HttpGet]
        [Route("{id}")]
        public virtual async Task<Forms> Get(int id)
        {
            try
            {
                var conditions = new Conditions();
                conditions.AddCondition("FormId", Helper.Lambda.ComparisonOperator.Equal, id);
                using (var manager = CrudManagerFactory.GetCrudManager<Forms>())
                {
                    var result = await manager.FindAsync(conditions, 0, 0, "FormId");
                    return result.ResultList.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                await Logger.ErrorAsync($"Error obteniendo elementos de {typeof(Forms).Name}", ex);
                throw;
            }
        }

        public override Task<Forms> Put([FromBody] Forms value)
        {
            return base.Put(value);
        }
    }
}