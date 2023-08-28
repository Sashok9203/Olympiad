using data_access.Entities;
using data_access.Entities.Configs;
using data_access.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Data
{
    internal class OlympiadDBContext :DbContext
    {
        public OlympiadDBContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connect = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=OlympiadDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Pooling = true";
            optionsBuilder.UseSqlServer(connect);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Award>(new AwardConfig());
            modelBuilder.ApplyConfiguration<City>(new CityConfig());
            modelBuilder.ApplyConfiguration<Country>(new CountryConfig());
            modelBuilder.ApplyConfiguration<Genre>(new GenreConfig());
            modelBuilder.ApplyConfiguration<Olympiad>(new OlympiadConfig());
            modelBuilder.ApplyConfiguration<Season>(new SeasonConfig());
            modelBuilder.ApplyConfiguration<Sport>(new SportConfig());
            modelBuilder.ApplyConfiguration<Sportsman>(new SportsmanConfig());
            modelBuilder.ApplyConfiguration<SportsmanAwardOlympiad>(new SportsmanAwardOlympiadConfig());
            DefaultData.Initialize(modelBuilder);
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Olympiad> Olympiads { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<Sportsman> Sportsmans { get; set; }
        public DbSet<SportsmanAwardOlympiad> SportsmanAwardOlympiads { get; set; }

    }
}
