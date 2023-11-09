using System.Xml;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace FactBot.App.Slash_Commands;

public class HelpCommand : ApplicationCommandModule
{
    [SlashCommand("help", "Gives information about the bot & available commands.")]
    public async Task HelpCommandAsync(InteractionContext ctx)
    {
        var helpEmbed = new DiscordEmbedBuilder()
        {
            Title = "📝 Getting Started",
            Color = DiscordColor.White,
            Description = "Get a random fact! Type one of the commands below to get a fact." +
                          $"{Environment.NewLine}FactBot powered by [DSharpPlus 4.4.2]" +
                          $"(https://dsharpplus.github.io/DSharpPlus/index.html) and [Docker](https://www.docker.com/).",
        };

        helpEmbed.AddField
        (
            "**🧠  Fact Commands**",
            $"📚  </fact:1171471344614002728> {Environment.NewLine}" +
            $"🔘  </factbutton:1171850287112265768>",
            inline: true
        );

        helpEmbed.AddField
        (
            "🛠️  Other Commands",
            $"🆘  </help:1171463418176348282> {Environment.NewLine}" +
            "🏓  </ping:1171459839264837692>",
            inline: true
        );

        var serverCount = ctx.Client.Guilds.Count;
        var shardCount = ctx.Client.ShardCount;
        var ping = ctx.Client.Ping;
        var botVersion = ctx.Client.VersionString;

        helpEmbed.WithFooter
        ($"*Bot Info  •  " +
         $"Total Servers: {serverCount}  •  " +
         $"Total Shards: {shardCount}  •  " +
         $"Ping: {ping}  •  " +
         $"Version: {botVersion}"
        );

        var addButton = new DiscordLinkButtonComponent
        (
            "https://discord.com/api/oauth2/authorize?client_id=1171170464421388308" +
            "&permissions=8&scope=bot%20applications.commands",
            "Add FactBot To A Server"
        );

        var apiButton = new DiscordLinkButtonComponent("https://api-ninjas.com/api/facts", "Utilize Fact API");

        var messageBuilder = new DiscordMessageBuilder()
            .AddEmbed(helpEmbed)
            .AddComponents(addButton, apiButton);

        await ctx.CreateResponseAsync
        (
            InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder(messageBuilder)
        );
    }
}