using DSharpPlus;
using DSharpPlus.EventArgs;

namespace FactBot.App.Interfaces;

public interface IButton
{
    public Task ButtonHandler(DiscordClient sender, ComponentInteractionCreateEventArgs e);
}