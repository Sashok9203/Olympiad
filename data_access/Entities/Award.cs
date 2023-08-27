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

        public int OlympiadId { get; set; }
        public Olympiad Olympiad { get; set; }

        public ICollection<Sportsman> Sportsmens { get; set; } = new HashSet<Sportsman>();
    }
}
