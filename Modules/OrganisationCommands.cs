using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using DiscordBot.Models;

namespace DiscordBot.Modules
{
    public class OrganisationCommands : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<OrganisationCommands> _logger;

        public OrganisationCommands(ILogger<OrganisationCommands> logger)
            => _logger = logger;

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
        [Command("generateoutposts")]
        public async Task GenerateOutposts()
        {
            
            ulong id = 802106966499000331;
            var channel = Context.Client.GetChannel(id) as IMessageChannel;

            Random randomLow = new Random();
            Random randomMedium = new Random();

            List<Outpost> lowOutposts = new List<Outpost>
            {
                new Outpost { OutpostRegion = "Gustaberg", LevelRange = "1-11"},
                new Outpost { OutpostRegion = "Ronfaure", LevelRange = "1-11"},
                new Outpost { OutpostRegion = "Norvallen", LevelRange = "24-27"},
                new Outpost { OutpostRegion = "Kolshushu", LevelRange = "27-30"},
                new Outpost { OutpostRegion = "Qufim", LevelRange = "20-24"}
                
            };

            List<Outpost> mediumOutposts = new List<Outpost>
            {
                new Outpost {OutpostRegion = "Aragoneu", LevelRange = "32-36"},
                new Outpost { OutpostRegion = "Derfland", LevelRange = "18-37"},
                new Outpost {OutpostRegion = "Li'Telor", LevelRange = "32-38"}
            };

            int lowIndex = randomLow.Next(lowOutposts.Count);
            
            var lowBuilder = new EmbedBuilder()
                .WithTitle(lowOutposts[lowIndex].OutpostRegion.ToString())
                .WithDescription(lowOutposts[lowIndex].LevelRange.ToString())
                .WithFooter("Conquest starts at 21:00 UTC+1 | 15:00 EST")
                .WithColor(new Color(66, 191, 245));

            var lowEmbed = lowBuilder.Build();

            
            await channel.SendMessageAsync(null,false,lowEmbed);

            int mediumIndex = randomLow.Next(mediumOutposts.Count);

            var mediumBuilder = new EmbedBuilder()
                .WithTitle(mediumOutposts[mediumIndex].OutpostRegion.ToString())
                .WithDescription(mediumOutposts[mediumIndex].LevelRange.ToString())
                .WithFooter("Conquest starts at 21:00 UTC+1 | 15:00 EST")
                .WithColor(new Color(66, 191, 245));

            var mediumEmbed = mediumBuilder.Build();


            await channel.SendMessageAsync(null, false, mediumEmbed);
        }
    }
}