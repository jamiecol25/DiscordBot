using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiscordBot.Database
{
    public class BankItems
    {
        private readonly DatabaseContext _context;

        public BankItems(DatabaseContext context)
        {
            _context = context;
        }


    }
}
