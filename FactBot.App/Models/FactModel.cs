using System.Text.Json.Serialization;

namespace FactBot.App.Models;

public class FactModel
{
    [JsonPropertyName("fact")]
    public string Fact { get; set; }
}