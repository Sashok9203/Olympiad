using data_access.Entities;
using data_access.Entityes;
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
        IRepository<Olympiad> Olympiads { get; }
        IRepository<SportsmanAwardOlympiad> SAOlympiad { get; }
        IRepository<Country> Countries { get; }
        void Save();
    }
}
