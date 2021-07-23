using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.Database
{
    public class DatabaseContext : DbContext 
    {
        public DbSet<BankItem> BankItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySql("server=localhost; user=root; password=Password1; database=ragnarok_ls; port=3306");
    }

    public class BankItem
    {
        public ulong Id { get; set; }
        public string ItemName { get; set; }
        public int AmountAvailable { get; set; }
    }
}
