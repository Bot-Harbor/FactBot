using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using FactBot.App.Interfaces;

namespace FactBot.App.Interactions;

public class InteractionHandler
{
    public Optional<DiscordColor> ExecuteColorEmbed(IColor command)
    {
        return command.Execute();
    }
    
    public Task ExecuteButton(IButton button, DiscordClient sender, ComponentInteractionCreateEventArgs e)
    {
        return button.Execute(sender, e);
    }
}