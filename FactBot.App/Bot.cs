using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using FactBot.App.Secrets;
using FactBot.App.Interactions;
using FactBot.App.Slash_Commands;

namespace FactBot.App;

public abstract class Bot : Discord
{
    private static DiscordClient Client { get; set; }
    private static readonly InteractionHandler InteractionHandler = new InteractionHandler();
    
    public static async Task RunBotAsync()
    {
        var discordConfig = new DiscordConfiguration()
        {
            Intents = DiscordIntents.All,
            Token = BotToken,
            TokenType = TokenType.Bot,
            // Will attempt to reconnect if bot crashes
            AutoReconnect = true
        };

        Client = new DiscordClient(discordConfig);
        
        SlashCommands();
        
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
        
        // Registers Slash Commands
        slashCommands.RegisterCommands<HelpCommand>();
        slashCommands.RegisterCommands<PingCommand>();
        slashCommands.RegisterCommands<FactButtonCommand>();
        slashCommands.RegisterCommands<FactCommand>();
    }
    
    private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
    {
        return Task.CompletedTask;
    }
}