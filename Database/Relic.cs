using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiscordBot.Database
{
    class Relic
    {
        [Key]
        public long RelicId { get; set; }
        public string RelicName { get; set; }
        public int StageOne { get; set; }
    }
}
