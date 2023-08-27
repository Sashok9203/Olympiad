using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities
{
    public class Award
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public ICollection<SportsmanAwardOlympiad> SportsmanAwardOlympiads { get; set; } = new HashSet<SportsmanAwardOlympiad>();
    }
}
