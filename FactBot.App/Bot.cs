using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using FactBot.App.Secrets;
using FactBot.App.Interactions;
using FactBot.App.Slash_Commands;
using Timer = System.Timers.Timer;

namespace FactBot.App;

public abstract class Bot
{
    public static DiscordClient Client { get; set; }
    private static readonly InteractionHandler InteractionHandler = new InteractionHandler();
    private static Timer Timer { get; set; } 

    public static async Task RunBotAsync()
    {
        var discordConfig = new DiscordConfiguration()
        {
            Intents = DiscordIntents.All,
            Token = Discord.BotToken,
            TokenType = TokenType.Bot,
            AutoReconnect = true
        };

        Client = new DiscordClient(discordConfig);

        SlashCommands();

        Client.Ready += Client_Ready;
        
        await Client.ConnectAsync();
        await Task.Delay(-1);
    }

    private static void SlashCommands()
    {
        var slashCommands = Client.UseSlashCommands();

        slashCommands.RegisterCommands<HelpCommand>();
        slashCommands.RegisterCommands<PingCommand>();
        slashCommands.RegisterCommands<FactCommand>();
    }

    private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
    {
        return Task.CompletedTask;
    }
}