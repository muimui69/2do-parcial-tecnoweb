using Aforo255.Cross.Http.Src;
using Aforo255.Cross.Token.Src;
using Aforo255.Cross.Discovery.Consul;
using Aforo255.Cross.Discovery.Mvc;
using Aforo255.Cross.Event.Src;
using Aforo255.Cross.Tracing.Src;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MSVenta.Venta.Repositories;
using MSVenta.Venta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Aforo255.Cross.Event.Src.Bus;
//using MSVenta.Venta.Messages.EventHandlers;
//using MSVenta.Venta.Messages.Events;
using Microsoft.Extensions.Hosting.Internal;
using Consul;
//using MSVenta.Venta.Messages.Commands;
//using MSVenta.Venta.Messages.CommandHandlers;


namespace MSVenta.Venta
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddDbContext<ContextDatabase>(
               opt =>
               {
                   opt.UseMySQL(Configuration["mysql:cn"]);
               });
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IAlmecenService, AlmacenService>();
            services.AddScoped<IDetalleVentaService, DetalleVentaService>();
            services.AddScoped<ProductoAlmacenService>();
            services.AddScoped<ICategoriaService, CategoriaService>();

            services.AddProxyHttp();
            services.AddScoped<IUsuarioService, UsuarioService>();


           /*Start RabbitMQ*/
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddRabbitMQ();
            //services.AddTransient<IRequestHandler<CategoriaCreateCommand, bool>, CategoriaCommandHandler>();
            //services.AddScoped<IEventHandler<CategoriaCreatedEvent>, CategoriaCreatedEventHandler>();
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

            app.UseAuthorization();

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
            //eventBus.Subscribe<CategoriaCreatedEvent, CategoriaCreatedEventHandler>();
        }
    }
}
