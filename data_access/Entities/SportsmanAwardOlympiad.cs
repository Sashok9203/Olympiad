using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities
{
    public class SportsmanAwardOlympiad
    {
        public int SportsmanId { get; set; }
        public Sportsman Sportsman { get; set; }

        public int OlympiadId { get; set; }
        public Olympiad_ Olympiad { get; set; }

        public int? AwardId { get; set; }
        public Award? Award { get; set; }

       
    }
}
