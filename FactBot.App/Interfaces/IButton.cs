using DSharpPlus;
using DSharpPlus.EventArgs;

namespace FactBot.App.Interfaces;

public interface IButton
{
    public Task Execute(DiscordClient sender, ComponentInteractionCreateEventArgs e);
}