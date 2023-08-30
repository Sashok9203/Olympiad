using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities
{
    public class Sport
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int SeasonId { get; set; }
        public Season Season { get; set; }

        public ICollection<Sportsman> Sportsmans { get; set; } = new HashSet<Sportsman>();
      
    }
}
