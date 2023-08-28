using data_access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Repositories
{
    public interface IUnitOW : IDisposable
    {
        IRepository<Sportsman> Sportsmans { get; }
        IRepository<Sport> Sports { get; }
        IRepository<Award> Awards { get; }
        public IRepository<SportsmanAwardOlympiad> SAOlympiad { get; }
        void Save();
    }
}
