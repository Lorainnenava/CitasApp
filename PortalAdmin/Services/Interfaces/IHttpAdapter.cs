namespace PortalAdmin.Services.Interfaces
{
    public interface IHttpAdapter
    {
        Task<TResponse> GetByConditionAsync<TResponse>(string url);
        Task<List<TResponse>> GetAllAsync<TResponse>(string url);
        Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest content);
        Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest content);
        Task<bool> PutAsync<TRequest>(string url, TRequest content);
        Task<bool> DeleteAsync(string url);
    }
}
