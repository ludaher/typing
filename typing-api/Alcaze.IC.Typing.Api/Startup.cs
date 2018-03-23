using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Alcaze.API.Factory;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Alcaze.IC.Typing.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Alcaze.IC.Typing.DTO.CustomEntities.Configuration.Instance = Configuration;
            CrudManagerFactory.CRUD_NAMESPACE = "Alcaze.IC.Typing.Crud";
            CrudManagerFactory.CRUD_VALIDATOR_NAMESPACE = "Alcaze.IC.Typing.Validator";
            ProcessManagerFactory.ENTITIES_NAMESPACE = CrudManagerFactory.ENTITIES_NAMESPACE = "Alcaze.IC.Typing.DTO";
            ProcessManagerFactory.PROCESSOR_NAMESPACE = "Alcaze.IC.Typing.Processor";
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy", corsBuilder.Build());
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("MyCorsPolicy"));
            });


            services
                .AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://216.69.181.183/IdentityServer/";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api1";
                    options.NameClaimType = "user_name";
                    options.RoleClaimType = "role";
                    //options.typ
                    //options.
                });

            services
                .AddMvcCore(options =>
                {
                    // require scope1 or scope2
                    var policy = ScopePolicy.Create("openid", "profile");
                    options.Filters.Add(new AuthorizeFilter(policy));
                })
                .AddAuthorization()
                .AddJsonFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
