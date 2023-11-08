using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace FactBot.App.Slash_Commands;

public class FactButtonCommand : ApplicationCommandModule
{
    [SlashCommand("factbutton", "Displays a button that can be pressed to generate a random fact.")]
    public async Task FactCommandAsync(InteractionContext ctx)
    {
        var messageEmbed = new DiscordEmbedBuilder()
        {
            Description = "Button Will Disable After `` 15 Mins ``",
            Color = DiscordColor.Black
        };
        
        var button = new DiscordButtonComponent
        (
            ButtonStyle.Primary, "factBtn", "Generate Fact", false
        );
        
        var disableButton = new DiscordButtonComponent
        (
            ButtonStyle.Primary, "factBtn", "Generate Fact", true
        );
        
        await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().AddEmbed(messageEmbed).AddComponents(button));
        
        await Task.Delay(TimeSpan.FromMinutes(15));

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddComponents(disableButton));
    }
} 
