using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using FactBot.App.Constants;

namespace FactBot.App.Slash_Commands;

public class Ping : ApplicationCommandModule
{
    [SlashCommand("ping", "Will pong back to server.")]
    public async Task PingCommandAsync(InteractionContext ctx)
    {
        var pingEmbed = new DiscordEmbedBuilder()
        {
            Title = "Pong 🏓",
            ImageUrl = Image.PingPongImg,
            Color = DiscordColor.Orange
        };
        
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
            .AddEmbed(pingEmbed));
    }
}