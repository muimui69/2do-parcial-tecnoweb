using Aforo255.Cross.Http.Src;
using Aforo255.Cross.Discovery.Consul;
using Aforo255.Cross.Discovery.Mvc;
using Aforo255.Cross.Event.Src;
using Aforo255.Cross.Tracing.Src;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MSVenta.Inventario.Repositories;
using MSVenta.Inventario.Services;
using MediatR;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using Consul;
using MSVenta.Inventario.Messages.Commands;
using MSVenta.Inventario.Messages.CommandHandlers;
using MSVenta.Inventario.Messages.Events;
using Aforo255.Cross.Event.Src.Bus;
using Npgsql;
using MSVenta.Inventario.Messages.EventHandlers;

namespace MSVenta.Inventario
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ContextDatabase>(
              options =>
              {
                  options.UseNpgsql(Configuration["postgres:cn"]);
              });

            services.AddScoped<IVentaService, VentaService>();

            services.AddScoped<IAlmacenService, AlmacenService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IProductoAlmacenService, ProductoAlmacenService>();
            services.AddScoped<IDetalleAjusteService, DetalleAjusteService>();
            services.AddScoped<IAjusteInventarioService, AjusteInventarioService>();

            services.AddProxyHttp();
            services.AddScoped<IUsuarioService, UsuarioService>();


            /*Start RabbitMQ*/
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddRabbitMQ();
            services.AddTransient<IRequestHandler<CategoriaCreateCommand, bool>, CategoriaCommandHandler>();
            /*End RabbitMQ*/

            /*Start - Consul*/
            services.AddSingleton<IServiceId, ServiceId>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddConsul();
            /*End - Consul*/

            /*Start - Tracer distributed*/
            services.AddJaeger();
            services.AddOpenTracing();
            /*End - Tracer distributed*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IHostApplicationLifetime applicationLifetime, IConsulClient consulClient)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var serviceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(serviceId);
            });
            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<CategoriaCreatedEvent, CategoriaEventHandler>();
        }
    }
}
