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

namespace Alcaze.IC.Typing.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Customers")]
    public class CustomersController : DefaultCrudController<Customer>
    {
        [HttpGet]
        [Route("{id}")]
        public virtual async Task<Customer> Get(int id)
        {
            try
            {
                var conditions = new Conditions();
                conditions.AddCondition("CustomerId", Helper.Lambda.ComparisonOperator.Equal, id);
                using (var manager = CrudManagerFactory.GetCrudManager<Customer>())
                {
                    var result = await manager.FindAsync(conditions, 0, 0, "CustomerId");
                    return result.ResultList.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                await Logger.ErrorAsync($"Error obteniendo elementos de {typeof(Customer).Name}", ex);
                throw;
            }
        }

    }
}