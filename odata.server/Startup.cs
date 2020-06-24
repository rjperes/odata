using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData;
using Microsoft.OData.Edm;
using odata.server.Models;
using System;
using System.Linq;
using System.Text.Json;

namespace odata.server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {        
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ParentChildContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<BlogContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services
                .AddOData()
                .EnableApiVersioning();

            services.Configure<ODataOptions>(options =>
            {
                options.UrlKeyDelimiter = ODataUrlKeyDelimiter.Parentheses;
            });

            services.AddODataQueryFilter(new PagingValidatorQueryAttribute());            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, VersionedODataModelBuilder modelBuilder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Use(async (context, next) =>
                    {
                        var errorFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                        if (errorFeature?.Error != null)
                        {
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            context.Response.ContentType = "application/json";

                            var response = EnableQueryAttribute.CreateErrorResponse(errorFeature.Error.Message, errorFeature.Error);
                            
                            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        }
                        else
                        {
                            await next();
                        }
                    });
                });
            }

            modelBuilder.OnModelCreated = (builder, model) =>
            {
                var version = model.GetAnnotationValue<ApiVersionAnnotation>(model).ApiVersion;
                model.SetEdmVersion(Version.Parse(version.ToString()));
            };

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                var models = modelBuilder.GetEdmModels();

                //foreach (var model in models)
                {
                    //var version = model.GetEdmVersion();
                    //endpoints.MapODataRoute("odata", $"odata/v{version}", model);
                    //endpoints.MapODataRoute("odata", "odata/v{version:apiversion}", models.Last());
                    endpoints.MapControllers();
                }
                
                endpoints.Select().Expand().OrderBy().Filter().Count().MaxTop(10);
                endpoints.EnableDependencyInjection();
            });
        }

        /*private static IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);

            builder
                .EntitySet<Parent>("Parents")
                .EntityType
                .Select()
                .Expand()
                .OrderBy()
                .Filter()
                .Count();

            builder
                .EntitySet<Child>("Children")
                .EntityType
                .Select()
                .Expand()
                .OrderBy()
                .Filter()
                .Count();

            builder
                .EntitySet<Blog>("Blogs")
                .EntityType
                .Select()
                .Expand()
                .OrderBy()
                .Filter()
                .Count();

            builder
                .EntitySet<Post>("Posts")
                .EntityType
                .Select()
                .Expand()
                .OrderBy()
                .Filter()
                .Count();

            var findByCreation = builder
                .EntitySet<Blog>("Blogs")
                .EntityType
                .Collection
                .Function("FindByCreation");

            findByCreation.ReturnsCollectionFromEntitySet<Blog>("Blogs");
            findByCreation.Parameter<DateTime>("date").Required();


            var countPosts = builder
                .EntitySet<Blog>("Blogs")
                .EntityType
                .Collection
                .Function("CountPosts");

            countPosts.Parameter<int>("id").Required();
            countPosts.Returns<int>();


            var count = builder
                .EntitySet<Blog>("Blogs")
                .EntityType
                .Action("Count");

            count.Parameter<int>("id").Required();
            count.Returns<int>();


            var update = builder
                .EntitySet<Blog>("Blogs")
                .EntityType
                .Action("Update");

            update.EntityParameter<Blog>("blog").Required();
            update.ReturnsFromEntitySet<Blog>("Blogs");

            builder
                .Function("CountBlogPosts")
                .Returns<int>();

            return builder.GetEdmModel();
        }*/
    }
}
