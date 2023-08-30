using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities
{
    public class Olympiad
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public int SeasonId { get; set; }
        public Season Season { get; set; }

        public string Description => Id == -1 ? "All" : $"{City?.Country?.Name} \"{City?.Name} {Year} {Season?.Name}\"" ; 

        public ICollection<SportsmanAwardOlympiad> SportsmanAward { get; set; } = new HashSet<SportsmanAwardOlympiad>();
    }
}
