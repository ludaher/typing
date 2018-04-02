using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PyS.Repository.DataAccess;
using System.Diagnostics;
using PyS.Repository.Api.Model;
using Newtonsoft.Json;
using PyS.Repository.Entities;
using Alcaze.API.Factory;
using Newtonsoft.Json.Linq;
using Alcaze.API;
using Alcaze.Helper.Exceptions;
using System.IO;
using Microsoft.Win32;
using PyS.Repository.Api.Attributes;

namespace PyS.Repository.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Files")]
    public class FilesController : Controller
    {
        /// <summary>
        /// Carga archivo en el repositorio
        /// Genera la propiedad assigned en false indicando que solo se almacenó
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Identificador único del archivo</returns>
        [HttpPut]
        public async Task<string> Put([FromBody] FileStorage entity)
        {
            using (var manager = CrudManagerFactory.GetCrudManager<FileStorage>())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(entity.JsonMetadata) == false)
                    {
                        var metadata = JObject.Parse(entity.JsonMetadata);
                        metadata.Add("CreatedBy", User.Identity.Name);
                        metadata.Add("CreatedOn", DateTime.Now);
                        metadata.Add("Assigned", false);
                        entity.JsonMetadata = metadata.ToString();
                    }
                    var result = await manager.InsertAsync(entity);
                    return result.Id;
                }
                catch (Exception ex)
                {
                    Trace.Fail(ex.ToString());
                    throw;
                }
            }
        }

        /// <summary>
        /// Actualiza archivo en el repositorio si no contiene archivo, únicamenta actualiza la metadata
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Post([FromBody] FileStorage entity)
        {
            using (var manager = CrudManagerFactory.GetCrudManager<FileStorage>())
            {
                try
                {
                    var metadata = JObject.Parse(entity.JsonMetadata);
                    metadata.Add("ModifiedBy", User.Identity.Name);
                    metadata.Add("ModifiedOn", DateTime.Now);
                    if (string.IsNullOrWhiteSpace(entity.DataBase64) == false)
                        metadata.Add("Assigned", false);
                    entity.JsonMetadata = metadata.ToString();
                    var result = await manager.UpdateAsync(entity);
                }
                catch (Exception ex)
                {
                    Trace.Fail(ex.ToString());
                    throw;
                }
            }
        }

        [HttpGet]
        public async Task<FindResult<FileStorage>> Get(string filter, int page = 0, int pageSize = 0, string orderBy = "", bool ascending = true)
        {
            using (var manager = CrudManagerFactory.GetCrudManager<FileStorage>())
            {
                var conditions = new Conditions();
                conditions.AddFilter(filter);
                return await manager.FindAsync(conditions, page, pageSize, orderBy, !ascending);
            }
        }

        [HttpGet("select/{select}")]
        public async Task<FindResult<object>> Get(string select, string filter, int page = 0, int pageSize = 0, string orderBy = "", bool ascending = true)
        {
            using (var manager = CrudManagerFactory.GetCrudManager<FileStorage>())
            {
                var conditions = new Conditions();
                conditions.AddFilter(filter);
                return await manager.FindSelectAsync(conditions, select, page, pageSize, orderBy, !ascending);
            }
        }


        /// The file information
        [HttpPost]
        [Route("attachment")]
        //[CustomRequestSizeLimitAttribute(valueCountLimit: int.MaxValue)]
        public async Task<string> SaveFileAttachment(IFormFile file, string metadata)
        {
            using (var stream = file.OpenReadStream())
            using (var manager = CrudManagerFactory.GetCrudManager<StreamStorage>())
            {
                try
                {
                    var metadataObj = JObject.Parse(metadata);
                    metadataObj.Add("CreatedBy", User.Identity.Name);
                    metadataObj.Add("CreatedOn", DateTime.Now);
                    metadataObj.Add("Assigned", false);

                    var entity = new StreamStorage() { FileName = file.FileName, Stream = stream, JsonMetadata = metadataObj.ToString() };
                    var result = await manager.InsertAsync(entity);
                    return result.Id;
                }
                catch (Exception ex)
                {
                    Trace.Fail(ex.ToString());
                    throw;
                }
            }
        }

        [HttpPut]
        [Route("attachment/{id}")]
        public async Task SaveFileAttachment(string id, ICollection<IFormFile> files, string metadata)
        {
            if (files.Any() == false)
                return;
            foreach (var file in files)
            {
                using (var stream = file.OpenReadStream())
                using (var manager = CrudManagerFactory.GetCrudManager<StreamStorage>())
                {
                    var entity = new StreamStorage() { Id = id, FileName = file.FileName, Stream = stream };
                    var result = await manager.UpdateAsync(entity);
                }
            }

        }

        [HttpGet]
        [Route("download/{id}")]
        //[EnableCors("AnyOrigin")]
        public async Task<IActionResult> Download(string id)
        {
            using (var manager = ProcessManagerFactory.GetProcessManager<FileStorage, FileStorage>())
            {
                var conditions = new Conditions();
                conditions.AddCondition("_id", Alcaze.Helper.Lambda.ComparisonOperator.Equal, id);
                var fileStorage = await manager.Execute(new FileStorage() { Id = id });
                if (fileStorage == null)
                    throw new LogicException("Archivo no encontrado");
                //Response.Headers.Add("Content-Length", (fileStorage.Length*8).ToString());
                return File(fileStorage.Data, MimeType(fileStorage.FileName), Path.GetFileName(fileStorage.FileName));
            }
        }

        [HttpGet]
        [Route("download")]
        //[EnableCors("AnyOrigin")]
        public async Task<IActionResult> DownloadFilter(string filter)
        {
            using (var manager = ProcessManagerFactory.GetProcessManager<FilterStorage, FileStorage>())
            {
                var conditions = new Conditions();
                conditions.AddFilter(filter);
                var fileStorage = await manager.Execute(new FilterStorage() { Filter = conditions });
                if (fileStorage == null)
                    throw new LogicException("Archivo no encontrado");
                //Response.Headers.Add("Content-Length", (fileStorage.Length*8).ToString());
                return File(fileStorage.Data, MimeType(fileStorage.FileName), Path.GetFileName(fileStorage.FileName));
            }
        }
        private string MimeType(string filename)
        {
            string mime = "application/octetstream";
            var extension = Path.GetExtension(filename);
            if (extension != null)
            {
                RegistryKey rk = Registry.ClassesRoot.OpenSubKey(extension.ToLower());

                if (rk != null && rk.GetValue("Content Type") != null)
                    mime = rk.GetValue("Content Type").ToString();
            }
            return mime;
        }

    }
}