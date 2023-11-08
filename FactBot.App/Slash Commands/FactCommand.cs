using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using FactBot.App.Interactions;
using FactBot.App.Services;

namespace FactBot.App.Slash_Commands;

public class FactCommand : ApplicationCommandModule
{
    private readonly InteractionHandler _interactionHandler = new InteractionHandler();
    
    [SlashCommand("fact", "Generates a random fact.")]
    public async Task CommandAsync(InteractionContext ctx)
    {
        var factServiceInstance = FactService.GetInstance();
        var facts = await factServiceInstance.GetAll(1);

        if (facts.Count != 0)
        {
            foreach (var fact in facts)
            {
                var factEmbed = new DiscordEmbedBuilder()
                {
                    Title = "Fact 📚 📙",
                    Description = $"```{fact.Fact}.```",
                    Color = _interactionHandler.ExecuteColorEmbed(new RandomEmbedColor())
                };
                
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().AddEmbed(embed: factEmbed));
            }
        }
        else
        {
           var errorEmbed = new DiscordEmbedBuilder()
                {
                    Title = "No Facts Available 🚫",
                    Color = DiscordColor.Red
                };
                
                await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder().AddEmbed(errorEmbed));
        }
    }
}