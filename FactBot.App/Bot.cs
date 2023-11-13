using System.Threading.Channels;
using System.Timers;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using FactBot.App.Secrets;
using FactBot.App.Interactions;
using FactBot.App.Models;
using FactBot.App.Services;
using FactBot.App.Slash_Commands;

namespace FactBot.App;

public abstract class Bot : Discord
{
    private static DiscordClient Client { get; set; }
    private static readonly InteractionHandler InteractionHandler = new InteractionHandler();
    private static readonly FactService FactServiceInstance = FactService.GetInstance();
    private static List<FactModel> _facts;

    public static async Task RunBotAsync()
    {
        var discordConfig = new DiscordConfiguration()
        {
            Intents = DiscordIntents.All,
            Token = BotToken,
            TokenType = TokenType.Bot,
            AutoReconnect = true
        };

        Client = new DiscordClient(discordConfig);

        SlashCommands();

        Client.Ready += Client_Ready;

        _facts = await FactServiceInstance.GetAll(1);

        var now = DateTime.Now;
        var next9Am = new DateTime(now.Year, now.Month, now.Day, 9, 0, 0);
        if (now > next9Am)
        {
            next9Am = next9Am.AddDays(1);
        }
        var delay = next9Am - now;
        
        var myTimer = new System.Timers.Timer(delay.TotalMilliseconds);
        myTimer.Elapsed += DailyFact;
        myTimer.Start();

        Client.ComponentInteractionCreated += async (sender, e) =>
        {
            await InteractionHandler.ExecuteButton(new FactButton(), sender, e);
        };

        await Client.ConnectAsync();
        await Task.Delay(-1);
    }

    private static void SlashCommands()
    {
        var slashCommands = Client.UseSlashCommands();

        slashCommands.RegisterCommands<HelpCommand>();
        slashCommands.RegisterCommands<PingCommand>();
        slashCommands.RegisterCommands<FactButtonCommand>();
        slashCommands.RegisterCommands<FactCommand>();
    }

    private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
    {
        return Task.CompletedTask;
    }

    private static async void DailyFact(object sender, ElapsedEventArgs e)
    {
        foreach (var guild in Client.Guilds)
        {
            var generalChannel = guild.Value.Channels.Values.FirstOrDefault
            (
                c =>
                    c.Type == ChannelType.Text && c.Name.ToLower() == "general" ||
                    c.Type == ChannelType.Text && c.Name.ToLower() == "y" ||
                    c.Type == ChannelType.Text && c.Name.ToLower() == "💬general💬"
            );
            
            if (generalChannel != null)
            {
                foreach (var fact in _facts)
                {
                    var builder = new DiscordEmbedBuilder()
                    {
                        Title = "Daily Fact 📅 📚",
                        Description = $"```{fact.Fact}.```",
                        Color = InteractionHandler.ExecuteColorEmbed(new RandomEmbedColor())
                    };

                    await generalChannel.SendMessageAsync(builder);
                }
            }
            else
            {
                Console.WriteLine($"General channel not found in guild {guild.Value.Name} ({guild.Value.Id})");
            }
        }
    }
}