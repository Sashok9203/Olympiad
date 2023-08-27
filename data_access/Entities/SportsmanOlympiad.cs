﻿using data_access.Entityes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entities
{
    public class SportsmanOlympiad
    {
        public int SportsmanId { get; set; }
        public Sportsman Sportsman { get; set; }

        public int OlympiadId { get; set; }
        public Olympiad Olympiad { get; set; }

    }
}
