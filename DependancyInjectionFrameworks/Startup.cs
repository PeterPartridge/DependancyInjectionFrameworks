using Castle.Facilities.AspNetCore;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using WindsorDependancyInjectionFrameworks.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using HelloWorld.Class;
using HelloWorld.Interface;

namespace WindsorDependancyInjectionFrameworks
{
    public class Startup
    {
        private static readonly WindsorContainer Container = new WindsorContainer();

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add application services to the application.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Setup component model contributors for making windsor services available to IServiceProvider
            Container.AddFacility<AspNetCoreFacility>(f => f.CrossWiresInto(services));

            // Add framework services.
            services.AddMvc();
            services.AddLogging((lb) => lb.AddConsole().AddDebug());
            services.AddSingleton<FrameworkMiddleware>(); // Do this if you don't care about using Windsor to inject dependencies

            // Custom application component registrations, ordering is important here
            RegisterApplicationComponents(services);

            // Castle Windsor integration, controllers, tag helpers and view components, this should always come after RegisterApplicationComponents
            return services.AddWindsor(Container,
                opts => opts.UseEntryAssembly(typeof(ValuesController).Assembly), // <- Recommended
                () => services.BuildServiceProvider(validateScopes: false)); // <- Optional
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // For making component registrations of middleware easier
            Container.GetFacility<AspNetCoreFacility>().RegistersMiddlewareInto(app);

            // Add framework configured middleware, use this if you dont have any DI requirements
            app.UseMiddleware<FrameworkMiddleware>();

            // Serve static files
            app.UseStaticFiles();

            // Mvc default route
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void RegisterApplicationComponents(IServiceCollection services)
        {
            // Application components
           // Container.Register(Component.For<IHttpContextAccessor>().ImplementedBy<HttpContextAccessor>());
            Container.Register(Component.For<IHelloWorld>().ImplementedBy<Speak>());
        }
    }
    public class FrameworkMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Do something before
            await next(context);
            // Do something after
        }
    }

    // Example of some custom user-defined middleware component. Resolves types from Windsor.
    public sealed class CustomMiddleware : IMiddleware
    {

        public CustomMiddleware(ILoggerFactory loggerFactory )
        {
       // put in custom middleWare
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Do something before
            await next(context);
            // Do something after
        }
    }

}