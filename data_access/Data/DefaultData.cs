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
            Sportsman[] sportsmans = generateSportsman().ToArray();
            modelBuilder.Entity<Sportsman>().HasData(sportsmans);
            modelBuilder.Entity<SportsmanAwardOlympiad>().HasData(SportsmanAwardOlympiadsInit(sportsmans));
        }

       

        public static IEnumerable<SportsmanAwardOlympiad> SportsmanAwardOlympiadsInit(Sportsman[] sportsmans)
        {
            Random rnd = new();
            for (int i = 0; i < Olympiads.Length; i++)
            {
                for (int x = 0; x < sportsmans.Length; x++)
                {
                    
                    if ((Sports[sportsmans[x].SportId-1].SeasonId == Olympiads[i].SeasonId)
                        && (Olympiads[i].Year - sportsmans[x].Birthday.Year >= 16) 
                        && (Olympiads[i].Year - sportsmans[x].Birthday.Year <= 39)) 

                    yield return new()
                    {
                        OlympiadId = Olympiads[i].Id,
                        SportsmanId = sportsmans[x].Id,
                        AwardId = rnd.Next(1, 101) < 70 ? null : rnd.Next(1, 4),
                    };
                }
            }
           
        }
        

        private static IEnumerable<Sportsman> generateSportsman()
        {
            int idIndex = 1;
            Random rnd = new ();
            DateTime start = new (1970,1,1);
            DateTime end = new (2005, 1, 1);
            for (int i = 1; i <= Countries.Length; i++)
            {
                for (int x = 1; x <= Sports.Length; x++)
                {
                    yield return new Sportsman
                    {
                        Id = idIndex++,
                        Name = names[rnd.Next(0, 100)],
                        Surname = surnames[rnd.Next(0, 100)],
                        CountryId = i,
                        Birthday = getDate(start, end),
                        SportId = x,
                        GenderId = rnd.Next(1, 3),
                    };
                }
            }
        }

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

        static readonly string[] names = 
        {
            "Britt","Michelle","Marisa","Cornelle","Levey","Foster","Fransisco","Christi","Theressa","Sapphira","Brod",
            "Waring","Percy","Dore","Valentina","Stefano","Sigfried","Jolynn","Codi","Carree","Anissa","Staford","Angelika",
            "Tremain","Devlen","Aldous","Cristobal","Naoma","Grayce","Carmencita","Dorella","Kiah","Tess","Tiphany",
            "Lazaro","Ellyn","Milena","Emlyn","Didi","Lauraine","Gaelan","Gretchen","Neddie","Skylar","Eddy","Elmira",
            "Germain","Ely","Marijo","Mariel","Sofie","Brandon","Obed","Walden","Brigitte","Arleta","Electra","Vicki",
            "Adelind","Kenyon","Star","Monty","Cecelia","Teddi","Nicola","Jolynn","Pammy","Basil","Diena","Clara","Brietta",
            "Prinz","Carina","Kikelia","Lalo","Sharity","Jeffrey","Clem","Rosanne","Annetta","Rubi","Hermione","Tim","Rosy",
            "Ethelin","Teresina","Avrom","Dorris","Aggie","Vin","Martainn","Flossi","Dru","Star","Diena","Lannie","Valeria",
            "Trevor","Mariam","Raff"
        };

        static readonly string[] surnames =
        {
            "Kearsley","Hurton","Moseby","Roth","Iban","Macklin","Georg","Stebbings","Diche","Rosenberg","Rosier","McKinlay",
            "Hush","Dyneley","McGovern","Uren","Kunze","Boreham","Wootton","Pirnie","Spurman","Morot","Jellybrand","Fforde",
            "Sandle","Paolicchi","Kliement","Crouse","Burnel","Halpeine","Castledine","Merrydew","Roder","Kryska","Dulwitch",
            "Ilewicz","Kingzeth","Grisdale","Americi","Riggoll","Moxstead","Haygreen","Balaam","Tembridge","Tallman","Chinnery",
            "Buckthorp","Edgerley","Kahan","Jendrassik","Sampson","Shera","Hasslocher","Brophy","Shutler","Coneron","Minton",
            "Pridham","Labbati","Ranyell","Jeffray","Klaes","Van Der Vlies","Halliburton","Hair","Glassman","Teager","Castree",
            "Terbeek","Dericot","Manoelli","Nodin","Carlow","Duffree","Le Floch","Landell","Verryan","Eames","Manthroppe","Tooby",
            "Lewsam","Walcot","Brodhead","Chapleo","Christoforou","Loveridge","Kemmis","Sheal","Carus","Mealand","Dreossi","Matthews",
            "Hamments","Samber","Dowbekin","Ilsley","D'eye","Fraczkiewicz","Brydon","Soares"
        };
        static DateTime getDate(DateTime start,DateTime end)
        {
            int range = end.Subtract(start).Days;
            return start.AddDays(new Random().Next(range));
        }
    }
}
