using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace FactBot.App.Slash_Commands;

public class PinnedFact : ApplicationCommandModule
{
    [SlashCommand("pinnedfact", "Displays a button that can be pinned to generate a random fact.")]
    public async Task FactCommandAsync(InteractionContext ctx)
    {
        var button = new DiscordButtonComponent(ButtonStyle.Success, "factBtn", "Generate Fact");

        // Can also add embed content 
        var embedBuilder = new DiscordInteractionResponseBuilder()
            .AddComponents(button);

        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, embedBuilder);
    }
}