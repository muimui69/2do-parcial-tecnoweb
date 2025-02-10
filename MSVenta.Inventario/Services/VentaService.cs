using MSVenta.Inventario.Models;
using Polly;
using Polly.CircuitBreaker;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Aforo255.Cross.Http.Src;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace MSVenta.Inventario.Services
{
    public class VentaService : IVentaService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public VentaService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        // 📌 CATEGORIA
        public async Task<bool> CreateCategoria(Categoria request)
        {
            string uri = _configuration["proxy:urlSale"];
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{uri}/categoria", content);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> UpdateCategoria(Categoria request)
        {
            string uri = _configuration["proxy:urlSale"];
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(uri + $"/categoria/{request.Id}", content);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> DeleteCategoria(int id)
        {
            string uri = _configuration["proxy:urlSale"];
            var response = await _httpClient.DeleteAsync(uri + $"/categoria/{id}");
            response.EnsureSuccessStatusCode();
            return true;
        }

        // 📌 PRODUCTO
        public async Task<bool> CreateProducto(Producto request)
        {
            string uri = _configuration["proxy:urlSale"];
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri + "/producto", content);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> UpdateProducto(Producto request)
        {
            string uri = _configuration["proxy:urlSale"];
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(uri + $"/producto/{request.Id}", content);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> DeleteProducto(int id)
        {
            string uri = _configuration["proxy:urlSale"];
            var response = await _httpClient.DeleteAsync(uri + $"/producto/{id}");
            response.EnsureSuccessStatusCode();
            return true;
        }

        // 📌 ALMACEN
        public async Task<bool> CreateAlmacen(Almacen request)
        {
            string uri = _configuration["proxy:urlSale"];
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri + "/almacen", content);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> UpdateAlmacen(Almacen request)
        {
            string uri = _configuration["proxy:urlSale"];
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(uri + $"/almacen/{request.Id}", content);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> DeleteAlmacen(int id)
        {
            string uri = _configuration["proxy:urlSale"];
            var response = await _httpClient.DeleteAsync(uri + $"/almacen/{id}");
            response.EnsureSuccessStatusCode();
            return true;
        }

        // 📌 PRODUCTO_ALMACEN
        public async Task<bool> CreateProductoAlmacen(ProductoAlmacen request)
        {
            string uri = _configuration["proxy:urlSale"];
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri + "/productoalmacen", content);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> UpdateProductoAlmacen(ProductoAlmacen request)
        {
            string uri = _configuration["proxy:urlSale"];
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(uri + $"/productoalmacen/{request.Id}", content);
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> DeleteProductoAlmacen(int id)
        {
            string uri = _configuration["proxy:urlSale"];
            var response = await _httpClient.DeleteAsync(uri + $"/productoalmacen/{id}");
            response.EnsureSuccessStatusCode();
            return true;
        }

        // 🔥 EJECUTAR CON PATRÓN DE RETRY Y CIRCUIT BREAKER
        public bool Execute<T>(T request, string action) where T : class
        {
            bool response = false;

            var circuitBreakerPolicy = Policy.Handle<Exception>()
                .CircuitBreaker(3, TimeSpan.FromSeconds(15));

            var retry = Policy.Handle<Exception>()
                    .WaitAndRetryForever(attempt => TimeSpan.FromSeconds(15))
                    .Wrap(circuitBreakerPolicy);

            retry.Execute(() =>
            {
                if (circuitBreakerPolicy.CircuitState == CircuitState.Closed)
                {
                    circuitBreakerPolicy.Execute(() =>
                    {
                        switch (request)
                        {
                            case Categoria categoria:
                                response = action switch
                                {
                                    "create" => CreateCategoria(categoria).Result,
                                    "update" => UpdateCategoria(categoria).Result,
                                    "delete" => DeleteCategoria(categoria.Id).Result,
                                    _ => false
                                };
                                break;
                            case Producto producto:
                                response = action switch
                                {
                                    "create" => CreateProducto(producto).Result,
                                    "update" => UpdateProducto(producto).Result,
                                    "delete" => DeleteProducto(producto.Id).Result,
                                    _ => false
                                };
                                break;
                            case Almacen almacen:
                                response = action switch
                                {
                                    "create" => CreateAlmacen(almacen).Result,
                                    "update" => UpdateAlmacen(almacen).Result,
                                    "delete" => DeleteAlmacen(almacen.Id).Result,
                                    _ => false
                                };
                                break;
                            case ProductoAlmacen productoAlmacen:
                                response = action switch
                                {
                                    "create" => CreateProductoAlmacen(productoAlmacen).Result,
                                    "update" => UpdateProductoAlmacen(productoAlmacen).Result,
                                    "delete" => DeleteProductoAlmacen(productoAlmacen.Id).Result,
                                    _ => false
                                };
                                break;
                        }
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
