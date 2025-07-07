using PortalAdmin.Helpers;
using PortalAdmin.Services.Interfaces;
using System.Net.Http.Json;
using System.Text.Json;

namespace PortalAdmin.Services.Http
{
    public class HttpAdapterClient(HttpClient httpClient) : IHttpAdapter
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private static async Task<TResponse> DeserializeOrThrowAsync<TResponse>(HttpResponseMessage response, JsonSerializerOptions options)
        {
            var result = await response.Content.ReadFromJsonAsync<TResponse>(options);
            return result ?? throw new Exception("La respuesta de la API es nula o no pudo deserializarse.");
        }

        public async Task<TResponse> GetByConditionAsync<TResponse>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            // Valida si la respuesta contiene errores (manejo centralizado)
            await HandleError.HttpErrorHandler(response);

            // Deserializa la respuesta y la devuelve; lanza una excepción si es nula
            return await DeserializeOrThrowAsync<TResponse>(response, _jsonOptions);
        }

        public async Task<List<TResponse>> GetAllAsync<TResponse>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            await HandleError.HttpErrorHandler(response);

            var result = await response.Content.ReadFromJsonAsync<List<TResponse>>(_jsonOptions);
            return result ?? [];
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest content)
        {
            var response = await _httpClient.PostAsJsonAsync(url, content);

            await HandleError.HttpErrorHandler(response);

            return await DeserializeOrThrowAsync<TResponse>(response, _jsonOptions);
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest content)
        {
            var response = await _httpClient.PutAsJsonAsync(url, content);

            await HandleError.HttpErrorHandler(response);

            return await DeserializeOrThrowAsync<TResponse>(response, _jsonOptions);
        }

        public async Task<bool> PutAsync<TRequest>(string url, TRequest content)
        {
            var response = await _httpClient.PutAsJsonAsync(url, content);
            await HandleError.HttpErrorHandler(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);

            await HandleError.HttpErrorHandler(response);

            return response.IsSuccessStatusCode;
        }
    }
}
