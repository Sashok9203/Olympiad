using data_access.Entities;
using data_access.Entities.Configs;
using data_access.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace data_access.Data
{
    internal  static class DefaultData
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(Cities);
            modelBuilder.Entity<Country>().HasData(Countries);
            modelBuilder.Entity<Gender>().HasData(Genders);
            modelBuilder.Entity<Season>().HasData(Seasons);
            modelBuilder.Entity<Sport>().HasData(Sports);
            modelBuilder.Entity<Olympiad_>().HasData(Olympiads);
            modelBuilder.Entity<Award>().HasData(Awards);
            modelBuilder.Entity<Sportsman>().HasData(Sportsmans);
            modelBuilder.Entity<SportsmanAwardOlympiad>().HasData(SportsmanAwardOlympiadsInit());
        }

       

        public static IEnumerable< SportsmanAwardOlympiad> SportsmanAwardOlympiadsInit()
        {
            List<SportsmanAwardOlympiad> list = new();
            Sportsman[] WinterSportsmans = Sportsmans.Where(x => Sports[x.SportId-1].SeasonId == 1).ToArray();
            Sportsman[] SumemrSportsmans = Sportsmans.Where(x => Sports[x.SportId-1].SeasonId == 2).ToArray();

            for (int i = 1; i <= 7; i++)
            {
                for (int s = 0; s < SumemrSportsmans.Length; s++)
                {
                    list.Add(new()
                    {
                        AwardId = new Random().Next(1, 101) < 60 ? null : new Random().Next(1, 4),
                        OlympiadId = i,
                        SportsmanId = SumemrSportsmans[s].Id
                    });
                }
            }

            for (int i = 8; i <= 14; i++)
            {
                for (int s = 0; s < WinterSportsmans.Length; s++)
                {
                    list.Add(new()
                    {
                        AwardId = new Random().Next(1, 101) < 60 ? null : new Random().Next(1, 4),
                        OlympiadId = i,
                        SportsmanId = WinterSportsmans[s].Id
                    });
                }

            }

            return list;
        }
        public static readonly Sportsman[] Sportsmans =
        {
            new (){ Id = 1, Name = "Norman", Surname = "McCoole", CountryId = 19, Birthday = new DateTime(1985, 6, 15), SportId = 9, GenderId = 2 },
            new (){ Id = 2, Name = "Cassandre", Surname = "Doddemeade", CountryId = 14, Birthday = new DateTime(1978, 4, 23), SportId = 31, GenderId = 2 },
            new (){ Id = 3, Name = "Chloe", Surname = "Denham", CountryId = 10, Birthday = new DateTime(1992, 9, 8), SportId = 42, GenderId = 2 },
            new (){ Id = 4, Name = "Nicol", Surname = "Tolcher", CountryId = 16, Birthday = new DateTime(1988, 11, 30), SportId = 5, GenderId = 2 },
            new (){ Id = 5, Name = "Michelle", Surname = "Coney", CountryId = 22, Birthday = new DateTime(1993, 7, 19), SportId = 4, GenderId = 2 ,PhotoPath = "Images/Sportsmans/box.png"},
            new (){ Id = 6, Name = "Candis", Surname = "Holhouse", CountryId = 12, Birthday = new DateTime(1982, 3, 8), SportId = 5, GenderId = 2 },
            new (){ Id = 7, Name = "Granny", Surname = "Tarply", CountryId = 23, Birthday = new DateTime(1974, 5, 12), SportId = 11, GenderId = 2 },
            new (){ Id = 8, Name = "Budd", Surname = "Tresler", CountryId = 7, Birthday = new DateTime(1981, 9, 28), SportId = 13,  GenderId = 2 },
            new (){ Id = 9, Name = "Moise", Surname = "Tonner", CountryId = 23, Birthday = new DateTime(1975, 2, 7), SportId = 1, GenderId = 1 },
            new (){ Id = 10, Name = "Bria", Surname = "Vallentin", CountryId = 17, Birthday = new DateTime(1989, 8, 15), SportId = 33, GenderId = 2 },
            new (){ Id = 11, Name = "Mei", Surname = "Pietruszewicz", CountryId = 11, Birthday = new DateTime(1983, 4, 2), SportId = 27, GenderId = 2 },
            new (){ Id = 12, Name = "Trueman", Surname = "Mannock", CountryId = 1, Birthday = new DateTime(1979, 11, 19), SportId = 9, GenderId = 2 },
            new (){ Id = 13, Name = "Caye", Surname = "Robben", CountryId = 21, Birthday = new DateTime(1986, 7, 8), SportId = 4, GenderId = 2 ,PhotoPath = "Images/Sportsmans/box.png"},
            new (){ Id = 14, Name = "Mason", Surname = "Tatlow", CountryId = 22, Birthday = new DateTime(1987, 10, 3), SportId = 22, GenderId = 2 },
            new (){ Id = 15, Name = "Clem", Surname = "Waters", CountryId = 12, Birthday = new DateTime(1980, 1, 17), SportId = 38, GenderId = 1 },
            new (){ Id = 16, Name = "Candie", Surname = "Ricold", CountryId = 20, Birthday = new DateTime(1985, 3, 25), SportId = 1, GenderId = 2 },
            new (){ Id = 17, Name = "Wanids", Surname = "Bellham", CountryId = 11, Birthday = new DateTime(1987, 2, 4), SportId = 13, GenderId = 2 },
            new (){ Id = 18, Name = "Virginia", Surname = "Lilly", CountryId = 19, Birthday = new DateTime(1988, 7, 21), SportId = 46, GenderId = 2 },
            new (){ Id = 19, Name = "Casey", Surname = "Shardlow", CountryId = 14, Birthday = new DateTime(1972, 11, 16), SportId = 39, GenderId = 1 },
            new (){ Id = 20, Name = "Clarance", Surname = "Edson", CountryId = 11, Birthday = new DateTime(1981, 3, 9), SportId = 43, GenderId = 2 },
            new (){ Id = 21, Name = "Arte", Surname = "Broz", CountryId = 10, Birthday = new DateTime(1984, 6, 12), SportId = 22, GenderId = 2 },
            new (){ Id = 22, Name = "Charmane", Surname = "Groven", CountryId = 10, Birthday = new DateTime(1986, 9, 30), SportId = 8, GenderId = 2 },
            new (){ Id = 23, Name = "Lyon", Surname = "Matuszkiewicz", CountryId = 22, Birthday = new DateTime(1982, 1, 6), SportId = 17, GenderId = 2 },
            new (){ Id = 24, Name = "Jerrold", Surname = "Rodrigues", CountryId = 17, Birthday = new DateTime(1990, 5, 18), SportId = 19, GenderId = 1 },
            new (){ Id = 25, Name = "Angy", Surname = "Grimsley", CountryId = 19, Birthday = new DateTime(1975, 8, 27), SportId = 3, GenderId = 1 },
            new (){ Id = 26, Name = "Gregorio", Surname = "Ogelbe", CountryId = 21, Birthday = new DateTime(1979, 12, 9), SportId = 37, GenderId = 1 },
            new (){ Id = 27, Name = "Kariotta", Surname = "Bilovsky", CountryId = 22, Birthday = new DateTime(1983, 7, 2), SportId = 35, GenderId = 1 },
            new (){ Id = 28, Name = "Otes", Surname = "Farens", CountryId = 14, Birthday = new DateTime(1972, 4, 15), SportId = 36, GenderId = 2 },
            new (){ Id = 29, Name = "Chelsie", Surname = "Larderot", CountryId = 2, Birthday = new DateTime(1971, 8, 9), SportId = 10, GenderId = 2 },
            new (){ Id = 30, Name = "Raimundo", Surname = "Pressdee", CountryId = 8, Birthday = new DateTime(1989, 11, 21), SportId = 45, GenderId = 1 },
            new (){ Id = 31, Name = "Rudolf", Surname = "Yakobovicz", CountryId = 15, Birthday = new DateTime(1980, 2, 4), SportId = 12, GenderId = 2 },
            new (){ Id = 32, Name = "Francklin", Surname = "Purchall", CountryId = 18, Birthday = new DateTime(1978, 5, 17), SportId = 11, GenderId = 2 },
            new (){ Id = 33, Name = "Kaitlynn", Surname = "Matejic", CountryId = 10, Birthday = new DateTime(1986, 9, 12), SportId = 3, GenderId = 2 },
            new (){ Id = 34, Name = "Aloise", Surname = "Tebbet", CountryId = 3, Birthday = new DateTime(1995, 1, 30), SportId = 20, GenderId = 1 },
            new (){ Id = 35, Name = "Braden", Surname = "McKellen", CountryId = 6, Birthday = new DateTime(1984, 6, 5), SportId = 20, GenderId = 2 },
            new (){ Id = 36, Name = "Guillema", Surname = "Haime", CountryId = 4, Birthday = new DateTime(1982, 3, 16), SportId = 33, GenderId = 2 },
            new (){ Id = 37, Name = "Tressa", Surname = "Faveryear", CountryId = 7, Birthday = new DateTime(1987, 10, 22), SportId = 30, GenderId = 2 },
            new (){ Id = 38, Name = "Antonella", Surname = "Rookwell", CountryId = 7, Birthday = new DateTime(1994, 7, 19), SportId = 44, GenderId = 2 },
            new (){ Id = 39, Name = "Paquito", Surname = "Mixer", CountryId = 9, Birthday = new DateTime(1988, 12, 7), SportId = 31, GenderId = 2 },
            new (){ Id = 40, Name = "Glenden", Surname = "Welchman", CountryId = 5, Birthday = new DateTime(1981, 4, 28), SportId = 46, GenderId = 1 },
            new (){ Id = 41, Name = "Oona", Surname = "Brockwell", CountryId = 5, Birthday = new DateTime(1983, 6, 3), SportId = 45, GenderId = 2 },
            new (){ Id = 42, Name = "Gerti", Surname = "Sallan", CountryId = 21, Birthday = new DateTime(1979, 8, 14), SportId = 4, GenderId = 2 ,PhotoPath = "Images/Sportsmans/box.png"},
            new (){ Id = 43, Name = "Crystie", Surname = "Sheber", CountryId = 5, Birthday = new DateTime(1986, 10, 9), SportId = 8, GenderId = 2 },
            new (){ Id = 44, Name = "Rubia", Surname = "Carloni", CountryId = 1, Birthday = new DateTime(1993, 2, 17), SportId = 8, GenderId = 1 },
            new (){ Id = 45, Name = "Krystle", Surname = "Drejer", CountryId = 13, Birthday = new DateTime(1988, 7, 5), SportId = 2, GenderId = 2 },
            new (){ Id = 46, Name = "Dew", Surname = "Knath", CountryId = 5, Birthday = new DateTime(1987, 4, 30), SportId = 34, GenderId = 1 },
            new (){ Id = 47, Name = "Angelle", Surname = "Peschke", CountryId = 11, Birthday = new DateTime(1974, 11, 27), SportId = 36, GenderId = 1 },
            new (){ Id = 48, Name = "Bertram", Surname = "Sill", CountryId = 14, Birthday = new DateTime(1982, 10, 15), SportId = 10, GenderId = 1 },
            new (){ Id = 49, Name = "Erroll", Surname = "Burgh", CountryId = 18, Birthday = new DateTime(1977, 9, 7), SportId = 34, GenderId = 1 },
            new (){ Id = 50, Name = "Ambur", Surname = "Erskine", CountryId = 21, Birthday = new DateTime(1988, 11, 1), SportId = 40, GenderId = 2 }
        };
        public static readonly Award[] Awards =
        {
            new (){ Id = 1, Name = "Gold"},
            new (){ Id = 2, Name = "Silver"},
            new (){ Id = 3, Name = "Bronze"},
        };
       

        public static readonly Sport[] Sports =
        {
             new(){Id = 1, Name = "Basketball", SeasonId = 2},
             new(){Id = 2, Name = "Badminton", SeasonId = 2},
             new(){Id = 3, Name = "Baseball", SeasonId = 2},
             new(){Id = 4, Name = "Boxing", SeasonId = 2},
             new(){Id = 5, Name = "Wrestling is free", SeasonId = 2},
             new(){Id = 6, Name = "Greco-Roman struggle", SeasonId = 2},
             new(){Id = 7, Name = "Bicycle sport", SeasonId = 2},
             new(){Id = 8, Name = "Water polo", SeasonId = 2},
             new(){Id = 9, Name = "Volleyball", SeasonId = 2},
             new(){Id = 10, Name = "Beach volleyball", SeasonId = 2},
             new(){Id = 11, Name = "Handball", SeasonId = 2},
             new(){Id = 12, Name = "Sports gymnastics", SeasonId = 2},
             new(){Id = 13, Name = "Artistic gymnastics", SeasonId = 2},
             new(){Id = 14, Name = "Academic dam", SeasonId = 2},
             new(){Id = 15, Name = "Kayaking and canoeing", SeasonId = 2},
             new(){Id = 16, Name = "Judo", SeasonId = 2},
             new(){Id = 17, Name = "Equestrian sport", SeasonId = 2},
             new(){Id = 18, Name = "Athletics", SeasonId = 2},
             new(){Id = 19, Name = "Sailing sport", SeasonId = 2},
             new(){Id = 20, Name = "swimming", SeasonId = 2},
             new(){Id = 21, Name = "Synchronized swimming", SeasonId = 2},
             new(){Id = 22, Name = "Jumping into the water", SeasonId = 2},
             new(){Id = 23, Name = "Trampoline jumping", SeasonId = 2},
             new(){Id = 24, Name = "Modern pentathlon", SeasonId = 2},
             new(){Id = 25, Name = "Archery", SeasonId = 2},
             new(){Id = 26, Name = "Bullet shooting", SeasonId = 2},
             new(){Id = 27, Name = "Bench shooting", SeasonId = 2},
             new(){Id = 28, Name = "Taekwondo", SeasonId = 2},
             new(){Id = 29, Name = "Tennis", SeasonId = 2},
             new(){Id = 30, Name = "Triathlon", SeasonId = 2},
             new(){Id = 31, Name = "Weightlifting", SeasonId = 2},
             new(){Id = 32, Name = "Fencing", SeasonId = 2},
             new(){Id = 33, Name = "Football", SeasonId = 2},
             new(){Id = 34, Name = "Field hockey", SeasonId = 2},

             new(){Id = 35, Name = "Biathlon", SeasonId = 1},
             new(){Id = 36, Name = "Alpine skiing", SeasonId = 1}, 
             new(){Id = 37, Name = "Curling", SeasonId = 1},
             new(){Id = 38, Name = "Skating sport ", SeasonId = 1},
             new(){Id = 39, Name = "Ski races ", SeasonId = 1},
             new(){Id = 40, Name = "Cross-country skiing", SeasonId = 1},
             new(){Id = 41, Name = "Ski jumping from a springboard", SeasonId = 1},
             new(){Id = 42, Name = "Snowboard ", SeasonId = 1},
             new(){Id = 43, Name = "Figure skating", SeasonId = 1},
             new(){Id = 44, Name = "Freestyle ", SeasonId = 1},
             new(){Id = 45, Name = "Hockey", SeasonId = 1},
             new(){Id = 46, Name = "Short track", SeasonId = 1},
        };

        public static readonly Season[] Seasons =
        {
            new (){ Id = 1, Name = "Winter"},
            new (){ Id = 2, Name = "Summer"},
        };

        


        public static readonly Gender[] Genders =
        {
            new (){ Id = 1, Name = "Male"},
            new (){ Id = 2, Name = "Female"},
        };

        public static readonly Country[] Countries =
        {
            new(){Id = 1,Name ="Sweden" ,FlagPath = "Images/Flags/Flag_of_Swedenn.png" },
            new(){Id = 2,Name ="China",FlagPath = "Images/Flags/Flag_of_China.png" },
            new(){Id = 3,Name ="Japan",FlagPath = "Images/Flags/Flag_of_Japan.png" },
            new(){Id = 4,Name ="France",FlagPath = "Images/Flags/Flag_of_France.png" },
            new(){Id = 5,Name ="Poland",FlagPath = "Images/Flags/Flag_of_Poland.png" },
            new(){Id = 6,Name ="Germany",FlagPath = "Images/Flags/Flag_of_Germany.png" },
            new(){Id = 7,Name ="Ukraine",FlagPath = "Images/Flags/Flag_of_Ukraine.png" },
            new(){Id = 8,Name ="Greece",FlagPath = "Images/Flags/Flag_of_Greece.png" },
            new(){Id = 9,Name ="Italy",FlagPath = "Images/Flags/Flag_of_Italy.png" },
            new(){Id = 10,Name ="Spain",FlagPath = "Images/Flags/Flag_of_Spain.png" },
            new(){Id = 11,Name ="Netherland",FlagPath = "Images/Flags/Flag_of_the_Netherlands.png" },
            new(){Id = 12,Name ="Norway",FlagPath = "Images/Flags/Flag_of_Norway.png" },
            new(){Id = 13,Name ="Denmark",FlagPath = "Images/Flags/Flag_of_Denmark.png" },
            new(){Id = 14,Name ="Finland",FlagPath = "Images/Flags/Flag_of_Finland.png" },
            new(){Id = 15,Name ="United Kingdom",FlagPath = "Images/Flags/Flag_of_the_United_Kingdom.png" },
            new(){Id = 16,Name ="United States",FlagPath = "Images/Flags/Flag_of_the_United_States.png" },
            new(){Id = 17,Name ="Australia",FlagPath = "Images/Flags/Flag_of_Australia.png" },
            new(){Id = 18,Name ="Canada",FlagPath = "Images/Flags/Flag_of_Canada.png" },
            new(){Id = 19,Name ="New Zealand",FlagPath = "Images/Flags/Flag_of_New_Zealand.png" },
            new(){Id = 20,Name ="Ireland",FlagPath = "Images/Flags/Flag_of_Ireland.png" },
            new(){Id = 21,Name ="South Africa",FlagPath = "Images/Flags/Flag_of_South_Africa.png" },
            new(){Id = 22,Name ="India",FlagPath = "Images/Flags/Flag_of_India.png" },
            new(){Id = 23,Name ="Singapore",FlagPath = "Images/Flags/Flag_of_Singapore.png" },
            new(){Id = 24,Name ="Malaysia" ,FlagPath = "Images/Flags/Flag_of_Malaysia.png" }
        };

        public static readonly Olympiad_[] Olympiads =
       {
           new(){Id = 1,Year=2008,CityId=5, SeasonId = 2},
           new(){Id = 2,Year=1992,CityId=29, SeasonId = 2},//Summer
           new(){Id = 3,Year=1996,CityId=84, SeasonId = 2},
           new(){Id = 4,Year=2000,CityId=85, SeasonId = 2},
           new(){Id = 5,Year=2004,CityId=22, SeasonId = 2},
           new(){Id = 6,Year=2012,CityId=79, SeasonId = 2},
           new(){Id = 7,Year=2020,CityId=7, SeasonId = 2},

           new(){Id = 8,Year=1992,CityId=12, SeasonId = 1},//Winter
           new(){Id = 9,Year=1994,CityId=72, SeasonId = 1},
           new(){Id = 10,Year=1998,CityId=8, SeasonId = 1},
           new(){Id = 11,Year=2002,CityId=83, SeasonId = 1},
           new(){Id = 12,Year=2006,CityId=27, SeasonId = 1},
           new(){Id = 13,Year=2010,CityId=89, SeasonId = 1},
           new(){Id = 14,Year=2022,CityId=5, SeasonId = 1},
        };

        public static readonly City[] Cities =
        {
            new City { Id = 1, CountryId = 1, Name = "Stockholm"},
            new City { Id = 2, CountryId = 1, Name = "Gothenburg"},
            new City { Id = 3, CountryId = 1, Name = "Malmö"},

            new City { Id = 4, CountryId = 2, Name = "Shanghai"},
            new City { Id = 5, CountryId = 2, Name = "Beijing"},
            new City { Id = 6, CountryId = 2, Name = "Guangzhou"},

            new City { Id = 7, CountryId = 3, Name = "Tokyo"},
            new City { Id = 8, CountryId = 3, Name = "Nagano"},
            new City { Id = 9, CountryId = 3, Name = "Kyoto"},

            new City { Id = 10, CountryId = 4, Name = "Paris"},
            new City { Id = 11, CountryId = 4, Name = "Marseille"},
            new City { Id = 12, CountryId = 4, Name = "Albertville"},

            new City { Id = 13, CountryId = 5, Name = "Warsaw"},
            new City { Id = 14, CountryId = 5, Name = "Kraków"},
            new City { Id = 15, CountryId = 5, Name = "Wrocław"},

            new City { Id = 16, CountryId = 6, Name = "Berlin"},
            new City { Id = 17, CountryId = 6, Name = "Munich"},
            new City { Id = 18, CountryId = 6, Name = "Hamburg"},

            new City { Id = 19, CountryId = 7, Name = "Kyiv"},
            new City { Id = 20, CountryId = 7, Name = "Kharkiv"},
            new City { Id = 21, CountryId = 7, Name = "Lviv"},

            new City { Id = 22, CountryId = 8, Name = "Athens"},
            new City { Id = 23, CountryId = 8, Name = "Thessaloniki"},
            new City { Id = 24, CountryId = 8, Name = "Patras"},

            new City { Id = 25, CountryId = 9, Name = "Rome"},
            new City { Id = 26, CountryId = 9, Name = "Milan"},
            new City { Id = 27, CountryId = 9, Name = "Turin"},

            new City { Id = 28, CountryId = 10, Name = "Madrid"},
            new City { Id = 29, CountryId = 10, Name = "Barcelona"},
            new City { Id = 30, CountryId = 10, Name = "Valencia"},

            new City { Id = 67, CountryId = 11, Name = "Amsterdam"},
            new City { Id = 68, CountryId = 11, Name = "Rotterdam"},
            new City { Id = 69, CountryId = 11, Name = "Utrecht"},

            new City { Id = 70, CountryId = 12, Name = "Oslo"},
            new City { Id = 71, CountryId = 12, Name = "Bergen"},
            new City { Id = 72, CountryId = 12, Name = "Lillehammer"},

            new City { Id = 73, CountryId = 13, Name = "Copenhagen"},
            new City { Id = 74, CountryId = 13, Name = "Aarhus"},
            new City { Id = 75, CountryId = 13, Name = "Odense"},

            new City { Id = 76, CountryId = 14, Name = "Helsinki"},
            new City { Id = 77, CountryId = 14, Name = "Espoo"},
            new City { Id = 78, CountryId = 14, Name = "Tampere"},

            new City { Id = 79, CountryId = 15, Name = "London"},
            new City { Id = 80, CountryId = 15, Name = "Birmingham"},
            new City { Id = 81, CountryId = 15, Name = "Manchester"},

            new City { Id = 82, CountryId = 16, Name = "New York"},
            new City { Id = 83, CountryId = 16, Name = "Salt Lake City"},
            new City { Id = 84, CountryId = 16, Name = "Atlanta"},

            new City { Id = 85, CountryId = 17, Name = "Sydney"},
            new City { Id = 86, CountryId = 17, Name = "Melbourne"},
            new City { Id = 87, CountryId = 17, Name = "Brisbane"},

            new City { Id = 88, CountryId = 18, Name = "Toronto"},
            new City { Id = 89, CountryId = 18, Name = "Vancouver"},
            new City { Id = 90, CountryId = 18, Name = "Montreal"},

            new City { Id = 91, CountryId = 19, Name = "Auckland"},
            new City { Id = 92, CountryId = 19, Name = "Wellington"},
            new City { Id = 93, CountryId = 19, Name = "Christchurch"},

            new City { Id = 94, CountryId = 20, Name = "Dublin"},
            new City { Id = 95, CountryId = 20, Name = "Cork"},
            new City { Id = 96, CountryId = 20, Name = "Limerick"},

            new City { Id = 97, CountryId = 21, Name = "Johannesburg"},
            new City { Id = 98, CountryId = 21, Name = "Cape Town"},
            new City { Id = 99, CountryId = 21, Name = "Durban"},

            new City { Id = 100, CountryId = 22, Name = "Mumbai"},
            new City { Id = 101, CountryId = 22, Name = "Delhi"},
            new City { Id = 102, CountryId = 22, Name = "Bangalore"},

            new City { Id = 103, CountryId = 23, Name = "Singapore"},
            new City { Id = 104, CountryId = 23, Name = "Singapore City"},
            new City { Id = 105, CountryId = 23, Name = "Jurong East"},

            new City { Id = 106, CountryId = 24, Name = "Kuala Lumpur"},
            new City { Id = 107, CountryId = 24, Name = "George Town"},
            new City { Id = 108, CountryId = 24, Name = "Ipoh" }
        };


    }
}
