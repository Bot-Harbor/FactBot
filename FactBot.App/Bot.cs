using System.Timers;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using FactBot.App.Secrets;
using FactBot.App.Interactions;
using FactBot.App.Slash_Commands;
using Timer = System.Threading.Timer;

namespace FactBot.App;

public abstract class Bot : Discord
{
    public static DiscordClient Client { get; set; }
    private static readonly InteractionHandler InteractionHandler = new InteractionHandler();

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
        
        DailyMessageTimer();
        
        Client.Ready += Client_Ready;

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

    private static void DailyMessageTimer()
    {
        var now = DateTime.Now;
        var scheduledTime = new DateTime(now.Year, now.Month, now.Day, 14, 0, 0);

        if (now > scheduledTime)
        {
            scheduledTime = scheduledTime.AddDays(1);
        }

        var delay = scheduledTime - now;

        var timer = new System.Timers.Timer(delay.TotalMilliseconds);
        timer.Elapsed += DailyFact.SendMessage;
        timer.Enabled = true;
        timer.AutoReset = true;
    }
}