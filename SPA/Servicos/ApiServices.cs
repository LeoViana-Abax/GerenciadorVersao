using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest request, string authToken)
    { 
        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync(url, content);
        if (response.IsSuccessStatusCode)
        {           
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }
        
        throw new InvalidOperationException($"Error {response.StatusCode}: {response.ReasonPhrase}");
    } 
}

