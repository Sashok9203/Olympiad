using data_access.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities
{
    public class Olympiad
    {
        public int Year { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public ICollection<Award> Awards { get; set; } = new HashSet<Award>();
        public ICollection<SportsmanOlympiad> SportsmanOlympiads { get; set; } = new HashSet<SportsmanOlympiad>();
    }
}
