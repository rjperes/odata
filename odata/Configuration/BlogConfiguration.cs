using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using odata.common;
using System;

namespace odata.server.Configuration
{
    public class BlogConfiguration : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            var products = builder
                .EntitySet<Blog>("Blogs")
                .EntityType
                .HasKey(p => p.BlogId);

            if (apiVersion == ApiVersion.Parse("1.0"))
            {
                builder
                    .EntitySet<Blog>("Blogs")
                    .EntityType
                    .Ignore(p => p.Url);
            }


            //------------------------------------------------------------
            var findByCreation = builder
                .EntitySet<Blog>("Blogs")
                .EntityType
                .Collection
                .Function("FindByCreation");

            findByCreation.ReturnsCollectionFromEntitySet<Blog>("Blogs");
            findByCreation.Parameter<DateTime>("date").Required();


            //------------------------------------------------------------
            var countPosts = builder
                .EntitySet<Blog>("Blogs")
                .EntityType
                .Collection
                .Function("CountPosts");

            countPosts.Parameter<int>("id").Required();
            countPosts.Returns<int>();


            //------------------------------------------------------------
            var count = builder
                .EntitySet<Blog>("Blogs")
                .EntityType
                .Action("Count");

            count.Parameter<int>("id").Required();
            count.Returns<int>();


            //------------------------------------------------------------
            var update = builder
                .EntitySet<Blog>("Blogs")
                .EntityType
                .Action("Update");

            update.EntityParameter<Blog>("blog").Required();
            update.ReturnsFromEntitySet<Blog>("Blogs");
        }
    }
}
