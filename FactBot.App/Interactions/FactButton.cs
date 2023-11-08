using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using FactBot.App.Interfaces;
using FactBot.App.Services;

namespace FactBot.App.Interactions;

public class FactButton : IButton
{
    private readonly InteractionHandler _interactionHandler = new InteractionHandler();

    public async Task Execute(DiscordClient sender, ComponentInteractionCreateEventArgs e)
    {
        if (e.Interaction.Data.CustomId == "factBtn")
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

                    await e.Interaction.CreateResponseAsync
                    (
                        InteractionResponseType.ChannelMessageWithSource, new
                            DiscordInteractionResponseBuilder().AddEmbed(embed: factEmbed)
                    );
                }
            }
            else
            {
                var errorEmbed = new DiscordEmbedBuilder()
                {
                    Title = "No Facts Available 🚫",
                    Color = DiscordColor.Red
                };

                await e.Interaction.CreateResponseAsync
                (
                    InteractionResponseType.ChannelMessageWithSource, new
                        DiscordInteractionResponseBuilder().AddEmbed(errorEmbed)
                );
            }
        }
    }
}