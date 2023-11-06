using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using FactBot.App.Constants;

namespace FactBot.App;

public abstract class Bot : Discord
{
    public static DiscordClient Client { get; set; }
    public static CommandsNextExtension Commands { get; set; }

    public static async Task RunBotAsync()
    {
        var discordConfig = new DiscordConfiguration()
        {
            Intents = DiscordIntents.All,
            Token = BotToken,
            TokenType = TokenType.Bot,
            //will attempt to reconnect if bot crashes
            AutoReconnect = true
        };

        Client = new DiscordClient(discordConfig);

        //Fired when this client has successfully completed its handshake with the websocket gateway.
        Client.Ready += Client_Ready;
    
        //Connects to gateway
        await Client.ConnectAsync();
        //Runs bot indefinitely
        await Task.Delay(-1);
    }
    
    private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
    {
        return Task.CompletedTask;
    }
}