using MSVenta.Inventario.DTOs;
using MSVenta.Inventario.Models;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Aforo255.Cross.Http.Src;

namespace MSVenta.Inventario.Services
{
    public class VentaService : IVentaService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClient _httpClient;
        private readonly ICategoriaService _categoriaService;


        public VentaService(IConfiguration configuration, IHttpClient httpClient, ICategoriaService categoriaService)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _categoriaService = categoriaService;
        }

        public async Task<bool> CreateVenta(Categoria request)
        {
            string uri = _configuration["proxy:urlSale"]; 
            var response = await _httpClient.PostAsync(uri + "/categoria", request);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public bool Execute(Categoria request)
        {
            bool response = false;

            var circuitBreakerPolicy = Policy.Handle<Exception>().
                CircuitBreaker(3, TimeSpan.FromSeconds(15));

            var retry = Policy.Handle<Exception>()
                    .WaitAndRetryForever(attemp => TimeSpan.FromSeconds(15))
                    .Wrap(circuitBreakerPolicy);

            retry.Execute(() =>
            {
                if (circuitBreakerPolicy.CircuitState == CircuitState.Closed)
                {
                    circuitBreakerPolicy.Execute(() =>
                    {
                        Categoria venta = new Categoria()
                        {
                            Id = request.Id,
                            Nombre = request.Nombre 
                        };
                        response = CreateVenta(venta).Result;
                    });
                }

                if (circuitBreakerPolicy.CircuitState != CircuitState.Closed)
                {
                    response = false;
                }
            });

            return response;
        }
    }
}
