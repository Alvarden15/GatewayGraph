using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate.AspNetCore;
using HotChocolate.Stitching;
using System;
using HotChocolate;

namespace GatewayGraph
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // If you need dependency injection with your query object add your query type as a services.
            // services.AddSingleton<Query>();

            // enable InMemory messaging services for subscription support.
            // services.AddInMemorySubscriptionProvider();

            // this enables you to use DataLoader in your resolvers.
            //services.AddDataLoaderRegistry();

            //services.AddDataLoaderRegistry();

            /*
            services.AddHttpClient("alumno",(sp,client)=>{
                client.BaseAddress= new Uri("https://localhost:5221/graphql/");
            });
            services.AddHttpClient("casa",(sp,client)=>{
                client.BaseAddress= new Uri("https://localhost:5551/graphql/");
            });
            
             */
            services.AddHttpClient("alumno",(sp,client)=>{
                client.BaseAddress= new Uri("http://52.167.142.169/graphql/");
            });
            services.AddHttpClient("casa",(sp,client)=>{
                client.BaseAddress= new Uri("http://52.232.182.12/graphql/");
            });
            services.AddStitchedSchema(s=>
            s.AddSchemaFromHttp("alumno").AddSchemaFromHttp("casa"));
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // enable this if you want tu support subscription.
            //app.UseWebSockets();
            app.UseGraphQL();
            app.UseHttpsRedirection();
            //app.UseHttpMethodOverride();
            // enable this if you want to use graphiql instead of playground.
            app.UseGraphiQL();
            //app.UsePlayground();
        }
    }
}
