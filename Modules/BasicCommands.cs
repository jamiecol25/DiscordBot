using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
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

            var message = "test";
                
            await channel.SendMessageAsync(message.ToString());

        }

        [Command("getitems")]
        public async Task Meme()
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync("https://localhost:44358/api/bankitems");

            JArray arr = JArray.Parse(result);

            JObject post = JObject.Parse(arr[0].ToString());

            var builder = new EmbedBuilder()
                .WithColor(new Color(181, 126, 220))
                .WithDescription(post["id"].ToString())
                .WithTitle(post["itemName"].ToString())
                .WithFooter($"🗨 {post["amountAvailable"]}");

            var embed = builder.Build();

            ulong id = 802106966499000331;
            var channel = Context.Client.GetChannel(id) as IMessageChannel;

            await channel.SendMessageAsync(null, false, embed);
        }
    }
}
