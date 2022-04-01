using Dopamine_API_Data_DOTNET5;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopamine_API_Data_DOTNET5
{
    public class DopamineContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DopamineContext(DbContextOptions<DopamineContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<PlayerData> PlayerData { get; set; } = null!; // TODO why is this here (( = null!; )) ?

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // combine both classes into one table
            modelBuilder.Entity<PlayerData>()
                .HasOne(a => a.Upgrades).WithOne(b => b.PlayerData)
                .HasForeignKey<Upgrades>(e => e.Id);
            modelBuilder.Entity<Upgrades>().ToTable("PlayerData");
            modelBuilder.Entity<PlayerData>().ToTable("PlayerData");

            // seed data
            PlayerData playerData = new PlayerData() { Id = 1, points = 10 };
            Upgrades upgrades =  new Upgrades() { Id = 1, BuildingOne = 1, BuildingTwo = 0 };
            //todoItem.Upgrades = upgrades;

            modelBuilder.Entity<PlayerData>().HasData(playerData);
            modelBuilder.Entity<Upgrades>().HasData(upgrades);
            // seed data
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#if ENABLED_FOR_LAZY_LOADING_USAGE
            string connectionString;

            connectionString = _configuration.GetConnectionString("DBConnectionString");

            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlite(connectionString);
            //#endif
        }
    }
}
