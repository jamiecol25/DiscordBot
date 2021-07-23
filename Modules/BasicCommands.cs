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

        [Command("addevent")]
        public async Task AddEvent(string title, string dateTime)
        {
            DateTime _event = DateTime.ParseExact(dateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

            var basFlag = "<:bas_flag:803585591291674655>";

            TimeZoneInfo estZone;
            try
            {
                estZone = TimeZoneInfo.FindSystemTimeZoneById("America/Cayman");
            }
            catch (TimeZoneNotFoundException)
            {
                estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            }

            DateTime estTime = TimeZoneInfo.ConvertTimeFromUtc(_event, estZone);
            var estDST = estTime.AddHours(-1);
            string ukFormat = _event.ToString("dd/MM/yyyy");

            ulong id = 801871478605611009;
            var channel = Context.Client.GetChannel(id) as IMessageChannel;

            var builder = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription($"📅")
                .AddField("Date:", ukFormat, false)
                .AddField("Time (UTC +1):", _event.ToShortTimeString(), true)
                .AddField("Time (EST):", estDST.ToShortTimeString(), true)
                .WithFooter("Please start gathering at the time shown above, events start 30 minutes after.")
                .WithColor(new Color(66, 191, 245));

            var embed = builder.Build();

            await channel.SendMessageAsync("@everyone");
            await channel.SendMessageAsync(null, false, embed);
            
            _logger.LogInformation($"{Context.User.Username} added an event.");
        }
    }
}
