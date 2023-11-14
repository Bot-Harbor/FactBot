using System.Timers;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using FactBot.App.Interactions;
using FactBot.App.Services;

namespace FactBot.App;

public abstract class DailyFact
{
    private static readonly InteractionHandler InteractionHandler = new InteractionHandler();

    public static async void SendMessage(object sender, ElapsedEventArgs e)
    {
        foreach (var guild in Bot.Client.Guilds)
        {
            var channel = guild.Value.Channels.Values.FirstOrDefault
            (c =>
                c.Type == ChannelType.Text && c.Name.ToLower() == "general" ||
                c.Type == ChannelType.Text && c.Name.ToLower() == "y" ||
                c.Type == ChannelType.Text && c.Name.ToLower() == "💬general💬"
            );

            var factServiceInstance = FactService.GetInstance();
            var facts = await factServiceInstance.GetAll(1);

            foreach (var fact in facts)
            {
                var builder = new DiscordEmbedBuilder()
                {
                    Title = "Daily Fact 📅 📚",
                    Description = $"```{fact.Fact}.```",
                    Color = InteractionHandler.ExecuteColorEmbed(new RandomEmbedColor())
                };

                if (channel != null)
                {
                    await channel.SendMessageAsync(builder);
                }
            }
        }
    }
}