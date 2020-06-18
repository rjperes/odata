using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using odata.common;
using System;
using System.Linq;

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

            services.AddOData();
            services.AddODataQueryFilter();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapODataRoute("odata", "odata", GetEdmModel(app.ApplicationServices));
                endpoints.Select().Expand().OrderBy().Filter().Count().MaxTop(10);
                endpoints.EnableDependencyInjection();
            });
        }

        private static IEdmModel GetEdmModel(IServiceProvider serviceProvider)
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
                .Collection
                .Action("Count");

            count.Parameter<int>("id").Required();
            count.Returns<int>();


            var update = builder
                .EntitySet<Blog>("Blogs")
                .EntityType
                .Collection
                .Action("Update");

            update.EntityParameter<Blog>("blog").Required();
            update.ReturnsFromEntitySet<Blog>("Blogs");

            return builder.GetEdmModel();
        }
    }
}
