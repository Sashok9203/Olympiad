using data_access.Entities;
using data_access.Entities.Configs;
using data_access.Entityes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Data
{
    internal  static class DefaultData
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(Cities);
            modelBuilder.Entity<Country>().HasData(Countries);
            modelBuilder.Entity<Genre>().HasData(Genres);
            modelBuilder.Entity<Photo>().HasData(Photos);
            modelBuilder.Entity<Season>().HasData(Seasons);
            modelBuilder.Entity<Sport>().HasData(Sports);
            modelBuilder.Entity<Olympiad>().HasData(Olympiads);
            modelBuilder.Entity<Award>().HasData(Awards);
            modelBuilder.Entity<Sportsman>().HasData(Sportsmans);
            modelBuilder.Entity<SportsmanOlympiad>().HasData(SportsmanOlympiads);
        }
        public static readonly SportsmanOlympiad[] SportsmanOlympiads =
       {
        };
        public static readonly Sportsman[] Sportsmans =
        {
        };
        public static readonly Award[] Awards =
        {
        };
        public static readonly Olympiad[] Olympiads =
        {
        };
        public static readonly Sport[] Sports =
        {
        };
        public static readonly Season[] Seasons =
        {
        };
        public static readonly Photo[] Photos =
        {
        };
        public static readonly Genre[] Genres =
        {
        };

        public static readonly Country[] Countries =
        {
            new(){Id = 1,  Name ="Sweden" },
            new(){Id = 2,  Name ="China" },
            new(){Id = 3,  Name ="Japan" },
            new(){Id = 4,  Name ="France"},
            new(){Id = 5,  Name ="Poland"},
            new(){Id = 6,  Name ="Germany"},
            new(){Id = 7,  Name = "Ukraine"}
        };

        public static readonly City[] Cities =
       {
            new(){ Id = 1, CountryId =  1, Name = "Stockholm" },
            new(){ Id = 2, CountryId =  2, Name = "Wuhan" },
            new(){ Id = 3, CountryId =  3, Name = "Tokyo" },
            new(){ Id = 4, CountryId =  4, Name = "Paris" },
            new(){ Id = 5, CountryId =  5, Name = "Warsaw" },
            new(){ Id = 6, CountryId =  6, Name = "Berlin" },
            new(){ Id = 7, CountryId =  7, Name = "Rivne" },
        };
    }
}
