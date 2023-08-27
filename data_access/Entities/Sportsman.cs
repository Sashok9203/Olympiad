using data_access.Entityes;
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

        public int? PhotoId { get; set; }
        public Photo? Photo { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public ICollection<Award> Awards { get; set; } = new HashSet<Award>();
        public ICollection<SportsmanOlympiad> SportsmanOlympiads { get; set; } = new HashSet<SportsmanOlympiad>();

    }
}
