using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities
{
    public class Photo
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public ICollection<Sportsman> Sportsmens { get; set; } = new HashSet<Sportsman>();
    }
}
