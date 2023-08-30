using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities
{
    public class Sportsman
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string FullName => Name + " " + Surname;

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public DateTime Birthday { get; set; }

        public int SportId { get; set; }
        public Sport Sport { get; set; }

        public string? PhotoPath { get; set; }

        public int GenderId { get; set; }
        public Gender Gender { get; set; }
   

        public ICollection<SportsmanAwardOlympiad> AwardOlympiads { get; set; } = new HashSet<SportsmanAwardOlympiad>();

    }
}
