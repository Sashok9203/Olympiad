using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entityes
{
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<City> Cities { get; set; } = new HashSet<City>();

        public ICollection<Sportsman> Sportsmens { get; set; } = new HashSet<Sportsman>();
    }
}
