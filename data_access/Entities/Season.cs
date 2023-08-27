﻿using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Entityes
{
    public class Season
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Sport> Sports { get; set; } = new HashSet<Sport>();
    }
}
