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
        public int Id { get; set; }

        public int Year { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
       
        public ICollection<SportsmanAwardOlympiad> SportsmanAwardOlympiads { get; set; } = new HashSet<SportsmanAwardOlympiad>();
    }
}
