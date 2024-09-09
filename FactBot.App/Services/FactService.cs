using System.Text.Json;
using FactBot.App.Secrets;
using FactBot.App.Models;

namespace FactBot.App.Services;

public class FactService
{
    private static FactService _instance;
    private FactService() { }

    public static FactService GetInstance()
    {
        if (_instance == null)
        {
            _instance = new FactService();
        }

        return _instance;
    }
    
    public async Task<List<FactModel>> GetAll()
    {
        using var client = new HttpClient();
        
        client.DefaultRequestHeaders.Add("X-Api-Key", ApiKey.FactApiKey);

        var result = await client.GetAsync($"https://api.api-ninjas.com/v1/facts");
        if (result.IsSuccessStatusCode)
        {
            var json = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<FactModel>>(json);
        }
        if (!result.IsSuccessStatusCode)
        {
            Console.WriteLine($"Fact Ninja API Status Code: {result.StatusCode}");
        }

        return new List<FactModel>();
    }
}