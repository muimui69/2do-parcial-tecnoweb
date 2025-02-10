using Aforo255.Cross.Http.Src;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System;

namespace MSVenta.Inventario.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IHttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public UsuarioService(IHttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetUsuario(int usuario_id)
        {
            string uri = _configuration["proxy:urlSecurity"];
            var url = $"{uri}/{usuario_id}";
            var response = await _httpClient.GetStringAsync(url);
            if (!string.IsNullOrEmpty(response))
            {
                return response;
            }
            return null;
        }

        public async Task<bool> ValidateUsuario(int usuario_id)
        {
            try
            {
                string uri = _configuration["proxy:urlSecurity"];
                var url = $"{uri}/{usuario_id}/validate";

                var response = await _httpClient.GetStringAsync(url);

                if (!string.IsNullOrEmpty(response))
                {
                    var jsonResponse = JObject.Parse(response);

                    var message = jsonResponse["message"]?.ToString();
                    if (message != null && message.Contains("Usuario no encontrado."))
                    {
                        return false;
                    }

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating usuario: {ex.Message}");
                return false;
            }
        }
    }
}
