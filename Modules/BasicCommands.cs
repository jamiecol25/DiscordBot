using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBot.Modules
{
    public class BasicCommands : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<BasicCommands> _logger;

        public BasicCommands(ILogger<BasicCommands> logger)
            => _logger = logger;

        [Command("clean")]
        public async Task PingAsync()
        {
            var channelID = Context.Channel.Id;
            var channel = Context.Client.GetChannel(channelID) as IMessageChannel;
            
            var messages = Context.Channel.GetMessagesAsync().CountAsync();
                
            await channel.SendMessageAsync(messages.ToString());

        }
    }
}
