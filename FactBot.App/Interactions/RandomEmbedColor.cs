using DSharpPlus.Entities;
using FactBot.App.Interfaces;

namespace FactBot.App.Interactions;

public class RandomEmbedColor : IColor
{
    private static readonly Random Random = new Random();

    public DiscordColor Execute()
    {
        return ShuffleColors().First();
    }

    private List<DiscordColor> ShuffleColors()
    { 
        var colors = new List<DiscordColor>
        {
            DiscordColor.SpringGreen, DiscordColor.Blue, DiscordColor.Orange, DiscordColor.Magenta, 
            DiscordColor.Red, DiscordColor.Yellow, DiscordColor.White, DiscordColor.VeryDarkGray
        };
        
        return Shuffle(colors);
    }
    
    private static List<T> Shuffle<T>(List<T> list)
    {
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = Random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }

        return list;
    }
}